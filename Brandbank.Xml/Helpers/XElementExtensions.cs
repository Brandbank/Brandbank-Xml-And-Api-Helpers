using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Brandbank.Xml.Helpers
{
    public static class XElementExtensions
    {
        public static XElement ValidateXml(this XElement xElement, string schemaPath)
        {
            return xElement;
        }

        public static T ConvertTo<T>(this XElement xElement) where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return xmlSerializer.Deserialize(xElement.CreateReader()) as T;
        }

        public static XElement ConvertToXml<T>(this T value) where T : class, new()
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            settings.Encoding = Encoding.UTF8;

            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, value);
                stream.Position = 0;

                using (XmlReader reader = XmlReader.Create(stream))
                {
                    return XElement.Load(reader);
                }
            }
        }
    }
}
