using System;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    /// <summary>
    /// Allows encapsulating the behavior of computing a relative path.
    /// </summary>
    public interface IRelativePathProvider
    {
        string GetRelativePath(string sourcePath, string destinationPath);
        string GetRelativePathFileToFile(string sourceFilePath, string destinationFilePath);
        string GetRelativePathFileToDirectory(string sourceFilePath, string destinationFilePath);
        string GetRelativePathDirectoryToFile(string sourceDirectoryPath, string destinationFilePath);
        string GetRelativePathDirectoryToDirectory(string sourceDirectoryPath, string destinationDirectoryPath);
    }
}
