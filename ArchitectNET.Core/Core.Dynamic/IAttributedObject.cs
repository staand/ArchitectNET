namespace ArchitectNET.Core.Dynamic
{
    public interface IAttributedObject : IDynamicObject
    {
        IObjectAttributeCollection Attributes { get; }
    }
}