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
using Visyn.Exceptions;

namespace Visyn.Serialize.Converters
{
    /// <summary>
    /// COnvert a string into a byte value
    /// </summary>
    public sealed class ByteConverter : CultureConverter<byte>
    {
        /// <summary>
        /// Convert a string to a byte value using the default decimal separator
        /// </summary>
        public ByteConverter() : this(ConverterFactory.DefaultDecimalSeparator) { }

        /// <summary>
        /// Convert a string to a byte
        /// </summary>
        /// <param name="decimalSeparator">decimal separator to use '.' or ','</param>
        public ByteConverter(string decimalSeparator) : base(decimalSeparator) { }

        /// <summary>
        /// Convert a string to a byte value
        /// </summary>
        /// <param name="text">string to parse</param>
        /// <returns>byte value</returns>
        protected override object ParseString(string text)
        {
            byte res;
            if (!byte.TryParse(StringHelper.RemoveBlanks(text), NumberStyles.Number, Culture, out res))
                throw new ConvertException(text, Type);
            return res;
        }
    }
}
