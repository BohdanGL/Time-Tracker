using System;

namespace TimeTracker.Models
{
    public class ActivityType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ActivityType type &&
                   ID == type.ID &&
                   Name == type.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name);
        }
    }
}
