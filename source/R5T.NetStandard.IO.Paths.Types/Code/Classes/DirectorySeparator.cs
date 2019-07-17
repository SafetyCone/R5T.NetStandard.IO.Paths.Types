using System;
using System.Linq;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Separates <see cref="PathSegment"/>s (usually directory names and the file name) in a path.
    /// </summary>
    public class DirectorySeparator : TypedString
    {
        public const string InvalidValue = null;
        public const DirectorySeparator Invalid = null;


        #region Static

        public static readonly string DefaultWindowsValue = Constants.WindowsDirectorySeparator;
        /// <summary>
        /// Separates directory path segments in Windows-style paths.
        /// </summary>
        public static readonly DirectorySeparator DefaultWindows = new DirectorySeparator(Constants.WindowsDirectorySeparator);

        public static readonly string DefaultNonWindowsValue = Constants.NonWindowsDirectorySeparator;
        /// <summary>
        /// Separates directory path segments in non-Windows-style paths.
        /// </summary>
        public static readonly DirectorySeparator DefaultNonWindows = new DirectorySeparator(Constants.NonWindowsDirectorySeparator);


        /// <summary>
        /// Determines if the input is a directory separator (either Windows or non-Windows).
        /// </summary>
        public static bool IsDirectorySeparator(string possibleDirectorySeparator)
        {
            var output = possibleDirectorySeparator == Constants.WindowsDirectorySeparator || possibleDirectorySeparator == Constants.NonWindowsDirectorySeparator;
            return output;
        }

        /// <summary>
        /// Determines if the input is a directory separator (either Windows or non-Windows).
        /// </summary>
        public static bool IsDirectorySeparator(char character)
        {
            var isDirectorySeparator = character == Constants.WindowsDirectorySeparatorChar || character == Constants.NonWindowsDirectorySeparatorChar;
            return isDirectorySeparator;
        }

        /// <summary>
        /// Determines if the input character equals the specified directory separator, and if the specified directory separator is a directory separator.
        /// </summary>
        public static bool IsDirectorySeparator(char character, char directorySeparator)
        {
            var isDirectorySeparator = DirectorySeparator.IsDirectorySeparator(directorySeparator);
            if (!isDirectorySeparator)
            {
                return false;
            }

            var output = character == directorySeparator;
            return output;
        }

        /// <summary>
        /// Determines if the input character equals the specified directory separator, and if the specified directory separator is a directory separator.
        /// </summary>
        public static bool IsDirectorySeparator(char character, string directorySeparator)
        {
            var isDirectorySeparator = DirectorySeparator.IsDirectorySeparator(directorySeparator);
            if (!isDirectorySeparator)
            {
                return false;
            }

            var output = character == directorySeparator.Single();
            return output;
        }

        public static bool IsInvalid(string directorySeparator)
        {
            var output = directorySeparator == DirectorySeparator.InvalidValue;
            return output;
        }

        /// <summary>
        /// Determines if the input directory separator is not the <see cref="DirectorySeparator.InvalidValue"/>, then if it is one of the Windows of non-Windows directory separators.
        /// </summary>
        public static bool IsValid(string directorySeparator)
        {
            var isInvalid = DirectorySeparator.IsInvalid(directorySeparator);
            if (isInvalid)
            {
                return false;
            }

            var output = DirectorySeparator.IsDirectorySeparator(directorySeparator);
            return output;
        }

        public static bool IsInvalid(DirectorySeparator directorySeparator)
        {
            var output = directorySeparator == DirectorySeparator.Invalid;
            return output;
        }

        public static bool HasInvalidValue(DirectorySeparator directorySeparator)
        {
            var output = DirectorySeparator.IsInvalid(directorySeparator.Value);
            return output;
        }

        /// <summary>
        /// Determines if the input directory separator is not the <see cref="DirectorySeparator.Invalid"/> instance, then whether the directory separator's value is valid.
        /// </summary>
        public static bool IsValid(DirectorySeparator directorySeparator)
        {
            var isInvalid = DirectorySeparator.IsInvalid(directorySeparator);
            if (isInvalid)
            {
                return false;
            }

            var output = DirectorySeparator.IsValid(directorySeparator.Value);
            return output;
        }

        public static ArgumentException GetInvalidDirectorySeparatorException(string found, string parameterName)
        {
            var output = new ArgumentException($"Invalid directory separator.\nExpected Windows ({Constants.WindowsDirectorySeparator}) or non-Windows ({Constants.NonWindowsDirectorySeparator}) directory separator.\nFound: {found}.", parameterName);
            return output;
        }

        /// <summary>
        /// Checks that the specified directory separator is actually a directory separator, and throws an exception if it is not.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="directorySeparator"/> is not a directory separator according to <see cref="DirectorySeparator.IsDirectorySeparator(string)"/>.</exception>
        public static void ValidateDirectorySeparatorArgument(string directorySeparator, string parameterName)
        {
            if (!DirectorySeparator.IsDirectorySeparator(directorySeparator))
            {
                throw DirectorySeparator.GetInvalidDirectorySeparatorException(directorySeparator, nameof(directorySeparator));
            }
        }

        #endregion


        public DirectorySeparator(string value)
            : base(value)
        {
        }
    }
}
