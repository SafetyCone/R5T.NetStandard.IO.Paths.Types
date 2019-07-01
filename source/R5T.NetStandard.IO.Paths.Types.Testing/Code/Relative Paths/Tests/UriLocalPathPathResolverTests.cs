using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    /// <summary>
    /// Path resolution tests using the Uri.LocalPath property.
    /// </summary>
    [TestClass]
    public class UriLocalPathPathResolverTests : PathResolverTestFixture
    {
        public UriLocalPathPathResolverTests()
            : base(new UriLocalPathPathResolver())
        {
        }
    }
}
