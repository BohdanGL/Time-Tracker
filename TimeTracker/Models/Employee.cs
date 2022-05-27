using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TimeTracker.Models
{
    public enum Sex { Male, Female }
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
        [JsonIgnore]
        public Project? Project { get; set; }
        public Role? Role { get; set; }
        [NotMapped]
        public string ProjectName
        {
            get
            {
                return Project.Name;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Employee employee &&
                   ID == employee.ID &&
                   Name == employee.Name &&
                   Sex == employee.Sex &&
                   BirthDate == employee.BirthDate &&
                   EqualityComparer<Project>.Default.Equals(Project, employee.Project) &&
                   EqualityComparer<Role>.Default.Equals(Role, employee.Role) &&
                   ProjectName == employee.ProjectName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name, Sex, BirthDate, Role, ProjectName);
        }
    }
}
