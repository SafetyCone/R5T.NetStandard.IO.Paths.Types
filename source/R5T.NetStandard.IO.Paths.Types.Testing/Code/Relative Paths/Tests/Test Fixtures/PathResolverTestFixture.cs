using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    public abstract class PathResolverTestFixture
    {
        #region Static

        public static string GetUnresolvedPathAndResolve(string sourcePath, string relativePath, Func<string, string> pathResolver)
        {
            var unresolvedPath = PathUtilities.GetUnresolvedPath(sourcePath, relativePath);

            var resolvedPath = pathResolver(unresolvedPath);
            return resolvedPath;
        }

        public static string GetUnresolvedFilePathAndResolve(string sourcePath, string relativeFilePath, IPathResolver pathResolver)
        {
            var resolvedPath = PathResolverTestFixture.GetUnresolvedPathAndResolve(sourcePath, relativeFilePath,
                (x) => pathResolver.ResolveFilePath(x));
            return resolvedPath;
        }

        #endregion


        public IPathResolver PathResolver { get; }


        public PathResolverTestFixture(IPathResolver pathResolver)
        {
            this.PathResolver = pathResolver;
        }


        #region Tests

        /// <summary>
        /// What happens if the source path is an indicated directory path (ends with a directory separator)?
        /// </summary>
        [TestMethod]
        public void WindowsIndicatedDirectoryPathToFileInTwiceDerivedDirectory()
        {
            var sourceFilePath = PathValues.WindowsDirectoryPath1;
            var relativeFilePath = RelativePathValues.WindowsDirectoryPath1ToWindowsFilePath1;
            var expected = PathValues.WindowsFilePath1;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        [TestMethod]
        public void NonWindowsDirectoryPathToFileInTwiceDerivedDirectory()
        {
            var sourceFilePath = PathValues.NonWindowsDirectoryPath1Unindicated;
            var relativeFilePath = RelativePathValues.NonWindowsDirectoryPath1ToNonWindowsFilePath1;
            var expected = PathValues.NonWindowsFilePath1;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        [TestMethod]
        public void WindowsDirectoryPathToFileInTwiceDerivedDirectory()
        {
            var sourceFilePath = PathValues.WindowsDirectoryPath1Unindicated;
            var relativeFilePath = RelativePathValues.WindowsDirectoryPath1ToWindowsFilePath1;
            var expected = PathValues.WindowsFilePath1;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        [TestMethod]
        public void NonWindowsFilePathToFileInParentDirectory()
        {
            var sourceFilePath = PathValues.NonWindowsFilePath1;
            var relativeFilePath = RelativePathValues.NonWindowsFilePath1ToNonWindowsFilePath3;
            var expected = PathValues.NonWindowsFilePath3;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        [TestMethod]
        public void WindowsFilePathToFileInParentDirectory()
        {
            var sourceFilePath = PathValues.WindowsFilePath1;
            var relativeFilePath = RelativePathValues.WindowsFilePath1ToWindowsFilePath3;
            var expected = PathValues.WindowsFilePath3;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        /// <summary>
        /// The resolution of a non-Windows path and the relative path to a file in the same directory is easy.
        /// </summary>
        [TestMethod]
        public void NonWindowsFilePathToFileInSameDirectory()
        {
            var sourceFilePath = PathValues.NonWindowsFilePath1;
            var relativeFilePath = RelativePathValues.NonWindowsFilePath1ToNonWindowsFilePath2;
            var expected = PathValues.NonWindowsFilePath2;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        /// <summary>
        /// The resolution of a Windows path and the relative path to a file in the same directory is easy.
        /// </summary>
        [TestMethod]
        public void WindowsFilePathToFileInSameDirectory()
        {
            var sourceFilePath = PathValues.WindowsFilePath1;
            var relativeFilePath = RelativePathValues.WindowsFilePath1ToWindowsFilePath2;
            var expected = PathValues.WindowsFilePath2;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        /// <summary>
        /// What is the resolution of a non-Windows path and an empty relative path?
        /// It should be just the path again.
        /// </summary>
        [TestMethod]
        public void NonWindowsSameFileRelativePath()
        {
            var sourceFilePath = PathValues.NonWindowsFilePath1;
            var relativeFilePath = RelativePathValues.SameToSame;
            var expected = sourceFilePath;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        /// <summary>
        /// What is the resolution of a Windows path and an empty relative path?
        /// It should be just the path again.
        /// </summary>
        [TestMethod]
        public void WindowsSameFileRelativePath()
        {
            var sourceFilePath = PathValues.WindowsFilePath1;
            var relativeFilePath = RelativePathValues.SameToSame;
            var expected = sourceFilePath;

            var resolvedPath = PathResolverTestFixture.GetUnresolvedFilePathAndResolve(sourceFilePath, relativeFilePath, this.PathResolver);

            Assert.AreEqual(expected, resolvedPath);
        }

        #endregion
    }
}
