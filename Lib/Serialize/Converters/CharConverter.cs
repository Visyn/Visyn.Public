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
using Visyn.Exceptions;

namespace Visyn.Serialize.Converters
{
    /// <summary>
    /// Allow characters to be converted to upper and lower case automatically.
    /// </summary>
    public sealed class CharConverter : ConverterBase<char>
    {
        /// <summary>
        /// whether we upper or lower case the character on input
        /// </summary>
        private enum CharFormat
        {
            /// <summary>
            /// Don't change the case
            /// </summary>
            NoChange = 0,

            /// <summary>
            /// Change to lower case
            /// </summary>
            Lower,

            /// <summary>
            /// change to upper case
            /// </summary>
            Upper,
        }

        /// <summary>
        /// default to not upper or lower case
        /// </summary>
        private readonly CharFormat _format = CharFormat.NoChange;


        /// <summary>
        /// Create a single character converter that does not upper or lower case result
        /// </summary>
        public CharConverter() : this("") // default,  no upper or lower case conversion
        { }

        /// <summary>
        /// Single character converter that optionally makes it upper (X) or lower case (x)
        /// </summary>
        /// <param name="format"> empty string for no upper or lower,  x for lower case,  X for Upper case</param>
        public CharConverter(string format)
        {
            switch (format.Trim())
            {
                case "x":
                case "lower":
                    _format = CharFormat.Lower;
                    break;

                case "X":
                case "upper":
                    _format = CharFormat.Upper;
                    break;

                case "":
                    _format = CharFormat.NoChange;
                    break;

                default:
                    throw new BadUsageException(
                        "The format of the Char Converter must be \"\", \"x\" or \"lower\" for lower case, \"X\" or \"upper\" for upper case");
            }
        }

        /// <summary>
        /// Extract the first character with optional upper or lower case
        /// </summary>
        /// <param name="text">String contents</param>
        /// <returns>Character (may be upper or lower case)</returns>
        public override object StringToField(string text)
        {
            if (string.IsNullOrEmpty(text)) return Char.MinValue;

            try
            {
                switch (_format)
                {
                    case CharFormat.NoChange: return text[0];

                    case CharFormat.Lower: return char.ToLower(text[0]);

                    case CharFormat.Upper: return char.ToUpper(text[0]);

                    default:
                        throw new ConvertException(text,
                            this.Type,
                            "Unknown char convert flag: " + _format);
                }
            }
            catch
            {
                throw new ConvertException(text, Type, "Upper or lower case of input string failed");
            }
        }

        /// <summary>
        /// Convert from a character to a string for output
        /// </summary>
        /// <param name="from">Character to convert from</param>
        /// <returns>String containing the character</returns>
        public override string FieldToString(object from)
        {
            switch (_format)
            {
                case CharFormat.NoChange: return Convert.ToChar(from).ToString();

                case CharFormat.Lower: return char.ToLower(Convert.ToChar(from)).ToString();

                case CharFormat.Upper: return char.ToUpper(Convert.ToChar(from)).ToString();

                default:
                    throw new ConvertException("", Type, "Unknown char convert flag " + _format);
            }
        }
    }
}
