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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Visyn.Exceptions;

namespace Visyn.Serialize.Converters
{
    /// <summary>
    /// Convert a value to a date time value
    /// </summary>
    public sealed class DateTimeMultiFormatConverter : ConverterBase<DateTime>
    {
        private readonly string[] _formats;

        /// <summary>
        /// Convert a value to a date time value using multiple formats
        /// </summary>
        public DateTimeMultiFormatConverter(string format1, string format2)
            : this(new[] { format1, format2 }) { }

        /// <summary>
        /// Convert a value to a date time value using multiple formats
        /// </summary>
        public DateTimeMultiFormatConverter(string format1, string format2, string format3)
            : this(new[] { format1, format2, format3 }) { }

        /// <summary>
        /// Convert a date time value to a string
        /// </summary>
        /// <param name="formats">list of formats to try</param>
        private DateTimeMultiFormatConverter(string[] formats)
        {
            foreach(var format in formats)
            {
                if(string.IsNullOrEmpty(format))
                    throw new BadUsageException("The format of the DateTime Converter can be null or empty.");

                try
                {   // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    DateTime.Now.ToString(format);
                }
                catch
                {
                    throw new BadUsageException($"The format: '{format} is invalid for the DateTime Converter.");
                }
            }
#if false
            for (var i = 0; i < formats.Length; i++)
            {
                if (formats[i] == null ||
                    formats[i] == string.Empty)
                    throw new BadUsageException("The format of the DateTime Converter can be null or empty.");

                try
                {   // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    DateTime.Now.ToString(formats[i]);
                }
                catch
                {
                    throw new BadUsageException($"The format: '{formats[i]} is invalid for the DateTime Converter.");
                }
            }
#endif

            _formats = formats;
        }

        /// <summary>
        /// Convert a date time value to a string
        /// </summary>
        /// <param name="text">DateTime value to convert</param>
        /// <returns>string DateTime value</returns>
        public override object StringToField(string text)
        {
            if (text == null)
                text = string.Empty;

            DateTime val;
            if (!DateTime.TryParseExact(text.Trim(), _formats, null, DateTimeStyles.None, out val))
            {
                string extra = " does not match any of the given formats: " + CreateFormats();
                throw new ConvertException(text, typeof(DateTime), extra);
            }
            return val;
        }

        /// <summary>
        /// Create a list of formats to pass to the DateTime tryparse function
        /// </summary>
        /// <returns>string DateTime value</returns>
        private string CreateFormats()
        {
            var sb = new StringBuilder();

            for (var i = 0; i < _formats.Length; i++)
            {
                if (i == 0)
                    sb.Append($"'{ _formats[i]}'");
                else
                    sb.Append($", '{_formats[i]}'");
            }

            var result = sb.ToString();
            var simplerResult = string.Join(", ", _formats.Select((f) => $"'{f}'"));
            Debug.Assert(result.Equals(simplerResult));
            return simplerResult;
        }

        /// <summary>
        /// Convert a date time value to a string (uses first format for output
        /// </summary>
        /// <param name="dateTime">DateTime value to convert</param>
        /// <returns>string DateTime value</returns>
        public override string FieldToString(object dateTime)
        {
            return dateTime == null ? string.Empty : Convert.ToDateTime(dateTime).ToString(_formats[0]);
        }
    }
}
