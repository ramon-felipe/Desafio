using Holiday.API.Controllers;
using Holiday.Application;
using Holiday.Domain;
using Holiday.Domain.Models;
using Holiday.Domain.RequestModels;
using Holiday.Repository;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;

namespace Holiday.Tests
{
    public class DeleteHolidayTests
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
        public void DeleteAHoliday_ShouldPass_Test()
        {
            // Arrange
            var addRequest = new HolidayAddRequestModel
            {
                HolidayType = HolidayType.Nacional,
                Name = "Feriado test",
                Day = 9,
                Month = 10,
                Year = 2021
            };

            //Act
            var newHoliday = _holidayApplication.AddHoliday(addRequest);
            var totalHolidays = _holidayDB.Holidays.Count;
            var lastHolidayId = _holidayDB.GetLastHolidayId();
            var deleteRequest = new HolidayDeleteRequestModel { Id = lastHolidayId };
            var deletedHoliday = _holidayApplication.DeleteHoliday(deleteRequest);

            //Assert
            Assert.AreEqual(_holidayDB.Holidays.Count, totalHolidays - 1);
            Assert.AreEqual(lastHolidayId, deletedHoliday.Id);
            Assert.AreEqual(addRequest.Name, deletedHoliday.Name);
            Assert.AreEqual(newHoliday.Id, deletedHoliday.Id);
        }

        [Test]
        public void DeleteAHoliday_ThatNotExists_ShouldReturnNull_Test()
        {
            // Arrange
            var nextHolidayId = _holidayDB.GetNextHolidayId();
            var deleteRequest = new HolidayDeleteRequestModel { Id = nextHolidayId };

            //Act
            var result = _holidayApplication.DeleteHoliday(deleteRequest);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void DeleteAllHolidays_WithOneItemInTheList_ShouldPass_Test()
        {
            // Arrange
            var addRequest = new HolidayAddRequestModel
            {
                HolidayType = HolidayType.Nacional,
                Name = "Feriado test",
                Day = 9,
                Month = 10,
                Year = 2021
            };

            //Act
            _holidayApplication.AddHoliday(addRequest);

            //Act
            _holidayApplication.DeleteAllHolidays();

            //Assert
            Assert.That(_holidayDB.Holidays.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeleteAllHolidays_WithThenItemsInTheList_ShouldPass_Test()
        {
            // Arrange
            var addRequest = new HolidayAddRequestModel
            {
                HolidayType = HolidayType.Nacional,
                Name = "Feriado test",
                Day = 9,
                Month = 10,
                Year = 2021
            };

            //Act
            for (int i = 0; i < 10; i++)
            {
                _holidayApplication.AddHoliday(addRequest);
            }

            //Act
            _holidayApplication.DeleteAllHolidays();

            //Assert
            Assert.That(_holidayDB.Holidays.Count, Is.EqualTo(0));
        }
    }
}