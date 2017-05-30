using Brandbank.Api.ReportData;
using System;

namespace Brandbank.Api.Clients
{
    public sealed class FeedbackClient : IFeedbackClient
    {
        private readonly DataReportSoapClient _dataReportSoapClient;
        private readonly ExternalCallerHeader _header;

        public FeedbackClient(Guid guid, string endpointAddress)
        {
            _dataReportSoapClient = new DataReportSoapClient();
            _header = new ExternalCallerHeader
            {
                ExternalCallerId = guid
            };
        }

        public int UploadCompressedFeedback(byte[] compressedFeedback)
        { 
            return _dataReportSoapClient.SupplyCompressedExtractionFeedback(_header, compressedFeedback);
        }

        public void Dispose()
        {
            _dataReportSoapClient.Close();
        }
    }
}
