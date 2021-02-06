namespace Calendar
{
    using System;

    public class Monthly : ScheduleItem
    {
        private readonly DayOfMonthRecord[] records;

        public Monthly(string text, DayOfMonthRecord[] records) : base(text)
        {
            this.records = records;
        }

        public DayOfMonthRecord[] Records
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
                    while (walking.Day != 1);
                    currentTime = walking;
                }

                foreach (var record in this.Records)
                {
                    DateTime matching = currentTime;
                    if (matching.Day != record.DayOfMonthValue)
                    {
                        matching = new DateTime(matching.Year, matching.Month, matching.Day, 0, 0, 0);
                        while (matching.Day != record.DayOfMonthValue)
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
