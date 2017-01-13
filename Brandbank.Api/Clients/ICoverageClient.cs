using System;

namespace Brandbank.Api.Clients
{
    public interface ICoverageClient : IDisposable
    {
        int UploadCompressedCoverage(byte[] compressedCoverage);
    }
}
