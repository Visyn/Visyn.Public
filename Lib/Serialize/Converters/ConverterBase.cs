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
using Visyn.Exceptions;
using Visyn.Types;

namespace Visyn.Serialize.Converters
{
    public abstract class ConverterBase<T> : ConverterBase
    {
        protected ConverterBase() : base(typeof(T))
        {
        }
    }

    /// <summary>
    /// Base class to provide bi-directional
    /// Field - String conversion.
    /// </summary>
    public abstract class ConverterBase : IFieldConverter
    {
        /// <summary>
        /// The default date time format string
        /// Note: yyyy-MM-dd HH:mm:ss.FFF is recognized by Excel as a Date/Time
        /// </summary>
        private static string _defaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss.FFF";

        private static string _defaultDateFormat = "yyyy-MM-dd";
        /// <summary>
        /// If the class returns false the engines don't pass null values to the converter. 
        /// If true the engines pass all the values to the converter.
        /// </summary>
        public virtual bool CustomNullHandling => false;
        public Type Type { get; }

        /// <summary>
        /// <para>Allow you to set the default Date Format used for the Converter.</para>
        /// <para>using the same CustomDateTimeFormat that is used in the .NET framework.</para>
        /// <para>By default: "ddMMyyyy"</para>
        /// </summary>
        public static string DefaultDateTimeFormat
        {
            get { return _defaultDateTimeFormat; }
            set
            {
                try
                {   // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    DateTime.Now.ToString(value);
                }
                catch
                {
                    throw new BadUsageException("The format: '" + value + " is invalid for the DateTime Converter.");
                }
                _defaultDateTimeFormat = value;
            }
        }



        public static string DefaultDateFormat
        {
            get { return _defaultDateFormat; }
            set
            {
                try
                {   // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                    DateTime.Now.ToString(value);
                }
                catch
                {
                    throw new BadUsageException($"The format: '{value} is invalid for the {nameof(DateConverter)}.");
                }
                _defaultDateFormat = value;
            }
        }

        protected ConverterBase(Type converterType)
        {
            Type = converterType;
        }

        /// <summary>
        /// Convert a string in the file to a field value.
        /// </summary>
        /// <param name="text">The string to convert.</param>
        /// <returns>The Field value.</returns>
        public abstract object StringToField(string text);

        /// <summary>
        /// Convert a field value to an string to write this to the file.
        /// </summary>
        /// <remarks>The basic implementation just returns  from.ToString();</remarks>
        /// <param name="field">The field values to convert.</param>
        /// <returns>The string representing the field value.</returns>
        public virtual string FieldToString(object field) => field?.ToString() ?? string.Empty;

        /// <summary>
        /// Throws a ConvertException with the passed values
        /// </summary>
        /// <param name="from">The source string.</param>
        /// <param name="errorMsg" >The custom error msg.</param>
        /// <exception cref="ConvertException">Throw exception with values</exception>
        [Obsolete("Use extension method in ConverterExtensions")]
        public void ThrowConvertException(string from, string errorMsg)
        {
            throw new ConvertException(from, Type, errorMsg);
        }
    }
}
