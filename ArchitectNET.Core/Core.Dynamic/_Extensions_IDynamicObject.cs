using System;
using System.Runtime.CompilerServices;
using ArchitectNET.Core.Dynamic.Classification;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core.Dynamic
{
    public static class _Extensions_IDynamicObject
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetRuntimeObject(this IDynamicObject dynamicObject)
        {
            var runtimeObject = dynamicObject.TryGetRuntimeObject();
            if (runtimeObject != null)
                return runtimeObject;
            throw new Exception(
                Resources.FormatString("13C139FA-3F87-41A5-94DA-0B34D4686D7E",
                                       dynamicObject,
                                       DynamicClass.RuntimeObjectHolder));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAssembly(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Assembly);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsConstructor(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Constructor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDynamicSystem(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.DynamicRuntime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEvent(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Event);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsField(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Field);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMember(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Member);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsMethod(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Method);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsModule(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Module);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsParameter(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Parameter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsProperty(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Property);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRuntimeObjectHolder(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.RuntimeObjectHolder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsType(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.Type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsTypeParameter(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            return dynamicObject.Class.IsSubclassOf(DynamicClass.TypeParameter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object TryGetRuntimeObject(this IDynamicObject dynamicObject)
        {
            Guard.ArgumentNotNull(dynamicObject, nameof(dynamicObject));
            if (!dynamicObject.Class.IsSubclassOf(DynamicClass.RuntimeObjectHolder))
                return null;
            return ((IRuntimeObjectHolder) dynamicObject).RuntimeObject;
        }
    }
}