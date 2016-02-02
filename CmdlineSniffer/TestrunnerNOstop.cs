using System;

namespace PyLauncher
{
    class TestrunnerNOstop : ITestrunner
    {
        public TestrunnerNOstop()
        { }

        public void RunTest(Parameter.Test testparameters, string serialno = "")
        {
            if (testparameters != null)
            {
                string parsaruments = "";
                foreach (string s in testparameters.Parsstring)
                {
                    if (s.Contains("SERIAL_NUMBER"))
                        parsaruments += (" " + serialno);
                    else
                        parsaruments += (" " + s);
                }
                string runcommandarguments = testparameters.arguments + parsaruments;
            }
        }
    }
}
