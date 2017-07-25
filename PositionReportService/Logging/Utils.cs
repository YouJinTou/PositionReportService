using System;

namespace Logging
{
    /// <summary>
    /// Exposes methods for miscellaneous methods used throughout the assembly.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// Gets the current time in GMT in the "HH:mm:ss MMM dd, yyyy" format.
        /// </summary>
        /// <returns>The formatted GMT time.</returns>
        public static string GetCurrentGmtDateFormatted()
        {
            DateTime gmtTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            string formattedTime = string.Format("GMT {0}", gmtTime.ToString("HH:mm:ss MMM dd, yyyy"));

            return formattedTime;
        }
    }
}
