using System;
using System.Collections;
using System.Collections.Generic;

namespace Visyn.Public.Log.SimpleLog
{
    public abstract class SimpleLogBase<TEventLevel, TEntry> : IExceptionLog<TEventLevel>, ILogItems<TEntry> where TEntry : class, ILogEntry<TEventLevel> where TEventLevel : IComparable
    {
        protected IDictionary<DateTime, TEntry> _entries { get; }
        public KeyValuePair<TEventLevel,Action<object>> ItemLoggedAction { get; set; }

        protected SimpleLogBase(IDictionary<DateTime, TEntry> dictionary = null)
        {
            _entries = dictionary ?? new Dictionary<DateTime, TEntry>();
           ;
        }

        #region Implementation of ILog<TEventLevel,TEntry>

        public TEventLevel LogLevel { get; set; }
        public IDictionary<DateTime, TEntry> Entries()
        {
            var _dictionary = new Dictionary<DateTime, TEntry>();
            foreach(var entry in _entries)
            {
                if (entry.Value != null)  _dictionary.Add(entry.Key, entry.Value);
            }
            return _dictionary;
        }

        public void LogItem(TEntry item)
        {
            if (item != null)
            {
                if (!_entries.ContainsKey(item.TimestampUtc))
                {
                    _entries.Add(item.TimestampUtc, item);
                }
                ExecuteOutputAction(item);
            }
        }

        protected void ExecuteOutputAction(TEntry item)
        {
            if (ItemLoggedAction.Value != null && (item.EventLevel).CompareTo(ItemLoggedAction.Key) <= 0)
            {
                var action = ItemLoggedAction.Value;
                action.BeginInvoke(item, null, null);
            }
        }
        protected void ExecuteOutputAction(object item)
        {
            if (ItemLoggedAction.Value != null )
            {
                var action = ItemLoggedAction.Value;
                action.BeginInvoke(item, null, null);
            }
        }

        public abstract void Log(object source, string message, TEventLevel level);

        public abstract void Log(object source, ICollection logItems, TEventLevel level, string prefix = null);

        public virtual void LogItem<T>(T item)
        {
            var entry = item as TEntry;
            if (entry != null)
            {
                if (!_entries.ContainsKey(entry.TimestampUtc))
                {
                    _entries.Add(entry.TimestampUtc, entry);
                }
                ExecuteOutputAction(item);
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