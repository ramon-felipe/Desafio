using System;
using System.ComponentModel.DataAnnotations;

namespace Holiday.Domain.RequestModels
{
    public class HolidayGetByIdRequestModel
    {
        [Required]
        [RegularExpression(@"^[1-9]\d*$",ErrorMessage = "Verify the Id. It must be a positive integer.")]
        public int Id { get; set; }
    }
}
