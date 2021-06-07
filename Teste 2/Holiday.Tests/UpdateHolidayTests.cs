using Holiday.Application;
using Holiday.Domain;
using Holiday.Domain.Models;
using Holiday.Repository;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using holiday = Holiday.Domain.Holiday;

namespace Holiday.Tests
{
    public class UpdateHolidayTests
    {
        private ILoggerFactory _loggerFactory;
        private ILogger<HolidayApplication> _holidayApplicationLogger;
        private InMemoryData _holidayDB;
        private IHolidayApplication _holidayApplication;

        [SetUp]
        public void Setup()
        {
            _loggerFactory = new LoggerFactory();
            _holidayApplicationLogger = new Logger<HolidayApplication>(_loggerFactory);
            _holidayDB = new InMemoryData();
            _holidayApplication = new HolidayApplication(_holidayApplicationLogger, _holidayDB);
        }

        [Test]
        public void UpdateHoliday_ShouldPass_Test()
        {
            // Arrange
            var holidayId = _holidayDB.GetLastHolidayId() + 1;
            var oldHoliday = new holiday {
                Id = holidayId,
                Date = new DateTime(2000, 12, 30),
                Name = "Test holiday",
                Type = HolidayType.Nacional
            };
            var updateRequest = new HolidayUpdateRequestModel
            {
                Id = holidayId,
                HolidayType = HolidayType.Nacional,
                Name = "Ano novo",
                Day = 9,
                Month = 12,
                Year = 2021
            };

            _holidayDB.Holidays.Add(oldHoliday);

            //Act
            var newHoliday = _holidayApplication.UpdateHoliday(updateRequest);

            //Assert
            Assert.AreEqual(newHoliday.Id, holidayId);
            Assert.AreEqual(newHoliday.Name, updateRequest.Name);
            Assert.AreEqual(0,
                newHoliday.Date.CompareTo(new DateTime(updateRequest.Year, updateRequest.Month, updateRequest.Day))
            );
            Assert.AreEqual(newHoliday.Type, updateRequest.HolidayType);
        }

        [Test]
        public void UpdateHoliday_ThatIdNotExists_ReturnNull_Test()
        {
            // Arrange
            var holidayId = _holidayDB.GetLastHolidayId() + 1;
            var updateRequest = new HolidayUpdateRequestModel
            {
                Id = holidayId + 1, // Get an holiday that not exists
                HolidayType = HolidayType.Nacional,
                Name = "Ano novo",
                Day = 9,
                Month = 12,
                Year = 2021
            };

            //Act
            var newHoliday = _holidayApplication.UpdateHoliday(updateRequest);

            //Assert
            Assert.IsNull(newHoliday);
        }

        [Test]
        public void UpdateHoliday_InvalidDate_ShouldGiveError_Test()
        {
            // Arrange
            var holidayId = _holidayDB.GetLastHolidayId() + 1;
            var oldHoliday = new holiday
            {
                Id = holidayId,
                Date = new DateTime(2000, 12, 30),
                Name = "Test holiday",
                Type = HolidayType.Nacional
            };
            var updateRequest = new HolidayUpdateRequestModel
            {
                Id = holidayId,
                HolidayType = HolidayType.Nacional,
                Name = "Ano novo",
                Day = 31,
                Month = 2,
                Year = 2021
            };

            _holidayDB.Holidays.Add(oldHoliday);

            //Act

            //Assert
            var ex = Assert.Throws<Exception>(() => _holidayApplication.UpdateHoliday(updateRequest));
            Assert.That(ex.Message, Is.EqualTo("Year, Month, and Day parameters describe an un-representable DateTime."));
        }
    }
}