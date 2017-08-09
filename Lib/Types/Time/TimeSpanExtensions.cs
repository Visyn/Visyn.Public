using System;

namespace Visyn.Public.Types.Time
{
    public static class TimeSpanExtensions
    { 
        public static TimeSpan Floor(this TimeSpan span, TimeSpan interval)
        {
            return new TimeSpan(span.Ticks - (span.Ticks % interval.Ticks));
        }

        public static TimeSpan Ceiling(this TimeSpan span, TimeSpan interval)
        {
            var overflow = span.Ticks % interval.Ticks;

            return overflow == 0 ? span : new TimeSpan(span.Ticks  + (interval.Ticks - overflow));
        }

        public static TimeSpan Round(this TimeSpan span, TimeSpan interval)
        {
            var halfIntervelTicks = (interval.Ticks + 1) >> 1;

            return new TimeSpan(span.Ticks + (halfIntervelTicks - ((span.Ticks + halfIntervelTicks) % interval.Ticks)));
        }

        public static TimeSpan Multiply(this TimeSpan span, double multiply)
        {
            var ticks = span.Ticks*multiply;
            if (ticks >= TimeSpan.MaxValue.Ticks) return TimeSpan.MaxValue;
            if (ticks <= TimeSpan.MinValue.Ticks) return TimeSpan.MinValue;
            return TimeSpan.FromTicks((long)ticks);
        }

        public static TimeSpan Divide(this TimeSpan span, double divisor)
        {
            var ticks = span.Ticks / divisor;
            if (ticks >= TimeSpan.MaxValue.Ticks) return TimeSpan.MaxValue;
            if (ticks <= TimeSpan.MinValue.Ticks) return TimeSpan.MinValue;
            return TimeSpan.FromTicks((long)ticks);
        }
    }
}