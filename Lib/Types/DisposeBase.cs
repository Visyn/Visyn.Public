#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
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

namespace Visyn.Types
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