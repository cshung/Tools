namespace Calendar.UserInterface
{
    using System.Windows.Input;

    public class AlertViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public AlertViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public Alert Alert
        {
            get;
            set;
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
