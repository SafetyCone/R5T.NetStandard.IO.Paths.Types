using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    [TestClass]
    public class UriMakeRelativeUriRelativePathTests
    {
        /// <summary>
        /// If the source directory path is indicated, then the 
        /// "C:\Directory1\" -> "C:\Directory1\Directory2\" => "Directory2/" (Note the non-Windows directory-indicated directory separator.)
        /// </summary>
        [TestMethod]
        public void DirectoryIndicatedToDirectoryIndicatedInDirectoryRelativePathWindows()
        {
            var sourceFilePath = PathValues.WindowsDirectoryPath1;
            var destinationFilePath = PathValues.WindowsDirectoryPath2;
            var expected = RelativePathValues.WindowsDirectoryPath1ToWindowsDirectoryPath2Uri;

            var relativePath = PathUtilities.GetRelativePathUsingUriMakeRelativeUri(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        /// <summary>
        /// If the source directory path is not indicated, then the Uri method assumes the source directory path is a file path, and thus the relative path to a file in the directory is instead a relative path to a file in a directory with the same name as the assumed source file path.
        /// The Uri generated relative path uses the non-Windows path separator.
        /// "C:\Directory1" -> "C:\Directory1\File4.txt" => "Directory1/File4.txt" (Note the non-Windows directory separator.)
        /// </summary>
        [TestMethod]
        public void DirectoryToFileInDirectoryRelativePathWindowsUri()
        {
            var sourceFilePath = PathValues.WindowsDirectoryPath1Unindicated;
            var destinationFilePath = PathValues.WindowsFilePath4;
            var expected = RelativePathValues.WindowsDirectoryPath1ToWindowsFilePath4Uri;

            var relativePath = PathUtilities.GetRelativePathUsingUriMakeRelativeUri(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        /// <summary>
        /// What is the relative path from a file to a file in the same directory? It should be "../File2.txt".
        /// However, the Uri.MakeRelativeUri()-based relative path computation erroneously says "File2.txt". This is erroneous because the Uri.LocalPath-based path resolution says the relative path "..\File2" does in fact resolve the to destination file.
        /// So, by the definition of relative paths, MakeRelativeUri() is wrong!
        /// "/mnt/Directory1/Directory2/Directory3/File1.txt" -> "/mnt/Directory1/Directory2/Directory3/File2.txt" => "File2.txt" (URI-based)
        /// </summary>
        [TestMethod]
        public void FileToFileSameDirectoryRelativePathNonWindowsUri()
        {
            var sourceFilePath = PathValues.NonWindowsFilePath1;
            var destinationFilePath = PathValues.NonWindowsFilePath2;
            var expected = RelativePathValues.NonWindowsFilePath1ToNonWindowsFilePath2Uri;

            var relativePath = PathUtilities.GetRelativePathUsingUriMakeRelativeUri(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        /// <summary>
        /// What is the relative path from a file to a file in the same directory? It should be "..\File2".
        /// However, the Uri.MakeRelativeUri()-based relative path computation erroneously says "File2". This is erroneous because the Uri.LocalPath-based path resolution says the relative path "..\File2" does in fact resolve the to destination file.
        /// So, by the definition of relative paths, MakeRelativeUri() is wrong!
        /// "C:\Directory1\Directory2\Directory3\File1.txt" -> "C:\Directory1\Directory2\Directory3\File2.txt" => "File2.txt" (URI-based)
        /// </summary>
        [TestMethod]
        public void FileToFileSameDirectoryRelativePathWindowsUri()
        {
            var sourceFilePath = PathValues.WindowsFilePath1;
            var destinationFilePath = PathValues.WindowsFilePath2;
            var expected = RelativePathValues.WindowsFilePath1ToWindowsFilePath2Uri;

            var relativePath = PathUtilities.GetRelativePathUsingUriMakeRelativeUri(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }
    }
}
