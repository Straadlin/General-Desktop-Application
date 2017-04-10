using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using General_Desktop_Application.Classes;

namespace General_Desktop_Application_UnitTest
{
    [TestClass]
    public class Decrypt_Test
    {
        private string stExpectedResult;
        private string stGettedResult;

        [TestMethod]
        public void Decrypt_BuildingString_ValueMustBeEquivalentWithStringA()
        {
            stExpectedResult = "Camille";

            stGettedResult = Tools.Decrypt("ny9jObdzs3s=");

            Assert.AreEqual(stGettedResult, stExpectedResult);
        }

        [TestMethod]
        public void Decrypt_BuildingString_ValueMustBeEquivalentWithStringB()
        {
            stExpectedResult = "Kitt";

            stGettedResult = Tools.Decrypt("Ccngc1ZFV8g=");

            Assert.AreEqual(stGettedResult, stExpectedResult);
        }
    }
}