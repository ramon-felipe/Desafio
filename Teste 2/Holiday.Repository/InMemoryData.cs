﻿using Holiday.Domain;
using System;
using System.Collections.Generic;
using holiday = Holiday.Domain.Holiday;
using System.Linq;
using Holiday.Repository.Interfaces;

namespace Holiday.Repository
{
    public class InMemoryData : IDataBase
    {
        public List<holiday> Holidays { get; set; }

        /// <summary>
        /// Initializes a list with holidays.
        /// </summary>
        public InMemoryData()
        {
            Holidays = new List<holiday>
            {
                new holiday{Id = 1, Date = new DateTime(2021, 09, 07), Name = "Independência do Brasil", Type = HolidayType.Nacional },
                new holiday{Id = 2, Date = new DateTime(2021, 07, 09), Name = "Revolução Constitucionalista de 1932", Type = HolidayType.Estadual },
                new holiday{Id = 3, Date = new DateTime(2021, 08, 06), Name = "Dia do Padroeiro", Type = HolidayType.Municipal },
            };
        }
        public IEnumerable<holiday> GetAllHolidays()
        {
            return Holidays;
        }

        public IEnumerable<holiday> GetHoliday(int month, int year)
        {
            DateTime date = new DateTime(year, month, 1);

            return Holidays.Where(h => h.Date.Month == month && h.Date.Year == year);
        }

        public holiday GetHoliday(int id)
        {
            return Holidays.FirstOrDefault(h => h.Id == id);
        }

        public holiday UpdateHoliday(int id, DateTime date, string name, HolidayType holidayType)
        {
            return Holidays.Where(h => h.Id == id)
                .Select(h => { h.Date = date; h.Name = name; h.Type = holidayType; return h; })
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the last holiday ID
        /// </summary>
        /// <returns></returns>
        public int GetLastHolidayId()
        {
            var totalHolidays = Holidays.Count;

            return totalHolidays > 0 ? Holidays.Max(h => h.Id) : 0;
        }

        /// <summary>
        /// Gets the next ID of the next holiday the will be added
        /// </summary>
        /// <returns></returns>
        public int GetNextHolidayId()
        {
            return GetLastHolidayId() + 1;
        }

        public holiday AddHoliday(DateTime date, string name, HolidayType holidayType)
        {
            var nextHolidayId = GetNextHolidayId();
            var holiday = new holiday
            {
                Id = nextHolidayId,
                Name = name,
                Date = date,
                Type = holidayType
            };

            Holidays.Add(holiday);

            return holiday;
        }

        public holiday DeleteHoliday(int id)
        {
            var holiday = GetHoliday(id);
            Holidays.Remove(holiday);

            return holiday;
        }

        public int DeleteAllHolidays()
        {
            var totalHolidaysDeleted = Holidays.Count;
            Holidays.Clear();

            return totalHolidaysDeleted;
        }
    }
}