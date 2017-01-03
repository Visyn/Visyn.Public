using System;
using System.Collections;

namespace Visyn.Public.Log.SimpleLog
{
    public class SimpleLog : SimpleLogBase<SeverityLevel,SimpleLogEntry>
    {
        #region Overrides of SimpleLogBase<SeverityLevel,SimpleLogEntry>

        public override void Log(object source, string message, SeverityLevel level)
        {
            LogItem(new SimpleLogEntry(source.ToString(), message, level));
        }
    

        public override void Log(object source, ICollection logItems, SeverityLevel level, string prefix = null)
        {
            foreach (var item in logItems)
            {
                LogItem(new SimpleLogEntry(source.ToString(), item.ToString(), level));
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
            LogItem(new SimpleLogEntry(sender?.ToString(), exception?.Message, SeverityLevel.Error));
            return true;
        }

        #endregion
    }
}