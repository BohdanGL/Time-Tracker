using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Dto.Project;
using TimeTracker.Models;

namespace TimeTracker.Services
{
    public interface IProjectService
    {
        Task<bool> AddAsync(CreateProjectRequest request);
        Task DeleteAsync(int id);
        Task EditAsync(int id, CreateProjectRequest request);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
    }
}