﻿#region Copyright (c) 2015-2018 Visyn
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
using System.Runtime.CompilerServices;
using Visyn.Io;
using Visyn.Log;

namespace Visyn.Exceptions
{
    public class ThrowException : IExceptionHandler
    {
        private readonly Func<object, Exception, bool> _handler;
        public ThrowException(Func<object, Exception, bool> handler=null)
        {
            _handler = handler;
        }

        #region Implementation of IExceptionHandler

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HandleException(object sender, Exception exception)
        {
            if(_handler?.Invoke(sender, exception) == true) return true;
            throw exception;
        }

        #endregion

        public static ThrowException WriteException(IOutputDevice device)
        {
            if (device == null) return new ThrowException();

            return new ThrowException((s, exc) =>
            {
                device.WriteLine($"{s}:{exc.Message}");
                return false;
            });
        }

        public static ThrowException LogException(ILog<SeverityLevel> log)
        {
            if (log == null) return new ThrowException();

            return new ThrowException((s, exc) =>
            {
                log.Log(s,exc.Message, SeverityLevel.Error);
                return false;
            });
        }
    }
}
