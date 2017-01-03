using System;

namespace Visyn.Public.Log.SimpleLog
{
    public class SimpleLogEntry : ILogEntry<SeverityLevel>
    {
        public static string ApplicationName { get; set; }
        public static string ComputerName { get; set; }

        public static string UserName { get; set; } 
        #region Implementation of ILogEntry<SeverityLevel>

        public string Application { get; }
        public string Computer { get; }
        public string User { get; }
        public SeverityLevel EventLevel { get; }
        public string Message { get; }
        public string Source { get; }
        public DateTime TimestampLocal { get; }
        public DateTime TimestampUtc { get; }

        #endregion

        public SimpleLogEntry(string source, string message, SeverityLevel level)
        {
            TimestampUtc = DateTime.UtcNow;
            TimestampLocal = DateTime.Now;
            Source = source;
            Message = message;
            EventLevel = level;

            // initialize from static variables
            Application = ApplicationName;
            Computer = ComputerName;
            User = UserName;
        }
    }
}