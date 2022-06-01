namespace Calendar.Maui
{
    using System;

    public interface IMainWindow
    {
        void StartTimer(TimeSpan fromNow);

        void Minimize();
    }
}
