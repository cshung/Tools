namespace Calendar.Maui
{
    using System;
    using System.Windows.Input;

    public class TodayCommand : ICommand
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public TodayCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.mainWindowViewModel.OnTodayButtonClicked();
        }
    }
}
