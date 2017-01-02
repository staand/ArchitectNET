using System.Threading.Tasks;

namespace ArchitectNET.DataModel
{
    public interface IDomainResolver
    {
        Task<IDomainMember> TryResolveMemberAsync(DomainMemberRef memberRef);
    }
}