using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    /// <summary>
    /// A test-fixture for <see cref="IRelativePathProvider"/> implementations.
    /// Goals:
    ///     * Windows vs. Non-Windows.
    ///     * Directory indicated vs. directory not-indicated.
    ///     * File/directory to file/directory.
    /// </summary>
    public abstract class RelativePathProviderTestFixture
    {
        


        private IRelativePathProvider RelativePathProvider { get; }


        public RelativePathProviderTestFixture(IRelativePathProvider relativePathProvider)
        {
            this.RelativePathProvider = relativePathProvider;
        }

        #region Test Methods

        #region Files

        ///// <summary>
        ///// If the source directory path is indicated, then the 
        ///// "C:\Directory1\" -> "C:\Directory1\File4.txt" => "File4.txt"
        ///// </summary>
        //[TestMethod]
        //public void DirectoryIndicatedToFileInDirectoryRelativePathWindows()
        //{
        //    var sourceFilePath = PathValues.WindowsDirectoryPath1Indicated;
        //    var destinationFilePath = PathValues.WindowsFilePath4;
        //    var expected = RelativePathValues.WindowsDirectoryPath1IndicatedToWindowsFilePath4;

        //    var relativePath = this.RelativePathProvider.GetRelativePath(sourceFilePath, destinationFilePath);

        //    Assert.AreEqual(expected, relativePath);
        //}

        public void Test()
        {

        }

        /// <summary>
        /// What is the Windows relative path from a file to a file in the same directory? It should be the parent directory path then the destination file name.
        /// "C:\Directory1\Directory2\Directory3\File1.txt" -> "C:\Directory1\Directory2\Directory3\File2.txt" => "..\File2.txt"
        /// </summary>
        [TestMethod]
        public void FileToFileInSameDirectoryWindows()
        {
            var sourceFilePath = PathValues.WindowsFilePath1;
            var destinationFilePath = PathValues.WindowsFilePath2;
            var expected = RelativePathValues.WindowsFilePath1ToWindowsFilePath2;

            var relativePath = this.RelativePathProvider.GetRelativePath(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        /// <summary>
        /// What is the relative path between two identical non-Windows file paths? It should be "" (String.Empty).
        /// "/mnt/Directory1/Directory2/Directory3/File1.txt" -> "/mnt/Directory1/Directory2/Directory3/File1.txt" => ""
        /// </summary>
        [TestMethod]
        public void SameFilePathRelativePathNonWindows()
        {
            var sourceFilePath = PathValues.NonWindowsFilePath1;
            var destinationFilePath = PathValues.NonWindowsFilePath1;
            var expected = RelativePathValues.SameToSame;

            var relativePath = this.RelativePathProvider.GetRelativePath(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        /// <summary>
        /// What is the relative path between two identical Windows file paths? It should be "" (String.Empty).
        /// "C:\Directory1\Directory2\Directory3\File1.txt" -> "C:\Directory1\Directory2\Directory3\File1.txt" => ""
        /// </summary>
        [TestMethod]
        public void SameFilePathRelativePathWindows()
        {
            var sourceFilePath = PathValues.WindowsFilePath1;
            var destinationFilePath = PathValues.WindowsFilePath1;
            var expected = RelativePathValues.SameToSame;

            var relativePath = this.RelativePathProvider.GetRelativePath(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        #endregion

        #region Directory

        /// <summary>
        /// For directory paths (indicated) the directory-to-directory specific function must be used.
        /// "C:\Directory1" -> "C:\Directory1" => ""
        /// </summary>
        [TestMethod]
        public void SameDirectoryPathRelativePathWindows()
        {
            var sourceDirectoryPath = PathValues.WindowsDirectoryPath1;
            var destinationDirectoryPath = PathValues.WindowsDirectoryPath1;
            var expected = RelativePathValues.SameToSame;

            var relativePath = this.RelativePathProvider.GetRelativePath(sourceDirectoryPath, destinationDirectoryPath);

            Assert.AreEqual(expected, relativePath);
        }

        /// <summary>
        /// What is the relative path between two identical Windows directory paths? It should be "" (String.Empty).
        /// For unindicated directory paths the directory-to-directory specific function must be used.
        /// "C:\Directory1" -> "C:\Directory1" => ""
        /// </summary>
        [TestMethod]
        public void SameDirectoryUnindicatedPathRelativePathWindows()
        {
            var sourceDirectoryPath = PathValues.WindowsDirectoryPath1Unindicated;
            var destinationDirectoryPath = PathValues.WindowsDirectoryPath1Unindicated;
            var expected = RelativePathValues.SameToSame;

            var relativePath = this.RelativePathProvider.GetRelativePathDirectoryToDirectory(sourceDirectoryPath, destinationDirectoryPath);

            Assert.AreEqual(expected, relativePath);
        }

        #endregion

        #endregion
    }
}
