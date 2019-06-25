using System;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates <see cref="PathSegment"/>s (usually directory names and the file name) in a path.
    /// </summary>
    public class DirectorySeparator : TypedString
    {
        #region Static

        /// <summary>
        /// Separates directory path segments in Windows-style paths.
        /// </summary>
        public static readonly DirectorySeparator DefaultWindows = new DirectorySeparator(Constants.DefaultWindowsDirectorySeparator);
        /// <summary>
        /// Separates directory path segments in non-Windows-style paths.
        /// </summary>
        public static readonly DirectorySeparator DefaultNonWindows = new DirectorySeparator(Constants.DefaultNonWindowsDirectorySeparator);

        #endregion



        public DirectorySeparator(string value)
            : base(value)
        {
        }
    }
}
