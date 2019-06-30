using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    [TestClass]
    public class UriLocalPathPathResolverTests : PathResolverTestFixture
    {
        public UriLocalPathPathResolverTests()
            : base(new UriLocalPathPathResolver())
        {
        }
    }
}
