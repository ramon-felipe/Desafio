﻿using Holiday.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Holiday.Domain.RequestModels
{
    public class HolidayAddRequestModel : IValidatableObject
    {
        [Required]
        [Range(1, 31, ErrorMessage = "Enter a day number between 1 and 31")]
        public int Day { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Enter a month number between 1 and 12")]
        public int Month { get; set; }

        [Required]
        [Range(1970, 2100, ErrorMessage = "Enter a year number between 1970 and 2100")]
        public int Year { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 2, ErrorMessage = "Enter a value between 0 and 2")]
        public HolidayType HolidayType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string newDate = Year.ToString() +"/"+ Month.ToString() + "/" + Day.ToString();
            var result = DateTime.TryParse(newDate, out _);

            if (! result)
            {
                yield return new ValidationResult($"Invalid date {newDate}.", new[] { "Date" });
            }
        }
    }
}
