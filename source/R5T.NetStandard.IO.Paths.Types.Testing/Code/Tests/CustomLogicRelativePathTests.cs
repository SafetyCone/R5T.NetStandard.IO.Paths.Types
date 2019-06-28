using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace R5T.NetStandard.IO.Paths.Types.Testing
{
    [TestClass]
    public class CustomLogicRelativePathTests : RelativePathProviderTestFixture
    {
        public CustomLogicRelativePathTests()
            : base(new CustomLogicRelativePathProvider())
        {
        }
    }
}
