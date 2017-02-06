using System.Globalization;

namespace ArchitectNET.Core.Localization.Support
{
    public class Locale : ILocale
    {
        private static readonly Locale _invariant;
        private readonly CultureInfo _culture;

        public Locale(CultureInfo culture)
        {
            Guard.ArgumentNotNull(culture, nameof(culture));
            _culture = culture;
        }

        static Locale()
        {
            _invariant = new Locale(CultureInfo.InvariantCulture);
        }

        public static Locale Invariant => _invariant;

        public bool Equals(ILocale otherLocale)
        {
            if (ReferenceEquals(this, otherLocale))
                return true;
            return otherLocale != null
                   && otherLocale.Culture.Equals(_culture);
        }

        public CultureInfo Culture => _culture;

        public override bool Equals(object otherObject)
        {
            if (ReferenceEquals(this, otherObject))
                return true;
            var otherLocale = otherObject as ILocale;
            return otherLocale != null
                   && Equals(otherLocale);
        }

        public override int GetHashCode()
        {
            return _culture.GetHashCode();
        }

        public override string ToString()
        {
            return _culture.ToString();
        }
    }
}