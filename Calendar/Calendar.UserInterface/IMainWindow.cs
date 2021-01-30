namespace Calendar.UserInterface
{
    using System;

    public interface IMainWindow
    {
        void StartTimer(DateTime targetTime);

        void Minimize();
    }
}
