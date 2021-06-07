using System;
using System.Collections.Generic;
using System.Text;
using holiday = Holiday.Domain.Holiday;


namespace Holiday.Repository.Interfaces
{
    public interface IDeleteHolidays
    {
        holiday DeleteHoliday(int id);
        int DeleteAllHolidays();
    }
}
