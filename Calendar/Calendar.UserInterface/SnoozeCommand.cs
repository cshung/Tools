namespace Calendar.UserInterface
{
    using System;
    using System.Windows.Input;

    public class SnoozeCommand : ICommand
    {
        private AlertViewModel alertViewModel;

        public SnoozeCommand(AlertViewModel alertViewModel)
        {
            this.alertViewModel = alertViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.alertViewModel.OnSnoozeButtonClicked();
        }
    }
}
