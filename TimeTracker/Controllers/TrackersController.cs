using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Dto.Tracker;
using TimeTracker.Models;
using TimeTracker.Services;

namespace TimeTracker.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackersController : ControllerBase
    {
        private readonly ITrackerService _trackerService;

        public TrackersController(ITrackerService trackerService)
        {
            _trackerService = trackerService;
        }


        [HttpGet]
        [Route("{employeeId:int}/{date}")]
        public async Task<ActionResult<IEnumerable<Tracker>>> GetAllUsersGetAllByEmployeeIdAndDate(int employeeId, DateTime date)
        {
            var trackers = await _trackerService.GetAllByEmployeeIdAndDateAsync(employeeId, date);

            if (trackers != null)
            {
                return new ObjectResult(trackers);
            }

            return NoContent();
        }


        [HttpGet]
        [Route("{employeeId:int}/{weekNumber:int}")]
        public async Task<ActionResult<IEnumerable<Tracker>>> GetAllByEmployeeIdAndWeekNumber(int employeeId, int weekNumber)
        {
            var trackers = await _trackerService.GetAllByEmployeeIdAndWeekNumberAsync(employeeId, weekNumber);

            if (trackers != null)
            {
                return new ObjectResult(trackers);
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> CreateTracker(CreateTrackerRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _trackerService.AddAsync(request);

                if (result)
                {
                    return Ok("Created Successfully");
                }

                return BadRequest();
            }

            return ValidationProblem("Bad Form");
        }
    }
}
