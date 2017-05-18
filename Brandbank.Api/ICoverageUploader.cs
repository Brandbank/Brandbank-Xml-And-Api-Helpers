using Brandbank.Api.Clients;
using Brandbank.Xml.Models.Coverage;

namespace Brandbank.Api
{
    public interface ICoverageUploader
    {
        int UploadCoverage(ReportType coverage);
    }
}
