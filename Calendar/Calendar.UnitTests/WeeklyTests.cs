namespace Calendar.UnitTests
{
    using Newtonsoft.Json;
    using System;
    using Xunit;

    public class WeeklyTests
    {
        [Theory]
        [InlineData(@"{""Records"":[{""DayOfWeekValue"":1,""DailyValue"":{""$type"":""SingleDaily"",""HourOfDay"":9,""MinuteOfDay"":0}}]}", @"""2021-01-25T00:00:00""", @"""2021-01-25T09:00:00""")]
        [InlineData(@"{""Records"":[{""DayOfWeekValue"":1,""DailyValue"":{""$type"":""SingleDaily"",""HourOfDay"":9,""MinuteOfDay"":0}}]}", @"""2021-01-25T09:00:00""", @"""2021-02-01T09:00:00""")]
        [InlineData(@"{""Records"":[{""DayOfWeekValue"":1,""DailyValue"":{""$type"":""SingleDaily"",""HourOfDay"":9,""MinuteOfDay"":0}}]}", @"""2021-01-25T10:00:00""", @"""2021-02-01T09:00:00""")]
        [InlineData(@"{""Records"":[{""DayOfWeekValue"":2,""DailyValue"":{""$type"":""SingleDaily"",""HourOfDay"":9,""MinuteOfDay"":0}}]}", @"""2021-01-25T10:00:00""", @"""2021-01-26T09:00:00""")]
        [InlineData(@"{""Records"": [{""DayOfWeekValue"": 2,""DailyValue"": {""$type"": ""SingleDaily"",""HourOfDay"": 9,""MinuteOfDay"": 0}}, {""DayOfWeekValue"": 1,""DailyValue"": {""$type"": ""SingleDaily"",""HourOfDay"": 9,""MinuteOfDay"": 0}}]}", @"""2021-01-25T08:00:00""", @"""2021-01-25T09:00:00""")]
        public void WeeklyTheory(string weeklyText, string testingNowText, string testingExpectedText)
        {
            Weekly weekly = JsonConvert.DeserializeObject<Weekly>(weeklyText, ScheduleItem.Jss())!;
            DateTime testingNow = JsonConvert.DeserializeObject<DateTime>(testingNowText, ScheduleItem.Jss())!; ;
            DateTime testingExpected = JsonConvert.DeserializeObject<DateTime>(testingExpectedText, ScheduleItem.Jss())!; ;
            DateTime scheduled = weekly.GetNextScheduledTime(testingNow);
            Assert.Equal(scheduled, testingExpected);
        }
    }
}
