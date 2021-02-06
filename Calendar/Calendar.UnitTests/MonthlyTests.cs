namespace Calendar.UnitTests
{
    using System;
    using Xunit;

    public class MonthlyTests
    {
        [Fact]
        public void EarlierToday()
        {
            Monthly monthly = new Monthly
            {
                Records = new DayOfMonthRecord[]
                {
                    new DayOfMonthRecord
                    {
                        DayOfMonthValue = 1,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 2, 1, 0, 0, 0);
            DateTime testingExpected = new DateTime(2021, 2, 1, 9, 0, 0);
            TestMonthly(monthly, testingNow, testingExpected);
        }

        [Fact]
        public void RightToday()
        {
            Monthly monthly = new Monthly
            {
                Records = new DayOfMonthRecord[]
                {
                    new DayOfMonthRecord
                    {
                        DayOfMonthValue = 1,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 2, 1, 9, 0, 0);
            DateTime testingExpected = new DateTime(2021, 3, 1, 9, 0, 0);
            TestMonthly(monthly, testingNow, testingExpected);
        }

        [Fact]
        public void LaterToday()
        {
            Monthly monthly = new Monthly
            {
                Records = new DayOfMonthRecord[]
                {
                    new DayOfMonthRecord
                    {
                        DayOfMonthValue = 1,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 2, 1, 10, 0, 0);
            DateTime testingExpected = new DateTime(2021, 3, 1, 9, 0, 0);
            TestMonthly(monthly, testingNow, testingExpected);
        }

        [Fact]
        public void Tomorrow()
        {
            Monthly monthly = new Monthly
            {
                Records = new DayOfMonthRecord[]
                {
                    new DayOfMonthRecord
                    {
                        DayOfMonthValue = 2,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 2, 1, 10, 0, 0);
            DateTime testingExpected = new DateTime(2021, 2, 2, 9, 0, 0);
            TestMonthly(monthly, testingNow, testingExpected);
        }

        [Fact]
        public void SoonerWins()
        {
            Monthly monthly = new Monthly
            {
                Records = new DayOfMonthRecord[]
                {
                    new DayOfMonthRecord
                    {
                        DayOfMonthValue = 2,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    },
                    new DayOfMonthRecord
                    {
                        DayOfMonthValue = 1,
                        DailyValue = new SingleDaily
                        {
                            HourOfDay = 9,
                            MinuteOfDay = 0,
                        }
                    }
                }
            };
            DateTime testingNow = new DateTime(2021, 2, 1, 8, 0, 0);
            DateTime testingExpected = new DateTime(2021, 2, 1, 9, 0, 0);
            TestMonthly(monthly, testingNow, testingExpected);
        }

        private static void TestMonthly(Monthly monthly, DateTime testingNow, DateTime testingExpected)
        {
            DateTime scheduled = monthly.GetNextScheduledTime(testingNow);
            Assert.Equal(scheduled, testingExpected);
        }
    }
}
