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

        public object Object
        {
            get { return _object; }
        }

        public IResourceType Type
        {
            get { return ResourceType.Object; }
        }

        public Stream OpenStream()
        {
            throw new Exception(Resources.GetString("45303E2B-ADA2-4F3A-B0BA-050748ED0273"));
        }

        public override string ToString()
        {
            return (_object ?? "<null>").ToString();
        }
    }
}