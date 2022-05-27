using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Interfaces
{
    public interface ITrackerService
    {
        Task<IEnumerable<Tracker>> GetAllByEmployeeIdAndDateAsync(int employeeId, DateTime date);
        Task<IEnumerable<Tracker>> GetAllByEmployeeIdAndWeekNumberAsync(int employeeId, int weekNumber);
    }
}