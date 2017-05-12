using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Brandbank.Xml.Helpers
{
    public static class StringExtensions
    {
        public static XmlNode ToXml(this string xmlContent)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            return doc.DocumentElement;
        }

        public static T ConvertXmlStringTo<T>(this string data) where T : class
        {
            using (var stringReader = new StringReader(data))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }

        public static float ConvertToFloat(this string item)
        {
            float retVal;
            if (float.TryParse(item, out retVal))
                return retVal;
            return retVal;
        }

        public static string NewIfNull(this string text)
        {
            return text ?? string.Empty;
        }

        public static string GetEan(this string value)
        {
            var split = value.Split('\t');
            return split[0].Substring(5).Trim();
        }

        public static string RemoveTabs(this string value)
        {
            return Regex.Replace(value, @"\r\n?|\n|\t", " ");
        }
    }
}
