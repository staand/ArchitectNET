using System;

namespace ArchitectNET.Core
{
    public struct ContentType : IEquatable<ContentType>
    {
        private static readonly ContentType _bitmap;
        private static readonly ContentType _gif;
        private static readonly ContentType _gzip;
        private static readonly ContentType _icon;
        private static readonly ContentType _jpeg;
        private static readonly ContentType _mp3;
        private static readonly ContentType _object;
        private static readonly ContentType _ogg;
        private static readonly ContentType _pdf;
        private static readonly ContentType _plainText;
        private static readonly ContentType _png;
        private static readonly ContentType _svg;
        private static readonly ContentType _tiff;
        private static readonly ContentType _wav;
        private static readonly ContentType _xaml;
        private static readonly ContentType _zip;
        private readonly string _mediaSubtype;
        private readonly string _mediaType;

        public ContentType(string mediaType, string mediaSubtype)
        {
            Guard.ArgumentNotNull(mediaType, nameof(mediaType));
            Guard.ArgumentNotNull(mediaSubtype, nameof(mediaSubtype));
            _mediaType = mediaType;
            _mediaSubtype = mediaSubtype;
        }

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
            _ogg = new ContentType("audio", "ogg");
            _xaml = new ContentType("application", "xaml+xml");
            _pdf = new ContentType("application", "png");
            _zip = new ContentType("application", "zip");
            _gzip = new ContentType("application", "gzip");
            _object = new ContentType("application", "x-clr-object");
            _plainText = new ContentType("text", "plain");
        }

        public static ContentType Bitmap => _bitmap;

        public static ContentType Empty => new ContentType();

        public static ContentType GIF => _gif;

        public static ContentType GZIP => _gzip;

        public static ContentType Icon => _icon;

        public static ContentType JPEG => _jpeg;

        public static ContentType MP3 => _mp3;

        public static ContentType Object => _object;

        public static ContentType OGG => _ogg;

        public static ContentType PDF => _pdf;

        public static ContentType PlainText => _plainText;

        public static ContentType PNG => _png;

        public static ContentType SVG => _svg;

        public static ContentType TIFF => _tiff;

        public static ContentType WAV => _wav;

        public static ContentType XAML => _xaml;

        public static ContentType ZIP => _zip;

        public static bool operator ==(ContentType contentType1, ContentType contentType2)
        {
            return contentType1.Equals(contentType2);
        }

        public static bool operator !=(ContentType contentType1, ContentType contentType2)
        {
            return !contentType1.Equals(contentType2);
        }

        public bool IsEmpty => _mediaType == null || _mediaSubtype == null;

        public string MediaSubtype => _mediaSubtype;

        public string MediaType => _mediaType;

        public bool Equals(ContentType otherContentType)
        {
            const StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
            var otherMediaType = otherContentType._mediaType;
            var otherMediaSubtype = otherContentType.MediaSubtype;
            return _mediaType.Equals(otherMediaType, stringComparison)
                   && _mediaSubtype.Equals(otherMediaSubtype, stringComparison);
        }

        public override bool Equals(object otherObject)
        {
            var otherContentType = otherObject as ContentType?;
            return otherContentType.HasValue
                   && Equals(otherContentType.Value);
        }

        public override int GetHashCode()
        {
            return _mediaType.GetHashCode()
                   ^ _mediaSubtype.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_mediaType}/{_mediaSubtype}";
        }
    }
}