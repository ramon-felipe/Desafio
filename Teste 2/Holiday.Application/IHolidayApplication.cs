using Holiday.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using holiday = Holiday.Domain.Holiday;

namespace Holiday.Application
{
    public interface IHolidayApplication
    {
        IEnumerable<holiday> GetAllHolidays();
        IEnumerable<holiday> GetHoliday(HolidayGetByDateRequestModel requestModel);
        holiday GetHoliday(HolidayGetByIdRequestModel requestModel);
        holiday UpdateHoliday(HolidayUpdateRequestModel requestModel);
        holiday AddHoliday(HolidayAddRequestModel requestModel);
        holiday DeleteHoliday(HolidayDeleteRequestModel requestModel);
        int DeleteAllHolidays();
    }
}
