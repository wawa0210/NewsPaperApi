using System;
using static System.TimeZone;

namespace CommonLib.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimeStamp(this DateTime dateTime)
        {
           var startTime = CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(dateTime - startTime).TotalMilliseconds;
        }
    }
}
