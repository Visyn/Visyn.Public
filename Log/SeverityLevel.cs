using Visyn.Public.Mathematics;

namespace Visyn.Public.Log
{
    public enum SeverityLevel
    {
        LogAlways,
        Critical,
        Error,
        Warning,
        Informational,
        Verbose
    }

    public static class SeverityLevelExtensions
    {
        public static IColor GetColor(this SeverityLevel level)
        {
            switch (level)
            {
                case SeverityLevel.LogAlways:
                    return ColorStruct.Red;
                case SeverityLevel.Critical:
                    return ColorStruct.Crimson;
                case SeverityLevel.Error:
                    return ColorStruct.Red;
                case SeverityLevel.Warning:
                    return ColorStruct.DarkOrange;
                case SeverityLevel.Informational:
                    return ColorStruct.Black;
                case SeverityLevel.Verbose:
                    return ColorStruct.Cyan;
                default:
                    return ColorStruct.Black;
            }
        }
    }
}
