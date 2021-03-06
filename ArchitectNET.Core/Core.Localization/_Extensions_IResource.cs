﻿using System;
using ArchitectNET.Core.Localization.Support;
using ArchitectNET.Core._Internal_;

namespace ArchitectNET.Core.Localization
{
    public static class _Extensions_IResource
    {
        public static string ExtractString(this IResource resource)
        {
            Guard.ArgumentNotNull(resource, nameof(resource));
            var textResource = resource as PlainTextResource;
            if (textResource != null)
                return textResource.Text;
            throw new Exception(Resources.FormatString("7AE394F5-E90B-4074-A269-169194AE9244", resource.Type));
        }
    }
}