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
        public const string WindowsFilePath1 = @"C:\Directory1\Directory2\Directory3\File1.txt";
        public const string WindowsFilePath2 = @"C:\Directory1\Directory2\Directory3\File2.txt";
        public const string WindowsFilePath3 = @"C:\Directory1\Directory2\File3.txt";
        public const string WindowsFilePath4 = @"C:\Directory1\File4.txt";
        public const string WindowsFilePath5 = @"C:\Directory1\Directory2\Directory3\Directory4\File5.txt";
        public const string WindowsFilePath6 = @"C:\File6.txt"; // Directly on root.

        public const string WindowsRootDirectoryPath1 = @"C:\";
        public const string WindowsRootDirectoryPath2 = @"F:\";

        public const string WindowsDirectoryPath1 = @"C:\Directory1";
        public const string WindowsDirectoryPath1Indicated = @"C:\Directory1\";
        public const string WindowsDirectoryPath2 = @"C:\Directory1\Directory2";
        public const string WindowsDirectoryPath2Indicated = @"C:\Directory1\Directory2\";
        public const string WindowsDirectoryPath3 = @"C:\Directory1\Directory2\Directory3";

        public const string NonWindowsFilePath1 = @"/mnt/Directory1/Directory2/Directory3/File1.txt";
        public const string NonWindowsFilePath2 = @"/mnt/Directory1/Directory2/Directory3/File2.txt";
        public const string NonWindowsFilePath3 = @"/mnt/Directory1/Directory2/File3.txt";
        public const string NonWindowsFilePath4 = @"/mnt/Directory1/File3.txt";


        private IRelativePathProvider RelativePathProvider { get; }


        public RelativePathProviderTestFixture(IRelativePathProvider relativePathProvider)
        {
            this.RelativePathProvider = relativePathProvider;
        }

        #region Test Methods

        ///// <summary>
        ///// 
        ///// </summary>
        //[TestMethod]
        //public void WindowsFileToFileSameDirectoryRelativePath()
        //{

        //}

        /// <summary>
        /// What is the relative path between two identical non-Windows file paths?
        /// It should be "" (String.Empty).
        /// </summary>
        [TestMethod]
        public void NonWindowsSameFileRelativePath()
        {
            var sourceFilePath = RelativePathProviderTestFixture.NonWindowsFilePath1;
            var destinationFilePath = RelativePathProviderTestFixture.NonWindowsFilePath1;
            var expected = String.Empty;

            var relativePath = this.RelativePathProvider.GetRelativePath(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        /// <summary>
        /// What is the relative path between two identical Windows file paths?
        /// It should be "" (String.Empty).
        /// </summary>
        [TestMethod]
        public void WindowsSameFileRelativePath()
        {
            var sourceFilePath = RelativePathProviderTestFixture.WindowsFilePath1;
            var destinationFilePath = RelativePathProviderTestFixture.WindowsFilePath1;
            var expected = String.Empty;

            var relativePath = this.RelativePathProvider.GetRelativePath(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        #endregion
    }
}
