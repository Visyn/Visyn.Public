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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Visyn.Exceptions;

namespace Visyn.Meta
{
    public class MetaProperties : IReadOnlyDictionary<string,object>, IEnumerable<KeyValuePair<string, object>> 
    {
        private readonly Dictionary<string, object> Properties;

        /// <exclude />
        public string Name => (string)Properties[nameof(Name)];

        public MetaProperties() : this("")
        {

        }
        public MetaProperties(string newName)
        {
            Properties = new Dictionary<string, object> { {nameof(Name),newName} };
        }

        public MetaProperties(IEnumerable<KeyValuePair<string, object>> properties)
        {
            Properties = new Dictionary<string, object>();
            foreach(var prop in properties)
            {
                Properties.Add(prop.Key,prop.Value);
            }
            Debug.Assert(Properties.ContainsKey(nameof(Name)));
        }

        public MetaProperties(string newName, MetaProperties properties) : this(properties)
        {
            Properties[nameof(Name)] = newName;
        }

        public void Add(KeyValuePair<string, object> item)
        {
            ((ICollection <KeyValuePair<string,object>>)Properties).Add(item);
        }

        public void Add(string key, object value)
        {
            Properties.Add(key, value);
        }

        public T Get<T>(string key, bool throwIfMissing, T defaultValue = default(T))
        {
            object value;
            if(TryGetValue(key, out value))
            {
                if(value is T)
                {
                    return (T)value;
                }
                throw new ArrayTypeMismatchException($"{nameof(MetaProperties)}[{key}] is type {value.GetType().Name}, not of expected type {typeof(T)}.");
            }
            if (throwIfMissing) throw MissingItemException.ItemMissing(GetType().Name, key, typeof(T), null);
            return defaultValue;
        }

        public void ReplaceName(string newName)
        {
            Properties[nameof(Name)] = newName;
        }

        public void AppendName(string suffix)
        {
            var name = Properties[nameof(Name)];
            Properties[nameof(Name)] = $"{name}{suffix}";
        }

        public MetaProperties CopyProperties(string suffix) 
            => new MetaProperties(Name + suffix, this);

        #region Implementation of IEnumerable

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => Properties.GetEnumerator();

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Properties).GetEnumerator();

        #endregion

        #region Implementation of IReadOnlyCollection<out KeyValuePair<string,object>>

        /// <summary>Gets the number of elements in the collection.</summary>
        /// <returns>The number of elements in the collection. </returns>
        public int Count => Properties.Count;

        #endregion

        #region Implementation of IReadOnlyDictionary<string,object>

        /// <summary>Determines whether the read-only dictionary contains an element that has the specified key.</summary>
        /// <returns>true if the read-only dictionary contains an element that has the specified key; otherwise, false.</returns>
        /// <param name="key">The key to locate.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key" /> is null.</exception>
        public bool ContainsKey(string key) => Properties.ContainsKey(key);

        /// <summary>Gets the value that is associated with the specified key.</summary>
        /// <returns>true if the object that implements the <see cref="T:System.Collections.Generic.IReadOnlyDictionary`2" /> interface contains an element that has the specified key; otherwise, false.</returns>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key" /> is null.</exception>
        public bool TryGetValue(string key, out object value) => Properties.TryGetValue(key, out value);

        /// <summary>Gets the element that has the specified key in the read-only dictionary.</summary>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        /// <param name="key">The key to locate.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key" /> is null.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found. </exception>
        public object this[string key] => Properties[key];

        /// <summary>Gets an enumerable collection that contains the keys in the read-only dictionary. </summary>
        /// <returns>An enumerable collection that contains the keys in the read-only dictionary.</returns>
        public IEnumerable<string> Keys => ((IReadOnlyDictionary<string,object>) (Properties)).Keys;

        /// <summary>Gets an enumerable collection that contains the values in the read-only dictionary.</summary>
        /// <returns>An enumerable collection that contains the values in the read-only dictionary.</returns>
        public IEnumerable<object> Values => ((IReadOnlyDictionary<string,object>) (Properties)).Values;

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"{Name} : {Properties.Count}";

        #endregion

    }
}