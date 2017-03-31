using Brandbank.Xml.Models.Message;
using Brandbank.Xml.Validation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brandbank.Xml.Validation.Helpers
{
    public static class BrandbankXMLExtensions
    {
        public static IEnumerable<string> Validate(this MessageType messageType, Func<ProductValidationData> GetProductValidationData)
        {
            return messageType.GetAllInvalidDataInMessage(GetProductValidationData());
        }

        public static async Task<IEnumerable<string>> ValidateAsync(this MessageType messageType, Func<Task<ProductValidationData>> GetProductValidationDataAsync)
        {
            return messageType.GetAllInvalidDataInMessage(await GetProductValidationDataAsync());
        }
    }
}
