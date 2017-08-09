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
using System.Collections.Generic;
using Visyn.Public.Geometry;
using Visyn.Public.HTML;

namespace Visyn.Public.Reporting.Html
{
    public interface IHtmlColorGenerator
    {
        HtmlColor BaseColor { get; set; }
        Func<int, HtmlColor, List<string>> ColorListFunc { get; }
        /// <summary>
        /// Generates a dictionary of HTML color strings based centered around color passed.
        /// A different, unique, output color will be generated for every key requested
        /// NOTE: If HtmlColor.Unknown is passed, this.BaseColor will be used
        /// </summary>
        Func<List<string>, HtmlColor, Dictionary<string, string>> ColorDictionaryFunc { get; }

        Dictionary<object, Func<IPoint, int, int, string>> RowFunction(Dictionary<object, Func<IPoint, int, int, string>> dict, HtmlColor baseColor);
    }
}