using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    /// <summary>
    /// A text-fixture for <see cref="IStringlyTypedPathOperationsProvider"/> implementations.
    /// </summary>
    public abstract class StringlyTypedPathOperationsProviderTestFixture
    {
        private IStringlyTypedPathOperationsProvider StringlyTypedPathOperationsProvider { get; }


        public StringlyTypedPathOperationsProviderTestFixture(IStringlyTypedPathOperationsProvider stringlyTypedPathOperationsProvider)
        {
            this.StringlyTypedPathOperationsProvider = stringlyTypedPathOperationsProvider;
        }


        /// <summary>
        /// Demonstrates that <see cref="IStringlyTypedPathOperationsProvider.CombineSimpleUnchecked(string, string)"/> is literally just string concatenation.
        /// </summary>
        [TestMethod]
        public void CombineSimpleUncheckedIsStringConcatenation()
        {
            var string1 = "APPLE";
            var string2 = "Banana";

            var outputPathSegment = this.StringlyTypedPathOperationsProvider.CombineSimpleUnchecked(string1, string2);

            var expectedOutputPathSegment = string1 + string2;

            Assert.AreEqual(expectedOutputPathSegment, outputPathSegment);
        }

        /// <summary>
        /// Demonstrates that the first path segment provided to <see cref="IStringlyTypedPathOperationsProvider.CombineSimpleUnchecked(string, string)"/> should be directory-indicated, else the result will be an invalid path.
        /// ("C:\Directory1", "File1.txt") => "C:\Directory1File1.txt."
        /// </summary>
        [TestMethod]
        public void CombineSimpleUncheckedSuccess()
        {
            var pathSegment1 = PathValues.WindowsDirectoryPath1Unindicated; // Note, unindicated.
            var pathSegment2 = PathValues.FileName1;

            var outputPathSegment = this.StringlyTypedPathOperationsProvider.CombineSimpleUnchecked(pathSegment1, pathSegment2);

            var expectedOutputPathSegment = pathSegment1 + pathSegment2;

            Assert.AreEqual(expectedOutputPathSegment, outputPathSegment);
        }

        /// <summary>
        /// Demonstrates that <see cref="IStringlyTypedPathOperationsProvider.CombineSimpleUnchecked(string, string)"/> will NOT check nor ensure the first path is directory indicated.
        /// ("C:\Directory1", "File1.txt") != "C:\Directory1\File1.txt".
        /// </summary>
        [TestMethod]
        public void CombineSimpleUncheckedFailure()
        {
            var pathSegment1 = PathValues.WindowsDirectoryPath1Unindicated; // Note, unindicated.
            var pathSegment2 = PathValues.FileName1;

            var outputPathSegment = this.StringlyTypedPathOperationsProvider.CombineSimpleUnchecked(pathSegment1, pathSegment2);

            var expectedOutputPathSegment = pathSegment1 + DirectorySeparator.DefaultWindows + pathSegment2;

            Assert.AreNotEqual(expectedOutputPathSegment, outputPathSegment);
        }

        /// <summary>
        /// Demonstrates that <see cref="IStringlyTypedPathOperationsProvider.CombineUnchecked(string, string, string)"/> is literally just string concatenation.
        /// </summary>
        [TestMethod]
        public void CombineSimpleIsStringConcatenation()
        {
            var string1 = "Hello";
            var string2 = "World!";
            var separator = " ";

            var outputPathSegment = this.StringlyTypedPathOperationsProvider.CombineUnchecked(string1, string2, separator);

            var expected = string1 + separator + string2;

            Assert.AreEqual(expected, outputPathSegment);
        }

        /// <summary>
        /// Demonstrates that the first path segment provided to <see cref="IStringlyTypedPathOperationsProvider.CombineUnchecked(string, string, string)"/> should NOT be directory-indicated, else the result will be an invalid path.
        /// ("C:\Directory1\", "File1.txt", "\") => "C:\Directory1\\File1.txt".
        /// </summary>
        [TestMethod]
        public void CombineSimpleSuccess()
        {
            var pathSegment1 = PathValues.WindowsDirectoryPath1;
            var pathSegment2 = PathValues.FileName1;
            var directorySeparator = DirectorySeparator.DefaultWindowsValue;

            var outputPathSegment = this.StringlyTypedPathOperationsProvider.CombineUnchecked(pathSegment1, pathSegment2, directorySeparator);

            var expected = pathSegment1 + directorySeparator + pathSegment2;

            Assert.AreEqual(expected, outputPathSegment);
        }

        /// <summary>
        /// Demonstrates that <see cref="IStringlyTypedPathOperationsProvider.CombineSimpleUnchecked(string, string)"/> will NOT check nor ensure the first path is NOT directory indicated.
        /// ("C:\Directory1\", "File1.txt", "\") != "C:\Directory1\File1.txt".
        /// </summary>
        [TestMethod]
        public void CombineSimpleFailure()
        {
            var pathSegment1 = PathValues.WindowsDirectoryPath1;
            var pathSegment2 = PathValues.FileName1;
            var directorySeparator = DirectorySeparator.DefaultWindowsValue;

            var outputPathSegment = this.StringlyTypedPathOperationsProvider.CombineUnchecked(pathSegment1, pathSegment2, directorySeparator);

            var expected = pathSegment1 + pathSegment2;

            Assert.AreNotEqual(expected, outputPathSegment);
        }
    }
}
