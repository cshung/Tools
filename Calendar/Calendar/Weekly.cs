namespace Calendar
{
    using System;

    public class Weekly : ScheduleItem
    {
        private readonly DayOfWeekRecord[] records;

        public Weekly(string text, DayOfWeekRecord[] records) : base(text)
        {
            this.records = records;
        }

        public DayOfWeekRecord[] Records
        {
            get
            {
                return this.records;
            }
        }

        public override DateTime GetNextScheduledTime(DateTime currentTime)
        {
            DateTime? soonest = null;
            bool current = true;
            while (soonest == null)
            {
                soonest = null;
                if (!current)
                {
                    DateTime walking = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 0, 0, 0);
                    do
                    {
                        walking = walking.AddDays(1);
                    }
                    while (walking.DayOfWeek != DayOfWeek.Sunday);
                    currentTime = walking;
                }

                foreach (var record in this.Records)
                {
                    DateTime matching = currentTime;
                    if (matching.DayOfWeek != record.DayOfWeekValue)
                    {
                        matching = new DateTime(matching.Year, matching.Month, matching.Day, 0, 0, 0);
                        while (matching.DayOfWeek != record.DayOfWeekValue)
                        {
                            matching = matching.AddDays(1);
                        }
                    }

                    DateTime scheduled = record.DailyValue.GetNextScheduledTime(matching);
                    if (scheduled.Day == matching.Day)
                    {
                        if (soonest == null || scheduled < soonest)
                        {
                            soonest = scheduled;
                        }
                    }
                }

                current = false;
            }

            return soonest.Value;
        }
    }
}
