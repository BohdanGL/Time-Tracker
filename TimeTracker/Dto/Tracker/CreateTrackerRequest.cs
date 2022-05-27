using System;
using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Dto.Tracker
{
    public class CreateTrackerRequest
    {
        [Required(ErrorMessage = "Enter employee id")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be > 0")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Enter the number of hours you worked")]
        [Range(1, 24, ErrorMessage = "Hours must be > 0 and <= 24")]
        public int Hours { get; set; }

        [Required(ErrorMessage = "Enter activity type")]
        public string ActivityType { get; set; }

        [Required(ErrorMessage = "Enter date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
