using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Holiday.Domain.RequestModels
{
    public class HolidayDeleteRequestModel
    {
        [Key]
        public int Id { get; set; }
    }
}
