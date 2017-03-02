using System;
using System.Runtime.CompilerServices;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core.Localization
{
    public static class _Extensions_ILocalizer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FormatString(this ILocalizer localizer, ID resourceID, params object[] formatArguments)
        {
            var resource = localizer.GetResource(resourceID);
            return string.Format(resource.ExtractString(), formatArguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IResource GetResource(this ILocalizer localizer, ID resourceID, ILocale locale = null)
        {
            Guard.ArgumentNotNull(localizer, nameof(localizer));
            var resource = localizer.TryGetResource(resourceID, locale);
            if (resource != null)
                return resource;
            throw new Exception(Resources.FormatString("01C7E020-142C-42D3-B437-C531E39541F7", resourceID, locale));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(this ILocalizer localizer, ID resourceID, ILocale locale = null)
        {
            var resource = localizer.GetResource(resourceID, locale);
            return resource.ExtractString();
        }
    }
}