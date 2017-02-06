namespace ArchitectNET.Core.Dynamic
{
    public interface IDynamicObjectProvider
    {
        IDynamicObject TryGetDynamicObject(object runtimeObject);
    }
}