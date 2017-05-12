using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Brandbank.Xml.Helpers
{
    public static class DirectoryExtensions
    {
        public static string DeleteOldDirectories(this string path, int daysToKeep = 30)
        {
            foreach (var directory in Directory.GetDirectories(path))
                if (Directory.GetCreationTime(directory) <= DateTime.Now.AddDays(-daysToKeep))
                    Directory.Delete(directory, true);
            return path;
        }

        public static string CreateDirectory(this string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string SaveToDirectory(this XmlNode xmlNode, string path, string name)
        {
            var fullPath = Path.Combine(path, name);
            File.WriteAllText(fullPath, xmlNode.OuterXml);
            return path;
        }

        public static MemoryStream ConvertXmlToStream(this XmlNode xmlNode)
        {
            return xmlNode.OuterXml.ConvertToStream();
        }

        public static MemoryStream ConvertToStream(this string value)
        {
            return new MemoryStream(Encoding.Default.GetBytes(value));
        }

        public static byte[] CompressMemoryStream(this MemoryStream memStreamIn, string zipEntryName)
        {
            var outputMemStream = new MemoryStream();
            var zipStream = new ZipOutputStream(outputMemStream);

            zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

            var newEntry = new ZipEntry(zipEntryName)
            {
                DateTime = DateTime.Now
            };

            zipStream.PutNextEntry(newEntry);

            StreamUtils.Copy(memStreamIn, zipStream, new byte[4096]);
            zipStream.CloseEntry();

            zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
            zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.

            outputMemStream.Position = 0;
            return outputMemStream.ToArray();
        }

        public static byte[] CompressFolder(this string pathToFolder)
        {
            var outputMemStream = new MemoryStream();

            using (var zipStream = new ZipOutputStream(outputMemStream))
            {
                zipStream.SetLevel(3); //0-9, 9 being the highest level of compression
                var folderOffset = pathToFolder.Length + (pathToFolder.EndsWith("\\") ? 0 : 1);

                zipStream.AddFilesToZip(pathToFolder, folderOffset);
                zipStream.IsStreamOwner = true;
                zipStream.Close();

                return outputMemStream.ToArray();
            }
        }

        private static void AddFilesToZip(this ZipOutputStream zipStream, string path, int folderOffset)
        {
            var files = Directory.GetFiles(path).Where(f => !f.EndsWith(".log"));
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                var entryName = file.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction

                var newEntry = new ZipEntry(entryName)
                {
                    DateTime = fileInfo.LastWriteTime, // Note the zip format stores 2 second granularity
                    Size = fileInfo.Length
                };

                zipStream.PutNextEntry(newEntry);

                using (var streamReader = File.OpenRead(file))
                {
                    StreamUtils.Copy(streamReader, zipStream, new byte[4096]);
                }
                zipStream.CloseEntry();
            }
        }
    }
}
