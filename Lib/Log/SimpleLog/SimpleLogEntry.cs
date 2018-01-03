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

namespace Visyn.Log.SimpleLog
{
    public class SimpleLogEntry<TEventLevel> : ILogEntry<TEventLevel>
    {
        public static string ApplicationName { get; set; }
        public static string ComputerName { get; set; }

        public static string UserName { get; set; } 
        #region Implementation of ILogEntry<SeverityLevel>

        public string Application { get; }
        public string Computer { get; }
        public string User { get; }
        public TEventLevel EventLevel { get; }
        public string Message { get; }
        public string Source { get; }
        public DateTime TimestampLocal { get; }
        public DateTime TimestampUtc { get; }

        #endregion

        public SimpleLogEntry(string source, string message, TEventLevel level)
        {
            TimestampUtc = DateTime.UtcNow;
            TimestampLocal = DateTime.Now;
            Source = source;
            Message = message;
            EventLevel = level;

            // initialize from static variables
            Application = ApplicationName;
            Computer = ComputerName;
            User = UserName;
        }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{TimestampLocal}, {EventLevel}, {Message}";
        }

        #endregion
    }
}