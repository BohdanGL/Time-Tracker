using System;
using System.Collections.Generic;

namespace TimeTracker.Models
{
    public class Tracker
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Employee Employee { get; set; }
        public string ProjectName
        {
            get
            {
                return Employee?.Project?.Name;
            }
        }
        public ActivityType ActivityType { get; set; }
        public int Hours { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Tracker tracker &&
                   ID == tracker.ID &&
                   Date == tracker.Date &&
                   EqualityComparer<Employee>.Default.Equals(Employee, tracker.Employee) &&
                   ProjectName == tracker.ProjectName &&
                   EqualityComparer<ActivityType>.Default.Equals(ActivityType, tracker.ActivityType) &&
                   Hours == tracker.Hours;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Date, Employee, ProjectName, ActivityType, Hours);
        }
    }
}
