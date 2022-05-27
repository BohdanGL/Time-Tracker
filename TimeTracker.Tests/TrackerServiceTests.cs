using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data;
using TimeTracker.Dto.Tracker;
using TimeTracker.Initializers;
using TimeTracker.Models;
using TimeTracker.Services;

namespace TimeTracker.Tests
{
    public class Tests
    {
        static AppDbContext _context;
        static ITrackerService trackerService;

        [SetUp]
        public static void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: "Test")
               .Options;

            _context = new AppDbContext(options);

            Initializer initializer = new Initializer(_context);

            initializer.InitializeRoles();
            initializer.InitializeActivityTypes();
            initializer.InitializeEmployees();
            initializer.InitializeProjects();
            initializer.InitializeTrackers();

            trackerService = new TrackerService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var tracker in _context.Trackers)
            {
                if (tracker.ID > 3)
                    _context.Trackers.Remove(tracker);
            }
            _context.SaveChanges();
        }

        private static IEnumerable<TestCaseData> GetAllByEmployeeIdAndDateData
        {
            get
            {
                Setup();
                yield return new TestCaseData(3, new DateTime(2022, 5, 20), new List<Tracker> {
                    _context.Trackers.FirstOrDefault(x=> x.ID == 3)});

                yield return new TestCaseData(5, new DateTime(2022, 5, 20), new List<Tracker> {
                    _context.Trackers.FirstOrDefault(x=> x.ID == 2)});
            }
        }

        [Test]
        [TestCaseSource(nameof(GetAllByEmployeeIdAndDateData))]
        public async Task TrackerService_GetAllByEmployeeIdAndDate(int employeeId, DateTime date,
            IEnumerable<Tracker> expected)
        {
            var result = await trackerService.GetAllByEmployeeIdAndDateAsync(employeeId, date);

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCaseSource(nameof(AddData))]
        public async Task TrackerService_Add(CreateTrackerRequest request, bool expected)
        {
            var result = await trackerService.AddAsync(request);

            Assert.AreEqual(expected, result);
        }

        private static IEnumerable<TestCaseData> AddData
        {
            get
            {
                yield return new TestCaseData(new CreateTrackerRequest
                {
                    EmployeeId = 1,
                    ActivityType = _context.ActivityTypes.FirstOrDefault(a => a.ID == 1).Name,
                    Date = DateTime.Now,
                    Hours = 4
                }, true);

                yield return new TestCaseData(new CreateTrackerRequest
                {
                    EmployeeId = 12,
                    ActivityType = _context.ActivityTypes.FirstOrDefault(a => a.ID == 1).Name,
                    Date = DateTime.Now,
                    Hours = 15
                }, false);

                yield return new TestCaseData(new CreateTrackerRequest
                {
                    EmployeeId = 2,
                    ActivityType = "Wrong!!!",
                    Date = DateTime.Now,
                    Hours = 15
                }, false);

                yield return new TestCaseData(new CreateTrackerRequest
                {
                    EmployeeId = 3,
                    ActivityType = _context.ActivityTypes.FirstOrDefault(a => a.ID == 1).Name,
                    Date = DateTime.Now.AddDays(2),
                    Hours = 12
                }, false);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetAllByEmployeeIdAndWeekNumberData))]
        public async Task TrackerService_GetAllByEmployeeIdAndWeekNumber(int employeeId, int weekNumber,
            IEnumerable<Tracker> expected)
        {
            var result = await trackerService.GetAllByEmployeeIdAndWeekNumberAsync(employeeId, weekNumber);

            Assert.AreEqual(expected, result);
        }

        private static IEnumerable<TestCaseData> GetAllByEmployeeIdAndWeekNumberData
        {
            get
            {
                yield return new TestCaseData(5, 20, new List<Tracker> {
                    _context.Trackers.FirstOrDefault(x=> x.ID == 2)});

                yield return new TestCaseData(1, 16, new List<Tracker> {
                    _context.Trackers.FirstOrDefault(x=> x.ID == 1)});
            }
        }
    }
}