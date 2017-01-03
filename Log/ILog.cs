using System.Collections;

namespace Visyn.Public.Log
{
    public interface ILog<TEventLevel> 
    {
        TEventLevel LogLevel { get; set; }

        void Log(object source, string message, TEventLevel level);
        void Log(object source, ICollection logItems, TEventLevel level, string prefix);
        void LogItem<TEntry>(TEntry item);
    }
}