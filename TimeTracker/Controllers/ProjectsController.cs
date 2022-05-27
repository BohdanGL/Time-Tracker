using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Dto.Project;
using TimeTracker.Models;
using TimeTracker.Services;

namespace TimeTracker.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return new ObjectResult(await _projectService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var project = await _projectService.GetByIdAsync(id);

            if (project == null)
            {
                return NoContent();
            }

            return project;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProject(CreateProjectRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _projectService.AddAsync(request);

                if (result)
                    return Ok("Created Successfully");

                return BadRequest();
            }

            return ValidationProblem("Bad Form");

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProject(int id, CreateProjectRequest request)
        {
            if (ModelState.IsValid)
            {
                await _projectService.EditAsync(id, request);

                return Ok("Updated Successfully");
            }

            return ValidationProblem("Bad Form");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var project = await _projectService.GetByIdAsync(id);

            if (project == null)
            {
                return NoContent();
            }

            await _projectService.DeleteAsync(id);

            return Ok("Deleted Successfully");
        }
    }
}
