using System;

namespace Visyn.Public.Exceptions
{
    public class NotAssignableException : Exception, IVisynException
    {
        public Type BaseType { get; }
        public Type DerivedType { get; }

        public NotAssignableException(string source, Type derivedType, Type baseType, Exception innerException=null) 
            : base($"{source}: {derivedType.Name} is not assignable to {baseType.Name}", innerException)
        {
            DerivedType = derivedType;
            BaseType = baseType;
        }
    }
}