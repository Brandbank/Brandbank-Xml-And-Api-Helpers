using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Validation.Helpers;
using Brandbank.Xml.Validation.Models;
using System.Collections.Generic;

namespace Brandbank.Xml.Validation
{
    public class XmlValidator : IXmlValidator
    {
        /// <summary>
        /// Validates Brandbank MessageType for invalid Ids.
        /// </summary>
        /// <param name="messageType">Class representation of XML to validate</param>
        /// <param name="productValidationData">Representation of Brandbank's data model</param>
        /// <returns>Descriptive error messages relating to invalid Ids</returns>
        public IEnumerable<string> Validate(MessageType messageType, ProductValidationData productValidationData)
        {
            return messageType.Validate(productValidationData);
        }
    }
}
