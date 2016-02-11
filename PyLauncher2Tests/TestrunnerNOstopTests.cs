using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PyLauncher.Tests
{
    [TestClass()]
    public class TestrunnerNOstopTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            //arrange
            TestrunnerNOstop TestrunnerNOstop = new TestrunnerNOstop();
            Parameter tests = new Parameter();
            tests.tests.Add(new Parameter.Test());
            tests.tests[0].workingdirectory = "c:\\workspace\\teststuff";
            tests.tests[0].filename = "python.exe";
            tests.tests[0].arguments = "C:\\workspace\\teststuff\\testtime.py";
            //Act
            TestrunnerNOstop.Load(tests.tests[0]);
            //Assert
            /*There should be a cmdline window popping up, with some 
            with some test scrip running*/
        }

        [TestMethod()]
        public void RuntestTest()
        {
            //arrange
           // Parameter tests;
            TestrunnerNOstop TestrunnerNOstop = new TestrunnerNOstop();
            string value = "C:\\workspace\\teststuff\\testtime.py";
            //Act Assert
            TestrunnerNOstop.Runtest(value, "python.exe", "c://workspace//teststuff");
            //Assert
            /*There should be a cmdline window popping up, with some
            with some test scrip running*/
        }
    }
}