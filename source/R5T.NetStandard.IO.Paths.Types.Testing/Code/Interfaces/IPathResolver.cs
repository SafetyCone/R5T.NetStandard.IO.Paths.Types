using System;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    public interface IPathResolver
    {
        string ResolvePath(string unresolvedPath);
        string ResolveFilePath(string unresolvedFilePath);
        string ResolveDirectoryPath(string unresolvedDirectoryPath);
    }
}
