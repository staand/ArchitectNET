using System;

namespace ArchitectNET.Core.Dynamic
{
    public interface IDynamicClass : IMetadata, IEquatable<IDynamicClass>
    {
        bool IsSubclassOf(IDynamicClass otherClass);
    }
}