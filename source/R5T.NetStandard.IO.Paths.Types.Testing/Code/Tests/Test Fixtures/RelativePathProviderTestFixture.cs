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
