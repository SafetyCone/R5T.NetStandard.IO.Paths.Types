using System;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    public class CustomLogicPathResolver : IPathResolver
    {
        public string ResolveDirectoryPath(string unresolvedDirectoryPath)
        {
            throw new NotImplementedException();
        }

        public string ResolveFilePath(string unresolvedFilePath)
        {
            throw new NotImplementedException();
        }

        public string ResolvePath(string unresolvedPath)
        {
            var resolvedPath = PathUtilities.ResolvePathUsingCustomLogic(unresolvedPath);
            return resolvedPath;
        }
    }
}
