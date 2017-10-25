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
using System.Reflection;
using Visyn.Exceptions;

namespace Visyn.Serialize.Converters
{
    public sealed class EnumConverter : ConverterBase
    {

        public EnumConverter(Type sourceEnum) : base(sourceEnum)
        {
            if (sourceEnum.GetTypeInfo().IsEnum == false)
                throw new BadUsageException($"The Input source Type must be an Enum but is of type {sourceEnum.Name}");
        }

        public override object StringToField(string text)
        {
            try
            {
                return Enum.Parse(Type, text.Trim(), true);
            }
            catch (ArgumentException)
            {
                throw new ConvertException(text, Type, $"The value {text} is not present in the Enum.");
            }
        }
    }
}
