using System.Collections.Generic;

namespace Visyn.Public.Io
{
    public static class OutputDevice
    {
        public static void Write(this IOutputDevice output, IEnumerable<string> lines)
        {
            var multiline = output as IOutputDeviceMultiline;
            if (multiline != null)
                multiline.Write(lines);
            else
                foreach (var line in lines) output.WriteLine(line);
        }
    }
}