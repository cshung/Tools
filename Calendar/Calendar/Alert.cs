namespace Calendar
{
    using System;

    public class Alert
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }

        public override bool Equals(object obj)
        {
            Alert that = obj as Alert;
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
