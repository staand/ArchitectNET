using ArchitectNET.Core.Collections;

namespace ArchitectNET.Core.Dynamic
{
    public interface IAssemblyModuleCollection : IFixedCollection<IModule>, IHasOwner<IAssembly>
    {
    }
}