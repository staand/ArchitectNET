namespace ArchitectNET.Core.Dynamic
{
    public interface IRuntimeObjectHolder : IDynamicObject
    {
        object RuntimeObject { get; }
    }
}