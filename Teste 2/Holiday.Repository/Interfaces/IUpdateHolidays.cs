using Holiday.Domain.Models;
using System;
using holiday = Holiday.Domain.Models.Holiday;

namespace Holiday.Repository.Interfaces
{
    public interface IUpdateHolidays
    {
        holiday UpdateHoliday(int id, DateTime date, string name, HolidayType holidayType);
    }
}
