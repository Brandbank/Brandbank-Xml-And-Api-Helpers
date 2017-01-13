using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Xml.Linq;

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

        public static T CreateDirectory<T>(this T returnItem, string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return returnItem;
        }

        public static string SaveToDirectory(this XElement element, string path, string name)
        {
            var fullPath = Path.Combine(path, name);
            File.WriteAllText(fullPath, element.ToString());
            return path;
        }

        public static byte[] CompressFolder(this string pathToFolder)
        {
            using (var outputMemStream = new MemoryStream())
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
            var files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                var fileInfo = new FileInfo(file);

                var entryName = file.Substring(folderOffset); // Makes the name in zip based on the folder
                entryName = ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction

                var newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fileInfo.LastWriteTime; // Note the zip format stores 2 second granularity
                newEntry.Size = fileInfo.Length;

                zipStream.PutNextEntry(newEntry);

                using (FileStream streamReader = File.OpenRead(file))
                {
                    StreamUtils.Copy(streamReader, zipStream, new byte[4096]);
                }
                zipStream.CloseEntry();
            }
        }
    }
}
