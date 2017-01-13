using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Brandbank.Xml.ImageWriter
{
    public interface IImageWriter
    {
        void SaveToDisk(Stream imageStream, string path);
    }
}
