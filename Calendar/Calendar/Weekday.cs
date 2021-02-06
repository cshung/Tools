namespace Calendar
{
    using System;

    public class Weekday : ScheduleItem
    {
        private readonly Daily dailyValue;

        public Weekday(string text, Daily dailyValue) : base(text)
        {
            this.dailyValue = dailyValue;
        }

        public Daily DailyValue
        {
            get
            {
                return this.dailyValue;
            }
        }

        public override DateTime GetNextScheduledTime(DateTime currentTime)
        {
            DayOfWeekRecord[] records = new DayOfWeekRecord[]
            {
                new DayOfWeekRecord(DayOfWeek.Monday, this.DailyValue),
                new DayOfWeekRecord(DayOfWeek.Tuesday, this.DailyValue),
                new DayOfWeekRecord(DayOfWeek.Wednesday, this.DailyValue),
                new DayOfWeekRecord(DayOfWeek.Thursday, this.DailyValue),
                new DayOfWeekRecord(DayOfWeek.Friday, this.DailyValue),
            };
            return new Weekly(this.Text, records).GetNextScheduledTime(currentTime);
        }
    }
}
