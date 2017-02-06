using System;
using System.IO;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core.Localization.Support
{
    public sealed class PlainTextResource : IResource
    {
        private readonly string _text;

        public PlainTextResource(string text)
        {
            Guard.ArgumentNotNull(text, nameof(text));
            _text = text;
        }

        public string Text => _text;

        public Stream OpenStream()
        {
            throw new Exception(Resources.GetString("89BB995E-A0D9-41DF-8303-C15EBECAA11D"));
        }

        public IResourceType Type => ResourceType.PlainText;

        public override string ToString()
        {
            return _text;
        }
    }
}