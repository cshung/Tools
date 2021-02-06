namespace Calendar
{
    public class DayOfMonthRecord
    {
        private readonly int dayOfMonthValue;
        private readonly Daily dailyValue;

        public DayOfMonthRecord(int dayOfMonthValue, Daily dailyValue)
        {
            this.dayOfMonthValue = dayOfMonthValue;
            this.dailyValue = dailyValue;
        }

        public int DayOfMonthValue
        {
            get
            {
                return this.dayOfMonthValue;
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
