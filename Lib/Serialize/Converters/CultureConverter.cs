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

using System.Globalization;

namespace Visyn.Serialize.Converters
{
    /// <summary>
    /// Convert a numeric value with separators into a value
    /// </summary>
    public abstract class CultureConverter<T> : ConverterBase<T>
    {
        /// <summary>
        /// Culture information based on the separator
        /// </summary>
        protected CultureInfo Culture;

        /// <summary>
        /// Convert to a type given a decimal separator
        /// </summary>
        /// <param name="T">type we are converting</param>
        /// <param name="decimalSeparator">Separator</param>
        protected CultureConverter(string decimalSeparator)// : base(T)
        {
            Culture = ConverterFactory.CreateCulture(decimalSeparator);
        }

        /// <summary>
        /// Convert the field to a string representation
        /// </summary>
        /// <param name="field">Object to convert</param>
        /// <returns>string representation</returns>
        public sealed override string FieldToString(object field)
        {
            return field?.ToString() ?? string.Empty;
            //return ((IConvertible) field)?.ToString(Culture) ?? string.Empty;
        }

        /// <summary>
        /// Convert a string to the object type
        /// </summary>
        /// <param name="text">String to convert</param>
        /// <returns>Object converted to</returns>
        public sealed override object StringToField(string text) => ParseString(text);

        /// <summary>
        /// Convert a string into the return object required
        /// </summary>
        /// <param name="text">Value to convert (string)</param>
        /// <returns>Converted object</returns>
        protected abstract object ParseString(string text);
    }
}
