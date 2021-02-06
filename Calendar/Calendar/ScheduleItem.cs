namespace Calendar
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public abstract class ScheduleItem
    {
        private readonly string text;

        public ScheduleItem(string text)
        {
            this.text = text;
        }

        public string Text
        {
            get
            {
                return this.text;
            }
        }

        public static JsonSerializerSettings Jss()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new KnownTypesBinder
                {
                    KnownTypes = new List<Type>
                    {
                        typeof(SingleDaily),
                        typeof(HourlyDaily),
                        typeof(Weekly),
                        typeof(Weekday),
                        typeof(Monthly)
                    }
                }
            };
        }

        public static ScheduleItem[] Load(string content)
        {
            JsonSerializerSettings jss = Jss();
            return JsonConvert.DeserializeObject<ScheduleItem[]>(content, jss)!;
        }


        public abstract DateTime GetNextScheduledTime(DateTime currentTime);
    }
}
