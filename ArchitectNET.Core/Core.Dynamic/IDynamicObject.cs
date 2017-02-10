namespace ArchitectNET.Core.Dynamic
{
    public interface IDynamicObject : IDynamicRuntimeAware, IHasName
    {
        IDynamicObjectClass Class { get; }
    }
}