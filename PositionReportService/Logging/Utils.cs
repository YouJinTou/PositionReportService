using System;

namespace Logging
{
    internal static class Utils
    {
        public static string GetCurrentGmtDateFormatted()
        {
            DateTime gmtTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            string formattedTime = string.Format("GMT {0}", gmtTime.ToString("HH:mm:ss MMM dd, yyyy"));

            return formattedTime;
        }
    }
}
