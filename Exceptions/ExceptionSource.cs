using System;
using System.Windows.Threading;
using Visyn.Public.Types;

namespace Visyn.Public.Exceptions
{
    public delegate bool ExceptionHandler(object source, Exception exception);
    public class ExceptionSource : DisposeBase, IExceptionSource, IExceptionHandler
    {
        private ExceptionHandler _exceptionHandler;
        private Dispatcher _dispatcher;

        /// <summary>
        /// Expose get access to dispatcher
        /// </summary>
        public Dispatcher Dispatcher => _dispatcher;

        /// <summary>
        /// Expose get access for ExceptionHandler delegate
        /// </summary>
        public ExceptionHandler ExceptionHandler => _exceptionHandler;

        [Obsolete("Need to make protected...")]
        public ExceptionSource() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionSource"/> class.
        /// </summary>
        /// <param name="exceptionHandler">The exception handler.</param>
        /// <param name="dispatcher">The dispatcher.</param>
        public ExceptionSource(ExceptionHandler exceptionHandler, Dispatcher dispatcher)
        {
            RegisterExceptionHandler(exceptionHandler,dispatcher);
        }

        #region Implementation of IExceptionSource

        /// <summary>
        /// Registers exception handler for exception routing
        /// If dispacher is specified, exception shall be thrown of thread specified by dispatcher
        /// </summary>
        /// <param name="exceptionHandler">Exception handler to delegate exception handling to</param>
        /// <param name="dispatcher">Dispatcher of thread to throw exception</param>
        public void RegisterExceptionHandler(ExceptionHandler exceptionHandler, Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            _exceptionHandler = exceptionHandler;
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
        public virtual bool HandleException(object sender, Exception exception)
        {
            if (_exceptionHandler == null) return false;

            if (_dispatcher == null || _dispatcher.CheckAccess()) return _exceptionHandler(sender, exception);
            _dispatcher.BeginInvoke(_exceptionHandler, sender, exception);
            return true;
        }

        #endregion

        #region Overrides of DisposeBase

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        ///         protected abstract void Dispose(bool disposing)
        ///        {
        ///            if (Disposing)
        ///            {
        ///                // dispose of managed resources
        ///            }
        ///            // dispose of unmanaged resources
        ///        }
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            _dispatcher = null;
            _exceptionHandler = null;
        }

        #endregion
    }
}