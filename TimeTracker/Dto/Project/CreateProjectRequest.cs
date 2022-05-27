using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Dto.Project
{
    public class CreateProjectRequest
    {
        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Enter end date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Enter employees ids")]
        public IEnumerable<int> EmployeesIds { get; set; }
    }
}
