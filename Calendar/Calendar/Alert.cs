namespace Calendar
{
    using System;

    public class Alert
    {
        private readonly string name;
        private readonly DateTime time;

        public Alert(string name, DateTime time)
        {
            this.name = name;
            this.time = time;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public DateTime Time
        {
            get
            {
                return this.time;
            }
        }

        public override bool Equals(object obj)
        {
            Alert? that = obj as Alert;
            if (that == null)
            {
                return false;
            }

            return this.Name.Equals(that.Name) && this.Time.Equals(that.Time);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.Time.GetHashCode();
        }
    }
}
