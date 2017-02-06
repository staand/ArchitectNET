using System;

namespace ArchitectNET.Core.Dynamic
{
    public interface IDynamicObjectCategory : IMetadata, IEquatable<IDynamicObjectCategory>
    {
        bool IsSubcategoryOf(IDynamicObjectCategory otherCategory);
    }
}