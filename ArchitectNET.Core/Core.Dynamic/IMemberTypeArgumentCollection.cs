using ArchitectNET.Core.Collections;

namespace ArchitectNET.Core.Dynamic
{
    public interface IMemberTypeArgumentCollection : IFixedCollection<IType>, IHasOwner<IGenericMember>
    {
    }
}