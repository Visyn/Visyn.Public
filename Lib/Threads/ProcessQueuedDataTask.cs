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
    public abstract class ProcessQueuedDataTask<T> : IExceptionHandler, IDisposable, ICollection<T>, IProducerConsumerCollection<T>, ICollection, IEnumerable, IEnumerable<T>
    {
        public abstract string Name { get; }
        public int Count => Data.Count;
        public bool TaskRunning => Task != null;
        public DateTime TaskStartTime { get; protected set; }
        public TimeSpan RateLimitTimeSpan { get; set; } = TimeSpan.Zero;

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

        protected ProcessQueuedDataTask(Action<ConcurrentQueue<T>> taskStartedAction,
            Action<ConcurrentQueue<T>> taskEndingAction, IExceptionHandler handler)
            : this(handler)
        {
            TaskStartedAction = taskStartedAction;
            TaskEndingAction = taskEndingAction;
        }

        public void Add(T data)
        {
            Data.Enqueue(data);
            Wait.Set();
            if (!TaskRunning) StartTask();
        }

        protected T Dequeue()
        {
            T result;
            return Data.TryDequeue(out result) ? result : default(T);
        }

        protected bool TryDequeue(out T result) => Data.TryDequeue(out result);


        public int TryDequeueAll(out List<T> items) => (items = DequeueMany().ToList()).Count;

        public IEnumerable<T> DequeueMany(int count = int.MaxValue)
        {
            while (count-- > 0 && !Data.IsEmpty)
            {
                T item;
                if (Data.TryDequeue(out item))
                {
                    yield return item;
                }
            }
        }

        public int TryDequeueMany(int count, out List<T> items) => (items = DequeueMany(count).ToList()).Count;

        public T Peek()
        {
            T result;
            return Data.TryPeek(out result) ? result : default(T);
        }

        public bool TryPeek(out T next) => Data.TryPeek(out next);

        public void Clear()
        {
            while (!Data.IsEmpty)
            {
                T item;
                Data.TryDequeue(out item);
            }
        }

        #region Implementation of IExecute

        public virtual async void Execute()
        {
            try
            {
                TaskStartedAction?.Invoke(Data);
            }
            catch (Exception exception)
            {
                if (!HandleException(this, exception)) throw;
            }
            if (Handler == null) throw new NullReferenceException($"Exception Handler must be non-null!");

            while (!CancelTokenSource.IsCancellationRequested)
            {
                try
                {
                    if (Data.IsEmpty)
                    {
                        Wait.WaitOne();
                        Wait.Reset();
                    }

                    if (CancelTokenSource.IsCancellationRequested) break;

                    if (!Data.IsEmpty)
                    {
                        ProcessData();
                    }
#if true
                    if (RateLimitTimeSpan.TotalMilliseconds > 0)
                    {
                        await Task.Delay((int)RateLimitTimeSpan.TotalMilliseconds, CancelTokenSource.Token);
                    }
#else
                    var start = DateTime.Now;
                    if (RateLimitTimeSpan.TotalMilliseconds > 0)
                    {
                        await Task.Delay((int) RateLimitTimeSpan.TotalMilliseconds, CancelTokenSource.Token);
                        var duration = DateTime.Now - start;
                        if(duration.TotalMilliseconds > 1000)
                            start = DateTime.MaxValue;;
                    }
                    else
                    {
                        RateLimitTimeSpan = TimeSpan.FromMilliseconds(2);
                    }
#endif
                }
                catch (Exception exception)
                {
                    if (!HandleException(this, exception)) throw;
                }
            }
            try
            {
                TaskEndingAction?.Invoke(Data);
            }
            catch (Exception exception)
            {
                if (!HandleException(this, exception)) throw;
            }
            CancelTokenSource.Dispose();
            CancelTokenSource = null;
            Task = null;
        }

#endregion IExecute

        public CancellationTokenSource CancelTokenSource { get; protected set; }

        public void StartTask()
        {
            if (Task != null) return;
            if(CancelTokenSource == null) CancelTokenSource = new CancellationTokenSource();

            Task = new Task(Execute, CancelTokenSource.Token, TaskCreationOptions.LongRunning);

            TaskStartTime = DateTime.Now;
            Task.Start();
        }

        public CancellationToken CancelToken { get; private set; }

        protected abstract void ProcessData();

        public virtual void StopTask()
        {
            if (!TaskRunning) return;
            CancelTokenSource?.Cancel();
            Wait?.Set();
        }

#region Implementation of IExceptionHandler

        public virtual bool HandleException(object sender, Exception exception)
        {
            if (CancelTokenSource.IsCancellationRequested) return true;
            return Handler?.HandleException(sender, exception) == true;
        }

#endregion IExceptionHandler

#region IDisposable

        public virtual void Dispose()
        {
            CancelTokenSource?.Dispose();
            Wait?.Dispose();
        }

#endregion

#region Implementation of IEnumerable

        public IEnumerator GetEnumerator() => DequeueMany().GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => DequeueMany().GetEnumerator();
#endregion Implementation of IEnumerable


#region Implementation of ICollection<T>

        /// <summary>
        /// Copies queued data to array.
        /// NOTE: Queue is not empties
        /// </summary>
        /// <param name="array">The array to populate.</param>
        /// <param name="index">The start index of the queued data to copy.</param>
        public void CopyTo(T[] array, int index) => Data.CopyTo(array, index);

        /// <summary>
        /// Copies queued data to array.
        /// NOTE: Queue is not empties
        /// </summary>
        /// <param name="array">The array to populate.</param>
        /// <param name="index">The start index of the queued data to copy.</param>
        public void CopyTo(Array array, int index) => Data.CopyTo((T[])array, index);

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        /// <value><c>true</c> if this instance is synchronized; otherwise, <c>false</c>.</value>
        public bool IsSynchronized => ((ICollection) Data).IsSynchronized;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The synchronize root.</value>
        public object SyncRoot => ((ICollection) Data).SyncRoot;


        public bool Contains(T item) => Data.Contains(item);

        public virtual bool Remove(T item)
        {
            throw new NotImplementedException($"Remove is not implemented in this implementation");
        }

        public bool IsReadOnly => false;

#endregion Implementation of ICollection<T>

#region Implementation of IProducerConsumerCollection<T>

        public T[] ToArray() => DequeueMany().ToArray();
        public List<T> ToList() => DequeueMany().ToList();

        public bool TryAdd(T item)
        {
            Add(item);
            return true;
        }

        public bool TryTake(out T item) => TryDequeue(out item);

#endregion
    }
}