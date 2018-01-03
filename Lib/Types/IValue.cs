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

using System.Diagnostics;

namespace Visyn.Types
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
    public interface IValue<out T> : IValue
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