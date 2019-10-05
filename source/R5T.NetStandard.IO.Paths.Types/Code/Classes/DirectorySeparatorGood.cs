using System;
using System.IO;


namespace R5T.NetStandard.IO.Paths
{
    public static class DirectorySeparatorGood
    {
        #region Stringly-Typed Directory Separators

        /// <summary>
        /// The invalid directory-separator value.
        /// (A null string.)
        /// </summary>
        public const string InvalidValue = null;


        /// <summary>
        /// Returns the <see cref="Path.DirectorySeparatorChar"/> value.
        /// Separates directory names in a hierarchical path. This is '\' on Windows.
        /// Example: "C:\temp\temp1\temp2\temp.txt".
        /// </summary>
        public static readonly string EnvironmentDefaultValue = Path.DirectorySeparatorChar.ToString();
        /// <summary>
        /// Returns the Windows directory-separator value ('\').
        /// Example: "C:\temp\temp1\temp2\temp.txt".
        /// </summary>
        public static readonly string WindowsValue = Constants.WindowsDirectorySeparator;
        public static readonly char WindowsValueChar = Constants.WindowsDirectorySeparatorChar;
        /// <summary>
        /// Returns the non-Windows directory-separator value ('/').
        /// Example: "/mnt/temp/temp1/temp2/temp.txt".
        /// </summary>
        public static readonly string NonWindowsValue = Constants.NonWindowsDirectorySeparator;
        public static readonly char NonWindowsValueChar = Constants.NonWindowsDirectorySeparatorChar;


        private static string zDefaultValue = DirectorySeparatorGood.EnvironmentDefaultValue;
        /// <summary>
        /// Allows setting a default directory separator value for use
        /// </summary>
        public static string DefaultValue
        {
            get
            {
                return DirectorySeparatorGood.zDefaultValue;
            }
            set
            {
                if (DirectorySeparatorGood.IsDirectorySeparator(value))
                {
                    DirectorySeparatorGood.zDefaultValue = value;
                }
                else
                {
                    var exception = DirectorySeparatorGood.GetInvalidDirectorySeparatorValueException(value);
                    throw exception;
                }
            }
        }

        /// <summary>
        /// Determines if the input is a directory separator (either Windows or non-Windows).
        /// </summary>
        public static bool IsDirectorySeparator(string possibleDirectorySeparator)
        {
            var isDirectorySeparator =
                possibleDirectorySeparator == DirectorySeparatorGood.WindowsValue ||
                possibleDirectorySeparator == DirectorySeparatorGood.NonWindowsValue;
            return isDirectorySeparator;
        }

        /// <summary>
        /// Determines if the input is a directory separator (either Windows or non-Windows).
        /// </summary>
        public static bool IsDirectorySeparator(char possibleDirectorySeparator)
        {
            var isDirectorySeparator =
                possibleDirectorySeparator == DirectorySeparatorGood.WindowsValueChar ||
                possibleDirectorySeparator == DirectorySeparatorGood.NonWindowsValueChar;
            return isDirectorySeparator;
        }

        /// <summary>
        /// Determines if the specified value equals the specified directory-separator, and if the specified directory separator is actually a directory separator.
        /// </summary>
        public static bool IsDirectorySeparator(char value, char possibleDirectorySeparator)
        {
            var areEqual = value == possibleDirectorySeparator;
            if(!areEqual)
            {
                return false;
            }

            var isDirectorySeparator = DirectorySeparatorGood.IsDirectorySeparator(possibleDirectorySeparator);
            if (!isDirectorySeparator)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if the specified value equals the specified directory-separator, and if the specified directory separator is actually a directory separator.
        /// </summary>
        public static bool IsDirectorySeparator(string value, string possibleDirectorySeparator)
        {
            var areEqual = value == possibleDirectorySeparator;
            if (!areEqual)
            {
                return false;
            }

            var isDirectorySeparator = DirectorySeparatorGood.IsDirectorySeparator(possibleDirectorySeparator);
            if (!isDirectorySeparator)
            {
                return false;
            }

            return true;
        }

        public static bool IsWindowsDirectorySeparator(char directorySeparator)
        {
            var isWindows = DirectorySeparatorGood.IsDirectorySeparator(directorySeparator, DirectorySeparatorGood.WindowsValueChar);
            return isWindows;
        }

        public static bool IsWindowsDirectorySeparator(string directorySeparator)
        {
            var isWindows = DirectorySeparatorGood.IsDirectorySeparator(directorySeparator, DirectorySeparatorGood.WindowsValue);
            return isWindows;
        }

        public static bool IsNonWindowsDirectorySeparator(char directorySeparator)
        {
            var isWindows = DirectorySeparatorGood.IsDirectorySeparator(directorySeparator, DirectorySeparatorGood.NonWindowsValueChar);
            return isWindows;
        }

        public static bool IsNonWindowsDirectorySeparator(string directorySeparator)
        {
            var isWindows = DirectorySeparatorGood.IsDirectorySeparator(directorySeparator, DirectorySeparatorGood.NonWindowsValue);
            return isWindows;
        }

        /// <summary>
        /// Determines if the given directory separator is the <see cref="DirectorySeparatorGood.InvalidValue"/>.
        /// </summary>
        public static bool IsInvalid(string directorySeparator)
        {
            var isInvalid = directorySeparator == DirectorySeparatorGood.InvalidValue;
            return isInvalid;
        }

        /// <summary>
        /// Determines if a character is a valid directory-separator.
        /// Since there is no invalid directory separator character, this is identical to whether the specified character is one of the Windows of non-Windows directory separators.
        /// </summary>
        public static bool IsValid(char directorySeparator)
        {
            var isValid = DirectorySeparatorGood.IsDirectorySeparator(directorySeparator);
            return isValid;
        }

        /// <summary>
        /// Determines if a string is a valid directory separator.
        /// Checks that specified directory separator is not the <see cref="DirectorySeparatorGood.InvalidValue"/>, then if it is one of the Windows of non-Windows directory separators.
        /// </summary>
        public static bool IsValid(string directorySeparator)
        {
            var isInvalid = DirectorySeparatorGood.IsInvalid(directorySeparator);
            if (isInvalid)
            {
                return false;
            }

            var output = DirectorySeparatorGood.IsDirectorySeparator(directorySeparator);
            return output;
        }

        /// <summary>
        /// Checks that the specified directory separator is actually a directory separator, and throws an exception if it is not.
        /// </summary>
        /// <exception cref="Exception">Thrown if the <paramref name="directorySeparator"/> is not a valid directory separator according to <see cref="DirectorySeparatorGood.IsValid(string)"/>.</exception>
        public static void Validate(char directorySeparator)
        {
            var isValid = DirectorySeparatorGood.IsValid(directorySeparator);
            if (!isValid)
            {
                var exception = DirectorySeparatorGood.GetInvalidDirectorySeparatorValueException(directorySeparator.ToString());
                throw exception;
            }
        }

        /// <summary>
        /// Checks that the specified directory separator is actually a directory separator, and throws an exception if it is not.
        /// </summary>
        /// <exception cref="Exception">Thrown if the <paramref name="directorySeparator"/> is not a valid directory separator according to <see cref="DirectorySeparatorGood.IsValid(string)"/>.</exception>
        public static void Validate(string directorySeparator)
        {
            var isValid = DirectorySeparatorGood.IsValid(directorySeparator);
            if (!isValid)
            {
                var exception = DirectorySeparatorGood.GetInvalidDirectorySeparatorValueException(directorySeparator);
                throw exception;
            }
        }

        public static void ValidateWindows(string windowsDirectorySeparator)
        {
            DirectorySeparatorGood.Validate(windowsDirectorySeparator);

            var isWindows = DirectorySeparatorGood.IsWindowsDirectorySeparator(windowsDirectorySeparator);
            if (!isWindows)
            {
                var exception = DirectorySeparatorGood.GetWindowsDirectorySeparatorValueExpectedException(windowsDirectorySeparator);
                throw exception;
            }
        }

        public static void ValidateNonWindows(string nonWindowsDirectorySeparator)
        {
            DirectorySeparatorGood.Validate(nonWindowsDirectorySeparator);

            var isWindows = DirectorySeparatorGood.IsNonWindowsDirectorySeparator(nonWindowsDirectorySeparator);
            if (!isWindows)
            {
                var exception = DirectorySeparatorGood.GetNonWindowsDirectorySeparatorValueExpectedException(nonWindowsDirectorySeparator);
                throw exception;
            }
        }

        /// <summary>
        /// Checks that the specified directory separator argument is actually a directory separator, and throws an exception if it is not.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="directorySeparator"/> is not a valid directory separator according to <see cref="DirectorySeparatorGood.IsValid(string)"/>.</exception>
        public static void Validate(char directorySeparator, string argumentName)
        {
            if (!DirectorySeparatorGood.IsValid(directorySeparator))
            {
                var exception = DirectorySeparatorGood.GetInvalidDirectorySeparatorValueArgumentException(directorySeparator.ToString(), argumentName);
                throw exception;
            }
        }

        /// <summary>
        /// Checks that the specified directory separator argument is actually a directory separator, and throws an exception if it is not.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="directorySeparator"/> is not a valid directory separator according to <see cref="DirectorySeparatorGood.IsValid(string)"/>.</exception>
        public static void Validate(string directorySeparator, string argumentName)
        {
            if (!DirectorySeparatorGood.IsValid(directorySeparator))
            {
                var exception = DirectorySeparatorGood.GetInvalidDirectorySeparatorValueArgumentException(directorySeparator, argumentName);
                throw exception;
            }
        }

        public static void ValidateWindows(string windowsDirectorySeparator, string argumentName)
        {
            DirectorySeparatorGood.Validate(windowsDirectorySeparator);

            var isWindows = DirectorySeparatorGood.IsWindowsDirectorySeparator(windowsDirectorySeparator);
            if (!isWindows)
            {
                var exception = DirectorySeparatorGood.GetWindowsDirectorySeparatorValueExpectedArgumentException(windowsDirectorySeparator, argumentName);
                throw exception;
            }
        }

        public static void ValidateNonWindows(string nonWindowsDirectorySeparator, string argumentName)
        {
            DirectorySeparatorGood.Validate(nonWindowsDirectorySeparator);

            var isWindows = DirectorySeparatorGood.IsNonWindowsDirectorySeparator(nonWindowsDirectorySeparator);
            if (!isWindows)
            {
                var exception = DirectorySeparatorGood.GetNonWindowsDirectorySeparatorValueExpectedArgumentException(nonWindowsDirectorySeparator, argumentName);
                throw exception;
            }
        }

        /// <summary>
        /// Between the Windows ('\') and the non-Windows ('/') directory separator, given one, return the other.
        /// Unchecked - If the input directory separator is neither the Windows nor non-Windows separator, the Windows separator is returned.
        /// </summary>
        public static string GetAlternateDirectorySeparatorUnchecked(string directorySeparator)
        {
            var isWindows = DirectorySeparatorGood.IsWindowsDirectorySeparator(directorySeparator);
            if (isWindows)
            {
                return DirectorySeparatorGood.NonWindowsValue;
            }
            else
            {
                return DirectorySeparatorGood.WindowsValue;
            }
        }

        /// <summary>
        /// Between the Windows ('\') and the non-Windows ('/') directory separator, given one, return the other.
        /// Checked - Validates the directory separator first.
        /// </summary>
        public static string GetAlternateDirectorySeparator(string directorySeparator)
        {
            DirectorySeparatorGood.Validate(directorySeparator);

            var alternateDirectorySeparator = DirectorySeparatorGood.GetAlternateDirectorySeparatorUnchecked(directorySeparator);
            return alternateDirectorySeparator;
        }

        /// <summary>
        /// Attempts to detect the directory separator (Windows or non-Windows) used within a path segment.
        /// Returns true if the a directory separator can be detected.
        /// Returns false if a directory separator cannot be detected, and sets the output <paramref name="directorySeparator"/> to the provided <paramref name="defaultDirectorySeparator"/> value.
        /// A path segment might have both Windows and non-Windows directory separators. Whichever occurs first in the path segment (thus, closer to the root) is dominant, and is returned as the path segment's directory separator.
        /// </summary>
        public static bool TryDetectDirectorySeparator(string pathSegment, out string directorySeparator, string defaultDirectorySeparator)
        {
            var indexOfWindows = pathSegment.IndexOf(DirectorySeparatorGood.WindowsValue);
            var indexOfNonWindows = pathSegment.IndexOf(DirectorySeparatorGood.NonWindowsValue);

            var windowsFound = StringHelper.IsFound(indexOfWindows);
            var nonWindowsFound = StringHelper.IsFound(indexOfNonWindows);

            // Neither Windows nor non-Windows directory is found, 
            if (!windowsFound && !nonWindowsFound)
            {
                directorySeparator = defaultDirectorySeparator;
                return false;
            }

            // Mixed path, go with whichever separator was found first (whichever is closer to the root).
            if (windowsFound && nonWindowsFound)
            {
                var windowsBeforeNonWindows = indexOfWindows < indexOfNonWindows; // There will never be an equals case, since two different characters cannot have the same index in a string. At least until quantum computing arrives!
                if (windowsBeforeNonWindows)
                {
                    directorySeparator = DirectorySeparatorGood.WindowsValue;
                    return true;
                }
                else
                {
                    directorySeparator = DirectorySeparatorGood.NonWindowsValue;
                    return true;
                }
            }

            // At this point, either the Windows or non-Windows directory separator was found.
            if (windowsFound)
            {
                directorySeparator = DirectorySeparatorGood.WindowsValue;
                return true;
            }
            else
            {
                directorySeparator = DirectorySeparatorGood.NonWindowsValue;
                return true;
            }
        }

        /// <summary>
        /// Attempts to detect the directory separator (Windows or non-Windows) used within a path segment.
        /// Returns true if a directory separator can be detected, false otherwise.
        /// If no directory separator is detected, the output <paramref name="directorySeparator"/> is set to the <see cref="DirectorySeparatorGood.InvalidValue"/>.
        /// </summary>
        public static bool TryDetectDirectorySeparatorOrInvalid(string pathSegment, out string directorySeparator)
        {
            var output = DirectorySeparatorGood.TryDetectDirectorySeparator(pathSegment, out directorySeparator, DirectorySeparatorGood.InvalidValue);
            return output;
        }

        /// <summary>
        /// Attempts to detect the directory separator (Windows or non-Windows) used within a path segment.
        /// Returns true if a directory separator can be detected, false otherwise.
        /// If no default directory separator is detected, the output <paramref name="directorySeparator"/> is set to the <see cref="DirectorySeparatorGood.DefaultValue"/>.
        /// </summary>
        public static bool TryDetectDirectorySeparatorOrDefault(string pathSegment, out string directorySeparator)
        {
            var output = DirectorySeparatorGood.TryDetectDirectorySeparator(pathSegment, out directorySeparator, DirectorySeparatorGood.DefaultValue);
            return output;
        }

        /// <summary>
        /// Attempts to detect the directory separator (Windows or non-Windows) used within a path segment, setting the output <paramref name="directorySeparator"/> to the <see cref="DirectorySeparatorGood.DefaultValue"/> if no directory separator can be detected.
        /// Returns true if a directory separator can be detected, false otherwise.
        /// </summary>
        public static bool TryDetectDirectorySeparator(string pathSegment, out string directorySeparator)
        {
            var output = DirectorySeparatorGood.TryDetectDirectorySeparatorOrDefault(pathSegment, out directorySeparator);
            return output;
        }

        /// <summary>
        /// Detects the directory separator used in a path segment.
        /// If no directory separator can be detected, throws an exception.
        /// </summary>
        /// <exception cref="Exception">Thrown when a path segment a directory separator cannot be detected (i.e. contains no directory separators).</exception>
        public static string DetectDirectorySeparator(string pathSegment)
        {
            var detectionSuccess = DirectorySeparatorGood.TryDetectDirectorySeparator(pathSegment, out var directorySeparator);
            if (!detectionSuccess)
            {
                throw new Exception($@"Unable to detect platform for path '{pathSegment}'.");
            }

            return directorySeparator;
        }

        /// <summary>
        /// Detects the directory separator used in a path segment.
        /// If no directory separator can be detected, returns the <see cref="DirectorySeparatorGood.DefaultValue"/>.
        /// </summary>
        public static string DetectDirectorySeparatorOrDefault(string pathSegment)
        {
            DirectorySeparatorGood.TryDetectDirectorySeparatorOrDefault(pathSegment, out var directorySeparator);

            return directorySeparator;
        }

        /// <summary>
        /// Detects the directory separator used in a path segment.
        /// If a directory separator cannot be detected (for example, if the path segment is a file-name, or the un-directory-indicated relative path between parent a child directory, which is just the directory name), return the specified default.
        /// </summary>
        public static string DetectDirectorySeparatorOrDefault(string pathSegment, string defaultDirectorySeparator)
        {
            DirectorySeparatorGood.TryDetectDirectorySeparator(pathSegment, out var directorySeparator, defaultDirectorySeparator);

            return directorySeparator;
        }

        /// <summary>
        /// Detects the directory separator used in a path segment, or if no directory separator can be detected, defaults to the Windows value.
        /// </summary>
        public static string DetectDirectorySeparatorOrDefaultWindows(string pathSegment)
        {
            var directorySeparator = DirectorySeparatorGood.DetectDirectorySeparatorOrDefault(pathSegment, DirectorySeparatorGood.WindowsValue);
            return directorySeparator;
        }

        /// <summary>
        /// Detects the directory separator used in a path segment, or if no directory separator can be detected, defaults to the non-Windows value.
        /// </summary>
        public static string DetectDirectorySeparatorOrDefaultNonWindows(string pathSegment)
        {
            var directorySeparator = DirectorySeparatorGood.DetectDirectorySeparatorOrDefault(pathSegment, DirectorySeparatorGood.NonWindowsValue);
            return directorySeparator;
        }

        /// <summary>
        /// Determines if any directory separator can be detected for a path segment.
        /// </summary>
        public static bool IsDirectorySeparatorDetected(string pathSegment)
        {
            var isAnySeparatorDetected = DirectorySeparatorGood.TryDetectDirectorySeparatorOrInvalid(pathSegment, out var _);
            return isAnySeparatorDetected;
        }

        /// <summary>
        /// Determines if the specified directory separator is detected for the path segment.
        /// Unchecked - No validation is performed on the input directory separator.
        /// </summary>
        public static bool IsDirectorySeparatorDetectedUnchecked(string pathSegment, string directorySeparator)
        {
            var isAnySeparatorDetected = DirectorySeparatorGood.TryDetectDirectorySeparatorOrInvalid(pathSegment, out var outDirectorySeparator);
            if (isAnySeparatorDetected)
            {
                var isDetected = directorySeparator == outDirectorySeparator;
                return isDetected;
            }

            return false;
        }

        /// <summary>
        /// Determines if the specified directory separator is detected for the path segment.
        /// The input directory separator is validated.
        /// </summary>
        public static bool IsDirectorySeparatorDetected(string pathSegment, string directorySeparator)
        {
            DirectorySeparatorGood.Validate(directorySeparator);

            var isDetected = DirectorySeparatorGood.IsDirectorySeparatorDetectedUnchecked(pathSegment, directorySeparator);
            return isDetected;
        }

        /// <summary>
        /// Determine if the Windows directory separator is detected in a path segment.
        /// </summary>
        public static bool IsWindowsDirectorySeparatorDetected(string pathSegment)
        {
            var isWindows = DirectorySeparatorGood.IsDirectorySeparatorDetectedUnchecked(pathSegment, DirectorySeparatorGood.WindowsValue);
            return isWindows;
        }

        /// <summary>
        /// Determine if the Windows directory separator is detected in a path segment, but if no directory separator is detected, assume the Windows directory separator was detected (return true).
        /// </summary>
        public static bool IsWindowsDirectorySeparatorDetectedAssumeWindows(string pathSegment)
        {
            var directorySeparator = DirectorySeparatorGood.DetectDirectorySeparatorOrDefaultWindows(pathSegment);

            var isWindows = DirectorySeparatorGood.IsWindowsDirectorySeparator(directorySeparator);
            return isWindows;
        }

        /// <summary>
        /// Determine if the non-Windows directory separator is detected in a path segment.
        /// </summary>
        public static bool IsNonWindowsDirectorySeparatorDetected(string pathSegment)
        {
            var isNonWindows = DirectorySeparatorGood.IsDirectorySeparatorDetectedUnchecked(pathSegment, DirectorySeparatorGood.NonWindowsValue);
            return isNonWindows;
        }

        /// <summary>
        /// Determine if the non-Windows directory separator is detected in a path segment, but if no directory separator is detected, assume the non-Windows directory separator was detected (return true).
        /// </summary>
        public static bool IsNonWindowsDirectorySeparatorDetectedAssumeNonWindows(string pathSegment)
        {
            var directorySeparator = DirectorySeparatorGood.DetectDirectorySeparatorOrDefaultNonWindows(pathSegment);

            var isNonWindows = DirectorySeparatorGood.IsNonWindowsDirectorySeparator(directorySeparator);
            return isNonWindows;
        }

        /// <summary>
        /// Determine if a path-segment contains ANY directory separator.
        /// </summary>
        public static bool ContainsDirectorySeparator(string pathSegment)
        {
            var output = pathSegment.Contains(DirectorySeparatorGood.WindowsValue)
                || pathSegment.Contains(DirectorySeparatorGood.NonWindowsValue);

            return output;
        }

        /// <summary>
        /// Determines if a path segment contains a specified directory separator.
        /// Detecting the use of a directory-separator by a path-segment requires an extra step beyond just determining if the path-segment contains the directory-separator.
        /// A path-segment can contain both Windows and non-Windows directory-separators, in which case the first directory-separator is the detected directory-separator because it is dominant.
        /// Unchecked - No validation is peformed on the specified directory separator. This is simply determining if one string (the directory separator) appears within another (the path-segment).
        /// </summary>
        public static bool ContainsDirectorySeparatorUnchecked(string pathSegment, string directorySeparator)
        {
            var output = pathSegment.Contains(directorySeparator);
            return output;
        }

        /// <summary>
        /// Determines if a path segment contains a specified directory separator.
        /// Detecting the use of a directory-separator by a path-segment requires an extra step beyond just determining if the path-segment contains the directory-separator.
        /// A path-segment can contain both Windows and non-Windows directory-separators, in which case the first directory-separator is the detected directory-separator because it is dominant.
        /// Validation is performed on the directory separator.
        /// </summary>
        public static bool ContainsDirectorySeparator(string pathSegment, string directorySeparator)
        {
            DirectorySeparatorGood.Validate(directorySeparator);

            var output = DirectorySeparatorGood.ContainsDirectorySeparatorUnchecked(pathSegment, directorySeparator);
            return output;
        }

        /// <summary>
        /// Determines if a path segment contains a Windows directory separator.
        /// </summary>
        public static bool ContainsWindowsDirectorySeparator(string pathSegment)
        {
            var output = DirectorySeparatorGood.ContainsDirectorySeparatorUnchecked(pathSegment, DirectorySeparatorGood.WindowsValue);
            return output;
        }

        /// <summary>
        /// Determines if a path segment contains a Windows directory separator.
        /// </summary>
        public static bool ContainsNonWindowsDirectorySeparator(string pathSegment)
        {
            var output = DirectorySeparatorGood.ContainsDirectorySeparatorUnchecked(pathSegment, DirectorySeparatorGood.NonWindowsValue);
            return output;
        }

        /// <summary>
        /// Determines if a path segment contains a Windows directory separator.
        /// </summary>
        public static bool ContainsMixedDirectorySeparator(string pathSegment)
        {
            var output = DirectorySeparatorGood.ContainsWindowsDirectorySeparator(pathSegment)
                && DirectorySeparatorGood.ContainsNonWindowsDirectorySeparator(pathSegment);

            return output;
        }

        #endregion

        #region Miscellaneous

        public static string GetInvalidDirectorySeparatorValueExceptionMessage(string invalidDirectorySeparator)
        {
            var message = $"Invalid directory separator value.\nExpected the Windows ('{DirectorySeparatorGood.WindowsValue}') or non-Windows ('{DirectorySeparatorGood.NonWindowsValue}') directory separator value.\nFound: '{invalidDirectorySeparator}'.";
            return message;
        }

        public static Exception GetInvalidDirectorySeparatorValueException(string found)
        {
            var message = DirectorySeparatorGood.GetInvalidDirectorySeparatorValueExceptionMessage(found);

            var exception = new Exception(message);
            return exception;
        }

        public static ArgumentException GetInvalidDirectorySeparatorValueArgumentException(string found, string parameterName)
        {
            var message = DirectorySeparatorGood.GetInvalidDirectorySeparatorValueExceptionMessage(found);

            var exception = new ArgumentException(message, parameterName);
            return exception;
        }

        public static string GetWindowsDirectorySeparatorValueExpectedExceptionMessage(string notWindowsDirectorySeparator)
        {
            var message = $"Windows directory-separator value ('{DirectorySeparatorGood.WindowsValue}') expected.\nFound: '{notWindowsDirectorySeparator}'.";
            return message;
        }

        public static Exception GetWindowsDirectorySeparatorValueExpectedException(string notWindowsDirectorySeparator)
        {
            var message = DirectorySeparatorGood.GetWindowsDirectorySeparatorValueExpectedExceptionMessage(notWindowsDirectorySeparator);

            var exception = new Exception(message);
            return exception;
        }

        public static ArgumentException GetWindowsDirectorySeparatorValueExpectedArgumentException(string found, string parameterName)
        {
            var message = DirectorySeparatorGood.GetWindowsDirectorySeparatorValueExpectedExceptionMessage(found);

            var exception = new ArgumentException(message, parameterName);
            return exception;
        }

        public static string GetNonWindowsDirectorySeparatorValueExpectedExceptionMessage(string notNonWindowsDirectorySeparator)
        {
            var message = $"Non-Windows directory-separator value ('{DirectorySeparatorGood.NonWindowsValue}') expected.\nFound: '{notNonWindowsDirectorySeparator}'.";
            return message;
        }

        public static Exception GetNonWindowsDirectorySeparatorValueExpectedException(string notNonWindowsDirectorySeparator)
        {
            var message = DirectorySeparatorGood.GetNonWindowsDirectorySeparatorValueExpectedExceptionMessage(notNonWindowsDirectorySeparator);

            var exception = new Exception(message);
            return exception;
        }

        public static ArgumentException GetNonWindowsDirectorySeparatorValueExpectedArgumentException(string found, string parameterName)
        {
            var message = DirectorySeparatorGood.GetNonWindowsDirectorySeparatorValueExpectedExceptionMessage(found);

            var exception = new ArgumentException(message, parameterName);
            return exception;
        }

        #endregion
    }
}
