//using System;

//using PathUtilities = R5T.NetStandard.IO.Paths.Utilities;


//namespace R5T.NetStandard.IO.Paths.Types.Testing
//{
//    class UriMakeRelativeUriRelativePathProvider : IRelativePathProvider
//    {
//        public string GetRelativePath(string sourcePath, string destinationPath)
//        {
//            var relativePath = PathUtilities.GetRelativePathUsingUriMakeRelativeUri(sourcePath, destinationPath);
//            return relativePath;
//        }

//        public string GetRelativePathDirectoryToFile(string sourceDirectoryPath, string destinationPath)
//        {
//            var relativePath = PathUtilities.GetRelativePathUsingUriMakeRelativeUri(sourceDirectoryPath, destinationPath);
//            return relativePath;
//        }

//        public string GetRelativePathFileToFile(string sourceFilePath, string destinationPath)
//        {
//            var relativePath = PathUtilities.GetRelativePathUsingUriMakeRelativeUri(sourceFilePath, destinationPath);
//            return relativePath;
//        }
//    }
//}
