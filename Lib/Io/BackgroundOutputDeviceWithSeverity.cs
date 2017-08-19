using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;
using Visyn.Log;

namespace Visyn.Io
{
    public class BackgroundOutputDeviceWithSeverity : BackgroundOutputDeviceMultiline,IOutputDevice<SeverityLevel>
    {
        private readonly IOutputDevice<SeverityLevel> _backgroundDeviceWithSeverity;
        public BackgroundOutputDeviceWithSeverity(IOutputDevice<SeverityLevel> outputDevice, Func<string, string> process) 
            : base(outputDevice, process)
        {
            _backgroundDeviceWithSeverity = outputDevice;
        }

        public BackgroundOutputDeviceWithSeverity(Dispatcher dispatcher, IOutputDevice<SeverityLevel> outputDevice, Func<string, string> process) 
            : this(outputDevice, process)
        {
            Dispatcher = dispatcher;
        }

        #region Overrides of BackgroundOutputDeviceMultiline

        protected override void ProcessData()
        {
            var count = Count;

            if (count <= 0)
            {
                Task.Delay(DelayIntervalMs);
                return;
            }
            if (count == 1)
            {
                ProcessItem();
                return;
            }
            // count > 1
            var items = new List<string>(count);
            var index = 0;

            while (count-- > 0)
            {
                if(items == null) items = new List<string>(count);
                Func<object> next;
                if(TryPeek(out next))
                {
                    if (next is Func<string>)
                    {
                        var item = DequeueItem() as string;
                        if(item != null) items.Add(item);
                    }
                    else
                    {
                        if (items.Count > 0)
                        {
                            ProcessStrings(items);
                            items = null;
                        }
                        ProcessItem();
                    }
                }

                if (index++ > 100) break;
            }
            if (items?.Count > 0) ProcessStrings(items);
        }



        private void ProcessItem()
        {
            var item = DequeueItem();
            var text = item as string;
            if (text != null) ProcessString(text);
            else if (item is MessageWithSeverityLevel)
            {
                ProcessMessageWithSeverity((MessageWithSeverityLevel) item);
            }
        }

        private void ProcessMessageWithSeverity(MessageWithSeverityLevel item)
        {
            var action = new Action(() => _backgroundDeviceWithSeverity.Write(item.Message,item.SeverityLevel));
            if (Dispatcher != null) Dispatcher.BeginInvoke(action);
            else action();
        }

        #endregion

        #region Implementation of IOutputDevice<SeverityLevel>

        public void Write(string text, SeverityLevel severity)
        {
            Add(() => new MessageWithSeverityLevel(text,severity));
        }

        public void WriteLine(string line, SeverityLevel severity)
        {
            Add(() => new MessageWithSeverityLevel(line, severity));
        }

        public void Write(Func<string> func, SeverityLevel severity)
        {
            Add(() => new MessageWithSeverityLevel(func.Invoke(), severity));
        }

        #endregion
    }
}