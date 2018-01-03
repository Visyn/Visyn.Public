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

namespace Visyn.Serialize
{
    /// <summary>
    /// Indicates the Conversion used in the <see cref="T:FileHelpers.FieldConverterAttribute"/>.
    /// </summary>
    public enum ConverterKind
    {
        /// <summary>Null Converter.</summary>
        None = 0,

        /// <summary>
        /// <para>Convert from or to <b>Date</b> values.</para>
        /// <para>Params: arg1 is the <b>string</b> with the date format.</para>
        /// </summary>
        Date,

        /// <summary>
        /// <para>Convert from or to <b>Boolean</b> values.</para>
        /// <para>Params: arg1 is the <b>TRUE</b> string</para>
        /// <para>Params: arg2 is the <b>FALSE</b> string</para>
        /// </summary>
        Boolean,

        /// <summary>
        /// <para>Convert from or to <b>Byte</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Byte,

        /// <summary>
        /// <para>Convert from or to <b>Int16 or short</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Int16,

        /// <summary>
        /// <para>Convert from or to <b>Int32 or int</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Int32,

        /// <summary>
        /// <para>Convert from or to <b>Int64 or long</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Int64,

        /// <summary>
        /// <para>Convert from or to <b>Decimal</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Decimal,

        /// <summary>
        /// <para>Convert from or to <b>Double</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Double,
        /// <summary>
        /// <para>Convert from or to <b>Double</b> values. Understands Percent '%' symbol 
        /// and if present returns number /100 only while reading</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        PercentDouble,

        /// <summary>
        /// <para>Convert from or to <b>Single</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        Single,

        /// <summary>
        /// <para>Convert from or to <b>Byte</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        SByte,

        /// <summary>
        /// <para>Convert from or to <b>UInt16 or unsigned short</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        UInt16,

        /// <summary>
        /// <para>Convert from or to <b>UInt32 or unsigned int</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        UInt32,

        /// <summary>
        /// <para>Convert from or to <b>UInt64 or unsigned long</b> values.</para>
        /// <para>Params: arg1 is the <b>decimal separator</b>, by default '.'</para>
        /// </summary>
        UInt64,

        /// <summary>
        /// <para>Convert from or to <b>Date</b> values using more than one valid format.</para>
        /// <para>Params: arg1 is a <b>string</b> with the main date format. This format is the unique used for write.</para>
        /// <para>Params: arg2 is a <b>string</b> with another valid read format.</para>
        /// <para>Params: arg3 is a <b>string</b> with another valid read format.</para>
        /// </summary>
        DateMultiFormat,

        // Added by Alexander Obolonkov 2007.11.08
        /// <summary>
        /// <para>Convert from or to <b>Char</b> values.</para>
        /// <para>Params: arg1 is a <b>string</b> with "" for default behavior, "x" for make the char ToLower or "X" for make it ToUpper.</para>
        /// </summary>
        Char,
        // Added by Alexander Obolonkov 2007.11.08
        /// <summary>
        /// <para>Convert from/to <b>Guid</b> values.</para>
        /// <para>Params: arg1 is a <b>string</b> with one of the Guid.ToString() formats: "N", "D", "B", or "P"</para>
        /// <para>   "N" ->  xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx </para>
        /// <para>   "D" ->  xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx </para>
        /// <para>   "B" ->  {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx} </para>
        /// <para>   "P" ->  (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx) </para>
        /// </summary>
        Guid
    }
}
