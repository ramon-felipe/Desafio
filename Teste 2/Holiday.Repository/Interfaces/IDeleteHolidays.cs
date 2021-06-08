using holiday = Holiday.Domain.Models.Holiday;

namespace Holiday.Repository.Interfaces
{
    public interface IDeleteHolidays
    {
        holiday DeleteHoliday(int id);
        int DeleteAllHolidays();
    }
}
