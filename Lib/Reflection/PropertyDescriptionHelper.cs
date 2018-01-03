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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Visyn.Reflection
{
    public static class PropertyDescriptionHelper
    {
        public static string GetDescription(this TypeInfo objectType)
        {
            if (objectType == null) throw new NullReferenceException($"Object Type cannot be null!");
            
            var attribute =
                objectType.GetCustomAttributes(typeof(DisplayAttribute), false)
                    .OfType<DisplayAttribute>()
                    .FirstOrDefault();
            if (!string.IsNullOrEmpty(attribute?.Description)) return attribute.Description;
            return objectType.Name;
        }


        public static string GetDescription(object value)
        {
            if (value == null) throw new NullReferenceException($"Cannot get description for null value");
            if (value is PropertyInfo) return GetDescription((PropertyInfo)value, null);
            var typeInfo = value.GetType().GetTypeInfo();
            if (typeInfo.IsValueType)
            {
                var fieldInfo = typeInfo.GetDeclaredField(value.ToString());
                var attribute = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)
                                            .OfType<DisplayAttribute>()
                                            .FirstOrDefault(); ;
                if (!string.IsNullOrEmpty(attribute?.Description)) return attribute.Description;
                
                return value.ToString();
            }
            return GetDescription(typeInfo);
        }


        public static string GetDescription(this PropertyInfo info, ICustomFormatter alternateFormatProvider = null)
        {
            if (info == null) throw new NullReferenceException($"PropertyInfo cannot be null!");
            var attribute =
                info.GetCustomAttributes(typeof(DisplayAttribute))
                    ?.OfType<DisplayAttribute>()
                    .FirstOrDefault();
            if (!string.IsNullOrEmpty(attribute?.Description))
            {
                return attribute?.Description;
            }
            if (alternateFormatProvider != null)
            {
                return alternateFormatProvider.Format(null, info.Name, null);
            }
            return info.Name;
        }
    }
}
