namespace Calendar
{
    using System;

    public class SingleDaily : Daily
    {
        private readonly int hourOfDay;
        private readonly int minuteOfDay;

        public SingleDaily(string text, int hourOfDay, int minuteOfDay) : base(text)
        {
            this.hourOfDay = hourOfDay;
            this.minuteOfDay = minuteOfDay;
        }

        public int HourOfDay
        {
            get
            {
                return this.hourOfDay;
            }
        }

        public int MinuteOfDay
        {
            get
            {
                return this.minuteOfDay;
            }
        }

        public override DateTime GetNextScheduledTime(DateTime currentTime)
        {
            if (currentTime.Hour < this.HourOfDay || (currentTime.Hour == this.HourOfDay && currentTime.Minute < this.MinuteOfDay))
            {
                return new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, this.HourOfDay, this.MinuteOfDay, 0);
            }
            else
            {
                DateTime today = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, this.HourOfDay, this.MinuteOfDay, 0);
                return today.AddDays(1);
            }
        }
    }
}
