using System;

using R5T.NetStandard.IO.Paths.Extensions;


namespace R5T.NetStandard.IO.Paths
{
    public abstract class FileToDirectoryRelativePathPair<TSourceFilePath, TDestinationDirectoryPath, TDirectoryRelativePath> : RelativePathPair<TSourceFilePath, TDestinationDirectoryPath, TDirectoryRelativePath>
        where TSourceFilePath : FilePath
        where TDestinationDirectoryPath : DirectoryPath
        where TDirectoryRelativePath : IDirectoryRelativePath
    {
        public FileToDirectoryRelativePathPair(TSourceFilePath sourcePath, TDestinationDirectoryPath destinationPath)
            : base(sourcePath, destinationPath)
        {
        }
    }


    public class FileToDirectoryRelativePathPair : FileToDirectoryRelativePathPair<FilePath, DirectoryPath, DirectoryRelativePath>
    {
        public FileToDirectoryRelativePathPair(FilePath sourcePath, DirectoryPath destinationPath)
            : base(sourcePath, destinationPath)
        {
        }

        protected override DirectoryRelativePath GetRelativePath(string relativePathValue)
        {
            return relativePathValue.AsDirectoryRelativePath();
        }
    }
}
