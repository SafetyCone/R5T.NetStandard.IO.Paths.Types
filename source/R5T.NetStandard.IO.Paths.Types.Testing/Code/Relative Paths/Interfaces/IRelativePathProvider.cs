using System;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    /// <summary>
    /// Allows encapsulating the behavior of computing a relative path.
    /// </summary>
    public interface IRelativePathProvider
    {
        string GetRelativePath(string sourcePath, string destinationPath);
    }
}
