﻿using System.Reflection;
using ArchitectNET.Core.Localization;
using ArchitectNET.Core.Localization.Support;

namespace ArchitectNET.Core._Internal_
{
    internal static class Resources
    {
        private static readonly ILocalizer _localizer;

        static Resources()
        {
            _localizer = new ResourceManagerLocalizer(
                Assembly.GetExecutingAssembly(),
                "ArchitectNET.Core._Resources_.!");
        }

        internal static string FormatString(ID resourceID, params object[] formatArguments)
        {
            return _localizer.FormatString(resourceID, formatArguments);
        }

        internal static string GetString(ID resourceID)
        {
            return _localizer.GetString(resourceID);
        }
    }
}