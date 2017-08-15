using System;
using System.Collections.Generic;
using System.Linq;
using Visyn.JetBrains;

namespace Visyn.Io
{
    public class OutputToCollection<T> : IOutputDeviceMultiline where T : class 
    {
        [NotNull]
        private ICollection<T> _collection { get; }
        public OutputToCollection(ICollection<T> collection, Action<IEnumerable<T>> addRangeFunction)
        {
            if (collection == null) throw new NullReferenceException($"{nameof(OutputToCollection<T>)}<{typeof(T).Name}> {nameof(collection)} must be non-null!");
            _collection = collection;
            if (addRangeFunction != null) AddRangeFunction = addRangeFunction;
            else if (collection is List<T>) AddRangeFunction = addRangeToList;
            else AddRangeFunction = AddRange;
        }


        public void Write(T text)
        {
            _collection.Add(text);
        }

        public void WriteLine(T line)
        {
            _collection.Add(line);
        }

        public void Write(Func<T> func)
        {
            _collection.Add(func());
        }

        public void Write(IEnumerable<T> lines)
        {
            AddRangeFunction(lines);
        }

        private Action<IEnumerable<T>> AddRangeFunction { get; }
        private void AddRange(IEnumerable<T> lines)
        {
            if (lines == null) return;
            foreach(var line in lines)
                _collection.Add(line);
        }

        private void addRangeToList(IEnumerable<T> lines)
        {
            ((List<T>)_collection).AddRange(lines);
        }

        #region Implementation of IOutputDevice

        public void Write(string text)
        {
            var instance = text as T ?? (T)Activator.CreateInstance(typeof(T), text);
            _collection.Add(instance);
        }

        public void WriteLine(string line)
        {
            var instance = line as T ?? (T)Activator.CreateInstance(typeof(T), line);
            _collection.Add(instance);
        }

        public void Write(Func<string> func)
        {
            WriteLine(func());
        }

        public void Write(IEnumerable<string> lines)
        {
            IEnumerable<T> instances;
            if (typeof(string) is T)
            {
                instances = lines.Select(line => line as T);
            }
            else
                instances = lines.Select(line => (T) Activator.CreateInstance(typeof(T), line));
            AddRangeFunction(instances);
        }

        #endregion
    }
}