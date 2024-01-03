using System;

namespace WeatherDisplay.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime WithOffset(this DateTime dateTime, int offsetInSeconds)
        {
            return dateTime.AddSeconds(offsetInSeconds);
        }

        public static DateTime WithOffset(this DateTime dateTime, TimeSpan offset)
        {
            return dateTime.Add(offset);
        }
    }
}
