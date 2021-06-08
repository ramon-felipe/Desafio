using Holiday.Application;
using Holiday.Domain.Models;
using Holiday.Domain.RequestModels;
using Holiday.Repository;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;

namespace Holiday.Tests
{
    public class AddHolidayTests
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
        public void AddHoliday_ShouldPass_Test()
        {
            // Arrange
            var request = new HolidayAddRequestModel
            {
                HolidayType = HolidayType.Nacional,
                Name = "Feriado test",
                Day = 9,
                Month = 10,
                Year = 2021
            };
            var totalHolidays = _holidayDB.Holidays.Count;

            //Act
            var newHoliday = _holidayApplication.AddHoliday(request);

            //Assert
            Assert.That(_holidayDB.Holidays.Count, Is.EqualTo(totalHolidays + 1));
            Assert.AreEqual(newHoliday.Id, _holidayDB.GetLastHolidayId());
            Assert.AreEqual(newHoliday.Name, request.Name);
            Assert.AreEqual(0,
                newHoliday.Date.CompareTo(new DateTime(request.Year, request.Month, request.Day))
            );
            Assert.AreEqual(newHoliday.Type, request.HolidayType);
        }

    }
}