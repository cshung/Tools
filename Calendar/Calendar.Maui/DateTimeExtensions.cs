namespace Calendar.Maui
{
    using System;

    public static class DateTimeExtensions
    {
        public static DateTime NowSafe()
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
        }
    }
}
