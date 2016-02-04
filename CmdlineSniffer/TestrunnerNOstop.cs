using System.Diagnostics;

namespace PyLauncher
{
    /*Test runner no stop will run a process and not
    stop */
    public class TestrunnerNOstop : ITestrunner
    {
        private Parameter.Test testparameters;

        public void Load(Parameter.Test testparameters, string serialno = "")
        {
            this.testparameters = testparameters;
            /*Get all the paramters to run the test
            and  add then to a long string*/
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
                //add the path of the .py file to the parsedarguments
                string commandarguments = testparameters.arguments + parsaruments;
                //After loading parameters run it
                Runtest(commandarguments, testparameters.filename, testparameters.workingdirectory);
            }
        }
        /*
        Run a test/processes by passing in atguments like:
        runcommandargumnets = "C:\\workspace\\teststuff\\testtime.py"
        filename = "python.exe"
        arguments = "C:\\workspace\\teststuff\\testtime.py"
        */
        public void Runtest(string runcommandarguments, string filename, string workingdirectory)
        {
            Process p = new Process(); // create process (i.e., the python program)
            p.StartInfo.FileName = filename;
            p.StartInfo.Arguments = runcommandarguments;
            p.StartInfo.RedirectStandardOutput = false;//check command line output
            p.StartInfo.RedirectStandardError = false;//Have to check error
            //p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.UseShellExecute = false; //we can read or not the output from stdout
            p.StartInfo.WorkingDirectory = workingdirectory;
            p.Start();
            //string g = p.StandardError.ReadToEnd();
            //string t = p.StandardOutput.ReadToEnd();//Reads the standard input
            //Console.WriteLine(t);//prints it out
            ///Console.WriteLine(g);//prints it out
            p.WaitForExit();
        }
    }
}
