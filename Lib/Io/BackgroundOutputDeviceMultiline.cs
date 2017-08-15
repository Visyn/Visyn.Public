using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Visyn.Io
{
    public class BackgroundOutputDeviceMultiline : BackgroundOutputDevice, IOutputDeviceMultiline
    {
        private readonly IOutputDeviceMultiline _multLineOutput;


        protected BackgroundOutputDeviceMultiline(IOutputDeviceMultiline outputDevice, Func<string, string> process) 
            : base(outputDevice, process)
        {
            _multLineOutput = outputDevice;
        }

        public BackgroundOutputDeviceMultiline(Dispatcher dispatcher, IOutputDeviceMultiline outputDevice, Func<string, string> process) 
            : this( outputDevice, process)
        {
            Dispatcher = dispatcher;
            Task.Run(_multLineOutput != null ? backgroundTaskMultiLine : new Action(BackgroundTask));
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
                if(count == 1)
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


        protected void ProcessStrings(List<string> items)
        {
            if (items == null) return;
            var itemsToPass = items.ToArray();
            var action = _multLineOutput != null ?
                        new Action(() => _multLineOutput.Write(itemsToPass)) :
                        new Action(() => OutputDevice.Write(itemsToPass));
            //     (() => { foreach(var item in items) OutputDevice.WriteLine(item); }) ;

            if (Dispatcher != null)
            {
                Dispatcher.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        #region Implementation of IOutputDeviceMultiline


        public void Write(IEnumerable<string> lines)
        {
            if (lines == null) return;
            foreach (var line in lines)
                WriteLine(line);
        }

        #endregion
    }
}