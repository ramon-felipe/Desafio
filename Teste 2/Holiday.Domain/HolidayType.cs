using System.ComponentModel;

namespace Holiday.Domain
{
    public enum HolidayType
    {
        /// <summary>
        /// Municipal holidays
        /// </summary>
        Municipal,

        /// <summary>
        /// Estadual holidays
        /// </summary>
        Estadual,

        /// <summary>
        /// Nacional holidays
        /// </summary>
        [Description("Feriado Nacional")]
        Nacional
    }
}