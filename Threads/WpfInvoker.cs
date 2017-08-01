using System;
using System.Windows.Threading;

namespace Visyn.Public.Threads
{
    public class WpfInvoker : IInvoker
    {
        public Dispatcher Dispatcher { get; }

        public WpfInvoker(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        public void Invoke(Delegate method, object[] args)
        {
            if (Dispatcher.CheckAccess())
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
            if (Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(method, param);
            }
            else
            {
                method(param);
            }
        }

        #region Implementation of IInvoker

        public void Invoke<T>(EventHandler<T> handler, T param)
        {
            if (handler == null) return;
            if (Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(handler, new object[] { param });
            }
            else
            {
                handler.Invoke(null, param);
            }
        }

        #endregion
    }
}