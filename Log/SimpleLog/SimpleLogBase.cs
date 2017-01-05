using System;
using System.Collections;
using System.Collections.Generic;

namespace Visyn.Public.Log.SimpleLog
{
    public abstract class SimpleLogBase<TEventLevel, TEntry> : IExceptionLog<TEventLevel> where TEntry : class, ILogEntry<TEventLevel>
    {
        public IDictionary<DateTime, TEntry> Entries { get; }

        protected SimpleLogBase(IDictionary<DateTime, TEntry> dictionary = null)
        {
            Entries = dictionary ?? new Dictionary<DateTime, TEntry>();
        }

        #region Implementation of ILog<TEventLevel,TEntry>

        public TEventLevel LogLevel { get; set; }
        public abstract void Log(object source, string message, TEventLevel level);

        public abstract void Log(object source, ICollection logItems, TEventLevel level, string prefix = null);

        public virtual void LogItem<T>(T item)
        {
            var entry = item as TEntry;
            if (entry != null)
            {
                if (!Entries.ContainsKey(entry.TimestampUtc))
                {
                    Entries.Add(entry.TimestampUtc, entry);
                }
            }
        }


        #endregion

        #region Implementation of IExceptionHandler

        /// <summary>
        /// Handles the exception
        /// If false is returned, sender should throw the exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <returns><c>true</c> if exception was handled, <c>false</c> otherwise.</returns>
        public abstract bool HandleException(object sender, Exception exception);

        #endregion
    }
}