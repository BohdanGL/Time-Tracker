using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Data;
using TimeTracker.Dto.Project;
using TimeTracker.Initializers;
using TimeTracker.Models;
using TimeTracker.Services;

namespace TimeTracker.Tests
{
    class ProjectServiceTests
    {
        static AppDbContext _context;
        static IProjectService projectService;

        [SetUp]
        public static void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "ProjectTest")
               .Options;

            _context = new AppDbContext(options);

            Initializer initializer = new Initializer(_context);

            initializer.InitializeRoles();
            initializer.InitializeActivityTypes();
            initializer.InitializeEmployees();
            initializer.InitializeProjects();
            initializer.InitializeTrackers();

            projectService = new ProjectService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var project in _context.Projects)
            {
                if (project.ID > 2)
                    _context.Projects.Remove(project);
            }
            _context.SaveChanges();
        }

        [Test]
        [TestCaseSource(nameof(AddData))]
        public async Task TrackerService_Add(CreateProjectRequest request, bool expected)
        {
            var result = await projectService.AddAsync(request);

            Assert.AreEqual(expected, result);
        }

        private static IEnumerable<TestCaseData> AddData
        {
            get
            {
                yield return new TestCaseData(new CreateProjectRequest
                {
                    Name="ETS",
                    StartDate=DateTime.Now,
                    EndDate=DateTime.Now.AddDays(10),
                    EmployeesIds=new[] { 1, 2, 3 },
                }, true);

                yield return new TestCaseData(new CreateProjectRequest
                {
                    Name = "ETS2",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(-10),
                    EmployeesIds = new[] { 1, 2, 3 },
                }, false);

                yield return new TestCaseData(new CreateProjectRequest
                {
                    Name = "",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(10),
                    EmployeesIds = new[] { 1, 2, 3 },
                }, false);

                yield return new TestCaseData(new CreateProjectRequest
                {
                    Name = null,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(10),
                    EmployeesIds = new[] { 1, 2, 3 },
                }, false);
            }
        }

        
        [Test]
        [TestCaseSource(nameof(GetAllData))]
        public async Task ProjectService_GetAll(IEnumerable<Project> expected)
        {
            var result = await projectService.GetAllAsync();

            Assert.AreEqual(expected, result);
        }

        private static IEnumerable<TestCaseData> GetAllData
        {
            get
            {
                Setup();
                yield return new TestCaseData(_context.Projects);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetByIdData))]
        public async Task ProjectService_GetById(int id, Project expected)
        {
            var result = await projectService.GetByIdAsync(id);

            Assert.AreEqual(expected, result);
        }

        private static IEnumerable<TestCaseData> GetByIdData
        {
            get
            {
                yield return new TestCaseData(1, _context.Projects.FirstOrDefault(x=> x.ID == 1));
                yield return new TestCaseData(2, _context.Projects.FirstOrDefault(x => x.ID == 2));
            }
        }
    }
}
