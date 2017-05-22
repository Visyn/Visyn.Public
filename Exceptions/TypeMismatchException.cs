using System;


namespace Visyn.Public.Exceptions
{
    public class TypeMismatchException : Exception, IVisynException
    {
        public Type Type { get; }


        public TypeMismatchException(Type type, string v, Exception innerException = null) : base(v, innerException)
        {
            Type = type;
        }
    }
}
