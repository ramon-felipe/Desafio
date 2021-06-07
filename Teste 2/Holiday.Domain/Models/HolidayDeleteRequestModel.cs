using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Holiday.Domain.Models
{
    public class HolidayDeleteRequestModel
    {
        [Key]
        public int Id { get; set; }
    }
}
