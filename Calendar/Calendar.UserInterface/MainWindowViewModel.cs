namespace Calendar.UserInterface
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Reflection;
    using System.Windows.Input;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IMainWindow mainWindow;
        private readonly MinimizeCommand minimizeCommand;
        private readonly TodayCommand todayCommand;
        private readonly Alarm alarm;
        private List<Alert>? futureAlerts;
        private List<Alert>? pastDueAlerts;

        public MainWindowViewModel(IMainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.alarm = new Alarm(DateTimeExtensions.NowSafe(), this.CreateScheduleItem());
            this.minimizeCommand = new MinimizeCommand(this);
            this.todayCommand = new TodayCommand(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand MinimizeCommand
        {
            get
            {
                return this.minimizeCommand;
            }
        }
        public ICommand TodayCommand
        {
            get
            {
                return this.todayCommand;
            }
        }

        public List<AlertViewModel> FutureAlerts
        {
            get
            {
                List<AlertViewModel> futureAlertModels = new List<AlertViewModel>();
                for (int i = 0; this.futureAlerts != null && i < this.futureAlerts.Count; i++)
                {
                    futureAlertModels.Add(new AlertViewModel(this, this.futureAlerts[i]));
                }

                return futureAlertModels;
            }
        }

        internal void OnTodayButtonClicked()
        {
            DateTime now = DateTime.Now;
            DateTime today = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            this.alarm.SetHandledTime(today);
            this.ScheduleNextAlert();
        }

        public List<AlertViewModel> PastDueAlerts
        {
            get
            {
                List<AlertViewModel> pastDueAlertModels = new List<AlertViewModel>();
                for (int i = 0; this.pastDueAlerts != null && i < this.pastDueAlerts.Count; i++)
                {
                    pastDueAlertModels.Add(new AlertViewModel(this, this.pastDueAlerts[i]));
                }

                return pastDueAlertModels;
            }
        }

        public void OnMinimizeButtonClicked()
        {
            this.mainWindow.Minimize();
        }

        public void OnMainWindowInitialized()
        {
            this.ScheduleNextAlert();
        }

        public void OnDoneButtonClicked(AlertViewModel alertViewModel)
        {
            this.alarm.Done(alertViewModel.Alert);
            this.ScheduleNextAlert();
        }

        public void OnSnoozeButtonClicked(AlertViewModel alertViewModel)
        {
            this.alarm.Snooze(alertViewModel.Alert);
            this.ScheduleNextAlert();
        }

        public void OnTimerTick()
        {
            this.ScheduleNextAlert();
        }

        private void ScheduleNextAlert()
        {
            DateTime now = DateTimeExtensions.NowSafe();
            this.pastDueAlerts = this.alarm.GetPastDues(now);
            this.futureAlerts = this.alarm.GetFutures(now);
            this.mainWindow.StartTimer(this.futureAlerts[0].Time);
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.FutureAlerts)));
                this.PropertyChanged(this, new PropertyChangedEventArgs(nameof(this.PastDueAlerts)));
            }

            if (this.pastDueAlerts.Count == 0)
            {
                this.mainWindow.Minimize();
                this.minimizeCommand.Enable();
            }
            else
            {
                this.minimizeCommand.Disable();
            }
        }

        private ScheduleItem[] CreateScheduleItem()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "schedule.json");
            string content = File.ReadAllText(path);
            return ScheduleItem.Load(content);
        }
    }
}
