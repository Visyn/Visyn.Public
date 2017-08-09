using System.Diagnostics;

namespace Visyn.Public.Types
{
    /// <summary>
    /// Interface IValue
    /// Non-generic marker class for  IValue&lt;T&gt;
    /// </summary>
    public interface IValue : IType
    {
        /// <summary>
        /// Values as.
        /// </summary>
        /// <returns>System.Object.</returns>
        object ValueAsObject();
    }

    /// <summary>
    /// Interface indicating a typed Value property is present
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValue<T> : IValue
    {
        /// <summary>
        /// Value of type T
        /// </summary>
        T Value { get; }
    }

    public static class ValueExtensions
    {
        public static T ValueAs<T>(this IValue value )
        {
            Debug.Assert(value.Type == typeof(T));
            return (T)value.ValueAsObject();
        }
    }
}