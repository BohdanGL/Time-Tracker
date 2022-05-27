using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Data;
using TimeTracker.Dto.Project;
using TimeTracker.Models;

namespace TimeTracker.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.Include(x => x.Employees).ThenInclude(x => x.Role).ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(x => x.Employees)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        private async Task<Project> Create(CreateProjectRequest request)
        {
            var project = new Project();

            if (string.IsNullOrEmpty(request.Name))
                return null;

            project.Name = request.Name;

            if (request.StartDate >= request.EndDate)
                return null;

            project.StartDate = request.StartDate;
            project.EndDate = request.EndDate;

            List<Employee> employees = new List<Employee>();

            foreach (var id in request.EmployeesIds)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(x => x.ID == id);
                if (employee != null)
                {
                    employee.Project = project;
                    employees.Add(employee);
                }

            }

            project.Employees = employees;

            return project;
        }

        public async Task<bool> AddAsync(CreateProjectRequest request)
        {
            var project = await Create(request);

            if (project == null)
                return false;

            await _context.Projects.AddAsync(project);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.ID == id);

            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditAsync(int id, CreateProjectRequest request)
        {
            var project = await Create(request);

            if (project == null)
                return;

            var entity = await GetByIdAsync(id);

            if (entity == null)
                return;

            entity.Name = project.Name;
            entity.StartDate = project.StartDate;
            entity.EndDate = project.EndDate;
            entity.Employees = project.Employees;

            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
