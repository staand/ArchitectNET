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
        private readonly string _mediaSubType;
        private readonly string _mediaType;

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

        public ContentType(string mediaType, string mediaSubType)
        {
            Guard.ArgumentNotNull(mediaType, "mediaType");
            Guard.ArgumentNotNull(mediaSubType, "mediaSubType");
            _mediaType = mediaType;
            _mediaSubType = mediaSubType;
        }

        public static ContentType Bitmap
        {
            get { return _bitmap; }
        }

        public static ContentType Empty
        {
            get { return new ContentType(); }
        }

        public static ContentType GIF
        {
            get { return _gif; }
        }

        public static ContentType GZIP
        {
            get { return _gzip; }
        }

        public static ContentType Icon
        {
            get { return _icon; }
        }

        public static ContentType JPEG
        {
            get { return _jpeg; }
        }

        public static ContentType MP3
        {
            get { return _mp3; }
        }

        public static ContentType Object
        {
            get { return _object; }
        }

        public static ContentType OGG
        {
            get { return _ogg; }
        }

        public static ContentType PDF
        {
            get { return _pdf; }
        }

        public static ContentType PlainText
        {
            get { return _plainText; }
        }

        public static ContentType PNG
        {
            get { return _png; }
        }

        public static ContentType SVG
        {
            get { return _svg; }
        }

        public static ContentType TIFF
        {
            get { return _tiff; }
        }

        public static ContentType WAV
        {
            get { return _wav; }
        }

        public static ContentType XAML
        {
            get { return _xaml; }
        }

        public static ContentType ZIP
        {
            get { return _zip; }
        }

        public bool IsEmpty
        {
            get { return _mediaType == null || _mediaSubType == null; }
        }

        public string MediaSubType
        {
            get { return _mediaSubType; }
        }

        public string MediaType
        {
            get { return _mediaType; }
        }

        public bool Equals(ContentType otherContentType)
        {
            return _mediaType.Equals(otherContentType._mediaType, StringComparison.OrdinalIgnoreCase)
                   && _mediaSubType.Equals(otherContentType._mediaSubType, StringComparison.OrdinalIgnoreCase);
        }

        public static bool operator ==(ContentType contentType1, ContentType contentType2)
        {
            return contentType1.Equals(contentType2);
        }

        public static bool operator !=(ContentType contentType1, ContentType contentType2)
        {
            return !contentType1.Equals(contentType2);
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
                   ^ _mediaSubType.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", _mediaType, _mediaSubType);
        }
    }
}