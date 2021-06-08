using Holiday.Domain.RequestModels;
using System.Collections.Generic;
using holiday = Holiday.Domain.Models.Holiday;


namespace Holiday.Application
{
    public interface IHolidayApplication
    {
        IEnumerable<holiday> GetAllHolidays();
        IEnumerable<holiday> GetHoliday(int month, int year);
        holiday GetHoliday(int id);
        holiday UpdateHoliday(HolidayUpdateRequestModel requestModel);
        holiday AddHoliday(HolidayAddRequestModel requestModel);
        holiday DeleteHoliday(int id);
        int DeleteAllHolidays();
    }
}
