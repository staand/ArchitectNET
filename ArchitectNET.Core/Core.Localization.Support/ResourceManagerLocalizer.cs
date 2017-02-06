using System.Reflection;
using System.Resources;

namespace ArchitectNET.Core.Localization.Support
{
    public class ResourceManagerLocalizer : ILocalizer
    {
        private readonly ResourceManager _resourceManager;

        public ResourceManagerLocalizer(Assembly assembly, string baseName)
        {
            Guard.ArgumentNotNull(assembly, nameof(assembly));
            Guard.ArgumentNotNull(baseName, nameof(baseName));
            _resourceManager = new ResourceManager(baseName, assembly);
        }

        public IResource TryGetResource(ID resourceID, ILocale targetLocale = null)
        {
            if (resourceID.IsEmpty)
                return null;
            var resourceKey = resourceID.TryExtractString();
            if (resourceKey == null)
                return null;
            var resourceManager = _resourceManager;
            var culture = (targetLocale ?? Locale.Invariant).Culture;
            var text = resourceManager.GetString(resourceKey, culture);
            if (text != null)
                return new PlainTextResource(text);
            var @object = resourceManager.GetObject(resourceKey, culture);
            if (@object != null)
                return new ObjectResource(@object);
            return null;
        }
    }
}