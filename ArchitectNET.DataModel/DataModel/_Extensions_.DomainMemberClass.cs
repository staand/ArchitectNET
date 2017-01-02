using ArchitectNET.Core;

namespace ArchitectNET.DataModel
{
    public static partial class _Extensions_
    {
        public static bool IsSuperclassOf(this IDomainMemberClass memberClass, IDomainMemberClass memberSubclass)
        {
            Guard.ArgumentNotNull(memberClass, "memberClass");
            Guard.ArgumentNotNull(memberSubclass, "memberSubclass");
            return memberSubclass.IsSubclassOf(memberClass);
        }
    }
}