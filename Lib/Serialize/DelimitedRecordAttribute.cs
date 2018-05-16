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

namespace Visyn.Serialize
{
    /// <summary>Indicates that this class represents a delimited record. </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class DelimitedRecordAttribute : TypedRecordAttribute, ITypedRecordAttribute
    {
        /// <summary>The string used as a field separator.</summary>
        public string Delimiter { get; set; }

        /// <summary>Indicates that this class represents a delimited record. </summary>
        /// <param name="delimiter">The separator string used to split the fields of the record.</param>
        public DelimitedRecordAttribute(string delimiter)
        {
            if (string.IsNullOrEmpty(delimiter))
                throw new ArgumentException("Given delimiter cannot be <> \"\"", nameof(delimiter));
            
            Delimiter = delimiter;
        }
    }
}
