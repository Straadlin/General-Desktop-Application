using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using General_Desktop_Application.Classes;

namespace General_Desktop_Application_UnitTest
{
    [TestClass]
    public class GetDefaulHash_Test
    {
        private string stExpectedResult;
        private string stGettedResult;

        [TestMethod]
        public void GetDefaulHash_BuildString_ValueMustBeEquivalentWithStringA()
        {
            stExpectedResult = "f0ac6b1a922ca52e5132ca786c9e3c34923bdf856a65c21f04bada4a0b3d33866f5f71e0925580b4aa53f881c80c555a043c095b11d88ee7271c6c15a0314a40";

            stGettedResult = Tools.GetDefaulHash("camille25");

            Assert.AreEqual(stGettedResult, stExpectedResult);
        }
    }
}