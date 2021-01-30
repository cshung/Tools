namespace Calendar.UnitTests
{
    using System;
    using Xunit;

    public class WeeklyTests
    {
        [Fact]
        public void EarlierToday()
        {
            Weekly weekly = new Weekly
            {
                Records = new DayOfWeekRecord[]
                {
                    new DayOfWeekRecord
                    {
                        DayOfWeekValue = DayOfWeek.Monday,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 1, 25, 0, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 25, 9, 0, 0);
            TestWeekly(weekly, testingNow, testingExpected);
        }

        [Fact]
        public void RightToday()
        {
            Weekly weekly = new Weekly
            {
                Records = new DayOfWeekRecord[]
                {
                    new DayOfWeekRecord
                    {
                        DayOfWeekValue = DayOfWeek.Monday,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 1, 25, 9, 0, 0);
            DateTime testingExpected = new DateTime(2021, 2, 1, 9, 0, 0);
            TestWeekly(weekly, testingNow, testingExpected);
        }

        [Fact]
        public void LaterToday()
        {
            Weekly weekly = new Weekly
            {
                Records = new DayOfWeekRecord[]
                {
                    new DayOfWeekRecord
                    {
                        DayOfWeekValue = DayOfWeek.Monday,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 1, 25, 10, 0, 0);
            DateTime testingExpected = new DateTime(2021, 2, 1, 9, 0, 0);
            TestWeekly(weekly, testingNow, testingExpected);
        }

        [Fact]
        public void Tomorrow()
        {
            Weekly weekly = new Weekly
            {
                Records = new DayOfWeekRecord[]
                {
                    new DayOfWeekRecord
                    {
                        DayOfWeekValue = DayOfWeek.Tuesday,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 1, 25, 10, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 26, 9, 0, 0);
            TestWeekly(weekly, testingNow, testingExpected);
        }

        [Fact]
        public void SoonerWins()
        {
            Weekly weekly = new Weekly
            {
                Records = new DayOfWeekRecord[]
                {
                    new DayOfWeekRecord
                    {
                        DayOfWeekValue = DayOfWeek.Tuesday,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    },
                    new DayOfWeekRecord
                    {
                        DayOfWeekValue = DayOfWeek.Monday,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 1, 25, 8, 0, 0);
            DateTime testingExpected = new DateTime(2021, 1, 25, 9, 0, 0);
            TestWeekly(weekly, testingNow, testingExpected);
        }

        private static void TestWeekly(Weekly weekly, DateTime testingNow, DateTime testingExpected)
        {
            DateTime scheduled = weekly.GetNextScheduledTime(testingNow);
            Assert.Equal(scheduled, testingExpected);
        }
    }
}
