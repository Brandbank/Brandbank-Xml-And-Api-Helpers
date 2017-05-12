using Brandbank.Xml.Models.Message;

namespace Brandbank.Xml.MessageHelpers
{
    public static class ImageTypeReaderExtensions
    {
        public static string GetFileName(this ImageType imageType)
        {
            return imageType.Specification.Filename;
        }

        public static string GetUrl(this ImageType imageType)
        {
            return imageType.Url.Value;
        }

        public static int GetShopTypeId(this ImageType imageType)
        {
            return imageType.ShotTypeId;
        }
    }
}
