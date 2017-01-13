using Brandbank.Xml.Helpers;
using Brandbank.Xml.Models.Message;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Brandbank.Api
{
    public class BrandbankApi
    {
        public static void UploadCompressedCoverage(
            string coverageDirectory,
            string xsdPath,
            Func<ValidationEventHandler> validationEventHandler,
            Func<Xml.Models.Coverage.ReportType> coverageGetter,
            Func<byte[], int> coverageUploader)
        {
            uploadCompressedData(coverageDirectory, xsdPath, validationEventHandler, coverageGetter, coverageUploader);
        }

        public static void UploadCompressedFeedback(
            string feedbackDirectory,
            string xsdPath,
            Func<ValidationEventHandler> validationEventHandler,
            Func<Xml.Models.Feedback.ReportType> feedbackGetter,
            Func<byte[], int> feedbackUploader,
            Action feedbackCallback)
        {
            uploadCompressedData(feedbackDirectory, xsdPath, validationEventHandler, feedbackGetter, feedbackUploader);
            feedbackCallback();
        }

        public static void GetUnsent(
            string xsdPath,
            Func<ValidationEventHandler> validationEventHandler,
            Func<XmlNode> messageDownloader,
            Func<MessageType, IBrandbankMessageSummary> productProcessor,
            Func<IBrandbankMessageSummary, IBrandbankMessageSummary> feedbackProcessor,
            Func<IBrandbankMessageSummary, IBrandbankMessageSummary> messageAcknowledger
            )
        {
            while (
                messageDownloader()
                    .ValidateXml(xsdPath, validationEventHandler)
                    .ConvertTo<MessageType>()
                    .Then(productProcessor)
                    .Then(feedbackProcessor)
                    .Then(messageAcknowledger)
                    .MessageHadProducts
                    );
        }

        private static void uploadCompressedData<T>(
            string directory,
            string xsdPath,
            Func<ValidationEventHandler> validationEventHandler,
            Func<T> productGetter,
            Func<byte[], int> uploader)
            where T : class, new()
        {
            var newDirectory = Path.Combine(directory, DateTime.Now.ToString("yyyyMMddHHmmss"));
            productGetter()
                .ConvertToXml()
                .ValidateXml(xsdPath, validationEventHandler)
                .CreateDirectory(newDirectory)
                .SaveToDirectory(newDirectory, "BrandbankData.xml")
                .CompressFolder()
                .Then(uploader);
        }
    }
}
