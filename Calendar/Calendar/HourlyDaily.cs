namespace Calendar
{
    using System;

    public class HourlyDaily : Daily
    {
        private readonly int startHour;
        private readonly int endHour;
        private readonly int minute;

        public HourlyDaily(string text, int startHour, int endHour, int minute) : base(text)
        {
            this.startHour = startHour;
            this.endHour = endHour;
            this.minute = minute;
        }

        public int StartHour
        {
            get
            {
                return this.startHour;
            }
        }

        public int EndHour
        {
            get
            {
                return this.endHour;
            }
        }

        public int Minute
        {
            get
            {
                return this.minute;
            }
        }

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
