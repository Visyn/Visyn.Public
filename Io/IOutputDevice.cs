using System;
using System.Collections;
using System.Collections.Generic;

namespace Visyn.Public.Io
{

    public interface IOutputDevice
    {
        void Write(string text);
        void WriteLine(string line);

        void Write(Func<string> func);
    }

    public interface IOutputDeviceMultiline : IOutputDevice
    {
        void Write(IEnumerable<string> lines);
    }
}