namespace Calendar
{
    using System;

    public class Weekday : ScheduleItem
    {
        public Daily DailyValue;

        public override DateTime GetNextScheduledTime(DateTime currentTime)
        {
            return new Weekly
            {
                Records = new DayOfWeekRecord[]
                {
                    new DayOfWeekRecord { DayOfWeekValue = DayOfWeek.Monday, DailyValue = this.DailyValue },
                    new DayOfWeekRecord { DayOfWeekValue = DayOfWeek.Tuesday, DailyValue = this.DailyValue },
                    new DayOfWeekRecord { DayOfWeekValue = DayOfWeek.Wednesday, DailyValue = this.DailyValue },
                    new DayOfWeekRecord { DayOfWeekValue = DayOfWeek.Thursday, DailyValue = this.DailyValue },
                    new DayOfWeekRecord { DayOfWeekValue = DayOfWeek.Friday, DailyValue = this.DailyValue },
                }
            }.GetNextScheduledTime(currentTime);
        }
    }
}
