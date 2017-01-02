using System.IO;

namespace ArchitectNET.Core.Localization
{
    public interface IResource
    {
        IResourceType Type { get; }
        Stream OpenStream();
    }
}