using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace PyLauncher.Tests
{
    [TestClass()]
    public class SerialIOportTests
    {
        SerialIOport IO = new SerialIOport();
         
        [TestMethod()]
        public void GetserialportnamesTest()
        {
            //Arrange
            SerialIOport IO = new SerialIOport();
            IO.Getserialportnames();
            //IO.CheckPortName("COM12");
            //Act
            Debug.WriteLine(IO.Getportstring());
            //Assert
            Assert.AreEqual(IO.CheckPortName(""), false);

        }

        //[TestMethod()]
        //public void CheckPortNameTest()
        //{
        //    Assert.Fail();
        //}
    }
}