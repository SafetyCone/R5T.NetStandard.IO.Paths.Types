using System;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    public class UriLocalPathPathResolver : IPathResolver
    {
        public string ResolveDirectoryPath(string unresolvedDirectoryPath)
        {
            var resolvedDirectoryPath = PathUtilities.ResolveDirectoryPath(unresolvedDirectoryPath);
            return resolvedDirectoryPath;
        }

        public string ResolveFilePath(string unresolvedFilePath)
        {
            var resolvedFilePath = PathUtilities.ResolveFilePathUsingUriLocalPath(unresolvedFilePath);
            return resolvedFilePath;
        }

        public string ResolvePath(string unresolvedPath)
        {
            var resolvedPath = PathUtilities.ResolvePathUsingUriLocalPath(unresolvedPath);
            return resolvedPath;
        }
    }
}
