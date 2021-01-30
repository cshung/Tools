namespace Calendar
{
    using System;

    public class HourlyDaily : Daily
    {
        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public int Minute { get; set; }

        public override DateTime GetNextScheduledTime(DateTime currentTime)
        {
            if (currentTime.Hour < this.StartHour || (currentTime.Hour == this.StartHour && currentTime.Minute < this.Minute))
            {
                return new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, this.StartHour, this.Minute, 0);
            }
            else if (currentTime.Hour > this.EndHour || (currentTime.Hour == this.EndHour && currentTime.Minute >= this.Minute))
            {
                DateTime earliestToday = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, this.StartHour, this.Minute, 0);
                return earliestToday.AddDays(1);
            }
            else
            {
                if (currentTime.Minute < this.Minute)
                {
                    return new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, this.Minute, 0);
                }
                else
                {
                    return new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour + 1, this.Minute, 0);
                }
            }
        }
    }
}
