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
using System.Diagnostics;

namespace Visyn.Serialize.Converters
{
    public class TimeSpanToHoursMinutesSecondsConverter : ConverterBase<TimeSpan>, IHasUnits
    {
        #region Implementation of IHasUnits
        public string Units => "HH:mm:ss";
        #endregion

        public override object StringToField(string text)
        {
            var split = text.Split(':');
            if (split?.Length != 3) return TimeSpan.Zero;
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            double milliseconds = 0.0;
            if (!int.TryParse(split[0], out hours))
            {
                hours = 0;
            }
            if (!int.TryParse(split[1], out minutes))
            {
                minutes = 0;
            }
            if(split[2].Contains("."))
            {
                if(double.TryParse(split[2], out milliseconds))
                {
                    seconds = (int)Math.Floor(milliseconds);
                    milliseconds = (milliseconds - seconds)*1000;
                    Debug.Assert(milliseconds >= 0.0 && milliseconds < 1000);
                }
                else
                {
                    seconds = 0;
                    milliseconds = 0.0;
                }
            }
            else
            {
                if (!int.TryParse(split[2], out seconds))
                {
                    seconds = 0;
                }
            }

            return new TimeSpan(hours, minutes, seconds, (int)milliseconds);
        }

        public override string FieldToString(object fieldValue)
        {
            if (!(fieldValue is TimeSpan)) return 0.ToString();
            var timespan = (TimeSpan)fieldValue;
            return timespan.Milliseconds == 0 ? 
                $"{timespan.Hours:00}:{timespan.Minutes:00}:{timespan.Seconds:00}" : 
                $"{timespan.Hours:00}:{timespan.Minutes:00}:{timespan.Seconds:00}.{timespan.Milliseconds:000}";
        }
    }
}
