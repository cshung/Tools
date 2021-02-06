namespace Calendar.UnitTests
{
    using Newtonsoft.Json;
    using System;
    using Xunit;

    public class SingleDailyTests
    {
        [Theory]
        [InlineData(@"{""HourOfDay"":1,""MinuteOfDay"":30}", @"""2021-01-01T00:00:00""", @"""2021-01-01T01:30:00""")]
        [InlineData(@"{""HourOfDay"":1,""MinuteOfDay"":30}", @"""2021-01-01T01:00:00""", @"""2021-01-01T01:30:00""")]
        [InlineData(@"{""HourOfDay"":1,""MinuteOfDay"":30}", @"""2021-01-01T01:30:00""", @"""2021-01-02T01:30:00""")]
        [InlineData(@"{""HourOfDay"":1,""MinuteOfDay"":30}", @"""2021-01-01T01:31:00""", @"""2021-01-02T01:30:00""")]
        [InlineData(@"{""HourOfDay"":1,""MinuteOfDay"":30}", @"""2021-01-01T02:00:00""", @"""2021-01-02T01:30:00""")]
        public void SingleDailyTheory(string hourlyDailyText, string testingNowText, string testingExpectedText)
        {
            SingleDaily singleDaily = JsonConvert.DeserializeObject<SingleDaily>(hourlyDailyText, ScheduleItem.Jss())!; ;
            DateTime testingNow = JsonConvert.DeserializeObject<DateTime>(testingNowText, ScheduleItem.Jss())!; ;
            DateTime testingExpected = JsonConvert.DeserializeObject<DateTime>(testingExpectedText, ScheduleItem.Jss())!; ;
            DateTime scheduled = singleDaily.GetNextScheduledTime(testingNow);
            Assert.Equal(testingExpected, scheduled);
        }
    }
}
