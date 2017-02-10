using System.Runtime.CompilerServices;

namespace ArchitectNET.Core.Dynamic.Classification
{
    public abstract class DynamicObjectClass : IDynamicObjectClass
    {
        private static readonly IDynamicObjectClass _assemblyClass;
        private static readonly IDynamicObjectClass _dynamicSystemClass;
        private static readonly IDynamicObjectClass _moduleClass;

        private readonly int _hashCode;

        static DynamicObjectClass()
        {
            _dynamicSystemClass = new DynamicRuntimeObjectClass();
            _assemblyClass = new AssemblyObjectClass();
            _moduleClass = new ModuleObjectClass();
        }

        protected DynamicObjectClass()
        {
            _hashCode = RuntimeHelpers.GetHashCode(this);
        }

        public static IDynamicObjectClass Assembly => _assemblyClass;

        public static IDynamicObjectClass DynamicSystem => _dynamicSystemClass;

        public static IDynamicObjectClass Module => _moduleClass;

        public virtual bool IsSubcategoryOf(IDynamicObjectClass otherCategory)
        {
            if (otherCategory == null)
                return false;
            return Equals(otherCategory)
                   || GetType().IsSubclassOf(otherCategory.GetType());
        }

        public virtual bool Equals(IDynamicObjectClass otherCategory)
        {
            return ReferenceEquals(this, otherCategory);
        }

        public abstract override string ToString();

        public override bool Equals(object otherObject)
        {
            var otherCategory = otherObject as IDynamicObjectClass;
            return otherCategory != null
                   && Equals(otherCategory);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}