using ArchitectNET.Core;
using ArchitectNET.Core.Collections;

namespace ArchitectNET.DataModel
{
    public interface ITypeSupertypeCollection : IFixedCollection<IType>, IHasOwner<IType>
    {
    }
}