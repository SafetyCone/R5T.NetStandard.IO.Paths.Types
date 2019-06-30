using System;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    class UriMakeRelativeUriRelativePathProvider : IRelativePathProvider
    {
        public string GetRelativePath(string sourcePath, string destinationPath)
        {
            var relativePath = PathUtilities.GetRelativePathUsingUriMakeRelativeUri(sourcePath, destinationPath);
            return relativePath;
        }
    }
}
