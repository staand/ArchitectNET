namespace ArchitectNET.Core.Dynamic
{
    public interface IDynamicObject : IDynamicRuntimeAware, IHasName
    {
        IDynamicClass Class { get; }
    }
}