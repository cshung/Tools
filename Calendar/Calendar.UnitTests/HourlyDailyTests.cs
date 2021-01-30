namespace Calendar.UnitTests
{
    using System;
    using Xunit;

    public class HourlyDailyTests
    {
        [Fact]
        public void EarlierHour()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 4;
            DateTime testingNow = new DateTime(2021, 1, 1, 0, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 1, 9, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        [Fact]
        public void EarlierMinute()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 16;
            DateTime testingNow = new DateTime(2021, 1, 1, 9, 29, 0);
            DateTime testingExpected = new DateTime(2021, 1, 1, 9, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        [Fact]
        public void RightStart()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 16;
            DateTime testingNow = new DateTime(2021, 1, 1, 9, 30, 0);
            DateTime testingExpected = new DateTime(2021, 1, 1, 10, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        [Fact]
        public void AfterEndHour()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 16;
            DateTime testingNow = new DateTime(2021, 1, 1, 17, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 2, 9, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        [Fact]
        public void AfterEndMinute()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 16;
            DateTime testingNow = new DateTime(2021, 1, 1, 16, 31, 0);
            DateTime testingExpected = new DateTime(2021, 1, 2, 9, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        [Fact]
        public void RightEnd()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 16;
            DateTime testingNow = new DateTime(2021, 1, 1, 16, 30, 0);
            DateTime testingExpected = new DateTime(2021, 1, 2, 9, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        [Fact]
        public void InsideWithinHour()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 16;
            DateTime testingNow = new DateTime(2021, 1, 1, 10, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 1, 10, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        [Fact]
        public void InsideRightNow()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 16;
            DateTime testingNow = new DateTime(2021, 1, 1, 10, 30, 0);
            DateTime testingExpected = new DateTime(2021, 1, 1, 11, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        [Fact]
        public void InsideNextHour()
        {
            HourlyDaily hourlyDaily = new HourlyDaily();
            hourlyDaily.StartHour = 9;
            hourlyDaily.Minute = 30;
            hourlyDaily.EndHour = 16;
            DateTime testingNow = new DateTime(2021, 1, 1, 9, 31, 0);
            DateTime testingExpected = new DateTime(2021, 1, 1, 10, 30, 0);
            TestHourlyDaily(hourlyDaily, testingNow, testingExpected);
        }

        private static void TestHourlyDaily(HourlyDaily hourlyDaily, DateTime testingNow, DateTime testingExpected)
        {
            DateTime scheduled = hourlyDaily.GetNextScheduledTime(testingNow);
            Assert.Equal(scheduled, testingExpected);
        }
    }
}
