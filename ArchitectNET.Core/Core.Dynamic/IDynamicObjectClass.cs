using System;

namespace ArchitectNET.Core.Dynamic
{
    public interface IDynamicObjectClass : IMetadata, IEquatable<IDynamicObjectClass>
    {
        bool IsSubcategoryOf(IDynamicObjectClass otherCategory);
    }
}