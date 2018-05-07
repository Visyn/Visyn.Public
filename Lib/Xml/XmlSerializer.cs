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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Visyn.Exceptions;
using Visyn.Serialize;

namespace Visyn.Xml
{
    public static class XmlSerialize
    {
        public static void Serialize(Type type, object data, TextWriter writer, Type[] extraTypes, ExceptionHandler exceptionHandler)
        {
            try
            {   // OnSerializing method call not implemented by XmlSerializer, do it manually
                InvokeDecoratedMethods<OnSerializingAttribute>(data);
                var serializer = new XmlSerializer(type, extraTypes);

                serializer.Serialize(writer, data);
                InvokeDecoratedMethods<OnSerializedAttribute>(data);
            }
            catch (Exception exc)
            {
                if (exceptionHandler?.Invoke($"{nameof(XmlSerialize)}.{nameof(Serialize)}", exc) != true) throw;
            }
        }

        public static void Serialize<T>(T data, TextWriter writer, ExceptionHandler exceptionHandler)
        {
            try
            {   // OnSerializing method call not implemented by XmlSerializer, do it manually
                InvokeDecoratedMethods<OnSerializingAttribute>(data);
                var serializer = new XmlSerializer(typeof(T));

                serializer.Serialize(writer, data);
                InvokeDecoratedMethods<OnSerializedAttribute>(data);
                InvokeDecoratedMethods<PortableOnSerializedAttribute>(data);
            }
            catch (Exception exc)
            {
                if (exceptionHandler?.Invoke($"{nameof(XmlSerialize)}.{nameof(Serialize)}", exc) != true) throw; 
            }
        }

        public static T Deserialize<T>(TextReader reader, ExceptionHandler exceptionHandler)
        {
            var data = default(T);
            try
            {
                var deserializer = new XmlSerializer(typeof(T));
                data = (T)deserializer.Deserialize(reader);

                // OnDeserialized method call not implemented by XmlSerializer, do it manually
                InvokeDecoratedMethods<OnDeserializedAttribute>(data);
                InvokeDecoratedMethods<PortableOnDeserializedAttribute>(data);
            }
            catch (Exception exc)
            {
                if (exceptionHandler?.Invoke($"{nameof(XmlSerialize)}.{nameof(Deserialize)}", exc) != true) throw;
            }
            return data;
        }

        private static readonly object[] _context = { new StreamingContext() };

        // OnDeserializing method call not implemented by XmlSerializer, do it manually
        public static void InvokeDecoratedMethods<TAttribute>(object data, bool recurse = true)
        {
            if (data == null) return;
            if (data is Type) return;

            var typeInfo = data.GetType().GetTypeInfo();
            if (typeInfo == null) return;
            foreach ( var method in typeInfo.DeclaredMethods.Where(
                    m => m.GetCustomAttributes(typeof(TAttribute), true).ToList()?.Count > 0))
            {
                var par = method.GetParameters();
                if (par.Length == 0) method.Invoke(data, new object[] {});
                else method.Invoke(data, _context);
            }

            if (!recurse) return;

            if (typeInfo.IsArray && typeInfo.AsType() != typeof(string))
            {
                var array = data as Array;
                if (array == null) return;
                foreach (var item in array)
                {
                    InvokeDecoratedMethods<TAttribute>(item, true);
                }
                return;
            }

            // Call recursively on nested classes
            foreach ( var prop in typeInfo.DeclaredProperties.Where(p => p.PropertyType.GetTypeInfo().IsClass && p.PropertyType != typeof(string)))
            {
                var paramInfo = prop.GetIndexParameters();
                if (paramInfo == null || paramInfo.Length == 0)
                {
                    var value = (object)prop.GetValue(data);
                    InvokeDecoratedMethods<TAttribute>(value);
                }
#if DEBUG
                else
                {
                    // This is an indexed property, not sure what to do with it at this point
                    foreach (var param in paramInfo)
                    {
                    }
                }
#endif
            }
        }
    }
}
