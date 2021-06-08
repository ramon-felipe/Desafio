using Holiday.Application;
using Holiday.Domain.Models;
using Holiday.Domain.RequestModels;
using Holiday.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [AllowAnonymous]
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
        [HttpGet("get-by-date/{month:int:min(1):max(12)}/{year:int:min(1900):max(2100)}")]
        [AllowAnonymous]
        public IActionResult GetHoliday(int month, int year)
        {
            var holidays = _holidayApplication
                .GetHoliday(month, year)
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
        [HttpGet("get-by-id/{id:int}", Name = "GetById")]
        [AllowAnonymous]
        public ActionResult<HolidayViewModel> GetHoliday(int id)
        {
            var holiday = _holidayApplication.GetHoliday(id);

            if (holiday == null)
                return NotFound("Holiday not found");

            _logger.LogInformation("A holiday was found successfully by its id");

            return Ok(holiday.ToModel());
        }

        /// <summary>
        /// Add a new holiday
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [Authorize(Roles = Roles.USER + "," + Roles.ADMIN)]
        public IActionResult AddHoliday(HolidayAddRequestModel requestModel)
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

            return CreatedAtAction(nameof(GetHoliday),
                                   routeValues: new { id = holiday.Id }, 
                                   holiday.ToModel());
        }

        /// <summary>
        /// Update a holiday identified by its ID
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [Authorize(Roles = Roles.ADMIN)]
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
        /// Delete a holiday
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [Authorize(Roles = Roles.ADMIN)]
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
        [Authorize(Roles = Roles.ADMIN)]
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
