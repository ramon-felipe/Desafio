using Holiday.API.Controllers;
using Holiday.Application;
using Holiday.Domain;
using Holiday.Domain.Models;
using Holiday.Repository;
using Holiday.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using holiday = Holiday.Domain.Holiday;

namespace Holiday.Tests
{
    public class GetHolidayTests
    {
        private HolidaysController _holidaysController;
        private ILoggerFactory _loggerFactory;
        private ILogger<HolidayApplication> _holidayApplicationLogger;
        private ILogger<HolidaysController> _holidaysControllerLogger;
        private InMemoryData _holidayDB;
        private IHolidayApplication _holidayApplication;

        [SetUp]
        public void Setup()
        {
            _loggerFactory = new LoggerFactory();
            _holidayApplicationLogger = new Logger<HolidayApplication>(_loggerFactory);
            _holidaysControllerLogger = new Logger<HolidaysController>(_loggerFactory);
            _holidayDB = new InMemoryData();
            _holidayApplication = new HolidayApplication(_holidayApplicationLogger, _holidayDB);
            _holidaysController = new HolidaysController(_holidaysControllerLogger, _holidayApplication);
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
            var requestModel = new HolidayGetByDateRequestModel
            {
                 Month = 09, Year = 2021
            };

            //Act
            var holidays = _holidayApplication.GetHoliday(requestModel);

            //Assert
            Assert.NotNull(holidays);
            Assert.That(holidays.Count() > 0);
        }

        [Test]
        public void GetHoliday_InvalidMonth_ShouldTrowExeption_Test()
        {
            // Arrange
            var requestModel = new HolidayGetByDateRequestModel
            {
                Month = 19,
                Year = 2021
            };

            //Act            

            //Assert
            var ex = Assert.Throws<Exception>(() => _holidayApplication.GetHoliday(requestModel));
            Assert.That(ex.Message, Is.EqualTo("Year, Month, and Day parameters describe an un-representable DateTime."));
            
        }

        [Test]
        public void GetHoliday_InvalidModelState_ShouldReturnBadRequest_Test()
        {
            // Arrange
            var requestModel = new HolidayGetByDateRequestModel
            {
                Month = 12,
                Year = 201
            };

            //Act
            _holidaysController.ModelState.AddModelError("error", "error");
            var result = _holidaysController.GetHoliday(requestModel);
            var badRequestResult = result as BadRequestObjectResult;

            //Assert
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("A bad request call was made. Verify the parameters you used.", badRequestResult.Value);
        }

        [Test]
        public void GetHoliday_ValidID_ShouldReturn_Test()
        {
            // Arrange
            var requestModel = new HolidayGetByIdRequestModel
            {
                Id = 1
            };

            //Act
            var result = _holidayApplication.GetHoliday(requestModel);

            //Assert
            Assert.That(result != null);
        }

/*        private void PopulateInMemoryData()
        {
            _holidayDB.Holidays = new List<holiday>
            {
                new holiday{Id = 1, Date = new DateTime(2021, 09, 07), Name = "Independência do Brasil", Type = HolidayType.Nacional },
                new holiday{Id = 2, Date = new DateTime(2021, 07, 09), Name = "Revolução Constitucionalista de 1932", Type = HolidayType.Estadual },
                new holiday{Id = 3, Date = new DateTime(2021, 08, 06), Name = "Dia do Padroeiro", Type = HolidayType.Municipal },
            };
        }*/
    }
}