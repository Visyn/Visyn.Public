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