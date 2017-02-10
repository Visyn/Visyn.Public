using System;

namespace Visyn.Public.Types.Time
{
    public static class DateTimeExtensions
    { 
        public static DateTime Floor(this DateTime date, TimeSpan interval)
        {
            return date.AddTicks(-(date.Ticks % interval.Ticks));
        }

        public static DateTime Ceiling(this DateTime date, TimeSpan interval)
        {
            var overflow = date.Ticks % interval.Ticks;

            return overflow == 0 ? date : date.AddTicks(interval.Ticks - overflow);
        }

        public static DateTime Round(this DateTime date, TimeSpan interval)
        {
            var halfIntervelTicks = (interval.Ticks + 1) >> 1;

            return date.AddTicks(halfIntervelTicks - ((date.Ticks + halfIntervelTicks) % interval.Ticks));
        }
        
        public static TimeSpan TimeSince1970(this DateTime date) => date - new DateTime(1970,1,1,0,0,0);

        public static double MillisecondsSince1970(this DateTime date) => (date - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;

        public static DateTime FromMillisecondsSince1970(this double milliseconds) => new DateTime(1970,1,1,0,0,0).AddMilliseconds(milliseconds);
    }
}