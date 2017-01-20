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