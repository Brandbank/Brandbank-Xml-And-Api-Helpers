using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Validation.Models;
using System.Collections.Generic;

namespace Brandbank.Xml.Validation.Helpers
{
    public static class BrandbankXMLExtensions
    {
        /// <summary>
        /// Validates Brandbank XML for invalid Ids.
        /// </summary>
        /// <param name="messageType">Class representation of XML to validate</param>
        /// <param name="productValidationData">Representation of Brandbank's data model</param>
        /// <returns>Descriptive error messages relating to invalid Ids</returns>
        public static IEnumerable<string> Validate(this MessageType messageType, ProductValidationData productValidationData)
        {
            return messageType.GetAllInvalidDataInMessage(productValidationData);
        }
    }
}
