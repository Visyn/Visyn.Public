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

using Visyn.Serialize;

namespace Visyn.Exceptions
{
    /// <summary>Indicates the wrong usage of the library.</summary>
//    [Serializable]
    public class BadUsageException : FileHelpersException
    {
        /// <summary>Creates an instance of an BadUsageException.</summary>
        /// <param name="message">The exception Message</param>
        public BadUsageException(string message) : base(message) {}

        /// <summary>Creates an instance of an BadUsageException.</summary>
        /// <param name="message">The exception Message</param>
        /// <param name="line">The line number where the problem was found</param>
        /// <param name="column">The column number where the problem was found</param>
        public BadUsageException(int line, int column, string message)
            : base(line, column, message) {}

        /// <summary>Creates an instance of an BadUsageException.</summary>
        /// <param name="message">The exception Message</param>
        /// <param name="line">Line to display in message</param>
        public BadUsageException(LineInfo line, string message)
            : this(line.mReader.LineNumber, line.mCurrentPos, message) {}
    }
}
