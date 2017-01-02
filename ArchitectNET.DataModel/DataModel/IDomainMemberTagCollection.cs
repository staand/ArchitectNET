using ArchitectNET.Core;
using ArchitectNET.Core.Collections;

namespace ArchitectNET.DataModel
{
    public interface IDomainMemberTagCollection : IFixedMap<string, ILiteral>, IHasOwner<IDomainMember>
    {
    }
}