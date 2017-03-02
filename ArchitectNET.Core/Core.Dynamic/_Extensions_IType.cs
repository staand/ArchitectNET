using System.Runtime.CompilerServices;

namespace ArchitectNET.Core.Dynamic
{
    public static class _Extensions_IType
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsByte(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(byte));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDecimal(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(decimal));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDouble(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(double));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInt16(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(short));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInt32(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(int));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInt64(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(long));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsObject(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(object));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSByte(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(sbyte));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSingle(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(float));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsString(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(string));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUInt16(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(ushort));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUInt32(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(uint));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUInt64(this IType type)
        {
            Guard.ArgumentNotNull(type, nameof(type));
            return ReferenceEquals(type.TryGetRuntimeObject(), typeof(ulong));
        }
    }
}