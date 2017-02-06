using ArchitectNET.Core.Collections;

namespace ArchitectNET.Core.Dynamic
{
    public interface IMemberParameterCollection : IFixedCollection<IParameter>, IHasOwner<IInvocableMember>
    {
    }
}