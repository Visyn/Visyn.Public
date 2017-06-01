using System;

namespace Visyn.Public.Text
{
    [Flags]
    public enum StringSplitIndexOptions
    {
        None = 0,
        RemoveEmptyEntries = 1,
        RemoveIndexChar = 2,
    }
}