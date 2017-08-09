#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

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
        protected ExceptionSource() { }

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