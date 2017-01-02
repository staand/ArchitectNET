namespace ArchitectNET.Core.Localization.Support
{
    public class ResourceType : IResourceType
    {
        private static readonly ResourceType _bitmap;
        private static readonly ResourceType _gif;
        private static readonly ResourceType _gzip;
        private static readonly ResourceType _icon;
        private static readonly ResourceType _jpeg;
        private static readonly ResourceType _mp3;
        private static readonly ResourceType _object;
        private static readonly ResourceType _ogg;
        private static readonly ResourceType _pdf;
        private static readonly ResourceType _plainText;
        private static readonly ResourceType _png;
        private static readonly ResourceType _svg;
        private static readonly ResourceType _tiff;
        private static readonly ResourceType _wav;
        private static readonly ResourceType _xaml;
        private static readonly ResourceType _zip;
        private readonly ContentType _contentType;

        static ResourceType()
        {
            _bitmap = new ResourceType(ContentType.Bitmap);
            _gif = new ResourceType(ContentType.GIF);
            _gzip = new ResourceType(ContentType.GZIP);
            _icon = new ResourceType(ContentType.Icon);
            _jpeg = new ResourceType(ContentType.JPEG);
            _mp3 = new ResourceType(ContentType.MP3);
            _object = new ResourceType(ContentType.Object);
            _ogg = new ResourceType(ContentType.OGG);
            _pdf = new ResourceType(ContentType.PDF);
            _plainText = new ResourceType(ContentType.PlainText);
            _png = new ResourceType(ContentType.PNG);
            _svg = new ResourceType(ContentType.SVG);
            _tiff = new ResourceType(ContentType.TIFF);
            _wav = new ResourceType(ContentType.WAV);
            _xaml = new ResourceType(ContentType.XAML);
            _zip = new ResourceType(ContentType.ZIP);
        }

        public ResourceType(ContentType contentType)
        {
            _contentType = contentType;
        }

        public static ResourceType Bitmap
        {
            get { return _bitmap; }
        }

        public static ResourceType GIF
        {
            get { return _gif; }
        }

        public static ResourceType GZIP
        {
            get { return _gzip; }
        }

        public static ResourceType Icon
        {
            get { return _icon; }
        }

        public static ResourceType JPEG
        {
            get { return _jpeg; }
        }

        public static ResourceType MP3
        {
            get { return _mp3; }
        }

        public static ResourceType Object
        {
            get { return _object; }
        }

        public static ResourceType OGG
        {
            get { return _ogg; }
        }

        public static ResourceType PDF
        {
            get { return _pdf; }
        }

        public static ResourceType PlainText
        {
            get { return _plainText; }
        }

        public static ResourceType PNG
        {
            get { return _png; }
        }

        public static ResourceType SVG
        {
            get { return _svg; }
        }

        public static ResourceType TIFF
        {
            get { return _tiff; }
        }

        public static ResourceType WAV
        {
            get { return _wav; }
        }

        public static ResourceType XAML
        {
            get { return _xaml; }
        }

        public static ResourceType ZIP
        {
            get { return _zip; }
        }

        public ContentType ContentType
        {
            get { return _contentType; }
        }

        public bool Equals(IResourceType otherType)
        {
            if (ReferenceEquals(this, otherType))
                return true;
            return otherType != null
                   && otherType.ContentType == ContentType;
        }

        public override bool Equals(object otherObject)
        {
            if (ReferenceEquals(this, otherObject))
                return true;
            var otherType = otherObject as IResourceType;
            return otherType != null
                   && Equals(otherType);
        }

        public override int GetHashCode()
        {
            return _contentType.GetHashCode();
        }

        public override string ToString()
        {
            return _contentType.ToString();
        }
    }
}