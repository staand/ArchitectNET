namespace ArchitectNET.Core.Dynamic
{
    public interface ITypedObject : IDynamicObject
    {
        IType Type { get; }
    }
}