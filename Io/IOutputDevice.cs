using System;

namespace Visyn.Public.Io
{
    public interface IOutputDevice
    {
        void Write(string text);
        void WriteLine(string line);

        void Write(Func<string> func);
    }
}