using System;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    class CustomLogicRelativePathProvider : IRelativePathProvider
    {
        public string GetRelativePath(string sourcePath, string destinationPath)
        {
            var relativePath = PathUtilities.GetRelativePathCustomLogic(sourcePath, destinationPath);
            return relativePath;
        }
    }
}
