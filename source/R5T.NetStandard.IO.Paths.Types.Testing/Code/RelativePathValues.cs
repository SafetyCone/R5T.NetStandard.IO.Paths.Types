using System;


namespace R5T.NetStandard.IO.Paths.Types
{
    public static class RelativePathValues
    {
        /// <summary>
        /// The relative path from a path to the same path is just the empty string.
        /// Windows: This has been validated in the Windows Explorer bar.
        /// Non-Windows: There seems to be no way to start a relative path with a file name in shell... However, resolution
        /// </summary>
        public const string SameToSame = @"";

        public const string WindowsFilePath1ToWindowsFilePath2 = @"..\File2.txt";
        public const string WindowsFilePath1ToWindowsFilePath3 = @"..\..\File3.txt";

        public const string WindowsDirectoryPath1ToWindowsFilePath1 = @"Directory2\Directory3\File1.txt";

        public const string NonWindowsFilePath1ToNonWindowsFilePath2 = @"../File2.txt";
        public const string NonWindowsFilePath1ToNonWindowsFilePath3 = @"../../File3.txt";

        public const string NonWindowsDirectoryPath1ToNonWindowsFilePath1 = @"Directory2/Directory3/File1.txt";
    }
}
