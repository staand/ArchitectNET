using System;
using System.IO;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core.Localization.Support
{
    public sealed class ObjectResource : IResource
    {
        private readonly object _object;

        public ObjectResource(object @object)
        {
            _object = @object;
        }

        public object Object => _object;

        public Stream OpenStream()
        {
            throw new Exception(Resources.GetString("45303E2B-ADA2-4F3A-B0BA-050748ED0273"));
        }

        public IResourceType Type => ResourceType.Object;

        public override string ToString()
        {
            return (_object ?? "<null>").ToString();
        }
    }
}