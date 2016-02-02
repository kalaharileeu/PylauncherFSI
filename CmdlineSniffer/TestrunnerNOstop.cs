using System;

namespace PyLauncher
{
    public class TestrunnerNOstop : ITestrunner
    {
        string runcommandarguments;

        public TestrunnerNOstop()
        {
            runcommandarguments = "";
        }

        public void Load(Parameter.Test testparameters, string serialno = "")
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
                runcommandarguments = testparameters.arguments + parsaruments;
            }
        }

        public void Runtest()
        {
            Console.WriteLine(runcommandarguments);
        }
    }
}
