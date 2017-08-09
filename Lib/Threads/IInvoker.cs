using System;

namespace Visyn.Public.Threads
{
    public interface IInvoker
    {
        void Invoke(Delegate method, object[] args);
        void Invoke<T>(Action<T> method, T param);
        void Invoke<T>(EventHandler<T> handler, T param);
    }
}