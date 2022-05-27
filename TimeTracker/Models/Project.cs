using System;
using System.Collections.Generic;

namespace TimeTracker.Models
{
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Employee> Employees { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Project project &&
                   ID == project.ID &&
                   Name == project.Name &&
                   StartDate == project.StartDate &&
                   EndDate == project.EndDate;
       }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name, StartDate, EndDate, Employees);
        }
    }
}
