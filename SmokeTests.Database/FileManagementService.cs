using System;
using System.IO;

namespace SystemTests.Database
{
    public class FileManagementService : IFileManagementService
    {
        public string GetFilePath(string fileName)
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), fileName, SearchOption.AllDirectories);
            if (files.Length < 1)
                throw new Exception($@"The file was not found in the bin directory. Expecting file: {Directory.GetCurrentDirectory()}\{fileName}");

            return files[0];
        }
    }
}
