#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
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

namespace Visyn.Public.Types.Time
{
    public static class DateTimeExtensions
    { 
        public static DateTime Floor(this DateTime date, TimeSpan interval) => date.AddTicks(-(date.Ticks % interval.Ticks));

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

        public static DateTime DateTime1970() => new DateTime(1970, 1, 1, 0, 0, 0);
        public static TimeSpan TimeSince1970(this DateTime date) => date - new DateTime(1970,1,1,0,0,0);

        public static double MillisecondsSince1970(this DateTime date) => (date - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;

        public static DateTime FromMillisecondsSince1970(this double milliseconds) => new DateTime(1970,1,1,0,0,0).AddMilliseconds(milliseconds);

        public static double SecondsSince1970(this DateTime date) => (date - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;

        public static DateTime FromSecondsSince1970(this double seconds) => new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(seconds);
    }
}