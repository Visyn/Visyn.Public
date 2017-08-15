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
using System.Diagnostics;
using System.Text;

namespace Visyn.Io
{
    public class DebugOutput : IOutputDevice
    {
        #region Implementation of IOutputDevice

        public void Write(string text)
        {
#if DEBUG
            if(text.Contains(Environment.NewLine))
            {
                var split = text.Split(new [] { Environment.NewLine} , StringSplitOptions.None);
                for(var i=0;i<split.Length-1;i++)
                {
                    WriteLine(split[i]);
                }
                text = split[split.Length - 1];
            }
            _builder?.Append(text);
#endif
        }

        public void WriteLine(string line)
        {
#if DEBUG
            Debug.WriteLine(_builder?.Length > 0 ? _builder.Append(line).ToString() : line);
#endif
        }

        public void Write(Func<string> func)
        {   // Note:  func() is called in case user function has a side-effect 
            // (bad practice on the users part...)
            Write(func());
        }


        #endregion

#if DEBUG
        private readonly StringBuilder _builder;

        public DebugOutput()
        {
            _builder = new StringBuilder(1024);
        }
#endif
    }
}
