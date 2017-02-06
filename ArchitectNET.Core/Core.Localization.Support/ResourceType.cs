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

        public ResourceType(ContentType contentType)
        {
            _contentType = contentType;
        }

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

        public static ResourceType Bitmap => _bitmap;

        public static ResourceType GIF => _gif;

        public static ResourceType GZIP => _gzip;

        public static ResourceType Icon => _icon;

        public static ResourceType JPEG => _jpeg;

        public static ResourceType MP3 => _mp3;

        public static ResourceType Object => _object;

        public static ResourceType OGG => _ogg;

        public static ResourceType PDF => _pdf;

        public static ResourceType PlainText => _plainText;

        public static ResourceType PNG => _png;

        public static ResourceType SVG => _svg;

        public static ResourceType TIFF => _tiff;

        public static ResourceType WAV => _wav;

        public static ResourceType XAML => _xaml;

        public static ResourceType ZIP => _zip;

        public bool Equals(IResourceType otherType)
        {
            if (ReferenceEquals(this, otherType))
                return true;
            return otherType != null
                   && otherType.ContentType == ContentType;
        }

        public ContentType ContentType => _contentType;

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