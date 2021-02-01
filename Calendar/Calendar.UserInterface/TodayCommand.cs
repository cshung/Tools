namespace Calendar.UserInterface
{
    using System;
    using System.Windows.Input;

    public class TodayCommand : ICommand
    {
        private readonly MainWindowViewModel mainWindowViewModel;
        private bool canExecute;

        public TodayCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.canExecute = true;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecute;
        }

        public void Execute(object parameter)
        {
            this.mainWindowViewModel.OnTodayButtonClicked();
        }
    }
}
