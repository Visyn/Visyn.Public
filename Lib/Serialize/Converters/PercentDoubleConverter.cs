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

using System.Globalization;
using Visyn.Exceptions;

namespace Visyn.Serialize.Converters
{
    /// <summary>
    /// This Class is specialized version of the Double Converter
    /// The main difference being that it can handle % sign at the end of the number
    /// It gives a value which is basically number / 100.
    /// </summary>
    public sealed class PercentDoubleConverter : CultureConverter<double>
    {
        /// <summary>
        /// Convert a value to a floating point from a percentage
        /// </summary>
        public PercentDoubleConverter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a value to a floating point from a percentage
        /// </summary>
        /// <param name="decimalSeparator">dot or comma for separator</param>
        public PercentDoubleConverter(string decimalSeparator) : base( decimalSeparator) { }

        /// <summary>
        /// Convert a string to an floating point from percentage
        /// </summary>
        /// <param name="text">String value to convert</param>
        /// <returns>floating point value</returns>
        protected override object ParseString(string text)
        {
            double result;
            var blanksRemoved = StringHelper.RemoveBlanks(text);
            if (blanksRemoved.EndsWith("%"))
            {
                if (double.TryParse(blanksRemoved, NumberStyles.Number | NumberStyles.AllowExponent, Culture, out result))
                    return result / 100.0;
                throw new ConvertException(text, Type);
            }
            else
            {
                if (double.TryParse(blanksRemoved, NumberStyles.Number | NumberStyles.AllowExponent, Culture, out result))
                    return result;
                throw new ConvertException(text, Type);
            }
        }
    }
}
