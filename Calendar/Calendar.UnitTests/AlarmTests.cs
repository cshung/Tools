namespace Calendar.UnitTests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class AlarmTests
    {
        [Fact]
        public void TestUpcoming()
        {
            ScheduleItem[] items = new ScheduleItem[]
            {
                new SingleDaily("Duolingo", 19, 0)
            };
            DateTime handledTime = new DateTime(2021, 1, 1, 17, 0, 0);
            Alarm alarm = new Alarm(handledTime, items);
            DateTime currentNow = new DateTime(2021, 1, 1, 18, 0, 0);
            List<Alert> alerts = alarm.GetFutures(currentNow);
            Assert.Single(alerts);
            Assert.Equal("Duolingo", alerts[0].Name);
            Assert.Equal(new DateTime(2021, 1, 1, 19, 0, 0), alerts[0].Time);
            List<Alert> pastdues = alarm.GetPastDues(currentNow);
            Assert.Empty(pastdues);
        }

        [Fact]
        public void TestPastdue()
        {
            ScheduleItem[] items = new ScheduleItem[]
            {
                new SingleDaily("Duolingo", 19, 0)
            };
            DateTime handledTime = new DateTime(2021, 1, 1, 17, 0, 0);
            Alarm alarm = new Alarm(handledTime, items);
            DateTime currentNow = new DateTime(2021, 1, 1, 20, 0, 0);
            List<Alert> alerts = alarm.GetFutures(currentNow);
            Assert.Single(alerts);
            Assert.Equal("Duolingo", alerts[0].Name);
            Assert.Equal(new DateTime(2021, 1, 2, 19, 0, 0), alerts[0].Time);
            List<Alert> pastdues = alarm.GetPastDues(currentNow);
            Assert.Single(pastdues);
            Assert.Equal("Duolingo", pastdues[0].Name);
            Assert.Equal(new DateTime(2021, 1, 1, 19, 0, 0), pastdues[0].Time);
            pastdues = alarm.GetPastDues(currentNow);
            Assert.Single(pastdues);
            Assert.Equal("Duolingo", pastdues[0].Name);
            Assert.Equal(new DateTime(2021, 1, 1, 19, 0, 0), pastdues[0].Time);
        }

        [Fact]
        public void TestDonePastdue()
        {
            ScheduleItem[] items = new ScheduleItem[]
            {
                new SingleDaily("Duolingo", 19, 0)
            };
            DateTime handledTime = new DateTime(2021, 1, 1, 17, 0, 0);
            Alarm alarm = new Alarm(handledTime, items);
            DateTime currentNow = new DateTime(2021, 1, 1, 20, 0, 0);
            List<Alert> alerts = alarm.GetFutures(currentNow);
            Assert.Single(alerts);
            Assert.Equal("Duolingo", alerts[0].Name);
            Assert.Equal(new DateTime(2021, 1, 2, 19, 0, 0), alerts[0].Time);
            List<Alert> pastdues = alarm.GetPastDues(currentNow);
            Assert.Single(pastdues);
            Assert.Equal("Duolingo", pastdues[0].Name);
            Assert.Equal(new DateTime(2021, 1, 1, 19, 0, 0), pastdues[0].Time);
            alarm.Done(pastdues[0]);
            pastdues = alarm.GetPastDues(currentNow);
            Assert.Empty(pastdues);
        }

        [Fact]
        public void TestSnooze()
        {
            ScheduleItem[] items = new ScheduleItem[]
            {
                new SingleDaily("Duolingo", 19, 0)
            };
            DateTime handledTime = new DateTime(2021, 1, 1, 17, 0, 0);
            Alarm alarm = new Alarm(handledTime, items);
            DateTime currentNow = new DateTime(2021, 1, 1, 19, 0, 0);
            List<Alert> alerts = alarm.GetFutures(currentNow);
            Assert.Single(alerts);
            Assert.Equal("Duolingo", alerts[0].Name);
            Assert.Equal(new DateTime(2021, 1, 2, 19, 0, 0), alerts[0].Time);
            List<Alert> pastdues = alarm.GetPastDues(currentNow);
            Assert.Single(pastdues);
            Assert.Equal("Duolingo", pastdues[0].Name);
            Assert.Equal(new DateTime(2021, 1, 1, 19, 0, 0), pastdues[0].Time);
            alarm.Snooze(pastdues[0]);
            alerts = alarm.GetFutures(currentNow);
            Assert.Single(alerts);
            Assert.Equal("Duolingo", alerts[0].Name);
            Assert.Equal(new DateTime(2021, 1, 1, 19, 5, 0), alerts[0].Time);
        }
    }
}
