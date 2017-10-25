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
using System.Globalization;
using Visyn.Exceptions;

namespace Visyn.Serialize.Converters
{
    /// <summary>
    /// Convert a value to a date time value
    /// </summary>
    public sealed class DateTimeConverter : ConverterBase<DateTime>
    {
        private readonly string _format;
        private readonly CultureInfo _culture;

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        public DateTimeConverter() : this(DefaultDateTimeFormat) { }

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        /// <param name="format">date format see .Net documentation</param>
        public DateTimeConverter(string format)
        {
            if (string.IsNullOrEmpty(format))
                throw new BadUsageException("The format of the DateTime Converter cannot be null or empty.");

            try
            {   // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                DateTime.Now.ToString(format);
            }
            catch
            {
                throw new BadUsageException($"The format: '{format}' is invalid for the DateTime Converter.");
            }

            _format = format;
        }

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        /// <param name="format">date format see .Net documentation</param>
        /// <param name="culture">The culture used to parse the Dates</param>
        public DateTimeConverter(string format, string culture) : this(format)
        {
 
            if (culture != null)
                throw new Exception($"Use constructor with {nameof(CultureInfo)} [i.e. CultureInfo.GetCultureInfo(culture)]");
                //_culture = CultureInfo.GetCultureInfo(culture);
        }

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        /// <param name="format">date format see .Net documentation</param>
        /// <param name="cultureInfo">The culture used to parse the Dates</param>
        public DateTimeConverter(string format, CultureInfo cultureInfo) : this(format)
        {
            if (cultureInfo != null)
                _culture = cultureInfo;
        }

        /// <summary>
        /// Convert a string to a date time value
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>DateTime value</returns>
        public override object StringToField(string text)
        {
            if (text == null) text = string.Empty;

            DateTime val;
            if (DateTime.TryParseExact(text.Trim(), _format, _culture, DateTimeStyles.None, out val)) return val;

            if (text.Length > _format.Length)
                throw new ConvertException(text, typeof(DateTime),
                    $" There are more chars in the Input String than in the Format string: '{_format}'");
            if (text.Length < _format.Length)
                throw new ConvertException(text, typeof(DateTime),
                    $" There are fewer chars in the Input String than in the Format string: '{ _format}'");
            throw new ConvertException(text, typeof(DateTime), $" Using the format: '{ _format }'");
        }

        /// <summary>
        /// Convert a date time value to a string
        /// </summary>
        /// <param name="dateTime">DateTime value to convert</param>
        /// <returns>string DateTime value</returns>
        public override string FieldToString(object dateTime)
        {
            return dateTime == null ? string.Empty : Convert.ToDateTime(dateTime).ToString(_format, _culture);
        }
    }
}
