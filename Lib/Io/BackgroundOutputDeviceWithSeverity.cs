using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;
using Visyn.Log;

namespace Visyn.Io
{
    public class BackgroundOutputDeviceWithSeverity : BackgroundOutputDeviceMultiline,IOutputDevice<SeverityLevel>
    {
        public BackgroundOutputDeviceWithSeverity(IOutputDeviceMultiline outputDevice, Func<string, string> process) 
            : base(outputDevice, process)
        {
        }

        public BackgroundOutputDeviceWithSeverity(Dispatcher dispatcher, IOutputDeviceMultiline outputDevice, Func<string, string> process) 
            : this(outputDevice, process)
        {
            Dispatcher = dispatcher;
        }

        private void backgroundTaskMultiLine()
        {
            TaskId = Task.CurrentId;
            var items = new List<string>();
            while (true)
            {
                var count = Queue.Count;

                if (count <= 0)
                {
                    Task.Delay(DelayIntervalMs);
                    continue;
                }
                if (count == 1)
                {
                    var text = DequeueText();
                    ProcessString(text);
                    continue;
                }
                // count > 1
                var index = 0;

                while (count-- > 0)
                {
                    var text = DequeueText();
                    if (text != null) items.Add(text);

                    if (index++ > 100) break;
                }
                if (items.Count <= 0) continue;
                if (items.Count == 1) ProcessString(items[0]);
                else ProcessStrings(items);

                items.Clear();
            }
        }

        #region Implementation of IOutputDevice<SeverityLevel>

        public void Write(string text, SeverityLevel severity)
        {
            Queue.Enqueue(() => new MessageWithSeverityLevel(text,severity));
        }

        public void WriteLine(string line, SeverityLevel severity)
        {
            Queue.Enqueue(() => new MessageWithSeverityLevel(line, severity));
        }

        public void Write(Func<string> func, SeverityLevel severity)
        {
            Queue.Enqueue(() => new MessageWithSeverityLevel(func.Invoke(), severity));
        }

        #endregion
    }
}