using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Windows.Threading;
using Visyn.JetBrains;

namespace Visyn.Io
{
    public class BackgroundOutputDevice : IOutputDevice
    {
        public Dispatcher Dispatcher { get; protected set; }
        public int Count => Queue.Count;
        public int DelayIntervalMs { get; protected set; } = 100;
        public int? TaskId { get; protected set; }

        [NotNull]
        protected readonly IOutputDevice OutputDevice;
     
        private readonly Func<string, string> _process;

        [NotNull]
        protected readonly ConcurrentQueue<Func<object>> Queue = new ConcurrentQueue<Func<object>>();

        /// <summary>
        /// Create background output device from any other IOutputDevice
        /// 
        /// </summary>
        /// <param name="outputDevice">Device to perform output (on background ThreadPool task)</param>
        /// <param name="process">Function that will process input and produce ultimate output.</param>
        protected BackgroundOutputDevice(IOutputDevice outputDevice, Func<string,string> process)
        {
            if (outputDevice == null) throw new NullReferenceException( $"{nameof(BackgroundOutputDevice)} {nameof(outputDevice)} must be non-null!");
            OutputDevice = outputDevice;
            _process = process;
            //var scheduler = TaskScheduler.Default;
        }

        public BackgroundOutputDevice( IOutputDevice outputDevice, Func<string, string> process, Dispatcher dispatcher) 
            : this(outputDevice,process)
        {
            Dispatcher = dispatcher;
            Task.Run(new Action(BackgroundTask));
        }

        protected void BackgroundTask()
        {
            TaskId = Task.CurrentId;
    
            while(true)
            {
                var count = Queue.Count;
                if(count > 0)
                {
                    var index = 0;
                    
                    var output = DequeueText();
                   
                    while(--count > 0)
                    {
                        output += DequeueText();
                        if (index++ > 10) break;;
                    }
                    ProcessString(output);
                }
                else
                {
                    Task.Delay(DelayIntervalMs);
                }
            }
        }

        protected string DequeueText()
        {
            var obj = DequeueItem();
            return obj as string;
        }

        protected object DequeueItem()
        {
            Func<object> function;
            return Queue.TryDequeue(out function) ? function.Invoke() : null;
        }

        protected void ProcessString(string output)
        {
            var action = new Action(() => OutputDevice.Write(output));
            if (Dispatcher != null) Dispatcher.BeginInvoke(action);
            else action();
        }

        #region Implementation of IOutputDevice

        public void Write(string text)
        {
            Queue.Enqueue(() =>_process != null ? _process(text) : text);
        }

        public void WriteLine(string line)
        {
            if (_process != null)
            {
                Queue.Enqueue(() => _process(line) + Environment.NewLine);
            }
            else
            {
                Queue.Enqueue(() => (line + Environment.NewLine));
            }
        }

        public void Write(Func<string> func)
        {
            if (func == null) return;
            Queue.Enqueue(_process != null ? (() => _process(func.Invoke())) : func);//func);
        }

        #endregion
    }
}