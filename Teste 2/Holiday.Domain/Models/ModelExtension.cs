using Holiday.Domain.ViewModels;

namespace Holiday.Domain.Models
{
    public static class ModelExtension
    {
        public static HolidayViewModel ToModel(this Holiday holiday)
        {
            return new HolidayViewModel
            {
                Id = holiday.Id,
                Name = holiday.Name,
                Date = holiday.Date.ToString("dd/MM/yyyy"),
                TypeName = holiday.Type.ToString(),
                TypeNumber = (int)holiday.Type
            };
        }
    }
}
