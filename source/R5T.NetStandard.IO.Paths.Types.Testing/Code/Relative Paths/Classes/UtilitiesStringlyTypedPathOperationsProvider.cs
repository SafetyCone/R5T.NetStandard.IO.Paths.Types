using System;

using PathUtilities = R5T.NetStandard.IO.Paths.UtilitiesGood;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    public class UtilitiesStringlyTypedPathOperationsProvider : IStringlyTypedPathOperationsProvider
    {
        public string CombineSimpleUnchecked(string directoryIndicatedPathSegment1, string pathSegment2)
        {
            var outputPathSegment = PathUtilities.CombineSimpleUnchecked(directoryIndicatedPathSegment1, pathSegment2);
            return outputPathSegment;
        }

        public string CombineUnchecked(string pathSegment1, string pathSegment2, string directorySeparator)
        {
            var outputPathSegment = PathUtilities.CombineUnchecked(pathSegment1, pathSegment2, directorySeparator);
            return outputPathSegment;
        }
    }
}
