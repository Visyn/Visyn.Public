#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
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
using System.Collections.Generic;
using System.Linq;
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
        }
        

        #region Overrides of BackgroundOutputDevice

        protected override int ProcessData()
        {
            var count = Count;

            if (count <= 0)
            {
                Task.Delay(DelayIntervalMs);
                return 0;
            }
            if (count == 1)
            {
                var text = DequeueText();
                return ProcessString(text);
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
            if (items.Count <= 0) return 0;
            if (items.Count == 1) return ProcessString(items[0]);
            else return ProcessStrings(items);
        }

        #endregion

        protected int ProcessStrings(ICollection<string> items)
        {
            if (items == null) return 0;
            var count = items.Count;
            switch (count)
            {
                case 0: return 0;
                case 1: return ProcessString(items.First());
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
                        action.Invoke();
                    }
                    return count;
            }
        }

        #region Implementation of IOutputDeviceMultiline


        public void Write(IEnumerable<string> lines)
        {
            if (lines == null) return;
            Add(() => lines);
        }

        #endregion
    }
    
}