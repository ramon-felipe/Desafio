using System;
using System.ComponentModel.DataAnnotations;

namespace Holiday.Domain.Models
{
    public class HolidayGetByDateRequestModel
    {
        [Required]
        [Range(1, 12, ErrorMessage = "Enter a month number between 1 and 12")]
        public int Month { get; set; }

        [Required]
        [Range(1970, 2100, ErrorMessage = "Enter a year number between 1970 and 2100")]
        public int Year { get; set; }
    }
}
