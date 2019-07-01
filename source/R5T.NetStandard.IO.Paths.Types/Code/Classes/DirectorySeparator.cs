using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates <see cref="PathSegment"/>s (usually directory names and the file name) in a path.
    /// </summary>
    public class DirectorySeparator : TypedString
    {
        public const string InvalidDirectorySeparatorValue = null;


        #region Static

        /// <summary>
        /// Separates directory path segments in Windows-style paths.
        /// </summary>
        public static readonly DirectorySeparator DefaultWindows = new DirectorySeparator(Constants.DefaultWindowsDirectorySeparator);
        /// <summary>
        /// Separates directory path segments in non-Windows-style paths.
        /// </summary>
        public static readonly DirectorySeparator DefaultNonWindows = new DirectorySeparator(Constants.DefaultNonWindowsDirectorySeparator);


        public static bool IsDirectorySeparator(char character)
        {
            var isDirectorySeparator = character == Constants.DefaultWindowsDirectorySeparatorChar || character == Constants.DefaultNonWindowsDirectorySeparatorChar;
            return isDirectorySeparator;
        }

        public static bool IsValid(string directorySeparator)
        {
            var output = directorySeparator != DirectorySeparator.InvalidDirectorySeparatorValue;
            return output;
        }

        #endregion


        public DirectorySeparator(string value)
            : base(value)
        {
        }
    }
}
