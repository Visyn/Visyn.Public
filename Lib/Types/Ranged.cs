#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;

namespace Visyn.Types
{
    public class Ranged<T> : IValue<T>
        where T : struct,IComparable,IComparable<T>,IEquatable<T>//, IConvertible
    {
        private readonly T _min;
        private readonly T _max;
        private T _value;

        public T Min => _min;
        public T Max => _max;
        public T Value
        {
            get { return _value; }
            set
            {
                if(typeof(T)==typeof(double))
                {
                    var d = value as double?;
                    if (d >= (_min as double?) && d < (_max as double?) + 1) _value = value;
                    else throw new ArgumentOutOfRangeException(nameof(value), $"Value [{value}] must be in range [{_min},{_max})");
                }
                else if (typeof(T) == typeof(Single))
                {
                    var d = value as Single?;
                    if (d >= (_min as Single?) && d < (_max as Single?) + 1) _value = value;
                    else throw new ArgumentOutOfRangeException(nameof(value), $"Value [{value}] must be in range [{_min},{_max})");
                }
                else if (value.CompareTo(_min) >= 0 && value.CompareTo(_max) <= 0) _value = value;
                else throw new ArgumentOutOfRangeException(nameof(value), $"Value [{value}] must be in range [{_min},{_max})");
            }
        }

        public Ranged(T min,T max)
        {
            if (min.CompareTo(max) >= 0)
                throw new ArgumentOutOfRangeException($"{nameof(min)} must be < {nameof(max)} [{min} !< {max}] : min.CompareTo(max) = {min.CompareTo(max)}");
            _min = min;
            _max = max;
            if(_value.CompareTo(_min) < 0 ) _value = _min;
            if (_value.CompareTo(_max) > 0) _value = _min;
        }

        #region Implementation of IType

        /// <summary>
        /// Type marker.  Typically used to indicate underlying
        /// generic type.
        /// </summary>
        public Type Type => typeof(T);

        /// <summary>
        /// Values as.
        /// </summary>
        /// <returns>System.Object.</returns>
        public object ValueAsObject() => Value;

        #endregion
    }
}
