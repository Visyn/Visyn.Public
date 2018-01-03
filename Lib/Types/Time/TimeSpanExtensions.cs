#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;

namespace Visyn.Types.Time
{
    public static class TimeSpanExtensions
    { 
        public static TimeSpan Floor(this TimeSpan span, TimeSpan interval) 
            => new TimeSpan(span.Ticks - (span.Ticks % interval.Ticks));

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