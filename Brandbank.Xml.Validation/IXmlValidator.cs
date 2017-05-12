using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Validation.Models;
using System.Collections.Generic;

namespace Brandbank.Xml.Validation
{
    public interface IXmlValidator
    {
        IEnumerable<string> Validate(MessageType messageType, ProductValidationData productValidationData);
    }
}
