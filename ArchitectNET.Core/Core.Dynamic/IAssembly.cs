namespace ArchitectNET.Core.Dynamic
{
    public interface IAssembly : IAttributedObject
    {
        IAssemblyModuleCollection Modules { get; }
    }
}