namespace ArchitectNET.Core.Dynamic
{
    public interface IDynamicObject : IDynamicSystemAware, IHasName
    {
        IDynamicObjectCategory Category { get; }
    }
}