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
    /// Converter exception
    /// </summary>
    public class ExceptionConverter : ConverterBase<ExceptionConverter>
    {
        /// <summary>
        /// Convert a field definition to a string
        /// </summary>
        /// <param name="exception">Convert exception object</param>
        /// <returns>Field as a string or null</returns>
        public override string FieldToString(object exception)
        {
            if (exception == null) return string.Empty;
            var f = exception as ConvertException;
            if (f != null)
            {
                return $"In the field '{ f.FieldName }': {f.Message.Replace(Environment.NewLine, " -> ")}";
            }
            return ((Exception)exception).Message.Replace(Environment.NewLine, " -> ");
        }

        /// <summary>
        /// Convert a general exception to a string
        /// </summary>
        /// <param name="text">exception to convert</param>
        /// <returns>Exception from field</returns>
        public override object StringToField(string text)
        {
            return new Exception(text);
        }
    }
}
