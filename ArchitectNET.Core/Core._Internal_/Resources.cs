using System.Reflection;
using ArchitectNET.Core.Localization;
using ArchitectNET.Core.Localization.Support;

namespace ArchitectNET.Core._Internal_
{
    /// <summary>
    /// An auxiliary class that provides services for getting localized resources which are compiled to assembly using *.RESX
    /// file.
    /// </summary>
    internal static class Resources
    {
        /// <summary>
        /// An instance of <see cref="ILocalizer" /> which provides access to assembly resources.
        /// </summary>
        private static readonly ILocalizer _localizer;

        /// <summary>
        /// Initializes <see cref="_localizer" /> field.
        /// </summary>
        static Resources()
        {
            _localizer = new ResourceManagerLocalizer(
                Assembly.GetExecutingAssembly(),
                "ArchitectNET.Core._Resources_.!");
        }

        /// <summary>
        /// Finds a string resource with the given <see cref="ID" /> and performs its formatting using
        /// <see cref="string.Format(string,object[])" /> method and the specified arguments.
        /// </summary>
        /// <param name="resourceID"> An <see cref="ID" /> of the string resource to be found. </param>
        /// <param name="formatArguments">
        /// An array of objects which should be used as formatting arguments in the
        /// <see cref="string.Format(string,object[])" /> method.
        /// </param>
        /// <returns>
        /// A <see cref="string" /> which is an assembly resource with the specified <paramref name="resourceID" /> and
        /// is formatted using <see cref="string.Format(string,object[])" /> method and the specified
        /// <paramref name="formatArguments" />.
        /// </returns>
        internal static string FormatString(ID resourceID, params object[] formatArguments)
        {
            return _localizer.FormatString(resourceID, formatArguments);
        }

        /// <summary>
        /// Finds a string resource with the given <see cref="ID" />.
        /// </summary>
        /// <param name="resourceID"> An <see cref="ID" /> of the string resource to be found. </param>
        /// <returns> A <see cref="string" /> which is an assembly resource with the specified <paramref name="resourceID" />. </returns>
        internal static string GetString(ID resourceID)
        {
            return _localizer.GetString(resourceID);
        }
    }
}