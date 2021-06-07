﻿using Holiday.Application;
using Holiday.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Holiday.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidaysController : ControllerBase
    {
        private readonly ILogger<HolidaysController> _logger;
        private readonly IHolidayApplication _holidayApplication;

        public HolidaysController(ILogger<HolidaysController> logger, 
                                 IHolidayApplication holidayApplication)
        {
            _logger = logger;
            _holidayApplication = holidayApplication;
        }

        /// <summary>
        /// Returns all the holidays
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<HolidayViewModel>> GetAll()
        {
            _logger.LogInformation("Calling GetAll holidays endpoint...");
            var holidays = _holidayApplication
                .GetAllHolidays()
                .Select(h => h.ToModel());

            if (!holidays.Any())
                return NoContent();

            _logger.LogInformation("Holidays returned successfully");


            return Ok(holidays);
        }

        /// <summary>
        /// Return the holidays by their month and year
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpGet("get-by-date")]
        public IActionResult GetHoliday(HolidayGetByDateRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"A bad request call was made. Verify the parameters you used. (mont: {requestModel.Month} / year {requestModel.Year})");
                return BadRequest("A bad request call was made. Verify the parameters you used.");
            }

            var holidays = _holidayApplication
                .GetHoliday(requestModel)
                .Select(h => h.ToModel());

            if (! holidays.Any())
                return NoContent();

            _logger.LogInformation("Holiday found successfully by its month-year");

            return Ok(holidays);
        }

        /// <summary>
        /// Return a holiday by its ID
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpGet("get-by-id")]
        public ActionResult<HolidayViewModel> GetHoliday(HolidayGetByIdRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"A bad request call was made.");
                return BadRequest("A bad request call was made. Verify the parameters you used.");
            }

            var holiday = _holidayApplication.GetHoliday(requestModel);

            if (holiday == null)
                return NotFound("Holiday not found");

            _logger.LogInformation("A holiday was found successfully by its id");

            return Ok(holiday.ToModel());
        }

        /// <summary>
        /// Update a holiday identified by its ID
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public ActionResult<HolidayViewModel> UpdateHoliday(HolidayUpdateRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"A bad request call was made.");
                return BadRequest("A bad request call was made. Verify the parameters you used.");
            }

            var holiday = _holidayApplication.UpdateHoliday(requestModel);

            if (holiday == null)
                return NotFound("Holiday not found");

            _logger.LogInformation("A new holiday was updated successfully");

            return Ok(holiday.ToModel());
        }

        /// <summary>
        /// Add a new holiday
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public ActionResult<HolidayViewModel> AddHoliday(HolidayAddRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"A bad request call was made.");
                return BadRequest("A bad request call was made. Verify the parameters you used.");
            }

            var holiday = _holidayApplication.AddHoliday(requestModel);

            if (holiday == null)
                return NotFound("Holiday not added");

            _logger.LogInformation("A new holiday was added successfully");

            return CreatedAtAction(nameof(GetHoliday), holiday.ToModel());
        }


        /// <summary>
        /// Delete a holiday
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public ActionResult<HolidayViewModel> DeleteHoliday(HolidayDeleteRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"A bad request call was made.");
                return BadRequest("A bad request call was made. Verify the parameters you used.");
            }

            var holiday = _holidayApplication.DeleteHoliday(requestModel);

            if (holiday == null)
                return NotFound("It was not possible to delete the holiday. Holiday not found.");

            _logger.LogInformation("Holiday deleted successfully");

            return Ok(holiday.ToModel());
        }

        /// <summary>
        /// Delete a holiday
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllHolidays()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"A bad request call was made.");
                return BadRequest("A bad request call was made. Verify the parameters you used.");
            }

            var totalDeletedHolidays = _holidayApplication.DeleteAllHolidays();

            if (totalDeletedHolidays == 0)
                return NotFound("No holidays were found for deletion.");

            _logger.LogInformation($"{totalDeletedHolidays} holidays deleted.");

            return Ok($"{totalDeletedHolidays} holiday(s) deleted.");
        }
    }
}
