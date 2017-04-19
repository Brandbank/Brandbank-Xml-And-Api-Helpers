using Newtonsoft.Json;
using System.IO;

namespace Brandbank.Xml.Helpers
{
    public static class StreamExtensions
    {
        public static T ConvertJsonStringTo<T>(this Stream stream) where T : class
        {
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }
    }
}
