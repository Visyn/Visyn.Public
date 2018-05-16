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
using System.Threading.Tasks;
using System.Windows.Threading;
using Visyn.Log;

namespace Visyn.Io
{
    public class BackgroundOutputDeviceWithSeverity : BackgroundOutputDeviceMultiline,IOutputDevice<SeverityLevel>
    {
        private readonly OutputToCollectionSeverity _backgroundDeviceWithSeverity;
        public BackgroundOutputDeviceWithSeverity(OutputToCollectionSeverity outputDevice, Func<string, string> process) 
            : base(outputDevice, process)
        {
            _backgroundDeviceWithSeverity = outputDevice;
        }

        public BackgroundOutputDeviceWithSeverity(Dispatcher dispatcher, OutputToCollectionSeverity outputDevice, Func<string, string> process) 
            : this(outputDevice, process)
        {
            Dispatcher = dispatcher;
        }

        #region Overrides of BackgroundOutputDeviceMultiline

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
                return ProcessItem();
            }
            // count > 1
            var items = new List<string>(count);
            var index = 0;

            var itemsProcessed = 0;
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
                            itemsProcessed += ProcessStrings(items);
                            items = null;
                        }
                        itemsProcessed += ProcessItem();
                    }
                }

                if (index++ > 100) break;
            }
            if (items?.Count > 0) itemsProcessed += ProcessStrings(items);
            return itemsProcessed;
        }



        private int ProcessItem()
        {
            var item = DequeueItem();

            if (item is string)
            {
                return ProcessString((string)item);
            }
            if (item is MessageWithSeverityLevel)
            {
                return ProcessMessageWithSeverity((MessageWithSeverityLevel)item);
            }
            var enumerable = item as IEnumerable<string>;
            if(enumerable != null)
            {
                if (enumerable is ICollection<string>) return ProcessStrings((ICollection<string>)enumerable);
                else return ProcessStrings(new List<string>(enumerable));
            }
#if DEBUG
            throw new ArrayTypeMismatchException($"{nameof(BackgroundOutputDeviceWithSeverity)}.{nameof(ProcessItem)} type {item.GetType().Name} not suported!");
#else
             return 0;
#endif
        }

        private int ProcessMessageWithSeverity(MessageWithSeverityLevel item)
        {
            var action = new Action(() =>
            	_backgroundDeviceWithSeverity.Write(item));
            if (Dispatcher != null) Dispatcher.BeginInvoke(action);
            else action();
            return 1;
        }

        #endregion

        #region Implementation of IOutputDevice<SeverityLevel>

        public void Write(string text, SeverityLevel level)
        {
            Add(() => new MessageWithSeverityLevel(text,level));
        }

        public void WriteLine(string line, SeverityLevel severity)
        {
            Add(() => new MessageWithSeverityLevel(line, severity));
        }

        public void Write(Func<string> func, SeverityLevel level)
        {
            Add(() => new MessageWithSeverityLevel(func.Invoke(), level));
        }

        #endregion
    }
}