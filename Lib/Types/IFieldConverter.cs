namespace Visyn.Types
{
    public interface IFieldConverter : IType
    {
        bool CustomNullHandling { get; }

        string FieldToString(object from);
        object StringToField(string text);
    }
}