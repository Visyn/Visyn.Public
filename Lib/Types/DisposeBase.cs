using System;

namespace Visyn.Public.Types
{
    public abstract class DisposeBase : IDisposable
    {
#if TEST
        public static int StaticCount=0;

        private static bool? _disposeFirstCalledWith;
        public static bool? DisposeFirstCalledWith
        {
            get { return _disposeFirstCalledWith; }
            set
            {
                if (value == null || _disposeFirstCalledWith == null) { _disposeFirstCalledWith = value; }
            }
        }
#endif
        ~DisposeBase()
        {
            Dispose(false);
#if TEST
            _disposeFirstCalledWith = true;
#endif
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
#if TEST
            StaticCount++;
            _disposeFirstCalledWith = true;
#endif
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        ///         protected abstract void Dispose(bool disposing)
        ///        {
        ///            if (Disposed) return; 
        ///            if (disposing)
        ///            {
        ///                // dispose of managed resources
        ///            }
        ///            // dispose of unmanaged resources
        ///            Disposed = true;
        ///        }
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected abstract void Dispose(bool disposing);
    }
}