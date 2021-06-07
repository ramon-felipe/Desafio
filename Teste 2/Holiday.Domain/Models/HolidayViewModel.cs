using System;
using System.Collections.Generic;
using System.Text;

namespace Holiday.Domain.Models
{
    public class HolidayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int TypeNumber { get; set; }
        public string TypeName { get; set; }
    }
}
