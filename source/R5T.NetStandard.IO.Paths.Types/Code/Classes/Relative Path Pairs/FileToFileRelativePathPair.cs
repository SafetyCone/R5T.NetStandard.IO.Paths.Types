using System;

using R5T.NetStandard.IO.Paths.Extensions;


namespace R5T.NetStandard.IO.Paths
{
    public abstract class FileToFileRelativePathPair<TSourceFilePath, TDestinationFilePath, TFileRelativePath> : RelativePathPair<TSourceFilePath, TDestinationFilePath, TFileRelativePath>
        where TSourceFilePath : FilePath
        where TDestinationFilePath : FilePath
        where TFileRelativePath : IFileRelativePath
    {
        public FileToFileRelativePathPair(TSourceFilePath sourcePath, TDestinationFilePath destinationPath)
            : base(sourcePath, destinationPath)
        {
        }
    }


    public class FileToFileRelativePathPair : FileToFileRelativePathPair<FilePath, FilePath, FileRelativePath>
    {
        public FileToFileRelativePathPair(FilePath sourcePath, FilePath destinationPath)
            : base(sourcePath, destinationPath)
        {
        }

        protected override FileRelativePath GetRelativePath(string relativePathValue)
        {
            return relativePathValue.AsFileRelativePath();
        }
    }
}
