using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    /// <summary>
    /// Tests the GetRelativePath() utility methods.
    /// </summary>
    [TestClass]
    public class UriMakeRelativeUriRelativePathTests : RelativePathProviderTestFixture
    {
        public UriMakeRelativeUriRelativePathTests()
            : base(new UriMakeRelativeUriRelativePathProvider())
        {
        }
    }
}
