using ArchitectNET.Core;
using ArchitectNET.DataModel.Classification;

namespace ArchitectNET.DataModel
{
    public static partial class _Extensions_
    {
        public static bool IsDomainModel(this IDomainMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            var domainModelClass = DomainMemberClass.DomainModel;
            var memberClass = member.Class;
            return ReferenceEquals(domainModelClass, memberClass)
                   || memberClass.IsSubclassOf(domainModelClass);
        }

        public static bool IsLiteral(this IDomainMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            var literalClass = DomainMemberClass.Literal;
            var memberClass = member.Class;
            return ReferenceEquals(literalClass, memberClass)
                   || memberClass.IsSubclassOf(literalClass);
        }

        public static bool IsQuery(this IDomainMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            var queryClass = DomainMemberClass.Query;
            var memberClass = member.Class;
            return ReferenceEquals(queryClass, memberClass)
                   || memberClass.IsSubclassOf(queryClass);
        }

        public static bool IsType(this IDomainMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            var typeClass = DomainMemberClass.Type;
            var memberClass = member.Class;
            return ReferenceEquals(typeClass, memberClass)
                   || memberClass.IsSubclassOf(typeClass);
        }
    }
}