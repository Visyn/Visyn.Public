using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Visyn.Exceptions;

namespace Visyn.Threads
{
    public abstract class ProcessQueuedDataTask<T> : IExceptionHandler, IDisposable
    {
        public abstract string Name { get; }
        public int Count => Data.Count;
        public bool TaskRunning => Task != null;
        public DateTime TaskStartTime { get; protected set; }

        public Action<ConcurrentQueue<T>> TaskStartedAction { get; set; }
        public Action<ConcurrentQueue<T>> TaskEndingAction { get; set; }

        private ConcurrentQueue<T> Data { get; }
        protected Task Task { get; private set; }
        protected IExceptionHandler Handler { get; set; }
        protected AutoResetEvent Wait { get; }

        protected ProcessQueuedDataTask()
        { 
            Data = new ConcurrentQueue<T>();
            Wait = new AutoResetEvent(true);
        }

        protected ProcessQueuedDataTask(IExceptionHandler handler) : this()
        {
            Debug.Assert(handler != null);
            if (handler == null) throw new NullReferenceException($"Exception handler required for task!");
            Handler = handler;
        }

        protected ProcessQueuedDataTask(Action<ConcurrentQueue<T>> taskStartedAction, Action<ConcurrentQueue<T>> taskEndingAction, IExceptionHandler handler)
            : this(handler)
        {
            TaskStartedAction = taskStartedAction;
            TaskEndingAction = taskEndingAction;
        }

        public void Add(T data)
        {
            Data.Enqueue(data);
            Wait.Set();
            if(!TaskRunning) StartTask();
        }

        protected T Dequeue()
        {
            T result;
            return Data.TryDequeue(out result) ? result : default(T);
        }
        protected T TryDequeue(out T result) => Data.TryDequeue(out result) ? result : default(T);

        public int TryDequeueAll(out List<T> items)
        {
            items = new List<T>(Count);
           
            while (!Data.IsEmpty)
            {
                T item;
                if (Data.TryDequeue(out item))
                {
                    items.Add(item);
                }
            }
            return items.Count;
        }

        protected T Peek()
        {
            T result;
            return Data.TryPeek(out result) ? result : default(T);
        }

        protected bool TryPeek(out T next) => Data.TryPeek(out next);

        public void Clear()
        {
            while (!Data.IsEmpty)
            {
                T item;
                Data.TryDequeue(out item);
            }
        }

        #region Implementation of IExecute

        public virtual void Execute()
        {
            TaskStartedAction?.Invoke(Data);
            
            if(Handler == null) throw new NullReferenceException($"Exception Handler must be non-null!");
            while (!_cancel.IsCancellationRequested)
            {
                try
                {
                    if (Data.IsEmpty)
                    {
                        Wait.WaitOne();
                        Wait.Reset();
                    }
                    if (_cancel.IsCancellationRequested)
                        break;
                    var empty = Data.IsEmpty;
                    var count = Data.Count;
                    if(!Data.IsEmpty)
                    {
                        ProcessData();
                    }
                }
                catch (Exception exception)
                {
                    if (!HandleException(this, exception)) throw;
                }
            }
            TaskEndingAction?.Invoke(Data);
            _cancel.Dispose();
            _cancel = null;
            Task = null;
        }

        private CancellationTokenSource _cancel;
        protected abstract void ProcessData();
        public void StartTask()
        {
            if (Task != null) return;
            _cancel = new CancellationTokenSource();
            Task = new Task(Execute, _cancel.Token,TaskCreationOptions.LongRunning);
       
            TaskStartTime = DateTime.Now;
            Task.Start();
        }

        public virtual void StopTask()
        {
            if (TaskRunning)
            {
                _cancel?.Cancel();
                Wait?.Set();
            }
        }

        #endregion

        #region Implementation of IExceptionHandler

        public virtual bool HandleException(object sender, Exception exception)
        {
            if (_cancel.IsCancellationRequested) return true;
            return Handler?.HandleException(sender,exception) == true;
        }

        #endregion

        #region IDisposable

        public virtual void Dispose()
        {
            _cancel?.Dispose();
            Wait?.Dispose();
        }

        #endregion
    }
}