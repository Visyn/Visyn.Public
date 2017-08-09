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

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Visyn.Public.Format
{
    public enum FormatConsumer
    {
        StringFormat,
        Excel,

        // ^^^ Insert new above here ^^^
        App1,
        App2,
        App3,
        App4,
        App5
    }




    /// <summary>
    /// Class FormatInfo : ReadOnlyDictionary&lt;FormatConsumer, string&gt;
    /// </summary>
    /// <seealso cref="System.Collections.ObjectModel.ReadOnlyDictionary{Visyn.Public.Format.FormatConsumer, System.String}" />
    public class FormatInfo : ReadOnlyDictionary<FormatConsumer, string>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> class that is a wrapper around the specified dictionary.</summary>
        /// <param name="dictionary">The dictionary to wrap.</param>
        public FormatInfo(IDictionary<FormatConsumer, string> dictionary) : base(dictionary)
        {
        }
    }
}
