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
    
    public class FormatInfo : ReadOnlyDictionary<FormatConsumer, string>
    {
         


        //public static ReadOnlyDictionary<FormatConsumer, string> CieFormatStrings(Type type)
        //{
        //    var cieFormatter = new Dictionary<FormatConsumer, string> {
        //        { FormatConsumer.StringFormat,"{0:F4}" },
        //        { FormatConsumer.Excel, "0.0000%" }
        //    };
        //    return new ReadOnlyDictionary<FormatConsumer, string>(cieFormatter);
        //}
        /// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> class that is a wrapper around the specified dictionary.</summary>
        /// <param name="dictionary">The dictionary to wrap.</param>
        public FormatInfo(IDictionary<FormatConsumer, string> dictionary) : base(dictionary)
        {
        }
    }
}
