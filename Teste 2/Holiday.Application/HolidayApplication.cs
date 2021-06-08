using Holiday.Domain.Models;
using Holiday.Domain.RequestModels;
using Holiday.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using holiday = Holiday.Domain.Models.Holiday;

namespace Holiday.Application
{
    public class HolidayApplication : IHolidayApplication
    {
        private readonly ILogger<HolidayApplication> _logger;
        private readonly IDataBase _holidayDB;

        public HolidayApplication(ILogger<HolidayApplication> logger,
                                  IDataBase database)
        {
            _logger = logger;
            _holidayDB = database;
        }

        public holiday AddHoliday(HolidayAddRequestModel requestModel)
        {
            try
            {
                _logger.LogInformation("Adding a new holiday...");

                var date = new DateTime(requestModel.Year, requestModel.Month, requestModel.Day);
                var holiday = _holidayDB
                    .AddHoliday(date, requestModel.Name, requestModel.HolidayType);

                return holiday;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error adding a new holiday. {e.Message}");
                throw new Exception($"Error adding a new holiday. {e.Message}");
            }
        }

        public int DeleteAllHolidays()
        {
            try
            {
                _logger.LogInformation("Deleting all holidays...");
                return _holidayDB.DeleteAllHolidays();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error deleting all holidays. {e.Message}");
                throw new Exception($"Error adding all holidays. {e.Message}");
            }
        }

        public holiday DeleteHoliday(int id)
        {
            try
            {
                _logger.LogInformation("Deleting a holiday...");
                return _holidayDB.DeleteHoliday(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error deleting a holiday. {e.Message}");
                throw new Exception($"Error adding a holiday. {e.Message}");
            }
        }

        public IEnumerable<holiday> GetAllHolidays()
        {
            try
            {
                _logger.LogInformation("Getting all holidays...");

                var holidays = _holidayDB
                    .GetAllHolidays();

                return holidays;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting all holidays. {e.Message}");
                throw new Exception($"Error getting all holidays. {e.Message}");
            }
        }

        public IEnumerable<holiday> GetHoliday(int month, int year)
        {
            try
            {
                _logger.LogInformation($"Getting a holiday using {month} as month and {year} as year...");
                var holidays = _holidayDB
                    .GetHoliday(month, year);

                return holidays;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting holiday. {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public holiday GetHoliday(int id)
        {
            try
            {
                _logger.LogInformation("Calling GetHoliday by id...");
                var holiday = _holidayDB.GetHoliday(id);

                return holiday;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error getting a holiday. {e.Message}");
                throw new Exception(e.Message);
            }
        }

        public holiday UpdateHoliday(HolidayUpdateRequestModel requestModel)
        {
            try
            {
                var date = new DateTime(requestModel.Year, requestModel.Month, requestModel.Day);
                _logger.LogInformation("Calling UpdateHoliday...");
                var holiday = _holidayDB.UpdateHoliday(requestModel.Id, date, requestModel.Name, requestModel.HolidayType);

                return holiday;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error updating a holiday. {e.Message}");
                throw new Exception(e.Message);
            }
        }
    }
}
