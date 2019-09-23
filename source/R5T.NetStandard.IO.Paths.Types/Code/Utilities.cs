using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using R5T.NetStandard.IO.Paths.Extensions;

using BaseUtilities = R5T.NetStandard.IO.Paths.Base.Utilities;


namespace R5T.NetStandard.IO.Paths
{
    /// <summary>
    /// Contains utilities for:
    /// * <see cref="Path"/> wrappers that add documentation and discussion to the System path utilities.
    /// * Operating on paths as string.
    /// * Operation on strongly-typed paths.
    /// </summary>
    public static class Utilities
    {
        #region System.IO.Path Wrappers

        /// <summary>
        /// Returns the <see cref="Path.AltDirectorySeparatorChar"/> value.
        /// Separates directory names in a hierarchical path, the alternative separator for systems following a different convention. This is '/' on Windows.
        /// Example: "/mnt/efs/temp.txt".
        /// </summary>
        public static readonly char AltDirectorySeparatorCharSystem = Path.AltDirectorySeparatorChar;
        /// <summary>
        /// Returns the <see cref="Path.DirectorySeparatorChar"/> value.
        /// Separates directory names in a hierarchical path. This is '\' on Windows.
        /// Example: "C:\temp\temp1\temp2\temp.txt".
        /// </summary>
        public static readonly char DirectorySeparatorCharSystem = Path.DirectorySeparatorChar;
        /// <summary>
        /// Returns the <see cref="Path.PathSeparator"/> value.
        /// Separates separate paths in an environment variable value. Generally ';' on Windows.
        /// Example: "C:\temp1;C:\temp2".
        /// </summary>
        public static readonly char PathSeparatorSystem = Path.PathSeparator;
        /// <summary>
        /// Returns the <see cref="Path.VolumeSeparatorChar"/> value.
        /// Separates the drive (or volume) token from the path. Generally ':' on Windows.
        /// Example: "C:\temp\temp.txt".
        /// </summary>
        public static readonly char VolumeSeparatorCharSystem = Path.VolumeSeparatorChar;
        //public static readonly char[] InvalidPathCharsSystem = Path.InvalidPathChars; // Obsolete.


        /// <summary>
        /// Wraps <see cref="Path.ChangeExtension(string, string)"/>. Changes the extension of the input file path.
        /// Example: ("C:\temp\temp.txt", "jpg") -> "C:\temp\temp.jpg"
        /// The file extension separator can be included, example: ("C:\temp\temp.txt", ".jpeg") -> "C:\temp\temp.jpeg"
        /// </summary>
        public static string ChangeExtensionSystem(string filePath, string extension)
        {
            var output = Path.ChangeExtension(filePath, extension);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.Combine(string[])"/>. Combines path segments into a single path.
        /// Example: (@"C:\", @"temp", @"temp.txt") -> "C:\temp\temp.txt".
        /// The method is broken and limited.
        /// Broken in that if any of the path segments startup with the platform directory separator, segments before them are ignored!
        /// Limited in that the executing platform directory separator will be used with no possibility of overloading to allow, for example, creating Linux paths on a Windows machine or Windows paths on a Linux machine.
        /// </summary>
        /// <remarks>
        /// For best results, make sure path segments do not start with the platform separator:
        /// Example: (@"C:\", @"\temp", @"temp.txt") -> "\temp\temp.txt".
        /// Example: (@"C:\", @"\temp", @"\temp.txt") -> "\temp.txt".
        /// 
        /// Uses current platform path separator with no overload to specify the path separator. This is a prolematic limitation.
        /// Example: (@"/mnt", @"efs", @"temp.txt") -> "/mnt\efs\temp.txt".
        /// </remarks>
        public static string CombineSystem(params string[] pathSegments)
        {
            var output = Path.Combine(pathSegments);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetDirectoryName(string)"/>. Returns full directory path.
        /// This System method is mis-named. It should be GetDirectoryPath().
        /// Example: (@"C:\temp\temp.txt") -> "C:\temp".
        /// The returned string will use the executing platform path separator, and if a directory path is given (path ends with a directory separator), the returned path with lack the path separator. 
        /// </summary>
        /// <remarks>
        /// Example: (@"C:\temp\temp") -> "C:\temp".
        /// Example: (@"C:\temp\temp\") -> "C:\temp\temp".
        /// Example: (@"/mnt/efs/temp.txt") -> "\mnt\efs" (on Windows).
        /// Example: (@"/mnt/efs/temp/") -> "\mnt\efs\temp" (on Windows).
        /// </remarks>
        public static string GetDirectoryNameSystem(string path)
        {
            var output = Path.GetDirectoryName(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetExtension(string)"/>. Returns the extension of the input file path.
        /// Example: (@"C:\temp\temp.txt") -> ".txt".
        /// Includes the file extension separator char '.', and does not change any capitalization.
        /// </summary>
        public static string GetExtensionSystem(string filePath)
        {
            var output = Path.GetExtension(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFileName(string)"/>. Returns the file-name and extension of the input file path.
        /// Example: (@"C:\temp\temp.txt") -> "temp.txt".
        /// This System method works the way it should.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "temp.txt".
        /// </remarks>
        public static string GetFileNameSystem(string filePath)
        {
            var output = Path.GetFileName(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFileNameWithoutExtension(string)"/>. Returns only the file-name (without file extension or file extension separator).
        /// Example: (@"C:\temp\temp.txt") -> "temp".
        /// This System method works the way it should.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "temp".
        /// </remarks>
        public static string GetFileNameWithoutExtensionSystem(string filePath)
        {
            var output = Path.GetFileNameWithoutExtension(filePath);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetFullPath(string)"/>. Prefixes the input path with either the current directory, or the current root drive (if the input starts with a path separator).
        /// Example: (@"temp.txt") -> "{Current Directory}\temp.txt".
        /// Example: (@"\temp.txt") -> "C:\temp.txt".
        /// A weird method with schizophrenic behavior based on whether the input begins with a path separator.
        /// </summary>
        /// <remarks>
        /// Example: (@"/temp.txt") -> "C:\temp.txt" (on Windows).
        /// </remarks>
        public static string GetFullPathSystem(string path)
        {
            var output = Path.GetFullPath(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetInvalidFileNameChars"/>. Returns the chars that are invalid in file-names on the currently executing platform.
        /// Example: () -> ",&lt;,&gt;,|,:,*,?,\,/ on Windows, plus ~35 that are not printable in the console window.
        /// This method should have overload that allows inputting a platform to get invalid file name characters for that platform.
        /// </summary>
        public static char[] GetInvalidFileNameCharsSystem()
        {
            var output = Path.GetInvalidFileNameChars();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetInvalidPathChars"/>. Returns the chars that are invalid in paths on the currently executing platform.
        /// Example: () -> |, plus ~35 that are not printable in the console window.
        /// This method should have overload that allows inputting a platform to get invalid path characters for that platform.
        /// </summary>
        public static char[] GetInvalidPathCharsSystem()
        {
            var output = Path.GetInvalidPathChars();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetPathRoot(string)"/>. Returns just the root of a path (generally a drive letter).
        /// Example: (@"C:\temp\temp.txt") -> "C:\".
        /// Does not work with non-Windows paths, at least when the currently executing platform is Windows.
        /// </summary>
        /// <remarks>
        /// Example: (@"/mnt/efs/temp.txt") -> "\".
        /// </remarks>
        public static string GetPathRootSystem(string path)
        {
            var output = Path.GetPathRoot(path);
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetRandomFileName()"/>. Returns a cryptographically-secure path segment like 'ulcdtig4.v53'
        /// Example () -> "ulcdtig4.v53".
        /// What's with the '.'? The function seems to be trying to produce a file-name with a random file extension. Instead it should produce a random path segment with no file extension separator.
        /// </summary>
        public static string GetRandomFileNameSystem()
        {
            var output = Path.GetRandomFileName();
            return output;
        }

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        // See: https://github.com/dotnet/standard/issues/962
        // Issue is .NET Core 2.1 (class library) has the new methods, while .NET Standard 2.0 does not. Note that .NET Standard 2.1 will have the methods, but it is not yet out!
        //public static string GetRelativePathSystem(string source, string path)
        //{
        //    var output = Path.GetRelativePath(relativeTo, path);
        //    return output;
        //}

        /// <summary>
        /// Wraps <see cref="Path.GetTempFileName()"/>. Returns the path to an actually created temporary file, with a temporary file name, in the %TEMP% directory (../{User}/AppData/Local/Temp).
        /// Example: () -> "C:\Users\david\AppData\Local\Temp\tmpB013.tmp"
        /// This method is a disaster. 1) It should *NOT* create 0 KB file at the given path! 2) it should just return the temp file-name ("tmpB013.tmp"), not the whole path. 3) It should include an -Path() method that takes a directory path to which to add the temp file-name.
        /// </summary>
        public static string GetTempFileNameSystem()
        {
            var output = Path.GetTempFileName();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.GetTempPath()"/>. Returns the directory-path of the current user's Temp directory.
        /// Example: () -> C:\Users\david\AppData\Local\Temp\
        /// This method should be called "GetTempDirectoryPath", but otherwise works as advertised.
        /// </summary>
        public static string GetTempPathSystem()
        {
            var output = Path.GetTempPath();
            return output;
        }

        /// <summary>
        /// Wraps <see cref="Path.HasExtension(string)"/>.
        /// Litterally just returns whether or not the given path has a file extension (this is useful in determining whether a path should be assumed to be a file-path or a directory-path).
        /// Example: ("C:\temp\temp.txt") -> True.
        /// Example: ("C:\temp\temp") -> False.
        /// This method should have an overload that allows testing whether a path has a specific file-extension.
        /// </summary>
        /// <remarks>
        /// Example: ("C:\temp\temp.") -> False.
        /// </remarks>
        public static bool HasExtensionSystem(string path)
        {
            var output = Path.HasExtension(path);
            return output;
        }

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        ///// <summary>
        ///// Returns whether the path is a Windows path and fully qualified.
        ///// Example: ("C:\temp\temp.txt") -> True.
        ///// This method does not work with non-Windows paths.
        ///// </summary>
        ///// <remarks>
        ///// Example: ("/mnt/efs/temp.txt") -> False (wrongly).
        ///// </remarks>
        //public static void IsPathFullyQualifiedSystem(string path)
        //{
        //    var output = Path.IsPathFullyQualified
        //}

        /// <summary>
        /// Wraps <see cref="Path.IsPathRooted(string)"/>.
        /// Returns whether the path starts with a Windows root drive, or with a non-Windows path separator.
        /// Example: ("C:\temp\temp.txt") -> True.
        /// Example: ("/mnt/efs/temp.txt") -> True.
        /// This method works well.
        /// </summary>
        public static bool IsPathRootedSystem(string path)
        {
            var output = Path.IsPathRooted(path);
            return output;
        }

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        //Join()

        // Somehow the System.IO.Path in the System.Runtime.Extensions assembly is different than the System.IO.Path in the netstandard assembly?
        //TryJoin()

        #endregion

        #region Separators

        /// <summary>
        /// Returns the System <see cref="Path.DirectorySeparatorChar"/> as the default.
        /// </summary>
        public static string GetDefaultDirectorySeparatorValue()
        {
            var output = Utilities.DirectorySeparatorCharSystem.ToString();
            return output;
        }

        /// <summary>
        /// Finds the first instance of a directory separator (whether Windows or non-Windows).
        /// A path might have both Windows and non-Windows directory separators. But whichever occurs first in the path is more dominant (closer to the root), so that is the path's directory separator.
        /// Returns true if the a directory separator can be detected.
        /// If no directory separator can be detected, the output <paramref name="directorySeparator"/> is null.
        /// </summary>
        public static bool TryDetectDirectorySeparator(string path, out string directorySeparator, string defaultDirectorySeparator)
        {
            var indexOfWindows = path.IndexOf(Constants.WindowsDirectorySeparator);
            var indexOfNonWindows = path.IndexOf(Constants.NonWindowsDirectorySeparator);

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
                    directorySeparator = Constants.WindowsDirectorySeparator;
                    return true;
                }
                else
                {
                    directorySeparator = Constants.NonWindowsDirectorySeparator;
                    return true;
                }
            }

            // At this point, either the Windows or non-Windows directory separator was found.
            if(windowsFound)
            {
                directorySeparator = Constants.WindowsDirectorySeparator;
                return true;
            }
            else
            {
                directorySeparator = Constants.NonWindowsDirectorySeparator;
                return true;
            }
        }

        public static bool TryDetectDirectorySeparatorOrInvalid(string path, out string directorySeparator)
        {
            var output = Utilities.TryDetectDirectorySeparator(path, out directorySeparator, DirectorySeparator.InvalidValue);
            return output;
        }

        public static bool TryDetectDirectorySeparatorOrDefault(string path, out string directorySeparator)
        {
            var defaultDirectorySeparator = Utilities.GetDefaultDirectorySeparatorValue();

            var output = Utilities.TryDetectDirectorySeparator(path, out directorySeparator, defaultDirectorySeparator);
            return output;
        }

        /// <summary>
        /// Uses the default directory separator.
        /// </summary>
        public static bool TryDetectDirectorySeparator(string path, out string directorySeparator)
        {
            var output = Utilities.TryDetectDirectorySeparatorOrDefault(path, out directorySeparator);
            return output;
        }

        /// <summary>
        /// Detect a directory separator in the path, or throws an exception.
        /// </summary>
        public static string DetectDirectorySeparator(string path)
        {
            var detectionSuccess = Utilities.TryDetectDirectorySeparator(path, out var directorySeparator);
            if(!detectionSuccess)
            {
                throw new Exception($@"Unable to detect platform for path '{path}'.");
            }

            return directorySeparator;
        }

        /// <summary>
        /// Detect a directory separator in the path, or uses the default.
        /// </summary>
        public static string DetectDirectorySeparatorOrDefault(string path)
        {
            Utilities.TryDetectDirectorySeparatorOrDefault(path, out var directorySeparator);

            return directorySeparator;
        }

        /// <summary>
        /// If a directory separator cannot be detected (for example, if the relative path between two directories is just the un-indicated directory name "DirectoryName"), return the specified default.
        /// </summary>
        public static string DetectDirectorySeparatorSpecifyDefault(string path, string defaultDirectorySeparator)
        {
            var detectionSuccess = Utilities.TryDetectDirectorySeparator(path, out var directorySeparator);
            if (!detectionSuccess)
            {
                return defaultDirectorySeparator;
            }

            return directorySeparator;
        }

        /// <summary>
        /// Detect a directory separator or default to Windows.
        /// </summary>
        public static string DetectDirectorySeparatorDefaultWindows(string path)
        {
            var directorySeparator = Utilities.DetectDirectorySeparatorSpecifyDefault(path, Constants.WindowsDirectorySeparator);
            return directorySeparator;
        }

        /// <summary>
        /// Detect a directory separator or default to non-Windows.
        /// </summary>
        public static string DetectDirectorySeparatorDefaultNonWindows(string path)
        {
            var directorySeparator = Utilities.DetectDirectorySeparatorSpecifyDefault(path, Constants.NonWindowsDirectorySeparator);
            return directorySeparator;
        }

        /// <summary>
        /// Determine if the Windows directory separator was detected.
        /// </summary>
        public static bool WindowsDirectorySeparatorDetected(string path)
        {
            Utilities.TryDetectDirectorySeparator(path, out var directorySeparator);

            var output = directorySeparator == Constants.WindowsDirectorySeparator;
            return output;
        }

        /// <summary>
        /// Determine if the Windows directory separator was detected, but if no directory separators were detected, assume the Windows directory separator was detected.
        /// </summary>
        public static bool WindowsDirectorySeparatorDetectedDefaultWindows(string path)
        {
            var directorySeparator = Utilities.DetectDirectorySeparatorDefaultWindows(path);

            var output = directorySeparator == Constants.WindowsDirectorySeparator;
            return output;
        }

        /// <summary>
        /// Determine if the non-Windows directory separator was detected.
        /// </summary>
        public static bool NonWindowsDirectorySeparatorDetected(string path)
        {
            Utilities.TryDetectDirectorySeparator(path, out var directorySeparator);

            var output = directorySeparator == Constants.NonWindowsDirectorySeparator;
            return output;
        }

        /// <summary>
        /// Determine if the non-Windows directory separator was detected, but if no directory separators were detected, assume the non-Windows directory separator was detected.
        /// </summary>
        public static bool NonWindowsDirectorySeparatorDetectedDefaultNonWindows(string path)
        {
            var directorySeparator = Utilities.DetectDirectorySeparatorDefaultNonWindows(path);

            var output = directorySeparator == Constants.NonWindowsDirectorySeparator;
            return output;
        }

        /// <summary>
        /// Between the Windows ('\') and the non-Windows ('/') directory separator, given one, return the other.
        /// If the input directory separator is neither the Windows nor non-Windows separator, the Windows separator is returned.
        /// </summary>
        public static string GetAlternateDirectorySeparator(string directorySeparator)
        {
            if (directorySeparator == Constants.WindowsDirectorySeparator)
            {
                return Constants.NonWindowsDirectorySeparator;
            }
            else
            {
                return Constants.WindowsDirectorySeparator;
            }
        }

        /// <summary>
        /// Ensures that the output path uses the specified path separator.
        /// </summary>
        public static string EnsureDirectorySeparator(string path, string directorySeparator)
        {
            var alternateDirectorySeparator = Utilities.GetAlternateDirectorySeparator(directorySeparator);

            var output = path.Replace(alternateDirectorySeparator, directorySeparator);
            return output;
        }

        /// <summary>
        /// Replaces all instances of <see cref="Constants.NonWindowsDirectorySeparator"/> with <see cref="Constants.WindowsDirectorySeparator"/>.
        /// </summary>
        public static string EnsureWindowsDirectorySeparator(string path)
        {
            var output = Utilities.EnsureDirectorySeparator(path, Constants.WindowsDirectorySeparator);
            return output;
        }

        /// <summary>
        /// Replaces all <see cref="Constants.WindowsDirectorySeparator"/> with <see cref="Constants.NonWindowsDirectorySeparator"/>.
        /// </summary>
        public static string EnsureNonWindowsDirectorySeparator(string path)
        {
            var output = Utilities.EnsureDirectorySeparator(path, Constants.NonWindowsDirectorySeparator);
            return output;
        }

        #endregion

        #region Strongly-Typed Separators

        public static DirectorySeparator GetDefaultDirectorySeparator()
        {
            var defaultDirectorySeparator = Utilities.GetDefaultDirectorySeparatorValue().AsDirectorySeparator();
            return defaultDirectorySeparator;
        }

        //public static DirectorySeparator DetectDirectorySeparator(PathSegment pathSegment)
        //{
        //    var directorySeparatorValue = Utilities.DetectDirectorySeparator(pathSegment.Value);

        //    if(directorySeparatorValue == DirectorySeparator.DefaultNonWindows.Value)
        //    {
        //        return DirectorySeparator.DefaultNonWindows;
        //    }
        //    else
        //    {
        //        return DirectorySeparator.DefaultWindows;
        //    }
        //}

        #endregion

        #region Paths as Strings

        public static bool ExistsFilePath(string filePath)
        {
            var output = File.Exists(filePath);
            return output;
        }

        public static bool ExistsDirectoryPath(string directoryPath)
        {
            var output = Directory.Exists(directoryPath);
            return output;
        }

        public static void DeleteFilePath(string filePath)
        {
            File.Delete(filePath);
        }

        public static void DeleteDirectoryPath(string directoryPath, bool recursive = true)
        {
            Directory.Delete(directoryPath, recursive);
        }

        /// <summary>
        /// If a Windows directory separator is detected in the path (see <see cref="Utilities.WindowsDirectorySeparatorDetected(string)"/>), then it is a Windows path.
        /// If no directory separator is detected (for example, if the path is just a directory name), assume Windows.
        /// </summary>
        public static bool IsWindowsPathDefault(string path)
        {
            var output = Utilities.WindowsDirectorySeparatorDetectedDefaultWindows(path);
            return output;
        }

        /// <summary>
        /// If a Windows directory separator is detected in the path (see <see cref="Utilities.WindowsDirectorySeparatorDetected(string)"/>), then it is a Windows path.
        /// If no directory separator is detected, don't assume Windows.
        /// </summary>
        public static bool IsWindowsPathStrict(string path)
        {
            var output = Utilities.WindowsDirectorySeparatorDetected(path);
            return output;
        }

        /// <summary>
        /// Base method uses the default method.
        /// </summary>
        public static bool IsWindowsPath(string path)
        {
            var output = Utilities.IsWindowsPathDefault(path);
            return output;
        }

        public static string EnsureWindowsPath(string path)
        {
            var windowsPath = Utilities.EnsureWindowsDirectorySeparator(path);
            return windowsPath;
        }

        /// <summary>
        /// If a non-Windows directory separator is detected in the path (see <see cref="Utilities.NonWindowsDirectorySeparatorDetected(string)"/>), then it is a non-Windows path.
        /// If no directory separator is detected (for example, if the path is just a directory name), assume non-Windows.
        /// </summary>
        public static bool IsNonWindowsPathDefault(string path)
        {
            var output = Utilities.NonWindowsDirectorySeparatorDetectedDefaultNonWindows(path);
            return output;
        }

        /// <summary>
        /// If a non-Windows directory separator is detected in the path (see <see cref="Utilities.NonWindowsDirectorySeparatorDetected(string)"/>), then it is a non-Windows path.
        /// /// If no directory separator is detected, don't assume non-Windows.
        /// </summary>
        public static bool IsNonWindowsPathStrict(string path)
        {
            var output = Utilities.NonWindowsDirectorySeparatorDetected(path);
            return output;
        }

        /// <summary>
        /// Base method uses the default method.
        /// </summary>
        public static bool IsNonWindowsPath(string path)
        {
            var output = Utilities.IsNonWindowsPathDefault(path);
            return output;
        }

        public static string EnsureNonWindowsPath(string path)
        {
            var nonWindowsPath = Utilities.EnsureNonWindowsDirectorySeparator(path);
            return nonWindowsPath;
        }

        /// <summary>
        /// Determines if a a path is directory indicated (if it ends with a directory separator).
        /// </summary>
        public static bool IsPathDirectoryIndicated(string path)
        {
            var lastChar = path.Last();

            var lastCharIsDirectorySeparator = DirectorySeparator.IsDirectorySeparator(lastChar);
            return lastCharIsDirectorySeparator;
        }

        /// <summary>
        /// If a path is not directory-indicated (ends with a directory separator), then it is possibly a file path.
        /// NOTE! This is only true if the caller has taken steps to ensure it is true! A directory path may NOT end in a directory separator. At the string-level how can a file path string be differentiated from a directory-path string? Directories ARE files!
        /// </summary>
        public static bool IsFilePath(string path)
        {
            var isFilePath = !Utilities.IsPathDirectoryIndicated(path);
            return isFilePath;
        }

        /// <summary>
        /// Determines if the destination of a path is a directory.
        /// Note: the path must exist!
        /// </summary>
        /// <remarks>
        /// See: https://stackoverflow.com/questions/1395205/better-way-to-check-if-a-path-is-a-file-or-a-directory
        /// </remarks>
        public static bool IsDirectory(string path)
        {
            var output = File.GetAttributes(path).HasFlag(FileAttributes.Directory);
            return output;
        }

        /// <summary>
        /// Determines if the destination of a path is a file.
        /// </summary>
        public static bool IsFile(string path)
        {
            var isDirectory = Utilities.IsDirectory(path);

            var output = !isDirectory;
            return output;
        }

        /// <summary>
        /// If a directory contains only directories, recursively, then 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static bool IsDirectoryRecursivelyEmpty(string directoryPath)
        {
            var countOfFiles = Directory.EnumerateFiles(directoryPath, SearchPatternHelper.All, SearchOption.AllDirectories).Count();

            var output = countOfFiles == 0;
            return output;
        }

        /// <summary>
        /// If a path is directory-indicated (ends with a directory separator), then is it possibly a directory path.
        /// NOTE! This is only true if the caller has taken steps to ensure it is true! A directory path may NOT end in a directory separator. At the string-level how can a file path string be differentiated from a directory-path string? Directories ARE files!
        /// </summary>
        public static bool IsDirectoryPath(string path)
        {
            var isFilePath = Utilities.IsPathDirectoryIndicated(path);
            return isFilePath;
        }

        /// <summary>
        /// File paths never end in a directory separator.
        /// </summary>
        public static string EnsureFilePathNotDirectoryIndicated(string filePath)
        {
            var output = Utilities.EnsurePathIsNotDirectoryIndicated(filePath);
            return output;
        }

        public static string EnsureDirectoryPathIsDirectoryIndicated(string directoryPath, string directorySeparator)
        {
            var lastCharIsDirectorySeparator = Utilities.IsPathDirectoryIndicated(directoryPath);
            if (!lastCharIsDirectorySeparator)
            {
                var output = directoryPath.Append(directorySeparator);
                return output;
            }

            return directoryPath;
        }

        public static string EnsureDirectoryPathIsDirectoryIndicated(string directoryPath)
        {
            var lastCharIsDirectorySeparator = Utilities.IsPathDirectoryIndicated(directoryPath);
            if (!lastCharIsDirectorySeparator)
            {
                var directorySeparator = Utilities.DetectDirectorySeparator(directoryPath);

                var output = Utilities.MakePathDirectoryIndicatedUnchecked(directoryPath, directorySeparator);
                return output;
            }

            return directoryPath;
        }

        public static string EnsurePathIsNotDirectoryIndicated(string path)
        {
            var lastCharIsDirectorySeparator = Utilities.IsPathDirectoryIndicated(path);
            if (lastCharIsDirectorySeparator)
            {
                var output = path.ExceptLast();
                return output;
            }

            return path;
        }

        /// <summary>
        /// Makes a path directory-indicated by appending the specified directory separator to the directory path.
        /// Unchecked - Performs no validation on the directory path to ensure it does not already end with a directory separator.
        /// </summary>
        public static string MakePathDirectoryIndicatedUnchecked(string directoryPath, string directorySeparator)
        {
            var output = directoryPath.Append(directorySeparator);
            return output;
        }

        public static bool IsPathRootIndicated(string path)
        {
            var pathBeginsWithRooting = Regex.IsMatch(path, Constants.RootIndicatedPathRegexPattern);
            return pathBeginsWithRooting;
        }

        public static string EnsureRootedPathIsRootIndicated(string path)
        {
            var isPathRootIndicated = Utilities.IsPathRootIndicated(path);
            if(!isPathRootIndicated)
            {
                var directorySeparator = Utilities.DetectDirectorySeparator(path);

                var output = path.Prefix(directorySeparator);
                return output;
            }

            return path;
        }

        public static string EnsureRelativePathIsNotRootIndicated(string path)
        {
            var isPathRootIndicated = Utilities.IsPathRootIndicated(path);
            if(isPathRootIndicated)
            {
                var directorySeparator = Utilities.DetectDirectorySeparator(path);

                var indexOfFirstDirectorySeparator = path.IndexOf(directorySeparator); // Note, will be found, else directory separator would not have been detected.

                var output = path.Substring(indexOfFirstDirectorySeparator + 1); // Everything after the first directory separator.
                return output;
            }

            return path;
        }

        /// <summary>
        /// If a path is root-indicated (see <see cref="Utilities.IsPathRootIndicated(string)"/>, then it is an absolute path.
        /// </summary>
        public static bool IsAbsolutePath(string path)
        {
            var output = Utilities.IsPathRootIndicated(path);
            return output;
        }

        /// <summary>
        /// If a path is not root-indicated (see <see cref="Utilities.IsPathRootIndicated(string)"/>, then it is a relative path.
        /// </summary>
        public static bool IsRelativePath(string path)
        {
            var output = !Utilities.IsPathRootIndicated(path);
            return output;
        }

        public static string GetRelativePathUsingUriMakeRelativeUri(string sourcePath, string destinationPath)
        {
            var sourceUri = new Uri(new Uri("file://"), sourcePath);
            var destinationUri = new Uri(new Uri("file://"), destinationPath);

            var relativeUri = sourceUri.MakeRelativeUri(destinationUri);

            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath;
        }

        public static string GetRelativePathOutputWindowsIfWindows(string sourcePath, string destinationPath)
        {
            var relativePath = Utilities.GetRelativePathUsingUriMakeRelativeUri(sourcePath, destinationPath);

            // The Uri.MakeRelativeUri() method outputs the non-Windows directory separator. If the input source path was a Windows path, output a Windows path.
            var isWindowsPath = Utilities.IsWindowsPathStrict(sourcePath);
            if(isWindowsPath)
            {
                var ensuredWindowsRelativePath = Utilities.EnsureWindowsDirectorySeparator(relativePath);
                return ensuredWindowsRelativePath;
            }

            return relativePath;
        }

        public static string GetRelativePathFileToFile(string sourceFilePath, string destinationFilePath)
        {
            var relativePath = Utilities.GetRelativePathOutputWindowsIfWindows(sourceFilePath, destinationFilePath);

            // If the source file is the same as the destination file (empty relative path), do not perform any adjustment of the relative path.
            if (relativePath == String.Empty)
            {
                return relativePath;
            }

            var adjustedRelativePath = Utilities.AdjustRelativePathForFileSource(sourceFilePath, relativePath);
            return adjustedRelativePath;
        }

        public static string AdjustRelativePathForFileSource(string sourceFilePath, string relativePath)
        {
            // The Uri.MakeRelativeUri() output requires special handling for file path sources since it always produces paths relative to the most derived directory path (which for a file path is the path of the directory containing the file).
            var directorySeparator = Constants.NonWindowsDirectorySeparator; // The Uri.MakeRelativeUri() method always produces paths using the non-Windows directory separator.
            var isWindowsPath = Utilities.IsWindowsPathStrict(sourceFilePath);
            if (isWindowsPath)
            {
                directorySeparator = Constants.WindowsDirectorySeparator;
            }

            var prefix = $"{Constants.DefaultParentDirectoryName}{directorySeparator}";

            var prefixedRelativePath = relativePath.Prefix(prefix);
            return prefixedRelativePath;
        }

        public static string GetRelativePathFileToDirectory(string sourceFilePath, string destinationDirectoryPath)
        {
            // The Uri.MakeRelativeUri() method requires directory paths to be directory-indicated, else the path is assumed to be a file path. This only matters for the source path.
            var directoryIndicatedDestinationDirectoryPath = Utilities.EnsureDirectoryPathIsDirectoryIndicated(destinationDirectoryPath);

            var relativePath = Utilities.GetRelativePathOutputWindowsIfWindows(sourceFilePath, destinationDirectoryPath);
            return relativePath;
        }

        public static string GetRelativePathDirectoryToFile(string sourceDirectoryPath, string destinationFilePath)
        {
            // The Uri.MakeRelativeUri() method requires directory paths to be directory-indicated, else the path is assumed to be a file path. This only matters for the source path.
            var directoryIndicatedSourceDirectoryPath = Utilities.EnsureDirectoryPathIsDirectoryIndicated(sourceDirectoryPath);

            var relativePath = Utilities.GetRelativePathOutputWindowsIfWindows(directoryIndicatedSourceDirectoryPath, destinationFilePath);
            return relativePath;
        }

        public static string GetRelativePathDirectoryToDirectory(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            // The Uri.MakeRelativeUri() method requires directory paths to be directory-indicated, else the path is assumed to be a file path. This only matters for the source path.
            var directoryIndicatedSourceDirectoryPath = Utilities.EnsureDirectoryPathIsDirectoryIndicated(sourceDirectoryPath);
            var directoryIndicatedDestinationDirectoryPath = Utilities.EnsureDirectoryPathIsDirectoryIndicated(destinationDirectoryPath);

            var relativePath = Utilities.GetRelativePathOutputWindowsIfWindows(directoryIndicatedSourceDirectoryPath, directoryIndicatedDestinationDirectoryPath);
            return relativePath;
        }

        public static string GetRelativePath(string sourcePath, string destinationPath)
        {
            var sourceIsFilePath = Utilities.IsFilePath(sourcePath);
            var destinationIsFilePath = Utilities.IsFilePath(sourcePath);

            string relativePath;
            if(sourceIsFilePath)
            {
                if (destinationIsFilePath)
                {
                    relativePath = Utilities.GetRelativePathFileToFile(sourcePath, destinationPath);
                }
                else
                {
                    relativePath = Utilities.GetRelativePathFileToDirectory(sourcePath, destinationPath);
                }
            }
            else
            {
                if(destinationIsFilePath)
                {
                    relativePath = Utilities.GetRelativePathDirectoryToFile(sourcePath, destinationPath);
                }
                else
                {
                    relativePath = Utilities.GetRelativePathDirectoryToDirectory(sourcePath, destinationPath);
                }
            }

            return relativePath;
        }

        public static string GetUnresolvedPath(string fromPath, string relativePath, string directorySeparator)
        {
            var ensuredFromPath = Utilities.EnsureDirectorySeparator(fromPath, directorySeparator);
            var ensuredRelativePath = Utilities.EnsureDirectorySeparator(relativePath, directorySeparator);

            // Treat the source path as a file path by removing any ending directory separator (in case the source path is an indicated directory path).
            var ensuredFromPathAsFilePath = Utilities.EnsureFilePathNotDirectoryIndicated(fromPath);

            var unresolvedFilePath = Utilities.CombineSimple(ensuredFromPathAsFilePath, ensuredRelativePath, directorySeparator);
            return unresolvedFilePath;
        }

        /// <summary>
        /// Detects the directory separator using the from path.
        /// </summary>
        public static string GetUnresolvedPath(string fromPath, string relativePath)
        {
            var directorySeparator = Utilities.DetectDirectorySeparator(fromPath);

            var unresolvedPath = Utilities.GetUnresolvedPath(fromPath, relativePath, directorySeparator);
            return unresolvedPath;
        }

        public static string ResolvePathUsingUriLocalPath(string unresolvedPath)
        {
            try
            {
                var unresolvedUri = new Uri(new Uri("file://"), unresolvedPath);

                var resolvedPath = unresolvedUri.LocalPath;
                return resolvedPath;
            }
            catch (UriFormatException uriFormatException)
            {
                var message = $"Failed to resolve path: {unresolvedPath}";
                throw new ArgumentException(message, nameof(unresolvedPath), uriFormatException);
            }
        }

        public static string ResolvePathUsingCustomLogic(string unresolvedPath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Given an unresolved path (ex: "C:\Temp1\Temp2\..\Temp3\Temp4.txt"), get a resolved path (ex: "C:\Temp1\Temp3\Temp4.txt").
        /// </summary>
        public static string ResolvePath(string unresolvedPath)
        {
            var resolvedPath = Utilities.ResolvePathUsingUriLocalPath(unresolvedPath);
            return resolvedPath;
        }

        public static string ResolveFilePathUsingUriLocalPath(string unresolvedFilePath)
        {
            var resolvedFilePath = Utilities.ResolvePathUsingUriLocalPath(unresolvedFilePath);

            var actualResolvedFilePath = Utilities.EnsureFilePathNotDirectoryIndicated(resolvedFilePath);
            return actualResolvedFilePath;
        }

        /// <summary>
        /// If the unresolved path is known to be the path of a file.
        /// </summary>
        public static string ResolveFilePath(string unresolvedFilePath)
        {
            var resolvedFilePath = Utilities.ResolveFilePathUsingUriLocalPath(unresolvedFilePath);
            return resolvedFilePath;
        }

        public static string ResolveDirectoryPath(string unresolvedDirectoryPath)
        {
            var resolvedDirectoryPath = Utilities.ResolvePath(unresolvedDirectoryPath);
            return resolvedDirectoryPath;
        }

        public static string Combine(string path1, string path2)
        {
            var directorySeparator = Utilities.DetectDirectorySeparatorOrDefault(path1);

            var combinedPath = Utilities.CombineSimpleUnchecked(path1, path2, directorySeparator);
            return combinedPath;
        }

        /// <summary>
        /// Combines two path segments into a single path segment.
        /// Simple - No directory separator is inserted between the paths (the first path segment is assumed to end with the required directory separator).
        /// Unchecked - No check is made to ensure the first path segment does, in fact, end with a directory separator.
        /// This literally just concatenates the two path segment strings, and returns outputPathSegment = <paramref name="directoryIndicatedPathSegment1"/> + <paramref name="pathSegment2"/>.
        /// </summary>
        public static string CombineSimpleUnchecked(string directoryIndicatedPathSegment1, string pathSegment2)
        {
            var outputPathSegment = directoryIndicatedPathSegment1 + pathSegment2;
            return outputPathSegment;
        }

        /// <summary>
        /// Simply concatenates two paths using the specified directory.
        /// Returns {<paramref name="path1"/>}{<paramref name="directorySeparator"/>}{<paramref name="path2"/>}.
        /// Performs no checks, and assumes that the specified directory separator is actually a directory separator.
        /// </summary>
        public static string CombineSimpleUnchecked(string path1, string path2, string directorySeparator)
        {
            var output = $"{path1}{directorySeparator}{path2}";
            return output;
        }

        /// <summary>
        /// Simply concatenates two paths using the specified directory.
        /// Returns {<paramref name="path1"/>}{<paramref name="directorySeparator"/>}{<paramref name="path2"/>}.
        /// Checks that the specified directory separator is actually a directory separator, and throws an <see cref="ArgumentException"/> if it is not.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="directorySeparator"/> is not a directory separator according to <see cref="DirectorySeparator.IsDirectorySeparator(string)"/>.</exception>
        public static string CombineSimple(string path1, string path2, string directorySeparator)
        {
            DirectorySeparator.ValidateDirectorySeparatorArgument(directorySeparator, nameof(directorySeparator));

            var output = $"{path1}{directorySeparator}{path2}";
            return output;
        }

        /// <summary>
        /// Combines path segments using the specified directory separator, after trimming both platform and platform-alternate directory separators, and replacment of platform-alternate directory separators with platform directory separators.
        /// * All segments except the last are trimmed of ending path segments.
        /// * All segments except the first are trimmed of starting path segments.
        /// * All segments have platform-alternate path separators replaced with platform path separators.
        /// </summary>
        public static string CombineUsingDirectorySeparatorWithoutResolution(string directorySeparator, params string[] pathSegments)
        {
            var directorySeparatorAlternate = Utilities.GetAlternateDirectorySeparator(directorySeparator);

            var nSegments = pathSegments.Length;

            // Trim both path separators from the ends of all segments except the last.
            for (int iSegment = 0; iSegment < nSegments - 1; iSegment++)
            {
                var pathSegment = pathSegments[iSegment];
                var trimmedPathSegment = pathSegment.TrimEnd(Constants.WindowsDirectorySeparatorChar, Constants.NonWindowsDirectorySeparatorChar);
                pathSegments[iSegment] = trimmedPathSegment;
            }

            // Trim both path separators from the starts of all segments after the first.
            for (int iSegment = 1; iSegment < nSegments; iSegment++)
            {
                var pathSegment = pathSegments[iSegment];
                var trimmedPathSegment = pathSegment.TrimStart(Constants.WindowsDirectorySeparatorChar, Constants.NonWindowsDirectorySeparatorChar);
                pathSegments[iSegment] = trimmedPathSegment;
            }

            // Replace all platform-alternate path separators with platform path separators.
            for (int iSegment = 0; iSegment < nSegments; iSegment++)
            {
                var pathSegment = pathSegments[iSegment];
                var replacedPathSegment = pathSegment.Replace(directorySeparatorAlternate, directorySeparator);
                pathSegments[iSegment] = replacedPathSegment;
            }

            var output = String.Join(directorySeparator, pathSegments);
            return output;
        }

        /// <summary>
        /// Combines path segments using the specified directory separator, and resolves the combined path.
        /// To get an unresolved path, use <see cref="Utilities.CombineUsingDirectorySeparatorWithoutResolution(string, string[])"/>.
        /// </summary>
        public static string CombineUsingDirectorySeparator(string directorySeparator, params string[] pathSegments)
        {
            var unresolvedCombinedPathSegment = Utilities.CombineUsingDirectorySeparatorWithoutResolution(directorySeparator, pathSegments);
            var combinedPathSegment = Utilities.ResolvePath(unresolvedCombinedPathSegment);
            return combinedPathSegment;
        }

        /// <summary>
        /// Combine path segments using the Windows directory separator.
        /// </summary>
        public static string CombineWindows(params string[] pathSegments)
        {
            var directorySeparator = Constants.WindowsDirectorySeparator;

            var output = Utilities.CombineUsingDirectorySeparator(directorySeparator, pathSegments);
            return output;
        }

        /// <summary>
        /// Combine path segments using the non-Windows directory separator.
        /// </summary>
        public static string CombineNonWindows(params string[] pathSegments)
        {
            var directorySeparator = Constants.NonWindowsDirectorySeparator;

            var output = Utilities.CombineUsingDirectorySeparator(directorySeparator, pathSegments);
            return output;
        }

        /// <summary>
        /// Gets the file-name portion of the provided file-path.
        /// </summary>
        public static string GetFileName(string filePath)
        {
            var fileName = Utilities.GetFileNameSystem(filePath);
            return fileName;
        }

        public static string GetFileNameWithoutExtension(string filePath)
        {
            var fileNameWithoutExtension = Utilities.GetFileNameWithoutExtensionSystem(filePath);
            return fileNameWithoutExtension;
        }

        public static string GetDirectoryPath(string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);
            return directoryPath;
        }

        /// <summary>
        /// Gets the name of the directory of the given directory-path.
        /// This behavior differs from <see cref="Utilities.GetDirectoryNameSystem(string)"/>, which misleadingly gives the full directory-path.
        /// </summary>
        public static string GetDirectoryName(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);

            var directoryName = directoryInfo.Name;
            return directoryName;
        }

        public static string GetDirectoryNameForFilePath(string filePath)
        {
            var directoryPath = Utilities.GetDirectoryPath(filePath);

            var directoryName = Utilities.GetDirectoryName(directoryPath);
            return directoryName;
        }

        public static string GetParentDirectoryPath(string directoryPath)
        {
            var directorySeparator = Utilities.DetectDirectorySeparator(directoryPath);

            var lastDirectorySeparatorIndex = directoryPath.LastIndexOf(directorySeparator);

            var parentDirectoryPath = directoryPath.Substring(0, lastDirectorySeparatorIndex);
            return parentDirectoryPath;
        }

        /// <summary>
        /// Simply concatenates the directory path, directory separator, and file name.
        /// Performs no checks, and assumes that the specified directory separator is actually a directory separator.
        /// </summary>
        public static string GetFilePathSimpleUnchecked(string directoryPath, string fileName, string directorySeparator)
        {
            var filePath = Utilities.CombineSimpleUnchecked(directoryPath, fileName, directorySeparator);
            return filePath;
        }

        /// <summary>
        /// Simply concatenates the directory path, directory separator, and file name.
        /// Checks that the specified directory separator is actually a directory separator, and throws an <see cref="ArgumentException"/> if it is not.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="directorySeparator"/> is not a directory separator according to <see cref="DirectorySeparator.IsDirectorySeparator(string)"/>.</exception>
        public static string GetFilePathSimple(string directoryPath, string fileName, string directorySeparator)
        {
            DirectorySeparator.ValidateDirectorySeparatorArgument(directorySeparator, nameof(directorySeparator));

            var filePath = Utilities.CombineSimpleUnchecked(directoryPath, fileName, directorySeparator); // Directory separator assumed to be checked at this point.
            return filePath;
        }

        /// <summary>
        /// Combines the directory path with the file name using the specified directory separator.
        /// The directory path is checked for whether it is directory-indicated.
        /// The output path is ensured to use the specified directory separator.
        /// Checks that the specified directory separator is actually a directory separator, and throws an exception if not.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="directorySeparator"/> is not a directory separator according to <see cref="DirectorySeparator.IsDirectorySeparator(string)"/>.</exception>
        public static string GetFilePath(string directoryPath, string fileName, string directorySeparator)
        {
            DirectorySeparator.ValidateDirectorySeparatorArgument(directorySeparator, nameof(directorySeparator));

            var indicatedDirectoryPath = Utilities.EnsureDirectoryPathIsDirectoryIndicated(directoryPath, directorySeparator);

            var filePath = Utilities.CombineSimpleUnchecked(indicatedDirectoryPath, fileName);

            var ensuredFilePath = Utilities.EnsureDirectorySeparator(filePath, directorySeparator);
            return ensuredFilePath;
        }

        #endregion

        #region Strongly-Typed Paths

        /// <summary>
        /// Gets the current directory path.
        /// Uses <see cref="Environment.CurrentDirectory"/>.
        /// </summary>
        public static DirectoryPath CurrentDirectoryPath
        {
            get
            {
                var output = BaseUtilities.CurrentDirectoryPathValue.AsDirectoryPath();
                return output;
            }
        }

        /// <summary>
        /// Gets the current user's profile directory path.
        /// Uses <see cref="Environment.SpecialFolder.UserProfile"/>.
        /// </summary>
        public static DirectoryPath UserProfileDirectoryPath
        {
            get
            {
                var output = BaseUtilities.UserProfileDirectoryPathValue.AsDirectoryPath();
                return output;
            }
        }

        /// <summary>
        /// Gets the path location of the executable file as specified by the first command line argument.
        /// </summary>
        /// <remarks>
        /// The executable file when debugging in Visual Studio is .exe.vshost, not just .exe.
        /// </remarks>
        public static FilePath ExecutablePathCommandLineArgument
        {
            get
            {
                var output = BaseUtilities.ExecutablePathCommandLineArgumentValue.AsFilePath();
                return output;
            }
        }

        /// <summary>
        /// Gets the path location of the executable file as specified by the entry assembly's location.
        /// </summary>
        public static FilePath ExecutablePathEntryAssembly
        {
            get
            {
                var output = BaseUtilities.ExecutablePathEntryAssemblyValue.AsFilePath();
                return output;
            }
        }

        /// <summary>
        /// Gets the path location of the executable via the default method, <see cref="Utilities.ExecutablePathCommandLineArgument"/>.
        /// </summary>
        /// <remarks>
        /// There are multiple ways to get the location of the executable, and depending on context (unit test, debugging in Visual Studio, or production) different locations are returned.
        /// The command line argument is chosen as the default since this is the way the program is actually run by the operating system.
        /// </remarks>
        public static FilePath ExecutablePath
        {
            get
            {
                var output = Utilities.ExecutablePathCommandLineArgument;
                return output;
            }
        }

        public static string ExecutableDirectoryPathValue
        {
            get
            {
                var executableFilePathValue = BaseUtilities.ExecutablePathValue;

                var output = Utilities.GetDirectoryPath(executableFilePathValue);
                return output;
            }
        }

        /// <summary>
        /// Gets the directory location of the executable as the directory containing the executable rooted path, <see cref="Utilities.ExecutablePath"/>.
        /// </summary>
        public static DirectoryPath ExecutableDirectoryPath
        {
            get
            {
                var executableFilePath = Utilities.ExecutablePath;

                var output = Utilities.GetDirectoryPath(executableFilePath);
                return output;
            }
        }

        ///// <summary>
        ///// If a path is root-indicated (see <see cref="Utilities.IsPathRootIndicated(string)"/>, then it is an absolute path.
        ///// </summary>
        //public static bool IsAbsolutePath(AbsolutePath absolutePath)
        //{
        //    var output = Utilities.IsAbsolutePath(absolutePath.Value);
        //    return output;
        //}

        //public static bool IsPathRootIndicated(string path)
        //{
        //    var pathBeginsWithRooting = Regex.IsMatch(path, Constants.RootIndicatedPathRegexPattern);
        //    return pathBeginsWithRooting;
        //}

        #endregion

        #region Directory-Name Strings

        public static string CombineDirectoryName(string directoryNameSegment1, string directoryNameSegment2, string directoryNameSegmentSeparator)
        {
            var directoryNameSegment = $"{directoryNameSegment1}{directoryNameSegmentSeparator}{directoryNameSegment2}";
            return directoryNameSegment;
        }

        public static string CombineDirectoryName(string directoryNameSegment1, string directoryNameSegment2)
        {
            var directoryNameSegment = Utilities.CombineDirectoryName(directoryNameSegment1, directoryNameSegment2, Constants.DefaultDirectoryNameSegmentSeparator);
            return directoryNameSegment;
        }

        #endregion

        #region Strongly-Typed Directory-Name

        /// <summary>
        /// Combines multiple <see cref="DirectoryNameSegment"/>s into a single <see cref="GeneralDirectoryNameSegment"/>.
        /// Without knowing the context of use, there is now way to know the type of the combined <see cref="DirectoryNameSegment"/>s.
        /// But, depending on context, the <see cref="GeneralDirectoryNameSegment"/> can be converted into the proper directory-name type.
        /// </summary>
        public static GeneralDirectoryNameSegment Combine(DirectoryNameSegmentSeparator directoryNameSegmentSeparator, params DirectoryNameSegment[] directoryNameSegments)
        {
            var directoryNameSegmentValue = directoryNameSegments.Join(directoryNameSegmentSeparator.Value);

            var directoryNameSegment = new GeneralDirectoryNameSegment(directoryNameSegmentValue);
            return directoryNameSegment;
        }

        public static GeneralDirectoryNameSegment Combine(params DirectoryNameSegment[] directoryNameSegments)
        {
            var directoryNameSegment = Utilities.Combine(DirectoryNameSegmentSeparator.Default, directoryNameSegments);
            return directoryNameSegment;
        }

        public static DirectoryName GetDirectoryName(DirectoryNameSegmentSeparator directoryNameSegmentSeparator, params DirectoryNameSegment[] directoryNameSegments)
        {
            var genericDirectoryNameSegment = Utilities.Combine(directoryNameSegmentSeparator, directoryNameSegments);

            var directoryName = genericDirectoryNameSegment.AsDirectoryName();
            return directoryName;
        }

        public static DirectoryName GetDirectoryName(params DirectoryNameSegment[] directoryNameSegments)
        {
            var directoryName = Utilities.GetDirectoryName(DirectoryNameSegmentSeparator.Default, directoryNameSegments);
            return directoryName;
        }

        #endregion

        #region File-Name Strings

        public static string CombineFileName(string fileNameSegment1, string fileNameSegment2, string fileNameSegmentSeparator)
        {
            var fileNameSegment = $"{fileNameSegment1}{fileNameSegmentSeparator}{fileNameSegment2}";
            return fileNameSegment;
        }

        public static string CombineFileName(string fileNameSegment1, string fileNameSegment2)
        {
            var fileNameSegment = Utilities.CombineFileName(fileNameSegment1, fileNameSegment2, Constants.DefaultFileNameSegmentSeparator);
            return fileNameSegment;
        }

        /// <summary>
        /// Creates a file-name from a file-name without extension, file-extension, using the specified file-extension separator.
        /// </summary>
        public static string GetFileName(string fileNameWithoutExtension, string fileExtension, string fileExtensionSeparator)
        {
            var fileName = Utilities.CombineFileName(fileNameWithoutExtension, fileExtension, fileExtensionSeparator);
            return fileName;
        }

        /// <summary>
        /// Creates a file-name from a file-name without extension, file-extension, using the <see cref="FileExtensionSeparator.Default"/> file-extension separator.
        /// </summary>
        public static string GetFileName(string fileNameWithoutExtension, string fileExtension)
        {
            var fileName = Utilities.GetFileName(fileNameWithoutExtension, fileExtension, FileExtensionSeparator.Default.Value);
            return fileName;
        }

        /// <summary>
        /// Returns the file-extension without the leading file-extension separator ("txt" instead of ".txt").
        /// </summary>
        public static string GetFileExtension(string filePath)
        {
            var fileExtension = Utilities.GetExtensionSystem(filePath).ExceptFirst();
            return fileExtension;
        }

        // Uses 
        public static string[] GetFileNameSegments(string fileNameWithoutExtension, params string[] fileNameSegmentSeparators)
        {
            var fileNameSegments = fileNameWithoutExtension.Split(fileNameSegmentSeparators, StringSplitOptions.None);
            return fileNameSegments;
        }

        #endregion

        #region Strongly-Typed File-Name

        /// <summary>
        /// Combines multiple <see cref="FileNameSegment"/>s into a single <see cref="GeneralFileNameSegment"/>.
        /// </summary>
        /// <remarks>
        /// Without knowing the context of use, there is now way to know the type of the combined <see cref="FileNameSegment"/>s.
        /// But, depending on context, the <see cref="GeneralFileNameSegment"/> can be converted into the proper file-name type.
        /// </remarks>
        public static GeneralFileNameSegment Combine(FileNameSegmentSeparator fileNameSegmentSeparator, params FileNameSegment[] fileNameSegments)
        {
            var fileNameSegmentValue = fileNameSegments.Join(fileNameSegmentSeparator.Value);

            var fileNameSegment = new GeneralFileNameSegment(fileNameSegmentValue);
            return fileNameSegment;
        }

        /// <summary>
        /// Combines multiple <see cref="FileNameSegment"/>s into a single <see cref="GeneralFileNameSegment"/>.
        /// Uses the <see cref="FileNameSegmentSeparator.Default"/> value.
        /// </summary>
        public static GeneralFileNameSegment Combine(params FileNameSegment[] fileNameSegments)
        {
            var fileNameSegment = Utilities.Combine(FileNameSegmentSeparator.Default, fileNameSegments);
            return fileNameSegment;
        }

        /// <summary>
        /// Combines a <see cref="FileNameWithoutExtension"/> with a <see cref="FileExtension"/>, using the specified <see cref="FileExtensionSeparator"/>.
        /// </summary>
        public static FileName Combine(FileNameWithoutExtension fileNameWithoutExtension, FileExtension fileExtension, FileExtensionSeparator fileExtensionSeparator)
        {
            var genericFileNameSegment = Utilities.Combine(fileExtensionSeparator, fileNameWithoutExtension, fileExtension);

            var fileName = genericFileNameSegment.AsFileName();
            return fileName;
        }

        /// <summary>
        /// Combines a <see cref="FileNameWithoutExtension"/> with a <see cref="FileExtension"/>.
        /// Uses the <see cref="FileExtensionSeparator.Default"/> value.
        /// </summary>
        public static FileName Combine(FileNameWithoutExtension fileNameWithoutExtension, FileExtension fileExtension)
        {
            var fileName = Utilities.Combine(fileNameWithoutExtension, fileExtension, FileExtensionSeparator.Default);
            return fileName;
        }

        /// <summary>
        /// Gets the <see cref="FileName"/> composed of the specified <see cref="FileNameWithoutExtension"/> and <see cref="FileExtension"/>, joined using the specified <see cref="FileExtensionSeparator"/>.
        /// </summary>
        public static FileName GetFileName(FileNameWithoutExtension fileNameWithoutExtension, FileExtension fileExtension, FileExtensionSeparator fileExtensionSeparator)
        {
            var fileName = Utilities.Combine(fileNameWithoutExtension, fileExtension, fileExtensionSeparator);
            return fileName;
        }

        /// <summary>
        /// Gets the <see cref="FileName"/> composed of the specified <see cref="FileNameWithoutExtension"/> and <see cref="FileExtension"/>.
        /// Uses the <see cref="FileExtensionSeparator.Default"/> value.
        /// </summary>
        public static FileName GetFileName(FileNameWithoutExtension fileNameWithoutExtension, FileExtension fileExtension)
        {
            var fileName = Utilities.Combine(fileNameWithoutExtension, fileExtension, FileExtensionSeparator.Default);
            return fileName;
        }

        public static FileName GetFileName(FilePath filePath)
        {
            var fileName = Utilities.GetFileName(filePath.Value).AsFileName();
            return fileName;
        }

        public static FileExtension GetFileExtension(FileName fileName)
        {
            var fileExtension = Utilities.GetFileExtension(fileName.Value).AsFileExtension();
            return fileExtension;
        }

        public static FileExtension GetFileExtension(FilePath filePath)
        {
            var fileExtension = Utilities.GetFileExtension(filePath.Value).AsFileExtension();
            return fileExtension;
        }

        /// <summary>
        /// Combines <see cref="FileNameSegment"/>s into a <see cref="FileNameWithoutExtension"/> using the specified <see cref="FileNameSegmentSeparator"/>.
        /// </summary>
        public static FileNameWithoutExtension GetFileNameWithoutExtension(FileNameSegmentSeparator fileNameSegmentSeparator, params FileNameSegment[] fileNameSegments)
        {
            var genericFileNameSegment = Utilities.Combine(fileNameSegmentSeparator, fileNameSegments);

            var fileNameWithoutExtension = genericFileNameSegment.AsFileNameWithoutExtension();
            return fileNameWithoutExtension;
        }

        /// <summary>
        /// Combines <see cref="FileNameSegment"/>s into a <see cref="FileNameWithoutExtension"/>.
        /// Uses the <see cref="FileNameSegmentSeparator.Default"/> value.
        /// </summary>
        public static FileNameWithoutExtension GetFileNameWithoutExtension(params FileNameSegment[] fileNameSegments)
        {
            var fileNameWithoutExtension = Utilities.GetFileNameWithoutExtension(FileNameSegmentSeparator.Default, fileNameSegments);
            return fileNameWithoutExtension;
        }

        /// <summary>
        /// Gets the <see cref="FileNameWithoutExtension"/> component of a <see cref="FilePath"/> (e.g. "temp" in "C:\Temp\temp.txt").
        /// </summary>
        public static FileNameWithoutExtension GetFileNameWithoutExtension(FilePath filePath)
        {
            var fileNameWithoutExtension = Utilities.GetFileNameWithoutExtension(filePath.Value).AsFileNameWithoutExtension();
            return fileNameWithoutExtension;
        }

        /// <summary>
        /// Gets the <see cref="FileNameWithoutExtension"/> component of a <see cref="FileName"/> (e.g. "temp" from "temp.txt").
        /// </summary>
        public static FileNameWithoutExtension GetFileNameWithoutExtension(FileName fileName)
        {
            var fileNameWithoutExtension = Utilities.GetFileNameWithoutExtensionSystem(fileName.Value).AsFileNameWithoutExtension(); // Note, ok to use System string-based method on file-name, since it works for the full file-path it will work for the file-name.
            return fileNameWithoutExtension;
        }

        public static FileNameSegment[] GetFileNameSegments(FileNameWithoutExtension fileNameWithoutExtension, params FileNameSegmentSeparator[] fileNameSegmentSeparators)
        {
            var output = Utilities.GetFileNameSegments(fileNameWithoutExtension.Value, fileNameSegmentSeparators.Select(x => x.Value).ToArray()).Select(x => x.AsFileNameSegment()).ToArray();
            return output;
        }

        #endregion

        #region Strongly-Typed Directory And File Paths

        public static bool Exists(FilePath filePath)
        {
            var output = File.Exists(filePath.Value);
            return output;
        }

        public static bool Exists(DirectoryPath directoryPath)
        {
            var output = File.Exists(directoryPath.Value);
            return output;
        }

        public static void Delete(FilePath filePath)
        {
            File.Delete(filePath.Value);
        }

        public static void DeleteFilePath(FilePath filePath)
        {
            // Can delete file path regardless of whether it exists.
            File.Delete(filePath.Value);
        }

        public static void Delete(DirectoryPath directoryPath, bool recursive = true)
        {
            Directory.Delete(directoryPath.Value, recursive);
        }

        public static void DeleteDirectoryPath(DirectoryPath directoryPath, bool recursive = true)
        {
            // Only delete directory if it exists, else this is throws an exception.
            if(Directory.Exists(directoryPath.Value))
            {
                Directory.Delete(directoryPath.Value, recursive);
            }
        }

        /// <summary>
        /// Combines <see cref="PathSegment"/>s using the specified directory path separator.
        /// Note, no path resolution is performed on the path segments.
        /// </summary>
        /// <remarks>
        /// An array of <see cref="PathSegment"/>s can only be combined into a <see cref="PathSegment"/>.
        /// Depending on context, a <see cref="PathSegment"/> could be converted to the proper type, for example a <see cref="DirectoryPath"/> or <see cref="FilePath"/>.
        /// </remarks>
        public static PathSegment Combine(DirectorySeparator directorySeparator, params PathSegment[] pathSegments)
        {
            var pathSegmentValues = pathSegments.Select(x => x.Value).ToArray();

            var directorySeparatorValue = directorySeparator.Value;

            var combinedValue = Utilities.CombineUsingDirectorySeparatorWithoutResolution(directorySeparatorValue, pathSegmentValues);

            var pathSegment = combinedValue.AsPathSegment();
            return pathSegment;
        }

        public static AbsolutePath Combine(DirectorySeparator directorySeparator, AbsolutePath absolutePath, PathSegment pathSegment)
        {
            var combinedPathValue = Utilities.CombineUsingDirectorySeparator(directorySeparator.Value, absolutePath.Value, pathSegment.Value);

            var combinedPath = combinedPathValue.AsAbsolutePath();
            return combinedPath;
        }

        public static AbsolutePath Combine(DirectorySeparator directorySeparator, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var relativePathSegment = Utilities.Combine(directorySeparator, pathSegments);

            var combinedPath = Utilities.Combine(directorySeparator, absolutePath, relativePathSegment);
            return combinedPath;
        }

        public static FilePath GetFilePath(DirectorySeparator directorySeparator, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var filePath = Utilities.Combine(directorySeparator, absolutePath, pathSegments).AsFilePath();
            return filePath;
        }

        public static FilePath GetFilePath(DirectoryPath directoryPath, FileName fileName, DirectorySeparator directorySeparator)
        {
            var filePath = Utilities.GetFilePath(directoryPath.Value, fileName.Value, directorySeparator.Value).AsFilePath();
            return filePath;
        }

        public static FilePath GetFilePath(DirectoryPath directoryPath, FileName fileName)
        {
            var defaultDirectorySeparator = Utilities.GetDefaultDirectorySeparator();

            var filePath = Utilities.GetFilePath(directoryPath, fileName, defaultDirectorySeparator);
            return filePath;
        }

        public static FileRelativePath GetRelativePath(FilePath fromFilePath, FilePath toFilePath)
        {
            var fileRelativePath = Utilities.GetRelativePathFileToFile(fromFilePath.Value, toFilePath.Value).AsFileRelativePath();
            return fileRelativePath;
        }

        public static FileRelativePath GetRelativePath(DirectoryPath fromDirectoryPath, FilePath toFilePath)
        {
            var fileRelativePath = Utilities.GetRelativePathDirectoryToFile(fromDirectoryPath.Value, toFilePath.Value).AsFileRelativePath();
            return fileRelativePath;
        }

        public static DirectoryRelativePath GetRelativePath(FilePath fromFilePath, DirectoryPath toDirectoryPath)
        {
            var directoryRelativePath = Utilities.GetRelativePathFileToDirectory(fromFilePath.Value, toDirectoryPath.Value).AsDirectoryRelativePath();
            return directoryRelativePath;
        }

        public static DirectoryRelativePath GetRelativePath(DirectoryPath fromDirectoryPath, DirectoryPath toDirectoryPath)
        {
            var directoryRelativePath = Utilities.GetRelativePathFileToFile(fromDirectoryPath.Value, toDirectoryPath.Value).AsDirectoryRelativePath();
            return directoryRelativePath;
        }

        public static DirectoryName GetDirectoryName(DirectoryPath directoryPath)
        {
            var directoryName = Utilities.GetDirectoryName(directoryPath.Value).AsDirectoryName();
            return directoryName;
        }

        public static DirectoryName GetDirectoryName(FilePath filePath)
        {
            var directoryPath = Utilities.GetDirectoryPath(filePath);

            var directoryName = Utilities.GetDirectoryName(directoryPath);
            return directoryName;
        }

        public static DirectoryPath GetDirectoryPath(FilePath filePath)
        {
            var directoryPath = Utilities.GetDirectoryPath(filePath.Value).AsDirectoryPath();
            return directoryPath;
        }

        public static DirectoryPath GetParentDirectoryPath(DirectoryPath directoryPath)
        {
            var parentDirectoryPath = Utilities.GetParentDirectoryPath(directoryPath.Value).AsDirectoryPath();
            return parentDirectoryPath;
        }

        public static DirectoryPath GetParentDirectoryPath(AbsolutePath path)
        {
            var parentDirectoryPath = Utilities.GetParentDirectoryPath(path.Value).AsDirectoryPath();
            return parentDirectoryPath;
        }

        public static DirectoryPath GetDirectoryPath(DirectorySeparator directorySeparator, AbsolutePath absolutePath, params PathSegment[] pathSegments)
        {
            var directoryPath = Utilities.Combine(directorySeparator, absolutePath, pathSegments).AsDirectoryPath();
            return directoryPath;
        }

        public static AbsolutePath ResolvePath(AbsolutePath unresolvedAbsolutePath)
        {
            var resolvedPath = Utilities.ResolvePath(unresolvedAbsolutePath.Value).AsAbsolutePath();
            return resolvedPath;
        }

        public static DirectoryPath ResolvePath(DirectoryPath unresolvedDirectoryPath)
        {
            var resolvedDirectoryPath = Utilities.ResolvePath(unresolvedDirectoryPath.Value).AsDirectoryPath();
            return resolvedDirectoryPath;
        }

        public static FilePath ResolvePath(FilePath unresolvedFilePath)
        {
            var resolvedFilePath = Utilities.ResolvePath(unresolvedFilePath.Value).AsFilePath();
            return resolvedFilePath;
        }

        #endregion
    }
}
