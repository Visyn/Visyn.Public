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
namespace Visyn.Public.CommandLine
{
    /// <summary>
    /// Interface ICommandLineArgs
    /// Interface for defining and parsing command line arguments
    /// </summary>
    public interface ICommandLineArgs
    {
        /// <summary>
        /// Try to parse command line arguments
        /// </summary>
        /// <param name="args">The arguments to parse.</param>
        /// <param name="result">The parsed result if successful.  Null if parse failed.</param>
        /// <returns><c>true</c> if arguments were successfully parsed, <c>false</c> otherwise.</returns>
        bool TryParse(string[] args, out ICommandLineArgs result);

        /// <summary>
        /// Try to parse command line arguments as a specific type
        /// </summary>
        /// <typeparam name="T">Type to parse as</typeparam>
        /// <param name="args">The arguments to parse.</param>
        /// <param name="result">The parsed result if successful.  null if parse failed.</param>
        /// <returns><c>true</c> if arguments were successfully parsed, <c>false</c> otherwise.</returns>
        bool TryParse<T>(string[] args, out T result) where T : class;
    }
}
