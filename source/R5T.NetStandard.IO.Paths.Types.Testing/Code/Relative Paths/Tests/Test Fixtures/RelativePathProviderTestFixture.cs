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

        /// <summary>
        /// What is the relative path from a file to a file in the same directory? It should be "..\File2".
        /// However, the Uri.MakeRelativeUri()-based relative path computation erroneously says "File2". This is erroneous because the Uri.LocalPath-based path resolution says the relative path "..\File2" does in fact resolve the to destination file.
        /// So, by the definition of relative paths, MakeRelativeUri() is wrong!
        /// </summary>
        [TestMethod]
        public void WindowsFileToFileSameDirectoryRelativePath()
        {
            var sourceFilePath = PathValues.WindowsFilePath1;
            var destinationFilePath = PathValues.WindowsFilePath2;
            var expected = RelativePathValues.WindowsFilePath1ToWindowsFilePath2;

            var relativePath = this.RelativePathProvider.GetRelativePath(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        /// <summary>
        /// What is the relative path between two identical non-Windows file paths?
        /// It should be "" (String.Empty).
        /// </summary>
        [TestMethod]
        public void NonWindowsSameFileRelativePath()
        {
            var sourceFilePath = PathValues.NonWindowsFilePath1;
            var destinationFilePath = PathValues.NonWindowsFilePath1;
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
            var sourceFilePath = PathValues.WindowsFilePath1;
            var destinationFilePath = PathValues.WindowsFilePath1;
            var expected = String.Empty;

            var relativePath = this.RelativePathProvider.GetRelativePath(sourceFilePath, destinationFilePath);

            Assert.AreEqual(expected, relativePath);
        }

        #endregion
    }
}
