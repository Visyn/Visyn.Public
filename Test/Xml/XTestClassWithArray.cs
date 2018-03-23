#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright(c) 2015-2018 Visyn
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
using System.Collections.Generic;
using System.IO;
using Visyn.Serialize;
using Visyn.Xml;

namespace Visyn.Test.Xml
{
    [Serializable]
    public class XTestClassWithArray : IXTestClass<XTestClassWithArray>
    {
        private string _someString;
        public string SomeString
        {
            get { return _someString; }
            set { _someString = value; }
        }

        public int Count { get; set; }

        private string[] _settings;
        public string[] Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        // These will be ignored
        [NonSerialized()]
#pragma warning disable 169
#pragma warning disable 414
        private int willBeIgnored1 = 1;
        private int willBeIgnored2 = 1;
#pragma warning restore 414
#pragma warning restore 169
        public XTestClassWithArray(int arrayCount)
        {
            Count = arrayCount;
            _settings = new string[arrayCount];
            for(int i=0;i<arrayCount;i++)
            {
                _settings[i] = $"array item {i}";
            }
        }

        public XTestClassWithArray()
        {
            _settings = new string[1000];
        }

        [PortableOnDeserialized]
        public void AfterDeserialization()
        {
            var newSettings = new string[Count];
            for(var i=0;i<Count;i++)
            {
                newSettings[i] = _settings[i];
            }
            _settings = newSettings;
        }

        //public void AddListItems(int count)
        //{

        //    for(int i=0;i<count;i++)
        //    {
        //        _settings.Add($"List item {_settings.Length}");
        //    }
        //}

        #region Implementation of IEquatable<TestClass>

        public bool Equals(XTestClassWithArray other)
        {
            if (other == null) return false;
            if (SomeString != other.SomeString) return false;
            if (Count != other.Count) return false;
            for(var i=0;i<Count;i++)
            {
                if (!Settings[i].Equals(other.Settings[i])) return false;
            }
            return true;
        }
        #endregion
        #region Implementation of IXTestClass<TestClassWithList>

        public IEnumerable<string> ExpectedXPaths()
        {
            yield return $"/{nameof(XTestClassWithArray)}/SomeString[1]";
            for (var i = 0; i < Count; i++)
                yield return $"/{nameof(XTestClassWithArray)}/Settings[1]/string[{i + 1}]";
        }

        public IEnumerable<string> ToXml()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (TextWriter tw = new StreamWriter(memoryStream))
            {
                XmlSerialize.Serialize(this, tw, null);
            }
            using (var tr = new StreamReader(memoryStream))
            {
                var list = new List<string>();
                while (true)
                {
                    var line = tr.ReadLine();
                    if (line == null) break;
                    list.Add(line);
                }
                return list;
            }
        }



        #endregion

    }
}
