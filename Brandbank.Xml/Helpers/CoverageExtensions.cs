using System;

namespace Brandbank.Xml.Helpers
{
    public static class CoverageExtensions
    {
        public static int UploadCoverageToBrandbank<T>(
            this T toUpload,
            Func<T, int> fn) =>
            fn(toUpload);
    }
}
