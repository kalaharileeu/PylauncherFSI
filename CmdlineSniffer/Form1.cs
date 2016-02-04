﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PyLauncher
{
    public partial class Form1 : Form
    {
        string status;
        string error;
        string test;
        TestrunnerNOstop testrunnernostop;
        Report Serverreporter;
        Readcmd CmdReader;
        XmlManager<Parameter> xmltestmanager;
        Parameter tests;
        Report report1;
        List<string> storedvalues;
        //string valuereport;
        bool keepreporting;
        
        public Form1()
        {
            //for running processe/tests
            testrunnernostop = new TestrunnerNOstop();
            storedvalues = new List<string>();
            status = "unknown";
            error = ".";
            //test = ".";
            Serverreporter = new Report();
            CmdReader = new Readcmd();
            InitializeComponent();
            xmltestmanager = new XmlManager<Parameter>();
            //tests = new Parameter();//I do not need this
            report1 = new Report();
            keepreporting = true;
            tests = xmltestmanager.Load("Content/parametersv2.xml");// the the xmlfile the start off serialization
            foreach (Parameter.Test t in tests.tests)
            {
                //listBox1.Items.Add(t.id);
                storedvalues.Add(t.id);
            }
            textBox1.Focus();
        }

        //this is a recursive, asynchrounous funstion function method runs asynchronously
        private async void runmainprocess()
        {
            if(listBox2.Items.Count != 0)
            {
                string testtorun = (string)(listBox2.Items[0]);
                test = testtorun;
                test = Regex.Replace(test, @"\s+", "%20");
                Debug.WriteLine(test);
                status = "Running";
                listBox3.Items.Add(testtorun);
                listBox2.Items.RemoveAt(0);

                //**********get all the task parametrs here*********************
                Parameter.Test thetest = new Parameter.Test();
                thetest = tests.tests.Find(x => x.id.Equals(testtorun));
                if (thetest != null)
                {
                    string parametertext = "";
                    foreach (string s in thetest.Parsstring)
                    {
                        if (s.Contains("SERIAL_NUMBER"))
                            parametertext += (" " + (string)textBox1.Text.Substring(textBox1.Text.Length - 12));
                        else
                            parametertext += (" " + s);//add all the arguments together
                    }
                    textBox4.Clear();
                    textBox4.Text = thetest.arguments + parametertext;
                }
                    //************END of finding the task parameters***********************

                    //string value = report1.Reporttoserver(status, test, ref error);
                    //A tast gets kickef off here
                    string t = await Task.Run(() => Runtest(testtorun));
                if (t.Contains("done"))
                {
                    status = "Complete";
                    shortreport();//Force a quick report of complete
                    listBox3.Items.Clear();
                    listBox4.Items.Add(testtorun);
                }
                //recursive call.
                runmainprocess();
            }
        }
        //makes a single complete report
        private async void shortreport()
        {
            keepreporting = false;
            await Task.Run(() => reporttoserver());
        }
        //continuous reporting
        private async void Reporting()
        {
            textBox2.Text = await Task.Run(() => reporttoserver());
            if (keepreporting || listBox2.Items.Count != 0)
            {
                if (listBox3.Items.Count != 0)//if no tasks running do not report
                {
                    Reporting(); //calls reporting againg
                }
                else
                {
                    button5.Enabled = true;//Enable diagnostics
                    status = "Complete";
                    keepreporting = false;//stop reporting
                    Reporting();
                }
            }
        }
        //This function only called from asynchronous function
        //sleep statement in function wait for the return value, might be better way to do. 
        private string reporttoserver()//only called from asynchronous functions
        {
            string value = "Default server message";
            value = report1.Reporttoserver(status, test, ref error, textBox3.Text);
            return value;
        }
        //This function only gets called from asychronous functions.
        private string Runtest(string itemtorun)
        {
            //The next set of code searches out the code from the 
            //instantiated test paremeters
            Parameter.Test theparameters = new Parameter.Test();
            theparameters = tests.tests.Find(x => x.id.Equals(itemtorun));
            if (theparameters != null)
            {
                string parsaruments = "";
                foreach(string s in theparameters.Parsstring)
                {
                    if (s.Contains("SERIAL_NUMBER"))
                        parsaruments += ( " " + 
                            (string)textBox1.Text.Substring(textBox1.Text.Length - 12));
                    else
                        parsaruments += (" " + s);
                }
                string runcommandarguments = theparameters.arguments + parsaruments;


                testrunnernostop.Load(theparameters,
                    (string)textBox1.Text.Substring(textBox1.Text.Length - 12));
                /*
                Process p = new Process(); // create process (i.e., the python program)
                
                p.StartInfo.FileName = Testtime.filename;//python.exe
                p.StartInfo.Arguments = runcommandarguments;
                p.StartInfo.RedirectStandardOutput = false;//check command line output
                p.StartInfo.RedirectStandardError = false;//Have to check error
                //p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.UseShellExecute = false; //we can read or not the output from stdout
                p.StartInfo.WorkingDirectory = Testtime.workingdirectory;//c://workspace//teststuff
                p.Start();
                //string g = p.StandardError.ReadToEnd();
                //string t = p.StandardOutput.ReadToEnd();//Reads the standard input
                //Console.WriteLine(t);//prints it out
                ///Console.WriteLine(g);//prints it out
                p.WaitForExit();
                if (p.ExitCode != 0)
                {               
                }
                */
               // itemtorun = Testtime.filename + Testtime.arguments + Testtime.Parsstring[0] +
               //     Testtime.Parsstring[1] + Testtime.Parsstring[2] + Testtime.Parsstring[3] + Testtime.Parsstring[4];
              //  Console.WriteLine(itemtorun);
            }
            else
            {
                Console.WriteLine(" Did not find the test id from XML file/n");
                return "Could not find the test";
            }

            return "done from launcher: ";
        }
    }
}

/*
        public string Runtest()//This test is run by runtest as a task
        {
            string s = "s 9";
            string t = "t 0";
            string c = "c 1";
            string mdl = "Frigate_Vogel_230V";
            Process p = new Process(); // create process (i.e., the python program
            p.StartInfo.FileName = "python.exe";
            //p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.UseShellExecute = false; // make sure we can read the output from stdout
            //p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.Arguments = 
            "C:\\Users\\cdelange\\workspace\\mercurial\\Smoketest\\smoke_test.py -" + s + " -" + t + " -" + c + " --config " + mdl; 
            //p.StartInfo.Arguments = "c:\\test\\testtime.py "; // start the python program with two parameters
            
            p.Start(); // start the process (the python program)

            p.WaitForExit();
            if (p.HasExited)
                p.Close();

            return "testid"  + " :done test";
        }

        public string Runtest3()//This test is run by runtest as a task
        {
            Process p = new Process(); // create process (i.e., the python program
            p.StartInfo.FileName = "python.exe";
            //p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.UseShellExecute = true; // make sure we can read the output from stdout
            //p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.Arguments = "c:\\test\\testtime.py "; // start the python program with two parameters

            p.Start(); // start the process (the python program)

            p.WaitForExit();
            if (p.HasExited)
                p.Close();

            return "done";
        }

        private string Runtest2()//WORKS take control of the input and the output pipe line
        {

            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = "python.exe";
            p.Arguments = "c:\\test\\testtime.py "; // start the python program with two parameters
            p.RedirectStandardOutput = true;
            p.UseShellExecute = false;
            p.RedirectStandardInput = true;
            p.CreateNoWindow = true;

            using (Process process = Process.Start(p))
            using (StreamWriter writer = process.StandardInput)
            using (StreamReader reader = process.StandardOutput)
            {
                Console.WriteLine("here");
                writer.WriteLine("doit");

                string result = null;

                while (!process.HasExited)
                { 
                    result = reader.ReadLine();
                    Console.WriteLine(result);
                }

                process.WaitForExit();
                if (process.HasExited)
                    process.Close();
            }

            return "done";

        }
*/
        /*
        public void Searchprocess()//searches through all the processes for cmd
        {
            List<Process> listprocess = new List<Process>();
            //var poList= Process.GetProcesses().Where(process => process.ProcessName.Contains("cmd"));
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Equals("cmd"))
                    listprocess.Add(p);
            }

            bool processloop = true;

            while (processloop)
            {
                foreach (Process p in listprocess)
                {
                    var a = p;
                    ExtFunc.FreeConsole();//if you use console application you must free self console
                    ExtFunc.AttachConsole((uint)a.Id);
                    var err = Marshal.GetLastWin32Error();
                    System.IntPtr ptr = ExtFunc.GetStdHandle(-11);
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    bool imrunning = true;
                    while (imrunning)
                    {
                        short cursor = (short)System.Console.CursorTop;//Find the bottomline where cursor resides
                        string checkforcursor = CmdReader.readvalue(ref ptr, cursor);
                        if (checkforcursor.StartsWith("C:\\>"))
                        {
                            status = "Complete";
                            imrunning = false;
                            Serverreporter.Reporttoserver(ref status, ref error);
                        }
                        else if (checkforcursor.StartsWith("C:\\"))
                        {
                            status = "Complete";
                            imrunning = false;
                            Serverreporter.Reporttoserver(ref status, ref error);
                        }
                        else
                        {
                            System.Console.ForegroundColor = ConsoleColor.Red;
                            status = "Running";
                            System.Console.Write(".");
                            imrunning = true;
                            Serverreporter.Reporttoserver(ref status, ref error);
                            //System.Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        Thread.Sleep(300);//2000
                    }
                    System.Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(300);//10 000One process closed move to the next
                }
                processloop = false;//No processes
            }
            Console.WriteLine("all the cmd processes has ended");
        }
*/
