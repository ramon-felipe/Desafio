using Holiday.Domain;
using System;
using holiday = Holiday.Domain.Holiday;


namespace Holiday.Repository.Interfaces
{
    public interface IUpdateHolidays
    {
        holiday UpdateHoliday(int id, DateTime date, string name, HolidayType holidayType);
    }
}
