#region Copyright (c) 2015-2017 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2017 Visyn
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
using NUnit.Framework;
using Visyn.Exceptions;
using Visyn.JetBrains;
using Visyn.Xml;

namespace Visyn.Test.Xml
{
    [TestFixture]
    public class XmlSerializeTests
    {
        [Test]
        public void SerializeDeserializeClassTest()
        {
            var testClassPath = GetTempFileName("xml");
            var first = "bob";
            var last = "smith";
            var testClass = new XTestClassWith2Properties(first,last);
            Assert.IsInstanceOf<XTestClassWith2Properties>(testClass);

            Assert.AreEqual(first, testClass.First);
            Assert.AreEqual(last, testClass.Last);

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
            Assert.IsFalse(File.Exists(testClassPath));

            serialize(testClass, testClassPath, null);
            Assert.IsTrue(File.Exists(testClassPath));

            var deserialized = deserialize<XTestClassWith2Properties>(testClassPath, null);

            Assert.IsInstanceOf<XTestClassWith2Properties>(deserialized);
            Assert.AreEqual(testClass, deserialized);
            Assert.IsTrue(testClass.Equals(deserialized));

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
        }

        [Test]
        public void DeserializeNonExistantFileTest()
        {
            var testClassPath = GetTempFileName("xml");

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
            Assert.IsFalse(File.Exists(testClassPath));

            Assert.Throws<FileNotFoundException>(() => deserialize<XTestClassWith2Properties>(testClassPath, null));
        }


        [Test]
        public void DeserializeTypeMismatchTest()
        {
            var testClassPath = GetTempFileName("xml");
            Assert.NotNull(testClassPath);
            var first = "bob";
            var last = "smith";
            var testClass = new XTestClassWith2Properties(first, last);;

            Assert.AreEqual(first, testClass.First);
            Assert.AreEqual(last, testClass.Last);
            Assert.IsInstanceOf<XTestClassWith2Properties>(testClass);

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
            Assert.IsFalse(File.Exists(testClassPath));

            serialize(testClass, testClassPath, null);
            Assert.IsTrue(File.Exists(testClassPath));

            var deserialized = deserialize<XTestClassWith2Properties>(testClassPath, null);

            Assert.IsInstanceOf<XTestClassWith2Properties>(deserialized);
            Assert.AreEqual(testClass, deserialized);

            // Attempt to deserialize with type mis-match
            Assert.Throws<InvalidOperationException>(() => deserialize<XTestClassWithList>(testClassPath, null));

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
        }

        [Test]
        public void SerializeDeserializeListTest()
        {
            var testClass = new XTestClassWithList() {SomeString = "bla"};
            var testClassPath = GetTempFileName("xml");

            Assert.IsInstanceOf<XTestClassWithList>(testClass);

            Assert.AreEqual(0, testClass.Settings.Count);
            testClass.Settings.Add("My Setting");
            testClass.Settings.Add("Another setting");
            Assert.AreEqual(2,testClass.Settings.Count);

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
            Assert.IsFalse(File.Exists(testClassPath));

            serialize(testClass, testClassPath, null);
            Assert.IsTrue(File.Exists(testClassPath));

            var deserialized = deserialize<XTestClassWithList>(testClassPath, null);

            Assert.IsInstanceOf<XTestClassWithList>(deserialized);
            Assert.AreEqual(testClass, deserialized);

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
        }

        [Test]
        public void SerializeDeserializeArrayTest()
        {
            var testClassPath = GetTempFileName("xml");
            const int arrayItems = 222;
            var testClass = new XTestClassWithArray(arrayItems);
            Assert.IsInstanceOf<XTestClassWithArray>(testClass);

            Assert.AreEqual(arrayItems, testClass.Settings.Length);

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
            Assert.IsFalse(File.Exists(testClassPath));

            serialize(testClass, testClassPath, null);
            Assert.IsTrue(File.Exists(testClassPath));

            var deserialized = deserialize<XTestClassWithArray>(testClassPath, null);

            Assert.IsInstanceOf<XTestClassWithArray>(deserialized);
            Assert.AreEqual(testClass, deserialized);

            // Attempt to deserialize with type mis-match
            Assert.Throws<InvalidOperationException>(() => deserialize<XTestClassWithList>(testClassPath, null));

            if (File.Exists(testClassPath)) File.Delete(testClassPath);
        }

        private static void serialize<T>([NotNull]T data, [NotNull]string filename, ExceptionHandler exceptionHandler)
        {
            Assert.NotNull(filename);
            using (var writer = new StreamWriter(filename))
            {
                Assert.NotNull(writer);
                Assert.IsInstanceOf<StreamWriter>(writer);
                XmlSerialize.Serialize(data, writer, exceptionHandler);
            }
        }

        private static T deserialize<T>([NotNull]string fileName, ExceptionHandler exceptionHandler)
        {
            Assert.NotNull(fileName);
            if (!File.Exists(fileName))
                Assert.False(File.Exists(fileName), $"{nameof(XmlSerializeTests)}.{nameof(deserialize)}<{typeof(T).Name}>({fileName}) does not exist!");

            using (var reader = new StreamReader(fileName))
            {
                return XmlSerialize.Deserialize<T>(reader, exceptionHandler);
            }
        }


        private static string GetTempFileName(string extension = null)
        {
            if (string.IsNullOrEmpty(extension)) return Path.GetTempFileName();
            if (!extension.StartsWith(".")) extension = "." + extension;
            return Path.GetTempFileName().Replace(".tmp", extension);
        }
    }
}