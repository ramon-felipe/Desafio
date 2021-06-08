using Holiday.Application;
using Holiday.Repository;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Linq;

namespace Holiday.Tests
{
    public class GetHolidayTests
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
        public void GetAllHolidays_ShouldPass_Test()
        {
            // Arrange

            //Act
            var holidays = _holidayApplication.GetAllHolidays();

            //Assert
            Assert.NotNull(holidays);
            Assert.That(holidays.Count() > 0);
        }

        [Test]
        public void GetHoliday_ValidRequest_ShouldPass_Test()
        {
            // Arrange
            int month = 09;
            int year = 2021;

            //Act
            var holidays = _holidayApplication.GetHoliday(month, year);

            //Assert
            Assert.NotNull(holidays);
            Assert.That(holidays.Count() > 0);
        }

        [Test]
        public void GetHoliday_InvalidMonth_ShouldTrowExeption_Test()
        {
            // Arrange
            int month = 19;
            int year = 2021;

            //Act

            //Assert
            var ex = Assert.Throws<Exception>(() => _holidayApplication.GetHoliday(month, year));
            Assert.That(ex.Message, Is.EqualTo("Year, Month, and Day parameters describe an un-representable DateTime."));
            
        }

        [Test]
        public void GetHoliday_ValidID_ShouldReturn_Test()
        {
            // Arrange
            int id = 1;

            //Act
            var result = _holidayApplication.GetHoliday(id);

            //Assert
            Assert.That(result != null);
        }
    }
}