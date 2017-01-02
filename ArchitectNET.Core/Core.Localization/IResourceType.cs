using System;

namespace ArchitectNET.Core.Localization
{
    public interface IResourceType : IMetadata, IEquatable<IResourceType>
    {
        ContentType ContentType { get; }
    }
}