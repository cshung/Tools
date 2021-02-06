namespace Calendar
{
    using System;
    using System.Collections.Generic;

    public class Alarm
    {
        private readonly ScheduleItem[] items;
        private readonly ISet<Alert> snoozes;
        private readonly ISet<Alert> done;
        private DateTime handledTime;

        public Alarm(DateTime handledTime, ScheduleItem[] items)
        {
            this.snoozes = new HashSet<Alert>();
            this.done = new HashSet<Alert>();
            this.items = items;
            this.handledTime = handledTime;
        }

        public void Snooze(Alert alert)
        {
            this.Done(alert);
            Alert snooze = new Alert(alert.Name, alert.Time + TimeSpan.FromMinutes(5));
            this.snoozes.Add(snooze);
        }

        public void Done(Alert alert)
        {
            this.snoozes.Remove(alert);
            this.done.Add(alert);
        }

        public List<Alert> GetFutures(DateTime now)
        {
            List<Alert> mostRecentAlerts = new List<Alert>();
            DateTime mostRecentAlertTime = DateTime.Now;
            foreach (Alert snooze in this.snoozes)
            {
                if (snooze.Time > now)
                {
                    if (mostRecentAlerts.Count == 0)
                    {
                        mostRecentAlerts.Add(snooze);
                        mostRecentAlertTime = snooze.Time;
                    }
                    else if (mostRecentAlertTime == snooze.Time)
                    {
                        mostRecentAlerts.Add(snooze);
                    }
                    else if (mostRecentAlertTime > snooze.Time)
                    {
                        mostRecentAlerts.Clear();
                        mostRecentAlerts.Add(snooze);
                        mostRecentAlertTime = snooze.Time;
                    }
                }
            }

            foreach (var item in this.items)
            {
                DateTime nextScheduledTime = item.GetNextScheduledTime(now);
                if (nextScheduledTime != null)
                {
                    if (mostRecentAlerts.Count == 0)
                    {
                        mostRecentAlerts.Add(new Alert(item.Text, nextScheduledTime));
                        mostRecentAlertTime = nextScheduledTime;
                    }
                    else if (mostRecentAlertTime == nextScheduledTime)
                    {
                        mostRecentAlerts.Add(new Alert(item.Text, nextScheduledTime));
                    }
                    else if (mostRecentAlertTime > nextScheduledTime)
                    {
                        mostRecentAlerts.Clear();
                        mostRecentAlerts.Add(new Alert(item.Text, nextScheduledTime));
                        mostRecentAlertTime = nextScheduledTime;
                    }
                }
            }

            return mostRecentAlerts;
        }

        public void SetHandledTime(DateTime today)
        {
            this.handledTime = today;
        }

        public List<Alert> GetPastDues(DateTime currentNow)
        {
            List<Alert> pastdues = new List<Alert>();
            DateTime cursor = this.handledTime;
            bool containPastdueItem = false;
            while (cursor < currentNow)
            {
                List<Alert> comingAlerts = this.GetFutures(cursor);
                if (comingAlerts[0].Time <= currentNow)
                {
                    foreach (var comingAlert in comingAlerts)
                    {
                        if (!this.done.Contains(comingAlert))
                        {
                            containPastdueItem = true;
                            pastdues.Add(comingAlert);
                        }
                    }

                    cursor = comingAlerts[0].Time;
                    if (!containPastdueItem)
                    {
                        foreach (var comingAlert in comingAlerts)
                        {
                            this.done.Remove(comingAlert);
                        }

                        this.handledTime = comingAlerts[0].Time;
                    }
                }
                else
                {
                    break;
                }
            }

            return pastdues;
        }
    }
}
