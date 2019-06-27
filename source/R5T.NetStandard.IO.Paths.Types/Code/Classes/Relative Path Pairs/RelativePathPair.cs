using System;


namespace R5T.NetStandard.IO.Paths
{
    public abstract class RelativePathPair<TSourcePath, TDestinationPath, TRelativePath>
        where TSourcePath : AbsolutePath
        where TDestinationPath : AbsolutePath
        where TRelativePath : IRelativePath
    {
        public TSourcePath SourcePath { get; }
        public TDestinationPath DestinationPath { get; }


        public RelativePathPair(TSourcePath sourcePath, TDestinationPath destinationPath)
        {
            this.SourcePath = sourcePath;
            this.DestinationPath = destinationPath;
        }

        protected abstract TRelativePath GetRelativePath(string relativePathValue);

        public TRelativePath GetRelativePath()
        {
            var relativePathValue = Utilities.GetRelativePath(this.SourcePath.Value, this.DestinationPath.Value);

            var relativePath = this.GetRelativePath(relativePathValue);
            return relativePath;
        }
    }
}
