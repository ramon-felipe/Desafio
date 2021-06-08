using Holiday.Domain.RequestModels;
using System.Collections.Generic;
using holiday = Holiday.Domain.Models.Holiday;


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
