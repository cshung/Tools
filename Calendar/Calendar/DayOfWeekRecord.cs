namespace Calendar
{
    using System;

    public class DayOfWeekRecord
    {
        private readonly DayOfWeek dayOfWeekValue;
        private readonly Daily dailyValue;

        public DayOfWeekRecord(DayOfWeek dayOfWeekValue, Daily dailyValue)
        {
            this.dayOfWeekValue = dayOfWeekValue;
            this.dailyValue = dailyValue;
        }

        public DayOfWeek DayOfWeekValue
        {
            get
            {
                return this.dayOfWeekValue;
            }
        }

        public Daily DailyValue
        {
            get
            {
                return this.dailyValue;
            }
        }
    }
}
