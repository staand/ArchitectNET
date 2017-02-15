using System;

namespace ArchitectNET.Core
{
    /// <summary>
    /// Lightweight representation of content type according to ultimately simplified MIME specification
    /// </summary>
    public struct ContentType : IEquatable<ContentType>
    {
        /// <summary>
        /// Backing field for <see cref="Bitmap" /> static property
        /// </summary>
        private static readonly ContentType _bitmap;

        /// <summary>
        /// Backing field for <see cref="GIF" /> static property
        /// </summary>
        private static readonly ContentType _gif;

        /// <summary>
        /// Backing field for <see cref="GZIP" /> static property
        /// </summary>
        private static readonly ContentType _gzip;

        /// <summary>
        /// Backing field for <see cref="Icon" /> static property
        /// </summary>
        private static readonly ContentType _icon;

        /// <summary>
        /// Backing field for <see cref="JPEG" /> static property
        /// </summary>
        private static readonly ContentType _jpeg;

        /// <summary>
        /// Backing field for <see cref="MP3" /> static property
        /// </summary>
        private static readonly ContentType _mp3;

        /// <summary>
        /// Backing field for <see cref="Object" /> static property
        /// </summary>
        private static readonly ContentType _object;

        /// <summary>
        /// Backing field for <see cref="OGG" /> static property
        /// </summary>
        private static readonly ContentType _ogg;

        /// <summary>
        /// Backing field for <see cref="PDF" /> static property
        /// </summary>
        private static readonly ContentType _pdf;

        /// <summary>
        /// Backing field for <see cref="PlainText" /> static property
        /// </summary>
        private static readonly ContentType _plainText;

        /// <summary>
        /// Backing field for <see cref="PNG" /> static property
        /// </summary>
        private static readonly ContentType _png;

        /// <summary>
        /// Backing field for <see cref="SVG" /> static property
        /// </summary>
        private static readonly ContentType _svg;

        /// <summary>
        /// Backing field for <see cref="TIFF" /> static property
        /// </summary>
        private static readonly ContentType _tiff;

        /// <summary>
        /// Backing field for <see cref="WAV" /> static property
        /// </summary>
        private static readonly ContentType _wav;

        /// <summary>
        /// Backing field for <see cref="XAML" /> static property
        /// </summary>
        private static readonly ContentType _xaml;

        /// <summary>
        /// Backing field for <see cref="ZIP" /> static property
        /// </summary>
        private static readonly ContentType _zip;

        /// <summary>
        /// Backing field for <see cref="MediaSubtype" /> property
        /// </summary>
        private readonly string _mediaSubtype;

        /// <summary>
        /// Backing field for <see cref="MediaType" /> property
        /// </summary>
        private readonly string _mediaType;

        /// <summary>
        /// Initializes all of static backing fields for some predefined content types
        /// (e.g. JPEG, PDF etc.)
        /// </summary>
        static ContentType()
        {
            _png = new ContentType("image", "png");
            _svg = new ContentType("image", "svg+xml");
            _icon = new ContentType("image", "x-icon");
            _gif = new ContentType("image", "gif");
            _jpeg = new ContentType("image", "jpeg");
            _tiff = new ContentType("image", "tiff");
            _bitmap = new ContentType("image", "bmp");
            _wav = new ContentType("audio", "x-wav");
            _mp3 = new ContentType("audio", "mp3");
            _ogg = new ContentType("application", "ogg");
            _xaml = new ContentType("application", "xaml+xml");
            _pdf = new ContentType("application", "pdf");
            _zip = new ContentType("application", "zip");
            _gzip = new ContentType("application", "gzip");
            _object = new ContentType("application", "x-clr-object");
            _plainText = new ContentType("text", "plain");
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ContentType" />
        /// </summary>
        /// <param name="mediaType"> Media type name </param>
        /// <param name="mediaSubtype"> Media subtype name </param>
        public ContentType(string mediaType, string mediaSubtype)
        {
            Guard.ArgumentNotNull(mediaType, nameof(mediaType));
            Guard.ArgumentNotNull(mediaSubtype, nameof(mediaSubtype));
            _mediaType = mediaType;
            _mediaSubtype = mediaSubtype;
        }

        /// <summary>
        /// Predefined content type for Windows Bitmap (*.BMP) image file ("image/bmp")
        /// </summary>
        public static ContentType Bitmap => _bitmap;

        /// <summary>
        /// Special value meaning "unknown content type"
        /// </summary>
        public static ContentType Empty => new ContentType();

        /// <summary>
        /// Predefined content type for Graphics Interchange Format (*.GIF) image file ("image/png")
        /// </summary>
        public static ContentType GIF => _gif;

        /// <summary>
        /// Predefined content type for GNU ZIP (*.GZIP) archive file ("application/gzip")
        /// </summary>
        public static ContentType GZIP => _gzip;

        /// <summary>
        /// Predefined content type for Windows Icon (*.ICO) file ("image/x-icon")
        /// </summary>
        public static ContentType Icon => _icon;

        /// <summary>
        /// Predefined content type for JFIF (*.JPEG, *.JPG) image file ("image/jpeg")
        /// </summary>
        public static ContentType JPEG => _jpeg;

        /// <summary>
        /// Predefined content type for MP3 (*.MP3) audio file ("audio/mp3")
        /// </summary>
        public static ContentType MP3 => _mp3;

        /// <summary>
        /// Special non-standard content type for plain CLR object of any type ("application/x-clr-object")
        /// </summary>
        public static ContentType Object => _object;

        /// <summary>
        /// Predefined content type for OGG (*.OGG, *.OGV, *.OGA, *.OGX, *.SPX, *.OPUS, *.OGM) audio file ("application/ogg")
        /// </summary>
        public static ContentType OGG => _ogg;

        /// <summary>
        /// Predefined content type for Portable Document Format (*.PDF) file
        /// ("application/pdf")
        /// </summary>
        public static ContentType PDF => _pdf;

        /// <summary>
        /// Predefined content type for plain text content ("text/plain")
        /// </summary>
        public static ContentType PlainText => _plainText;

        /// <summary>
        /// Predefined content type for Portable Network Graphics (*.PNG) image file
        /// ("image/png")
        /// </summary>
        public static ContentType PNG => _png;

        /// <summary>
        /// Predefined content type for Scalable Vector Graphics (*.SVG) file
        /// ("image/svg+xml")
        /// </summary>
        public static ContentType SVG => _svg;

        /// <summary>
        /// Predefined content type for Tag Image File Format (*.TIFF) image file ("image/tiff")
        /// </summary>
        public static ContentType TIFF => _tiff;

        /// <summary>
        /// Predefined content type for WAVE (*.WAV) audio file ("audio/x-wav")
        /// </summary>
        public static ContentType WAV => _wav;

        /// <summary>
        /// Predefined content type for Extensible Application Markup Language (*.XAML) file ("application/xaml+xml")
        /// </summary>
        public static ContentType XAML => _xaml;

        /// <summary>
        /// Predefined content type for ZIP (*.ZIP) archive file ("application/zip")
        /// </summary>
        public static ContentType ZIP => _zip;

        /// <summary>
        /// Determines whether two specified content types have the same value. Just calls and returns result of the
        /// <see cref="Equals(ContentType)" /> method
        /// </summary>
        /// <param name="contentType1"> The first comparing <see cref="ContentType" /> </param>
        /// <param name="contentType2"> The second comparing <see cref="ContentType" /> </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="contentType1" /> is equal to <paramref name="contentType2" />, otherwise
        /// <see langword="false" />
        /// </returns>
        public static bool operator ==(ContentType contentType1, ContentType contentType2)
        {
            return contentType1.Equals(contentType2);
        }

        /// <summary>
        /// Determines whether two specified content types have different values. Just calls and returns an inverted result of the
        /// <see cref="Equals(ContentType)" /> method
        /// </summary>
        /// <param name="contentType1"> The first comparing <see cref="ContentType" /> </param>
        /// <param name="contentType2"> The second comparing <see cref="ContentType" /> </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="contentType1" /> is NOT equal to <paramref name="contentType2" /> otherwise
        /// <see langword="false" />
        /// </returns>
        public static bool operator !=(ContentType contentType1, ContentType contentType2)
        {
            return !contentType1.Equals(contentType2);
        }

        /// <summary>
        /// Returns <see langword="true" /> if this instance reperesents an unknown content type, otherwise
        /// <see langword="false" />
        /// </summary>
        public bool IsEmpty => _mediaType == null || _mediaSubtype == null;

        /// <summary>
        /// Returns media subtype name
        /// </summary>
        public string MediaSubtype => _mediaSubtype;

        /// <summary>
        /// Returns media type name
        /// </summary>
        public string MediaType => _mediaType;

        /// <summary>
        /// Determines whether this instance and <paramref name="otherContentType" /> have the same value
        /// </summary>
        /// <param name="otherContentType"> The <see cref="ContentType" /> instance to compare to this instance </param>
        /// <returns> <see langword="true" /> if <paramref name="otherContentType" /> is equal to this instance </returns>
        /// <remarks>
        /// Current implementation performas CASE-INSENSITIVE comparison for <see cref="MediaType" /> and
        /// <see cref="MediaSubtype" /> properties
        /// </remarks>
        public bool Equals(ContentType otherContentType)
        {
            var isEmpty = IsEmpty;
            var isOtherEmpty = otherContentType.IsEmpty;
            if (isEmpty != isOtherEmpty)
                return false;
            if (isEmpty)
                return true;
            const StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
            var otherMediaType = otherContentType._mediaType;
            var otherMediaSubtype = otherContentType.MediaSubtype;
            return _mediaType.Equals(otherMediaType, stringComparison)
                   && _mediaSubtype.Equals(otherMediaSubtype, stringComparison);
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="ContentType" /> object, have
        /// the same value
        /// </summary>
        /// <param name="otherObject"> The object to compare to this instance </param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="otherObject" /> is a <see cref="ContentType" /> and its value is
        /// the same as this instance, otherwise <see langword="false" />
        /// </returns>
        public override bool Equals(object otherObject)
        {
            var otherContentType = otherObject as ContentType?;
            return otherContentType.HasValue
                   && Equals(otherContentType.Value);
        }

        /// <summary>
        /// Returns a hash code of this <see cref="ContentType" />
        /// </summary>
        /// <returns> A 32-bit signed integer hash code </returns>
        public override int GetHashCode()
        {
            var mediaType = _mediaType;
            var mediaSubtype = _mediaSubtype;
            if (mediaType == null || mediaSubtype == null)
                return 0;
            return mediaType.GetHashCode()
                   ^ mediaSubtype.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of this <see cref="ContentType" />
        /// </summary>
        /// <returns>
        /// <see cref="string" /> object representing this instance. Returned <see cref="string" /> is always a valid
        /// MIME-specifier
        /// </returns>
        public override string ToString()
        {
            return $"{_mediaType}/{_mediaSubtype}";
        }
    }
}