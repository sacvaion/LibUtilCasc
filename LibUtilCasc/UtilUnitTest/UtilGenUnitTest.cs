using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilUnitTest
{
    [TestClass]
    public class UtilGenUnitTest
    {
        [TestMethod]
        public void CreateIdTransactionSuccess()
        {
            //Arrange
            String numerico;
            //Act
            numerico = LibUtilCasc.UtilGen.CreateIdTransaction(DateTime.Now);
            //Assert
            Assert.IsNotNull(numerico);
        }


        [TestMethod]
        public void ExtraerNumericoSuccess()
        {
            //Arrange
            Decimal numerico = 0;
            //Act
            numerico = LibUtilCasc.UtilGen.ExtraerNumerico("ABD123456", "2");
            //Assert
            Assert.IsNotNull(numerico);
        }
    }
}
