using System;

namespace Brandbank.Api.Clients
{
    public interface IFeedbackClient : IDisposable
    {
        int UploadCompressedFeedback(byte[] compressedFeedback);
    }
}