using Holiday.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using holiday = Holiday.Domain.Holiday;


namespace Holiday.Repository.Interfaces
{
    public interface IAddHoliday
    {
        holiday AddHoliday(DateTime date, string name, HolidayType holidayType);
    }
}
