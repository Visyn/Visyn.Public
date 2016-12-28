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
