using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    [TestClass]
    public class WindowsOrNonWindowsPathTests
    {
        #region Absolute Paths

        #region Windows

        /// <summary>
        /// A Windows file path is a Windows path.
        /// </summary>
        [TestMethod]
        public void IsWindowsFilePath1()
        {
            var path = PathValues.WindowsFilePath1;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }
        
        /// <summary>
        /// A non-Windows file path is not a Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotWindowsFilePath1()
        {
            var path = PathValues.NonWindowsFilePath1;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A Windows directory path (indicated) is a Windows path.
        /// </summary>
        [TestMethod]
        public void IsWindowsDirectoryPath1()
        {
            var path = PathValues.WindowsDirectoryPath1;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// An unindicated Windows directory path is a Windows path.
        /// </summary>
        [TestMethod]
        public void IsWindowsDirectoryPath1Unindicated()
        {
            var path = PathValues.WindowsDirectoryPath1Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A non-Windows directory path (indicated) is not a Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotWindowsDirectoryPath1()
        {
            var path = PathValues.NonWindowsDirectoryPath1;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// An unindicated non-Windows directory path is not a Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotWindowsDirectoryPath1Unindicated()
        {
            var path = PathValues.NonWindowsDirectoryPath1Unindicated;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        #endregion

        #region Non-Windows

        /// <summary>
        /// A non-Windows file path is a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNonWindowsFilePath1()
        {
            var path = PathValues.NonWindowsFilePath1;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A Windows file path is not a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotNonWindowsFilePath1()
        {
            var path = PathValues.WindowsFilePath1;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A non-Windows directory path (indicated) is a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNonWindowsDirectoryPath1()
        {
            var path = PathValues.NonWindowsDirectoryPath1;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// Un unindicated non-Windows directory path is a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNonWindowsDirectoryPath1Unindicated()
        {
            var path = PathValues.NonWindowsDirectoryPath1Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A Windows directory path (indicated) is not a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotNonWindowsDirectoryPath1()
        {
            var path = PathValues.WindowsDirectoryPath1;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// An unindicated Windows directory path is not a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotNonWindowsDirectoryPath1Unindicated()
        {
            var path = PathValues.WindowsDirectoryPath1Unindicated;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        #endregion

        #endregion

        #region Relative Paths

        #region Windows

        /// <summary>
        /// A Windows file-to-file relative path is a Windows path.
        /// </summary>
        [TestMethod]
        public void IsWindowsRelativeFilePath1()
        {
            var path = RelativePathValues.WindowsFilePath1ToWindowsFilePath2;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A non-Windows file-to-file relative path is not a Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotWindowsRelativeFilePath1()
        {
            var path = RelativePathValues.NonWindowsFilePath1ToNonWindowsFilePath2;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A Windows directory-name-only relative path (unindicated) is a Windows path.
        /// Note: A directory name by itself has not directory separator path information.
        /// This shows that the <see cref="Utilities.IsWindowsPath(string)"/> uses the <see cref="Utilities.IsNonWindowsPathDefault(string)"/> to infer a default Windows path.
        /// </summary>
        [TestMethod]
        public void IsWindowsDirectoryNameOnlyRelativeDirectoryPath1()
        {
            var path = RelativePathValues.DirectoryNameOnlyRelativePathUnindicatedWindows;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A non-Windows directory-name-only relative path (unindicated) is a Windows path.
        /// Note: A directory name by itself has not directory separator path information.
        /// This shows that the <see cref="Utilities.IsWindowsPath(string)"/> uses the <see cref="Utilities.IsWindowsPathDefault(string)"/> to infer a default Windows path.
        /// Which for supposedly non-Windows paths will return TRUE!
        /// </summary>
        [TestMethod]
        public void IsNonWindowsDirectoryNameOnlyRelativeDirectoryPath1()
        {
            var path = RelativePathValues.DirectoryNameOnlyRelativePathUnindicatedNonWindows; // Non-Windows.

            var expected = true; // Correct.

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A directory-name-only relative path (unindicated) is not *strictly* a Windows path.
        /// Shows how to use the Strict method to determine if a path can actually be determined to be a Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotWindowsRelativeDirectoryPath1Strict()
        {
            var path = RelativePathValues.DirectoryNameOnlyRelativePathUnindicated;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPathStrict(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// If an unindicated relative directory path contains a file separator, then it can be strictly determined to be a Windows path.
        /// </summary>
        [TestMethod]
        public void IsWindowsRelativeDirectoryPathUnindicated()
        {
            var path = RelativePathValues.WindowsDirectoryPath1ToWindowsDirectoryPath3Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPathStrict(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// If an unindicated relative directory path contains a file separator, then it can be strictly determined to not be a Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotWindowsRelativeDirectoryPathUnindicated()
        {
            var path = RelativePathValues.NonWindowsDirectoryPath1ToNonWindowsDirectoryPath3Unindicated;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPathStrict(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// Any indicated relative directory path can be strictly determined to be a Windows path.
        /// </summary>
        [TestMethod]
        public void IsWindowsRelativeDirectoryPath()
        {
            var path = RelativePathValues.WindowsDirectoryPath1ToWindowsDirectoryPath2; // Indicated is default.

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPathStrict(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// Any indicated relative directory path can be strictly determined to not be a Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotWindowsRelativeDirectoryPath()
        {
            var path = RelativePathValues.NonWindowsDirectoryPath1ToNonWindowsDirectoryPath2; // Indicated is default.

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPathStrict(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        #endregion

        #region Non-Windows

        /// <summary>
        /// A non-Windows file-to-file relative path is a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNonWindowsRelativeFilePath1()
        {
            var path = RelativePathValues.NonWindowsFilePath1ToNonWindowsFilePath2;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A Windows file-to-file relative path is not a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotNonWindowsRelativeFilePath1()
        {
            var path = RelativePathValues.WindowsFilePath1ToWindowsFilePath2;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A non-Windows directory-name-only relative path (unindicated) is non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNonWindowsRelativeDirectoryPath1()
        {
            var path = RelativePathValues.DirectoryNameOnlyRelativePathUnindicatedNonWindows;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A Windows directory-name-only relative path (unindicated) is non-Windows path.
        /// Note: A directory name by itself has not directory separator path information.
        /// This shows that the <see cref="Utilities.IsNonWindowsPath(string)"/> uses the <see cref="Utilities.IsNonWindowsPathDefault(string)"/> to infer a default Windows path.
        /// Which for supposedly Windows paths will return TRUE!
        /// </summary>
        [TestMethod]
        public void IsNotNonWindowsRelativeDirectoryPath1()
        {
            var path = RelativePathValues.DirectoryNameOnlyRelativePathUnindicatedWindows; // Windows.

            var expected = true; // Correct.

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        /// <summary>
        /// A directory-name-only relative path (unindicated) is not *strictly* a non-Windows path.
        /// Shows how to use the Strict method to determine if a path can actually be determined to be a non-Windows path.
        /// </summary>
        [TestMethod]
        public void IsNotNonWindowsRelativeDirectoryPath1Strict()
        {
            var path = RelativePathValues.DirectoryNameOnlyRelativePathUnindicated;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPathStrict(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        #endregion

        #endregion
    }
}
