using System.Globalization;

namespace Epam.ItMarathon.ApiService.Api.Validators.Common
{
    /// <summary>
    /// Validation for the dates.
    /// </summary>
    public static class DateValidators
    {
        /// <summary>
        /// Validation if the date is in correct UTC ISO format.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Returns true if format is correct, other way - false.</returns>
        public static bool DateCorrectUtcIsoFormat(string date)
        {
            return DateTime.TryParse(date, CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out _);
        }
    }
}