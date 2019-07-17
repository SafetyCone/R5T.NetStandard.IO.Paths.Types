using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Created to allow sorting out the complexity of methods in the <see cref="Utilities"/> class.
    /// Allows side-by-side comparison with <see cref="Utilities"/>, and functionality is only transfered into <see cref="UtilitiesGood"/> when it is ready.
    /// </summary>
    public static class UtilitiesGood
    {
        #region Stringly-Typed Paths

        /// <summary>
        /// Combines two path segments into a single path segment.
        /// Simple - No directory separator is inserted between the paths (the first path segment is assumed to end with the required directory separator).
        /// Unchecked - No check is made to ensure the first path segment does, in fact, end with a directory separator.
        /// This literally just concatenates the two path segment strings, and returns outputPathSegment = <paramref name="directoryIndicatedPathSegment1"/> + <paramref name="pathSegment2"/>.
        /// </summary>
        public static string CombineSimpleUnchecked(string directoryIndicatedPathSegment1, string pathSegment2)
        {
            var outputPathSegment = $"{directoryIndicatedPathSegment1}{pathSegment2}";
            return outputPathSegment;
        }

        /// <summary>
        /// Combines two path segments into a single path segment.
        /// Unchecked - No check is done to ensure the first path segment does NOT end with a directory separator, nor that the directory separator is actually a directory separator.
        /// This is just string concatenation: outputPathSegment = <paramref name="pathSegment1"/> + <paramref name="directorySeparator"/> + <paramref name="pathSegment2"/>.
        /// </summary>
        public static string CombineUnchecked(string pathSegment1, string pathSegment2, string directorySeparator)
        {
            var outputPathSegment = $"{pathSegment1}{directorySeparator}{pathSegment2}";
            return outputPathSegment;
        }

        #endregion
    }
}
