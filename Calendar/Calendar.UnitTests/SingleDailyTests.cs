namespace Calendar.UnitTests
{
    using System;
    using Xunit;

    public class SingleDailyTests
    {
        [Fact]
        public void EarlierHour()
        {
            SingleDaily singleDaily = new SingleDaily();
            singleDaily.HourOfDay = 1;
            singleDaily.MinuteOfDay = 0;
            DateTime testingNow = new DateTime(2021, 1, 1, 0, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 1, 1, 0, 0);
            TestSingleDaily(singleDaily, testingNow, testingExpected);
        }

        [Fact]
        public void EarlierMinute()
        {
            SingleDaily singleDaily = new SingleDaily();
            singleDaily.HourOfDay = 1;
            singleDaily.MinuteOfDay = 30;
            DateTime testingNow = new DateTime(2021, 1, 1, 1, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 1, 1, 30, 0);
            DateTime scheduled = singleDaily.GetNextScheduledTime(testingNow);
            TestSingleDaily(singleDaily, testingNow, testingExpected);
        }

        [Fact]
        public void RightNow()
        {
            SingleDaily singleDaily = new SingleDaily();
            singleDaily.HourOfDay = 1;
            singleDaily.MinuteOfDay = 0;
            DateTime testingNow = new DateTime(2021, 1, 1, 1, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 2, 1, 0, 0);
            TestSingleDaily(singleDaily, testingNow, testingExpected);
        }

        [Fact]
        public void Later()
        {
            SingleDaily singleDaily = new SingleDaily();
            singleDaily.HourOfDay = 1;
            singleDaily.MinuteOfDay = 0;
            DateTime testingNow = new DateTime(2021, 1, 1, 1, 1, 0);
            DateTime testingExpected = new DateTime(2021, 1, 2, 1, 0, 0);
            DateTime scheduled = singleDaily.GetNextScheduledTime(testingNow);
            TestSingleDaily(singleDaily, testingNow, testingExpected);
        }

        private static void TestSingleDaily(SingleDaily singleDaily, DateTime testingNow, DateTime testingExpected)
        {
            DateTime scheduled = singleDaily.GetNextScheduledTime(testingNow);
            Assert.Equal(scheduled, testingExpected);
        }
    }
}
