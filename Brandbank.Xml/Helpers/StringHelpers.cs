using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Brandbank.Xml.Helpers
{
    public static class StringHelpers
    {
        public static XmlNode ToXml(this string xmlContent)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlContent);
            return doc.DocumentElement;
        }
    }
}
