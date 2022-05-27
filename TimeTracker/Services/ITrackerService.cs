using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Dto.Tracker;
using TimeTracker.Models;

namespace TimeTracker.Services
{
    public interface ITrackerService
    {
        Task<IEnumerable<Tracker>> GetAllByEmployeeIdAndDateAsync(int employeeId, DateTime date);
        Task<IEnumerable<Tracker>> GetAllByEmployeeIdAndWeekNumberAsync(int employeeId, int weekNumber);
        Task<bool> AddAsync(CreateTrackerRequest request);
    }
}
