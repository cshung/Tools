namespace Calendar.Maui
{
    using System.Windows.Input;

    public class AlertViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;
        private readonly Alert alert;

        public AlertViewModel(MainWindowViewModel mainWindowViewModel, Alert alert)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.alert = alert;
        }

        public Alert Alert
        {
            get
            {
                return this.alert;
            }
        }

        public ICommand SnoozeCommand
        {
            get
            {
                return new SnoozeCommand(this);
            }
        }

        public ICommand DoneCommand
        {
            get
            {
                return new DoneCommand(this);
            }
        }

        public void OnSnoozeButtonClicked()
        {
            this.mainWindowViewModel.OnSnoozeButtonClicked(this);
        }

        internal void OnDoneButtonClicked()
        {
            this.mainWindowViewModel.OnDoneButtonClicked(this);
        }
    }
}
