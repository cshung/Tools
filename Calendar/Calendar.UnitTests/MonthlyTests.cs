namespace Calendar.UnitTests
{
    using Newtonsoft.Json;
    using System;
    using Xunit;

    public class MonthlyTests
    {
        [Theory]
        [InlineData(@"{""Records"":[{""DayOfMonthValue"":1,""DailyValue"":{""$type"":""SingleDaily"",""HourOfDay"":9,""MinuteOfDay"":0}}]}", @"""2021-02-01T00:00:00""", @"""2021-02-01T09:00:00""")]
        [InlineData(@"{""Records"":[{""DayOfMonthValue"":1,""DailyValue"":{""$type"":""SingleDaily"",""HourOfDay"":9,""MinuteOfDay"":0}}]}", @"""2021-02-01T09:00:00""", @"""2021-03-01T09:00:00""")]
        [InlineData(@"{""Records"":[{""DayOfMonthValue"":1,""DailyValue"":{""$type"":""SingleDaily"",""HourOfDay"":9,""MinuteOfDay"":0}}]}", @"""2021-02-01T10:00:00""", @"""2021-03-01T09:00:00""")]
        [InlineData(@"{""Records"":[{""DayOfMonthValue"":2,""DailyValue"":{""$type"":""SingleDaily"",""HourOfDay"":9,""MinuteOfDay"":0}}]}", @"""2021-02-01T10:00:00""", @"""2021-02-02T09:00:00""")]
        [InlineData(@"{""Records"": [{""DayOfMonthValue"": 2,""DailyValue"": {""$type"": ""SingleDaily"",""HourOfDay"": 9,""MinuteOfDay"": 0}}, {""DayOfMonthValue"": 1,""DailyValue"": {""$type"": ""SingleDaily"",""HourOfDay"": 9,""MinuteOfDay"": 0}}]}", @"""2021-02-01T08:00:00""", @"""2021-02-01T09:00:00""")]
        public void MonthlyTheory(string monthlyText, string testingNowText, string testingExpectedText)
        {
            Monthly monthly = JsonConvert.DeserializeObject<Monthly>(monthlyText, ScheduleItem.Jss())!;
            DateTime testingNow = JsonConvert.DeserializeObject<DateTime>(testingNowText, ScheduleItem.Jss())!; ;
            DateTime testingExpected = JsonConvert.DeserializeObject<DateTime>(testingExpectedText, ScheduleItem.Jss())!; ;
            DateTime scheduled = monthly.GetNextScheduledTime(testingNow);
            Assert.Equal(scheduled, testingExpected);
        }
    }
}
