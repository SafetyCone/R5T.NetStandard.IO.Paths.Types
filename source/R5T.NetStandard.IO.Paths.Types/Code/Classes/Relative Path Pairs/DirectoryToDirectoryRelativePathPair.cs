using System;

using R5T.NetStandard.IO.Paths.Extensions;


namespace R5T.NetStandard.IO.Paths
{
    public abstract class DirectoryToDirectoryRelativePathPair<TSourceDirectoryPath, TDestinationDirectoryPath, TDirectoryRelativePath> : RelativePathPair<TSourceDirectoryPath, TDestinationDirectoryPath, TDirectoryRelativePath>
        where TSourceDirectoryPath : DirectoryPath
        where TDestinationDirectoryPath : DirectoryPath
        where TDirectoryRelativePath : IDirectoryRelativePath
    {
        public DirectoryToDirectoryRelativePathPair(TSourceDirectoryPath sourcePath, TDestinationDirectoryPath destinationPath)
            : base(sourcePath, destinationPath)
        {
        }
    }


    public class DirectoryToDirectoryRelativePathPair : DirectoryToDirectoryRelativePathPair<DirectoryPath, DirectoryPath, DirectoryRelativePath>
    {
        public DirectoryToDirectoryRelativePathPair(DirectoryPath sourcePath, DirectoryPath destinationPath)
            : base(sourcePath, destinationPath)
        {
        }

        protected override DirectoryRelativePath GetRelativePath(string relativePathValue)
        {
            return relativePathValue.AsDirectoryRelativePath();
        }
    }
}
