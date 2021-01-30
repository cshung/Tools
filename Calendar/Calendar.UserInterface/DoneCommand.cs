namespace Calendar.UserInterface
{
    using System;
    using System.Windows.Input;

    public class DoneCommand : ICommand
    {
        private readonly AlertViewModel alertViewModel;

        public DoneCommand(AlertViewModel alertViewModel)
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
            this.alertViewModel.OnDoneButtonClicked();
        }
    }
}
