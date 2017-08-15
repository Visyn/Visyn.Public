using System.Collections.Generic;

namespace Visyn.Io
{
    public interface IDelimitedHeader
    {
        IEnumerable<string> ToHeaderArray();
    }
    public interface IDelimitedString
    {
        string ToDelimitedString(string delimiter);
        string DelimitedHeader(string delimiter);
    }

    public interface IDelimitedData : IDelimitedHeader
    {
        IEnumerable<string> ToStringArray();
 //       IEnumerable<string> ToHeaderArray();
    }

    public interface ISaveDelimitedFile
    {
        bool SaveDelimitedFile(string filename = null, string delimiter = ",");
    }
}