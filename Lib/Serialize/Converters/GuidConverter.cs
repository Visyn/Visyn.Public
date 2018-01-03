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

namespace Visyn.Serialize.Converters
{
    /// <summary>
    ///  Convert a GUID to and from a field value
    /// </summary>
    public sealed class GuidConverter : ConverterBase<Guid>
    {
        /// <summary>
        /// D or N or B or P (default is D: see Guid.ToString(string format))
        /// </summary>
        private readonly string _format;

        /// <summary>
        /// Create a GUID converter with the default format code "D"
        /// </summary>
        public GuidConverter() : this("D") // D or N or B or P (default is D: see Guid.ToString(string format))
        { }

        /// <summary>
        /// Create a GUID converter with formats as defined for GUID
        /// N, D, B or P
        /// </summary>
        /// <param name="format">Format code for GUID</param>
        public GuidConverter(string format)
        {
            if (string.IsNullOrEmpty(format)) format = "D";

            format = format.Trim().ToUpper();

            if (!(format == "N" || format == "D" || format == "B" || format == "P"))
                throw new BadUsageException("The format of the Guid Converter must be N, D, B or P.");

            _format = format;
        }

        /// <summary>
        /// Convert a GUID string to a GUID object for the record object
        /// </summary>
        /// <param name="text">String representation of the GUID</param>
        /// <returns>GUID object or GUID empty</returns>
        public override object StringToField(string text)
        {
            if (string.IsNullOrEmpty(text)) return Guid.Empty;

            try
            {
                return new Guid(text);
            }
            catch
            {
                throw new ConvertException(text, typeof(Guid));
            }
        }

        /// <summary>
        /// Output GUID as a string field
        /// </summary>
        /// <param name="guid">Guid object</param>
        /// <returns>GUID as a string depending on format</returns>
        public override string FieldToString(object guid) => ((Guid?)guid)?.ToString(_format) ?? string.Empty;
    }
}
