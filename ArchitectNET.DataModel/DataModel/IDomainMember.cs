using ArchitectNET.Core;

namespace ArchitectNET.DataModel
{
    public interface IDomainMember : IHasID, IHasAlias, IDomainModelAware
    {
        IDomainMemberClass Class { get; }
        IType Powertype { get; }
        IDomainRole Role { get; }
        IDomainMemberTagCollection Tags { get; }
    }
}