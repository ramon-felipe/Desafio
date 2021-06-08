using Holiday.Domain.Models;
using System;
using holiday = Holiday.Domain.Models.Holiday;


namespace Holiday.Repository.Interfaces
{
    public interface IAddHoliday
    {
        holiday AddHoliday(DateTime date, string name, HolidayType holidayType);
    }
}
