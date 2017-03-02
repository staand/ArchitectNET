using System.Runtime.CompilerServices;
using ArchitectNET.Core.Dynamic.Classification;

namespace ArchitectNET.Core.Dynamic
{
    public static class _Extensions_IMember
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInternal(this IMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            return member.Class.IsSubclassOf(DynamicClass.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPrivate(this IMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            return member.Class.IsSubclassOf(DynamicClass.Private);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsProtected(this IMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            return member.Class.IsSubclassOf(DynamicClass.Protected);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsProtectedInternal(this IMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            return member.Class.IsSubclassOf(DynamicClass.ProtectedInternal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPublic(this IMember member)
        {
            Guard.ArgumentNotNull(member, nameof(member));
            return member.Class.IsSubclassOf(DynamicClass.Public);
        }
    }
}