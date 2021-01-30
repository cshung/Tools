namespace Calendar
{
    using System;

    public class SingleDaily : Daily
    {
        public int HourOfDay { get; set; }

        public int MinuteOfDay { get; set; }

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
