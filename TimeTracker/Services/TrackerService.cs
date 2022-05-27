using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Data;
using TimeTracker.Dto.Tracker;
using TimeTracker.Models;

namespace TimeTracker.Services
{
    public class TrackerService : ITrackerService
    {
        private readonly AppDbContext _context;

        public TrackerService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Tracker>> GetAllByEmployeeIdAndDateAsync(int employeeId, DateTime date)
        {
            var trackers = await _context.Trackers
                .Include(x => x.ActivityType)
                .Include(x => x.Employee).ThenInclude(x => x.Role)
                .Include(x => x.Employee).ThenInclude(x => x.Project)
                .ToListAsync();

            return trackers.FindAll(t => t.Employee.ID == employeeId && t.Date.ToShortDateString() == date.ToShortDateString());
        }

        public async Task<IEnumerable<Tracker>> GetAllByEmployeeIdAndWeekNumberAsync(int employeeId, int weekNumber)
        {
            if (weekNumber < 0 || employeeId < 0)
                return null;

            var start = new DateTime(2022, 1, 1, 0, 0, 0);

            start = start.AddDays(7 * (weekNumber - 1));

            var end = start.AddDays(8);// range

            var trackers = await _context.Trackers
                .Include(x => x.ActivityType)
                .Include(x => x.Employee).ThenInclude(x => x.Role)
                .Include(x => x.Employee).ThenInclude(x => x.Project)
                .ToListAsync();

            return trackers.FindAll(t => t.Employee.ID == employeeId && (start <= t.Date && t.Date < end));
        }

        public async Task<bool> AddAsync(CreateTrackerRequest request)
        {
            var tracker = new Tracker();

            if (request.Date.Date > DateTime.Now.Date)
                return false;

            tracker.Date = request.Date;

            var employee = await _context.Employees.FirstOrDefaultAsync(t => t.ID == request.EmployeeId);

            if (employee == null)
                return false;

            tracker.Employee = employee;

            tracker.Hours = request.Hours;

            var activityType = await _context.ActivityTypes.FirstOrDefaultAsync(a => a.Name == request.ActivityType);

            if (activityType == null)
                return false;

            tracker.ActivityType = activityType;

            await _context.Trackers.AddAsync(tracker);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
