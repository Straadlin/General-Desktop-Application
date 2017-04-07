using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using General_Desktop_Application.Classes;

namespace General_Desktop_Application_UnitTest
{
    [TestClass]
    public class Encrypt_Test
    {
        private string stExpectedResult;
        private string stGettedResult;

        [TestMethod]
        public void Encrypt_BuildingString_ValueMustBeTheSameWithStringA()
        {
            stExpectedResult = "ny9jObdzs3s=";

            stGettedResult = Tools.Encrypt("Camille");

            Assert.AreEqual(stGettedResult, stExpectedResult);
        }

        [TestMethod]
        public void Encrypt_BuildingString_ValueMustBeTheSameWithStringB()
        {
            stExpectedResult = "Ccngc1ZFV8g=";

            stGettedResult = Tools.Encrypt("Kitt");

            Assert.AreEqual(stGettedResult, stExpectedResult);
        }
    }
}