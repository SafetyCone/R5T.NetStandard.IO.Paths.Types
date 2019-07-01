using System;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    class PathUtilitiesRelativePathProvider : IRelativePathProvider
    {
        public string GetRelativePath(string sourcePath, string destinationPath)
        {
            var relativePath = PathUtilities.GetRelativePath(sourcePath, destinationPath);
            return relativePath;
        }

        public string GetRelativePathDirectoryToDirectory(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            var relativePath = PathUtilities.GetRelativePath(sourceDirectoryPath, destinationDirectoryPath);
            return relativePath;
        }

        public string GetRelativePathDirectoryToFile(string sourceDirectoryPath, string destinationFilePath)
        {
            var relativePath = PathUtilities.GetRelativePath(sourceDirectoryPath, destinationFilePath);
            return relativePath;
        }

        public string GetRelativePathFileToDirectory(string sourceFilePath, string destinationFilePath)
        {
            var relativePath = PathUtilities.GetRelativePath(sourceFilePath, destinationFilePath);
            return relativePath;
        }

        public string GetRelativePathFileToFile(string sourceFilePath, string destinationFilePath)
        {
            var relativePath = PathUtilities.GetRelativePath(sourceFilePath, destinationFilePath);
            return relativePath;
        }
    }
}
