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
using System.Reflection;
using Visyn.Exceptions;
using Visyn.Types;

namespace Visyn.Serialize.Converters
{
    /// <summary>
    /// Class that provides static methods that returns a default 
    /// <see cref="ConverterBase">Converter</see> to the basic types.
    /// </summary>
    /// <remarks>
    ///     Used by the <see cref="FieldConverterAttribute"/>.
    /// </remarks>
    public static class ConverterFactory
    {
        public const string DefaultDecimalSeparator = ".";

        #region "  CreateCulture  "

        /// <summary>
        /// Return culture information for with comma decimal separator or comma decimal separator
        /// </summary>
        /// <param name="decimalSeperator">Decimal separator string</param>
        /// <returns>Cultural information based on separator</returns>
        internal static CultureInfo CreateCulture(string decimalSeperator)
        {
            var ci = new CultureInfo(CultureInfo.CurrentCulture.Name);

            switch (decimalSeperator)
            {
                case ".":
                    ci.NumberFormat.NumberDecimalSeparator = ".";
                    ci.NumberFormat.NumberGroupSeparator = ",";
                    break;
                case ",":
                    ci.NumberFormat.NumberDecimalSeparator = ",";
                    ci.NumberFormat.NumberGroupSeparator = ".";
                    break;
                default:
                    throw new BadUsageException("You can only use '.' or ',' as decimal or group separators");
            }
            return ci;
        }

        #endregion

        #region "  GetDefaultConverter  "

        /// <summary>
        /// Check the type of the field and then return a converter for that particular type
        /// </summary>
        /// <param name="fieldName">Field name to check</param>
        /// <param name="fieldType">Type of the field to check</param>
        /// <returns>Converter for this particular field</returns>
        [Obsolete("This should be re-written to find converters via reflection")]
        public static IFieldConverter GetDefaultConverter(string fieldName, Type fieldType)
        {
            if (fieldType.IsArray)
            {
                if (fieldType.GetArrayRank() != 1) {
                    throw new BadUsageException($"The array field: '{fieldName}' has more than one dimension and is not supported by the library.");
                }

                fieldType = fieldType.GetElementType();

                if (fieldType.IsArray) {
                    throw new BadUsageException($"The array field: '{ fieldName}' is a jagged array and is not supported by the library.");
                }
            }
            var fieldTypeInfo = fieldType.GetTypeInfo();
            if (fieldTypeInfo.IsValueType &&
                fieldTypeInfo.IsGenericType &&
                fieldTypeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                fieldType = fieldTypeInfo.GenericTypeArguments[0]; //GetGenericArguments[0];
            }

            // Try to assign a default Converter
            if (fieldType == typeof (string)) return null;
            if (fieldType == typeof (Int16)) return new Int16Converter();

            if (fieldType == typeof (Int32)) return new Int32Converter();

            if (fieldType == typeof (Int64))  return new Int64Converter();

            if (fieldType == typeof (SByte)) return new SByteConverter();

            if (fieldType == typeof (UInt16)) return new UInt16Converter();

            if (fieldType == typeof (UInt32)) return new UInt32Converter();

            if (fieldType == typeof (UInt64)) return new UInt64Converter();

            if (fieldType == typeof (byte)) return new ByteConverter();

            if (fieldType == typeof (decimal)) return new DecimalConverter();

            if (fieldType == typeof (double)) return new DoubleConverter();

            if (fieldType == typeof (Single)) return new SingleConverter();

            if (fieldType == typeof (DateTime)) return new DateTimeConverter();
            if (fieldType == typeof(TimeSpan)) return new TimeSpanToSecondsConverter();

            if (fieldType == typeof (bool)) return new BooleanConverter();

            if (fieldType == typeof (char)) return new CharConverter();
            if (fieldType == typeof (Guid)) return new GuidConverter();
            if (fieldTypeInfo.IsEnum) return new EnumConverter(fieldType);

            throw new BadUsageException($"The field: '{ fieldName}' has the type: {fieldType.Name} that is not a system type, so this field need a CustomConverter ( Please Check the docs for more Info).");
        }

        #endregion
    }


    //// Added by Alexander Obolonkov 2007.11.08
    //public sealed class StringConverter : ConverterBase
    //{
    //    string mFormat;

    //    public StringConverter()
    //        : this(null)
    //    {
    //    }

    //    public StringConverter(string format)
    //    {
    //            //throw new BadUsageException("The format of the String Converter can be null or empty.");

    //        if (String.IsNullOrEmpty(format))
    //            mFormat = null;
    //        else
    //        {
    //            mFormat = format;

    //            try
    //            {
    //                string tmp = String.Format(format, "Any String");
    //            }
    //            catch
    //            {
    //                throw new BadUsageException(
    //                    String.Format("The format: '{0}' is invalid for the String Converter.", format));
    //            }
    //        }
    //    }

    //    public override object StringToField(string from)
    //    {
    //        if (from == null)
    //            return string.Empty;
    //        if (from.Length == 0)
    //            return string.Empty;

    //        try
    //        {
    //            if (mFormat == null)
    //                return from;
    //            else
    //                return String.Format(mFormat, from);

    //            //if (m_intMaxLength > 0)
    //            //    strRet = strRet.Substring(0, m_intMaxLength);
    //        }
    //        catch
    //        {
    //            throw new ConvertException(from, typeof(String), "TODO Extra Info");
    //        }

    //    }

    //    public override string FieldToString(object from)
    //    {
    //        if (from == null)
    //            return string.Empty;
    //        else
    //            return String.Format(mFormat, from);
    //    }
    //}

}
