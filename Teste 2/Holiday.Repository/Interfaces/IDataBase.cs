using System;
using System.Collections.Generic;
using holiday = Holiday.Domain.Models.Holiday;

namespace Holiday.Repository.Interfaces
{
    public interface IDataBase : IGetHolidays, IUpdateHolidays, IAddHoliday, IDeleteHolidays
    {
        int GetLastHolidayId();
    }
}
