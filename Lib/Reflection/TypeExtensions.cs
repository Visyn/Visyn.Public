using System;
using System.Reflection;

namespace Visyn.Reflection
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns a value that indicates whether the specified type can be assigned to the current type.
        /// Note: This method exists in non-portable Win32.  Extension method created for compatibility.
        /// </summary>
        /// <param name="baseType">The type.</param>
        /// <param name="derivedType">Type of the class or interface to check.</param>
        /// <returns>true if the specified type can be assigned to this type; otherwise, false.</returns>
        public static bool IsAssignableFrom(this Type baseType, Type derivedType) 
            => baseType.GetTypeInfo().IsAssignableFrom(derivedType.GetTypeInfo());

        /// <summary>
        /// Returns a value that indicates whether the specified type can be assigned to the current type.
        /// Note: This method exists in non-portable Win32.  Extension method created for compatibility.
        /// </summary>
        /// <param name="derivedType">Type of the class or interface to check.</param>
        /// <param name="baseType">The type.</param>
        /// <returns>true if the specified type can be assigned to this type; otherwise, false.</returns>
        public static bool IsAssignableTo(this Type derivedType, Type baseType)
            => baseType.GetTypeInfo().IsAssignableFrom(derivedType.GetTypeInfo());
    }
}
