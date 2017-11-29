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
using System.Collections.Generic;
using System.IO;
using Visyn.Xml;

namespace Visyn.Test.Xml
{
    [Serializable()]
    public class XTestClassWith2Properties : IXTestClass<XTestClassWith2Properties>
    {
        public string First { get; set; }
        public string Last { get; set; }

        public XTestClassWith2Properties() {}
    
        public XTestClassWith2Properties(string first, string last)
        {
            First = first;
            Last = last;
        }

        public IEnumerable<string> ToXml()
        {
            var memoryStream = new MemoryStream();
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

        public IEnumerable<string> ExpectedXPaths()
        {
            return new[]
            {
                @"/XClassWith2Properties/First[1]",
                @"/XClassWith2Properties/Last[1]"
            };
        }
        #region Equality members

        public bool Equals(XTestClassWith2Properties other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(First, other.First) && string.Equals(Last, other.Last);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((XTestClassWith2Properties) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode =  0;
                hashCode = (hashCode * 397) ^ (First?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Last?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}