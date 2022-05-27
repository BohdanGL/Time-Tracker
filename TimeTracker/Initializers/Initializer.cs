using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Initializers
{
    public class Initializer
    {
        private readonly AppDbContext _context;

        public Initializer(AppDbContext context)
        {
            _context = context;
        }

        public void InitializeRoles()
        {
            if (_context.Roles.Any())
                return;

            _context.Roles.Add(new Role { Name = "software engineer" });
            _context.Roles.Add(new Role { Name = "business analyst" });
            _context.Roles.Add(new Role { Name = "tester" });
            _context.Roles.Add(new Role { Name = "project manager" });

            _context.SaveChanges();
        }

        public void InitializeActivityTypes()
        {
            if (_context.ActivityTypes.Any())
                return;

            _context.ActivityTypes.Add(new ActivityType { Name = "Regular work" });
            _context.ActivityTypes.Add(new ActivityType { Name = "Overtime" });

            _context.SaveChanges();
        }

        public void InitializeEmployees()
        {
            if (_context.Employees.Any())
                return;

            _context.Employees.Add(new Employee
            {
                Name = "Oleg",
                Sex = Sex.Male,
                BirthDate = new DateTime(2003, 10, 9),
                Role = _context.Roles.FirstOrDefault(x => x.Name == "tester")
            });

            _context.Employees.Add(new Employee
            {
                Name = "Alex",
                Sex = Sex.Male,
                BirthDate = new DateTime(2004, 10, 9),
                Role = _context.Roles.FirstOrDefault(x => x.Name == "software engineer")
            });

            _context.Employees.Add(new Employee
            {
                Name = "Katya",
                Sex = Sex.Female,
                BirthDate = new DateTime(2000, 11, 23),
                Role = _context.Roles.FirstOrDefault(x => x.Name == "project manager")
            });

            _context.Employees.Add(new Employee
            {
                Name = "Oksana",
                Sex = Sex.Female,
                BirthDate = new DateTime(1999, 5, 9),
                Role = _context.Roles.FirstOrDefault(x => x.Name == "project manager")
            });

            _context.Employees.Add(new Employee
            {
                Name = "Andrey",
                Sex = Sex.Male,
                BirthDate = new DateTime(2003, 11, 23),
                Role = _context.Roles.FirstOrDefault(x => x.Name == "tester")
            });

            _context.Employees.Add(new Employee
            {
                Name = "Mark",
                Sex = Sex.Male,
                BirthDate = new DateTime(1998, 1, 23),
                Role = _context.Roles.FirstOrDefault(x => x.Name == "software engineer")
            });

            _context.SaveChanges();
        }

        public void InitializeProjects()
        {
            if (_context.Projects.Any())
                return;

            _context.Projects.Add(new Project
            {
                Name = "Calculator",
                StartDate = new DateTime(2003, 10, 10),
                EndDate = new DateTime(2004, 11, 11),
                Employees = new List<Employee> { _context.Employees.FirstOrDefault(x=> x.Name == "Oleg"),
            _context.Employees.FirstOrDefault(x=> x.Name == "Alex"),
            _context.Employees.FirstOrDefault(x=> x.Name == "Katya")}
            });

            _context.Projects.Add(new Project
            {
                Name = "Finance Manager",
                StartDate = new DateTime(2020, 9, 9),
                EndDate = new DateTime(2021, 11, 11),
                Employees = new List<Employee> { _context.Employees.FirstOrDefault(x=> x.Name == "Mark"),
            _context.Employees.FirstOrDefault(x=> x.Name == "Andrey"),
            _context.Employees.FirstOrDefault(x=> x.Name == "Oksana")}
            });

            _context.SaveChanges();

            //add project to employees
            foreach (var project in _context.Projects)
            {
                foreach (var employee in project.Employees)
                {
                    employee.Project = project;
                }
            }

            _context.SaveChanges();
        }

        public void InitializeTrackers()
        {
            if (_context.Trackers.Any())
                return;

            _context.Trackers.Add(new Tracker
            {
                Date = new DateTime(2022, 4, 20),
                Employee = _context.Employees.FirstOrDefault(x => x.Name == "Oleg"),
                ActivityType = _context.ActivityTypes.FirstOrDefault(x => x.Name == "Regular work"),
                Hours = 8
            });

            _context.Trackers.Add(new Tracker
            {
                Date = new DateTime(2022, 5, 20),
                Employee = _context.Employees.FirstOrDefault(x => x.Name == "Andrey"),
                ActivityType = _context.ActivityTypes.FirstOrDefault(x => x.Name == "Overtime"),
                Hours = 2
            });

            _context.Trackers.Add(new Tracker
            {
                Date = new DateTime(2022, 5, 20),
                Employee = _context.Employees.FirstOrDefault(x => x.Name == "Katya"),
                ActivityType = _context.ActivityTypes.FirstOrDefault(x => x.Name == "Regular work"),
                Hours = 9
            });

            _context.SaveChanges();
        }
    }
}
