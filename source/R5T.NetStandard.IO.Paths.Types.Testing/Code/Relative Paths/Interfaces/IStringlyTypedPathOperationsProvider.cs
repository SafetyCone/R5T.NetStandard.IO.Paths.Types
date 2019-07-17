﻿using System;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    /// <summary>
    /// Provides path operations for stringly-typed paths.
    /// </summary>
    public interface IStringlyTypedPathOperationsProvider
    {
        /// <summary>
        /// Combines two path segments into a single path segment.
        /// Simple - No directory separator is inserted between the paths (the first path segment is assumed to end with the required directory separator).
        /// Unchecked - No check is made to ensure the first path segment does, in fact, end with a directory separator.
        /// This is literally just concatenation of the two strings.
        /// </summary>
        string CombineSimpleUnchecked(string directoryIndicatedPathSegment1, string pathSegment2);

        /// <summary>
        /// Combines two path segments into a single path segment.
        /// Unchecked - No check is done to ensure the first path segment does NOT end with a directory separator, nor that the directory separator is actually a directory separator.
        /// This is just string concatenation.
        /// </summary>
        string CombineUnchecked(string pathSegment1, string pathSegment2, string directorySeparator);

        /// <summary>
        /// Ensures that <see cref="directoryIndicatedPathSegment1"/> is actually directory indicated (using logic to determine path separator to use).
        /// </summary>
        string CombineSimple(string directoryIndicatedPathSegment1, string pathSegment2);

        string CombineSimple(string nonDirectoryIndicatedPathSegment1, string pathSegment2, string directorySeparator);
    }
}
