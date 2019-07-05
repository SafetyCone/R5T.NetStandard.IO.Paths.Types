using System;


namespace R5T.NetStandard.IO.Paths
{
    public static class AbsolutePathExtensions
    {
        //public static bool IsAbsolute(this AbsolutePath absolutePath)
        //{
        //    var output = Utilities.IsAbsolutePath(absolutePath.Value);
        //}

        public static FilePath AsFilePath(this AbsolutePath absolutePath)
        {
            var filePath = new FilePath(absolutePath.Value);
            return filePath;
        }

        public static DirectoryPath AsDirectoryPath(this AbsolutePath absolutePath)
        {
            var directoryPath = new DirectoryPath(absolutePath.Value);
            return directoryPath;
        }
    }
}
