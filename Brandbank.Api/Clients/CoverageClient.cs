using Brandbank.Api.ReportData;
using System;

namespace Brandbank.Api.Clients
{
    public class CoverageClient : ICoverageClient
    {
        private readonly DataReportSoapClient _dataReportSoapClient;
        private readonly ExternalCallerHeader _header;

        public CoverageClient(Guid guid)
        {
            if (guid == null)
                throw new NullReferenceException("Guid cannot be null");

            _dataReportSoapClient = new DataReportSoapClient();
            _header = new ExternalCallerHeader
            {
                ExternalCallerId = guid
            };
        }

        public CoverageClient(Guid guid, DataReportSoapClient client)
        {
            if (guid == null)
                throw new NullReferenceException("Guid cannot be null");
            if (client == null) throw new ArgumentNullException("client");

            _dataReportSoapClient = client;
            _header = new ExternalCallerHeader
            {
                ExternalCallerId = guid
            };
        }

        public int UploadCompressedCoverage(byte[] compressedCoverage)
        {
            return _dataReportSoapClient.SupplyCompressedCoverageReport(_header, compressedCoverage);
        }

        public void Dispose()
        {
            _dataReportSoapClient.Close();
        }
    }
}
