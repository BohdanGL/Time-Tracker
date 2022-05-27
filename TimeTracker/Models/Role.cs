using System;

namespace TimeTracker.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Role role &&
                   ID == role.ID &&
                   Name == role.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name);
        }
    }
}
