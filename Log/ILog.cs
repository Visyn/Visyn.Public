using System;
using System.Collections;
using System.Collections.Generic;

namespace Visyn.Public.Log
{
    public interface ILogItems<TEntry>
    {
        IDictionary<DateTime, TEntry> Entries();

        void LogItem(TEntry item);
    }

    public interface ILog<TEventLevel>
    {
        TEventLevel LogLevel { get; set; }
        

        void Log(object source, string message, TEventLevel level);
        void Log(object source, ICollection logItems, TEventLevel level, string prefix);
    }
}