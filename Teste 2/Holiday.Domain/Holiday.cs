using Holiday.Domain.Models;
using System;

namespace Holiday.Domain
{
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public HolidayType Type { get; set; }
    }
}
