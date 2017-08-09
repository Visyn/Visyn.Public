using System;
using System.Diagnostics;
using System.Text;
using Visyn.Public.Io;

namespace Visyn.Core.Io
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
