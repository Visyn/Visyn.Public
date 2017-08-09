﻿#region Copyright (c) 2015-2017 Visyn
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

using Visyn.Public.Mathematics;

namespace Visyn.Public.Log
{
    public enum SeverityLevel
    {
        LogAlways,
        Critical,
        Error,
        Warning,
        Informational,
        Verbose
    }

    public static class SeverityLevelExtensions
    {
        public static IColor GetColor(this SeverityLevel level)
        {
            switch (level)
            {
                case SeverityLevel.LogAlways: return ColorStruct.Red;
                case SeverityLevel.Critical: return ColorStruct.Crimson;
                case SeverityLevel.Error: return ColorStruct.Red;
                case SeverityLevel.Warning: return ColorStruct.DarkOrange;
                case SeverityLevel.Informational: return ColorStruct.Black;
                case SeverityLevel.Verbose: return ColorStruct.Cyan;
                default:  return ColorStruct.Black;
            }
        }
    }
}