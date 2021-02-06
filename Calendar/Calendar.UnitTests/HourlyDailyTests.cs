namespace Calendar.UnitTests
{
    using Newtonsoft.Json;
    using System;
    using Xunit;

    public class HourlyDailyTests
    {
        [Theory]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T00:00:00""", @"""2021-01-01T09:30:00""")]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T09:29:00""", @"""2021-01-01T09:30:00""")]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T09:30:00""", @"""2021-01-01T10:30:00""")]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T09:31:00""", @"""2021-01-01T10:30:00""")]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T10:00:00""", @"""2021-01-01T10:30:00""")]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T10:30:00""", @"""2021-01-01T11:30:00""")]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T16:30:00""", @"""2021-01-02T09:30:00""")]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T16:31:00""", @"""2021-01-02T09:30:00""")]
        [InlineData(@"{""StartHour"":9,""EndHour"":16,""Minute"":30}", @"""2021-01-01T17:00:00""", @"""2021-01-02T09:30:00""")]
        public void HourlyDailyTheory(string hourlyDailyText, string testingNowText, string testingExpectedText)
        {
            HourlyDaily hourlyDaily = JsonConvert.DeserializeObject<HourlyDaily>(hourlyDailyText, ScheduleItem.Jss())!; ;
            DateTime testingNow = JsonConvert.DeserializeObject<DateTime>(testingNowText, ScheduleItem.Jss())!; ;
            DateTime testingExpected = JsonConvert.DeserializeObject<DateTime>(testingExpectedText, ScheduleItem.Jss())!; ;
            DateTime scheduled = hourlyDaily.GetNextScheduledTime(testingNow);
            Assert.Equal(testingExpected, scheduled);
        }
    }
}
