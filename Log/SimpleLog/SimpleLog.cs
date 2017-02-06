using System;
using System.Collections;

namespace Visyn.Public.Log.SimpleLog
{
    public class SimpleLog<TEventLevel> : SimpleLogBase<TEventLevel,SimpleLogEntry<TEventLevel>> where TEventLevel : struct
    {
        private TEventLevel ErrorLevel { get; }
        public SimpleLog(TEventLevel errorLevel)
        {
            ErrorLevel = errorLevel;
        }

        #region Overrides of SimpleLogBase<SeverityLevel,SimpleLogEntry>

        public override void Log(object source, string message, TEventLevel level)
        {
            LogItem(new SimpleLogEntry<TEventLevel>(source.ToString(), message, level));
        }
    

        public override void Log(object source, ICollection logItems, TEventLevel level, string prefix = null)
        {
            foreach (var item in logItems)
            {
                LogItem(new SimpleLogEntry<TEventLevel>(source.ToString(), item.ToString(), level));
            }
        }
  
        /// <summary>
        /// Handles the exception
        /// If false is returned, sender should throw the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <returns><c>true</c> if exception was handled, <c>false</c> otherwise.</returns>
        public override bool HandleException(object sender, Exception exception)
        {
            if (typeof(TEventLevel) == typeof(SeverityLevel))
                LogItem(new SimpleLogEntry<TEventLevel>(sender?.ToString(), exception?.Message, ErrorLevel));
            return true;
        }

        #endregion
    }
}