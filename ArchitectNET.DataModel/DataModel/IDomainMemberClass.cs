using System;
using ArchitectNET.Core;

namespace ArchitectNET.DataModel
{
    public interface IDomainMemberClass : IMetadata, IEquatable<IDomainMemberClass>, IHasAlias
    {
        bool IsSubclassOf(IDomainMemberClass otherMemberClass);
    }
}