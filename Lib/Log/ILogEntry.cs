using System;

namespace Visyn.Public.Log
{
    public interface ILogEntry<T>
    {
        string Application { get; }
        string Computer { get; }
        string User { get; }
        T EventLevel { get; }
        string Message { get; }
        string Source { get; }
        DateTime TimestampLocal { get; }
        DateTime TimestampUtc { get; }
    }
}