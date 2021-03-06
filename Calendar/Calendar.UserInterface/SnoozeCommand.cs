﻿namespace Calendar.UserInterface
{
    using System;
    using System.Windows.Input;

    public class SnoozeCommand : ICommand
    {
        private readonly AlertViewModel alertViewModel;

        public SnoozeCommand(AlertViewModel alertViewModel)
        {
            this.alertViewModel = alertViewModel;
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
            this.alertViewModel.OnSnoozeButtonClicked();
        }
    }
}
