using System;
using System.Runtime.CompilerServices;
using Visyn.Public.Io;
using Visyn.Public.Log;

namespace Visyn.Public.Exceptions
{
    public class SupressException : IExceptionHandler
    {
        private readonly Func<object, Exception, bool> _handler;
        public SupressException(Func<object, Exception, bool> handler=null)
        {
            _handler = handler;
        }

        #region Implementation of IExceptionHandler

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HandleException(object sender, Exception exception) => _handler?.Invoke(sender, exception) ?? true;

        #endregion

        public static SupressException WriteException(IOutputDevice device)
        {
            if (device == null) return new SupressException(null);

            return new SupressException((s, exc) =>
            {
                device.WriteLine($"{s}:{exc.Message}");
                return true;
            });
        }

        public static SupressException LogException(ILog<SeverityLevel> log)
        {
            if (log == null) return new SupressException(null);

            return new SupressException((s, exc) =>
            {
                log.Log(s,exc.Message, SeverityLevel.Error);
                return true;
            });
        }
    }
}
