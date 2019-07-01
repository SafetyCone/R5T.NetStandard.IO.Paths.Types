using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    [TestClass]
    public class WindowsOrNonWindowsPathTests
    {
        #region Absolute Paths

        [TestMethod]
        public void IsWindowsFilePath1()
        {
            var path = PathValues.WindowsFilePath1;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotWindowsFilePath1()
        {
            var path = PathValues.NonWindowsFilePath1;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsWindowsDirectoryPath1()
        {
            var path = PathValues.WindowsDirectoryPath1Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotWindowsDirectoryPath1()
        {
            var path = PathValues.NonWindowsFilePath1;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNonWindowsFilePath1()
        {
            var path = PathValues.NonWindowsFilePath1;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotNonWindowsFilePath1()
        {
            var path = PathValues.WindowsFilePath1;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNonWindowsDirectoryPath1()
        {
            var path = PathValues.NonWindowsDirectoryPath1Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotNonWindowsDirectoryPath1()
        {
            var path = PathValues.WindowsFilePath1;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        #endregion

        #region Relative Paths

        [TestMethod]
        public void IsWindowsRelativeFilePath1()
        {
            var path = RelativePathValues.WindowsFilePath1ToWindowsFilePath2;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotWindowsRelativeFilePath1()
        {
            var path = RelativePathValues.NonWindowsFilePath1ToNonWindowsFilePath2;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsWindowsRelativeDirectoryPath1()
        {
            var path = RelativePathValues.WindowsDirectoryPath1ToWindowsDirectoryPath2Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotWindowsRelativeDirectoryPath1()
        {
            var path = RelativePathValues.NonWindowsDirectoryPath1ToNonWindowsDirectoryPath2Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotWindowsRelativeDirectoryPath1Strict()
        {
            var path = RelativePathValues.NonWindowsDirectoryPath1ToNonWindowsDirectoryPath2Unindicated;

            var expected = false;

            var isWindowsPath = Utilities.IsWindowsPathStrict(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNonWindowsRelativeFilePath1()
        {
            var path = RelativePathValues.NonWindowsFilePath1ToNonWindowsFilePath2;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotNonWindowsRelativeFilePath1()
        {
            var path = RelativePathValues.WindowsFilePath1ToWindowsFilePath2;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNonWindowsRelativeDirectoryPath1()
        {
            var path = RelativePathValues.NonWindowsDirectoryPath1ToNonWindowsDirectoryPath2Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotNonWindowsRelativeDirectoryPath1()
        {
            var path = RelativePathValues.WindowsDirectoryPath1ToWindowsDirectoryPath2Unindicated;

            var expected = true;

            var isWindowsPath = Utilities.IsNonWindowsPath(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        [TestMethod]
        public void IsNotNonWindowsRelativeDirectoryPath1Strict()
        {
            var path = RelativePathValues.WindowsDirectoryPath1ToWindowsDirectoryPath2Unindicated;

            var expected = false;

            var isWindowsPath = Utilities.IsNonWindowsPathStrict(path);

            Assert.AreEqual(expected, isWindowsPath);
        }

        #endregion
    }
}
