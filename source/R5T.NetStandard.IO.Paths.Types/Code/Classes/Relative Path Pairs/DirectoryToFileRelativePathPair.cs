using System;

using R5T.NetStandard.IO.Paths.Extensions;


namespace R5T.NetStandard.IO.Paths
{
    public abstract class DirectoryToFileRelativePathPair<TSourceDirectoryPath, TDestinationFilePath, TFileRelativePath> : RelativePathPair<TSourceDirectoryPath, TDestinationFilePath, TFileRelativePath>
        where TSourceDirectoryPath : DirectoryPath
        where TDestinationFilePath : FilePath
        where TFileRelativePath : IFileRelativePath
    {
        public DirectoryToFileRelativePathPair(TSourceDirectoryPath sourcePath, TDestinationFilePath destinationPath)
            : base(sourcePath, destinationPath)
        {
        }
    }


    public class DirectoryToFileRelativePathPair : DirectoryToFileRelativePathPair<DirectoryPath, FilePath, FileRelativePath>
    {
        public DirectoryToFileRelativePathPair(DirectoryPath sourcePath, FilePath destinationPath)
            : base(sourcePath, destinationPath)
        {
        }

        protected override FileRelativePath GetRelativePath(string relativePathValue)
        {
            return relativePathValue.AsFileRelativePath();
        }
    }
}
