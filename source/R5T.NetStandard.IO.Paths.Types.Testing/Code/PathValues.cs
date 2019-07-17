using System;


namespace R5T.NetStandard.IO.Paths.Types
{
    public static class PathValues
    {
        public const string FileName1 = "File1.txt";
        public const string WindowsFileName1 = PathValues.FileName1;
        public const string NonndowsFileName1 = PathValues.FileName1;

        public const string WindowsFilePath1 = @"C:\Directory1\Directory2\Directory3\File1.txt";
        public const string WindowsFilePath2 = @"C:\Directory1\Directory2\Directory3\File2.txt";
        public const string WindowsFilePath3 = @"C:\Directory1\Directory2\File3.txt";
        public const string WindowsFilePath4 = @"C:\Directory1\File4.txt";
        public const string WindowsFilePath5 = @"C:\Directory1\Directory2\Directory3\Directory4\File5.txt";
        public const string WindowsFilePath6 = @"C:\File6.txt"; // Directly on root.

        public const string WindowsRootDirectoryPath1 = @"C:\";
        public const string WindowsRootDirectoryPath2 = @"F:\";

        public const string WindowsDirectoryPath1Unindicated = @"C:\Directory1";
        public const string WindowsDirectoryPath1 = @"C:\Directory1\";
        public const string WindowsDirectoryPath2Unindicated = @"C:\Directory1\Directory2";
        public const string WindowsDirectoryPath2 = @"C:\Directory1\Directory2\";
        public const string WindowsDirectoryPath3UnIndicated = @"C:\Directory1\Directory2\Directory3";
        public const string WindowsDirectoryPath3 = @"C:\Directory1\Directory2\Directory3\";

        public const string NonWindowsFilePath1 = @"/mnt/Directory1/Directory2/Directory3/File1.txt";
        public const string NonWindowsFilePath2 = @"/mnt/Directory1/Directory2/Directory3/File2.txt";
        public const string NonWindowsFilePath3 = @"/mnt/Directory1/Directory2/File3.txt";
        public const string NonWindowsFilePath4 = @"/mnt/Directory1/File3.txt";

        public const string NonWindowsDirectoryPath1Unindicated = @"/mnt/Directory1";
        public const string NonWindowsDirectoryPath1 = @"/mnt/Directory1/";
        public const string NonWindowsDirectoryPath2Unindicated = @"/mnt/Directory1/Directory2";
        public const string NonWindowsDirectoryPath2 = @"/mnt/Directory1/Directory2/";
    }
}
