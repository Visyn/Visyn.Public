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
using Visyn.JetBrains;

namespace Visyn.Threads
{
    public class WpfInvoker : IInvoker
    {
        [NotNull]
        public Dispatcher Dispatcher { get; }

        public WpfInvoker(Dispatcher dispatcher)
        {
            if (dispatcher == null) throw new NullReferenceException($"{nameof(dispatcher)} can not be null!");
            Dispatcher = dispatcher;
        }

        #region Implementation of IInvoker

        public void Invoke(Delegate method, object[] args)
        {
#if DEBUG
            if (method == null) throw new NullReferenceException($"{nameof(method)} can not be null!");
#else
            if(method == null) return;
#endif
            // ReSharper disable once PossibleNullReferenceException
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(method, args);
            }
            else
            {
                Invoke(method, args);
            }
        }

        public void Invoke<T>(Action<T> method, T param)
        {
#if DEBUG
            if (method == null) throw new NullReferenceException($"{nameof(method)} can not be null!");
#else
            if(method == null) return;
#endif
            // ReSharper disable once PossibleNullReferenceException
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(method, param);
            }
            else
            {
                method(param);
            }
        }


        public void Invoke<T>(EventHandler<T> handler, object sender, T param)
        {
            if (handler == null) return;
            // ReSharper disable once PossibleNullReferenceException
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(handler, new object[] { sender, param });
            }
            else
            {
                handler.Invoke(sender, param);
            }
        }

        public void Invoke(Action action)
        {
#if DEBUG
            if (action == null) throw new NullReferenceException($"{nameof(action)} can not be null!");
#else
            if(action == null) return;
#endif
            // ReSharper disable once PossibleNullReferenceException
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public void BeginInvoke<T>(Action<T> action, T param)
        {
#if DEBUG
            if (action == null) throw new NullReferenceException($"{nameof(action)} can not be null!");
#else
            if(action == null) return;
#endif
            Dispatcher.BeginInvoke(action,new object[] {param});
        }

        public void BeginInvoke(Action action)
        {
#if DEBUG
            if (action == null) throw new NullReferenceException($"{nameof(action)} can not be null!");
#else
            if(action == null) return;
#endif
            Dispatcher.BeginInvoke(action);
        }
        #endregion
    }
}