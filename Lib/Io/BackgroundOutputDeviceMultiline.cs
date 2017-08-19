using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Visyn.Io
{
    public class BackgroundOutputDeviceMultiline : BackgroundOutputDevice, IOutputDeviceMultiline
    {
        public override string Name { get; } = "Multiline Output Device";
        private readonly IOutputDeviceMultiline _multLineOutput;

        protected BackgroundOutputDeviceMultiline(IOutputDevice outputDevice, Func<string, string> process)
            : base(outputDevice, process)
        {
            _multLineOutput = outputDevice as IOutputDeviceMultiline;
        }

        public BackgroundOutputDeviceMultiline(Dispatcher dispatcher, IOutputDevice outputDevice, Func<string, string> process)
            : this(outputDevice, process)
        {
            Dispatcher = dispatcher;
            //var t = Task.Run(_multLineOutput != null ? backgroundTaskMultiLine : new Action(BackgroundTask));
            //var id = t.Id;
        }
        

        #region Overrides of BackgroundOutputDevice

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
                var text = DequeueText();
                ProcessString(text);
                return;
            }
            // count > 1
            var items = new List<string>(count);
            var index = 0;

            while (count-- > 0)
            {
                var text = DequeueText();
                if (text != null) items.Add(text);

                if (index++ > 100) break;
            }
            if (items.Count <= 0) return;
            if (items.Count == 1) ProcessString(items[0]);
            else ProcessStrings(items);
        }

        #endregion

        protected int ProcessStrings(List<string> items)
        {
            if (items == null) return 0;
            var count = items.Count;
            switch (count)
            {
                case 0: return 0;
                case 1: return ProcessString(items[0]);
                default:
                    var action = _multLineOutput != null ?
                        new Action(() => _multLineOutput.Write(items)) :
                        new Action(() => OutputDevice.Write(items));

                    if (Dispatcher != null)
                    {
                        Dispatcher.BeginInvoke(action);
                    }
                    else
                    {
                        action();
                    }
                    return count;
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