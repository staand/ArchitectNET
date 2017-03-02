using System.Runtime.CompilerServices;

namespace ArchitectNET.Core.Dynamic
{
    public static class _Extensions_IDynamicClass
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSuperclassOf(this IDynamicClass @class, IDynamicClass subclass)
        {
            Guard.ArgumentNotNull(@class, nameof(@class));
            Guard.ArgumentNotNull(subclass, nameof(subclass));
            return subclass.IsSubclassOf(@class);
        }
    }
}