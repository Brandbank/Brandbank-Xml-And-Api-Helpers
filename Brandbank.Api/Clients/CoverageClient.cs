using Brandbank.Api.ReportData;
using System;
using System.ServiceModel;

namespace Brandbank.Api.Clients
{
    public class CoverageClient : ICoverageClient
    {
        private readonly DataReportSoapClient _dataReportSoapClient;
        private readonly ExternalCallerHeader _header;

        public CoverageClient(Guid guid, DataReportSoapClient client)
        {
            _dataReportSoapClient = client;
            _header = new ExternalCallerHeader
            {
                ExternalCallerId = guid
            };
        }

        public int UploadCompressedCoverage(byte[] compressedCoverage)
        {
            try
            {
                return _dataReportSoapClient.SupplyCompressedCoverageReport(_header, compressedCoverage);
            }
            catch (CommunicationException e)
            {
                _dataReportSoapClient.Abort();
                throw new CommunicationException("CommunicationException", e);
            }
            catch (TimeoutException e)
            {
                _dataReportSoapClient.Abort();
                throw new TimeoutException("TimeoutException", e);
            }
            catch (Exception e)
            {
                _dataReportSoapClient.Abort();
                throw new Exception("Exception", e);
            }
        }

        public void Dispose()
        {
            _dataReportSoapClient.Close();
        }
    }
}
