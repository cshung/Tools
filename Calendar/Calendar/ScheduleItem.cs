namespace Calendar
{
    using System;

    public abstract class ScheduleItem
    {
        public string Text { get; set; }

        public abstract DateTime GetNextScheduledTime(DateTime currentTime);
    }
}
