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