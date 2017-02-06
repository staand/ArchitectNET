using System.Runtime.CompilerServices;

namespace ArchitectNET.Core.Dynamic.Support
{
    public abstract class DynamicObjectCategory : IDynamicObjectCategory
    {
        private static readonly IDynamicObjectCategory _assemblyCategory;
        private static readonly IDynamicObjectCategory _dynamicSystemCategory;
        private static readonly IDynamicObjectCategory _moduleCategory;

        private readonly int _hashCode;

        protected DynamicObjectCategory()
        {
            _hashCode = RuntimeHelpers.GetHashCode(this);
        }

        static DynamicObjectCategory()
        {
            _dynamicSystemCategory = new DynamicSystemObjectCategory();
            _assemblyCategory = new AssemblyObjectCategory();
            _moduleCategory = new ModuleObjectCategory();
        }

        public static IDynamicObjectCategory Assembly => _assemblyCategory;

        public static IDynamicObjectCategory DynamicSystem => _dynamicSystemCategory;

        public static IDynamicObjectCategory Module => _moduleCategory;

        public virtual bool IsSubcategoryOf(IDynamicObjectCategory otherCategory)
        {
            if (otherCategory == null)
                return false;
            return Equals(otherCategory)
                   || GetType().IsSubclassOf(otherCategory.GetType());
        }

        public virtual bool Equals(IDynamicObjectCategory otherCategory)
        {
            return ReferenceEquals(this, otherCategory);
        }

        public abstract override string ToString();

        public override bool Equals(object otherObject)
        {
            var otherCategory = otherObject as IDynamicObjectCategory;
            return otherCategory != null
                   && Equals(otherCategory);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
    }
}