using System;
using System.Collections.Generic;
using System.Text;
using holiday = Holiday.Domain.Holiday;

namespace Holiday.Repository.Interfaces
{
    public interface IGetHolidays
    {
        IEnumerable<holiday> GetAllHolidays();
        IEnumerable<holiday> GetHoliday(int month, int year);
        holiday GetHoliday(int id);
    }
}
