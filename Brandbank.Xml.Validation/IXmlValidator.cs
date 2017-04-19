using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Validation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation
{
    public interface IXmlValidator
    {
        IEnumerable<string> Validate(MessageType messageType, ProductValidationData productValidationData);
    }
}
