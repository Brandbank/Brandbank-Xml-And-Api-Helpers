using Brandbank.Xml.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Brandbank.Xml.Validation.Tests
{
    public static class TestExtensions
    {
        public static MessageType ConvertToModel(this string xmlString)
        {
            var serializer = new XmlSerializer(typeof(MessageType));
            var sr = new System.IO.StringReader(xmlString);
            return (MessageType)serializer.Deserialize(new System.Xml.XmlTextReader(sr));
        }
    }
}
