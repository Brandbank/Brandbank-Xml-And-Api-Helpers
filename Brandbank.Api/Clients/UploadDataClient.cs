using Brandbank.Api.ExtractData;
using Brandbank.Api.UploadData;
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Brandbank.Api.Clients
{
    public class UploadDataClient : IUploadDataClient
    {
        private readonly UploadClient _uploadClient;
        private readonly UserCredentialHeader _header;
        private readonly int _wait;

        public UploadDataClient(Guid guid, int wait = 20)
        {
            _uploadClient = new UploadClient(
                BrandbankHttpsBinding("BasicHttpBinding_IUpload"),
                BrandbankEndpointAddress(guid, "https://api.brandbank.com/svc/DataImport/Upload.svc"));

            _header = new UserCredentialHeader
            {
                UserCredential = guid
            };
            _wait = wait;
        }

        public UploadResponse UploadMessage(byte[] message)
        {
            using (var data = new MemoryStream(message))
               return _uploadClient.UploadZip(_header, data);
        } 

        public UploadResponse GetResponse(UploadResponse uploadResponse)
        {
            while (uploadResponse.Status == UploadResponse.UploadStatuses.Pending)
            {
                if (uploadResponse.Status == UploadResponse.UploadStatuses.Pending)
                    Thread.Sleep(TimeSpan.FromSeconds(_wait));
                uploadResponse = _uploadClient.GetUploadResponse(_header, uploadResponse.ReceiptId);
            }
            return uploadResponse;
        }

        public void Dispose()
        {
            _uploadClient.Close();
        }

        private static BasicHttpsBinding BrandbankHttpsBinding(string name)
        {
            return new BasicHttpsBinding(BasicHttpsSecurityMode.Transport)
            {
                Name = name,
                //MaxReceivedMessageSize = int.MaxValue,
                //MaxBufferPoolSize = int.MaxValue,
                //ReaderQuotas = new XmlDictionaryReaderQuotas
                //{
                //    MaxArrayLength = int.MaxValue,
                //    MaxStringContentLength = int.MaxValue
                //},
                Security = new BasicHttpsSecurity
                {
                    Mode = BasicHttpsSecurityMode.Transport
                }
            };
        }

        private static EndpointAddress BrandbankEndpointAddress(Guid authenticationGuid, string endpointAddress, string ns = "http://www.i-label.net/Partners/WebServices/DataFeed/2005/11")
        {
            var endpointAddressBuilder = new EndpointAddressBuilder
            {
                Uri = new Uri(endpointAddress),
            };
            endpointAddressBuilder.Headers.Add(
                AddressHeader.CreateAddressHeader(
                    "UserCredentialHeader",
                    ns,
                    new ExternalCallerHeader { ExternalCallerId = authenticationGuid }
                    ));
            return endpointAddressBuilder.ToEndpointAddress();
        }
    }
}
