using System;
using System.IO;

using R5T.NetStandard.IO.Paths.Extensions;


namespace R5T.NetStandard.IO.Paths
{
    public static class DirectoryPathExtensions
    {
        public static bool Exists(this DirectoryPath directoryPath)
        {
            var exists = Directory.Exists(directoryPath.Value);
            return exists;
        }

        public static DirectoryName GetDirectoryName(this DirectoryPath directoryPath)
        {
            var directoryName = new DirectoryInfo(directoryPath.Value).Name.AsDirectoryName();
            return directoryName;
        }
    }
}
