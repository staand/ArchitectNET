using System;
using System.Globalization;

namespace ArchitectNET.Core.Localization
{
    public interface ILocale : IEquatable<ILocale>
    {
        CultureInfo Culture { get; }
    }
}