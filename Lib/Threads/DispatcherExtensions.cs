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
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Visyn.Threads
{
    public static class DispatcherExtensions
    {
        [Conditional("DEBUG")]
        public static void AssertAccess(this Dispatcher dispatcher, string message=null, [CallerMemberName] string caller=null)
        {
            Debug.Assert(dispatcher.CheckAccess(), 
                (!string.IsNullOrEmpty(message) ? $"{caller} {message}" : $"{caller}: Not called on associated thread!"));
        }

        public static void CheckAccessAndThrow(this Dispatcher dispatcher, string message = null, [CallerMemberName] string caller = null)
        {
            if(!dispatcher.CheckAccess())
                throw new UnauthorizedAccessException((!string.IsNullOrEmpty(message) ?
                    $"{caller}: {message}" : 
                    $"{caller}: Not called on associated thread!"));
        }

        /// <summary>
        /// Invokes action on the specified dispatcher after delay.
        /// </summary>
        /// <param name="dispatcher">The dispatcher to invoke action with.</param>
        /// <param name="delay">The delay.</param>
        /// <param name="action">The action.</param>
        public static void DelayedInvoke(this Dispatcher dispatcher, TimeSpan delay, Action action)
        {
            Task.Delay((int) (delay.TotalMilliseconds)).ContinueWith( (a) =>
            {
                dispatcher.Invoke(action);
            });
        }
    }
}
