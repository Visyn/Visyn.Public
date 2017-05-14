using System;
using System.Runtime.CompilerServices;
using Visyn.Public.Io;
using Visyn.Public.Log;

namespace Visyn.Public.Exceptions
{
    public class ThrowException : IExceptionHandler
    {
        private readonly Func<object, Exception, bool> _handler;
        public ThrowException(Func<object, Exception, bool> handler=null)
        {
            _handler = handler;
        }

        #region Implementation of IExceptionHandler

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HandleException(object sender, Exception exception)
        {
            if(_handler?.Invoke(sender, exception) == true) return true;
            throw exception;
        }

        #endregion

        public static ThrowException WriteException(IOutputDevice device)
        {
            if (device == null) return new ThrowException();

            return new ThrowException((s, exc) =>
            {
                device.WriteLine($"{s}:{exc.Message}");
                return false;
            });
        }

        public static ThrowException LogException(ILog<SeverityLevel> log)
        {
            if (log == null) return new ThrowException();

            return new ThrowException((s, exc) =>
            {
                log.Log(s,exc.Message, SeverityLevel.Error);
                return false;
            });
        }
    }
}
