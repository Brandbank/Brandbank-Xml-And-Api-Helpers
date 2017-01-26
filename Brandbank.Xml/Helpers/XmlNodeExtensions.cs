using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Brandbank.Xml.Helpers
{
    public static class XmlNodeExtensions
    {
        public static XmlNode ValidateXml(this XmlNode xmlNode, string schemaPath, string nameSpace, Func<ValidationEventHandler> validationEventHandler)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            using (var sr = new StreamReader(schemaPath))
                schemas.Add(nameSpace, XmlReader.Create(sr));

            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = schemas,
                ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings,
            };
            settings.ValidationEventHandler += validationEventHandler();

            using (var xmlStream = new StringReader(xmlNode.OuterXml))
            using (var validator = XmlReader.Create(xmlStream, settings))
            {
                while(validator.Read());
            }
            return xmlNode;
        }

        public static T ConvertTo<T>(this XmlNode xmlNode) where T : class, new()
        {
            return xmlNode.OuterXml.ConvertXmlStringTo<T>();
        }

        public static XmlNode ConvertToXml<T>(this T obj) where T : class, new()
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            settings.Encoding = Encoding.UTF8;

            var xmlSerializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                xmlSerializer.Serialize(xmlWriter, obj);

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(stringWriter.ToString());
            return xmlDocument.DocumentElement;
        }
    }
}
