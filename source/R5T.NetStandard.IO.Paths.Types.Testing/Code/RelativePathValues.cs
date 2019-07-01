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
        public const string WindowsFilePath1ToWindowsFilePath2Uri = @"File2.txt";
        public const string WindowsFilePath1ToWindowsFilePath3 = @"..\..\File3.txt";

        public const string WindowsDirectoryPath1ToWindowsFilePath1 = @"Directory2\Directory3\File1.txt";
        public const string WindowsDirectoryPath1ToWindowsFilePath4 = @"Directory1\File4.txt";
        public const string WindowsDirectoryPath1ToWindowsFilePath4Uri = @"Directory1/File4.txt"; // Note non-Windows path separator.
        public const string WindowsDirectoryPath1IndicatedToWindowsFilePath4 = @"File4.txt";

        public const string WindowsDirectoryPath1ToWindowsDirectoryPath2Unindicated = @"Directory2";
        public const string WindowsDirectoryPath1ToWindowsDirectoryPath2 = @"Directory2\";
        public const string WindowsDirectoryPath1ToWindowsDirectoryPath2Uri = @"Directory2/"; // Note the non-Windows path separator.

        public const string NonWindowsFilePath1ToNonWindowsFilePath2 = @"../File2.txt";
        public const string NonWindowsFilePath1ToNonWindowsFilePath2Uri = @"File2.txt";
        public const string NonWindowsFilePath1ToNonWindowsFilePath3 = @"../../File3.txt";

        public const string NonWindowsDirectoryPath1ToNonWindowsFilePath1 = @"Directory2/Directory3/File1.txt";

        public const string NonWindowsDirectoryPath1ToNonWindowsDirectoryPath2Unindicated = @"Directory2";
        public const string NonWindowsDirectoryPath1ToNonWindowsDirectoryPath2 = @"Directory2\";
    }
}
