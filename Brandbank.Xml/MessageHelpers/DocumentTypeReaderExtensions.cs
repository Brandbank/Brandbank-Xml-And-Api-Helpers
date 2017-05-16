using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;

namespace Brandbank.Xml.MessageHelpers
{
    public static class DocumentTypeReaderExtensions
    {
        public static string GetDocumentUrl(this DocumentType documentType)
        {
            return documentType.NewIfNull().Url.Value;
        }
    }
}
