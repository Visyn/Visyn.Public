using System;

namespace Visyn.Public.Types
{
    public class ValueWithDefault<T> : IValue<T>
    {
        #region Implementation of IType

        public Type Type => typeof(T);
        public object ValueAsObject() => Value;

        #endregion

        #region Implementation of IValue<T>

        public T Value { get; set; }

        #endregion

        public T DefaultValue { get; }

        public ValueWithDefault(T defaultValue)
        {
            Value = defaultValue;
            DefaultValue = defaultValue;
        }

        public void ResetToDefault()
        {
            Value = DefaultValue;
        }
    }
}
