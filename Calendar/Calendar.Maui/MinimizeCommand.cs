namespace Calendar.Maui
{
    using System;
    using System.Windows.Input;

    public class MinimizeCommand : ICommand
    {
        private readonly MainWindowViewModel mainWindowViewModel;
        private bool canExecute;

        public MinimizeCommand(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.canExecute = false;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.canExecute;
        }

        public void Enable()
        {
            if (!this.canExecute)
            {
                this.canExecute = true;
                if (this.CanExecuteChanged != null)
                {
                    this.CanExecuteChanged(this, new EventArgs());
                }
            }
        }

        public void Disable()
        {
            if (this.canExecute)
            {
                this.canExecute = false;
                if (this.CanExecuteChanged != null)
                {
                    this.CanExecuteChanged(this, new EventArgs());
                }
            }
        }

        public void Execute(object parameter)
        {
            this.mainWindowViewModel.OnMinimizeButtonClicked();
        }
    }
}
