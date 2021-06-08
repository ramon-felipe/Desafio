using System.Collections.Generic;
using holiday = Holiday.Domain.Models.Holiday;

namespace Holiday.Repository.Interfaces
{
    public interface IGetHolidays
    {
        IEnumerable<holiday> GetAllHolidays();
        IEnumerable<holiday> GetHoliday(int month, int year);
        holiday GetHoliday(int id);
    }
}
