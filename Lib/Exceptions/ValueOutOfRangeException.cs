using System;

namespace Visyn.Public.Exceptions
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(string message) : base(message)
        {
        }

        public static void CheckAndThrow(string propertyName, int value, int minAllowed, int maxAllowed)
        {
            if(value < minAllowed || value > maxAllowed)
                throw new ValueOutOfRangeException($"{propertyName} [{value}] is not in range [{minAllowed},[{maxAllowed}]");
        }
    }
}
