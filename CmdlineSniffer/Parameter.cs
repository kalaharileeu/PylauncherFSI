﻿using System.Collections.Generic;

using System.Xml.Serialization;

namespace PyLauncher
{
    // This is the class that will be deserialized.
    public class Parameter
    {
        //[XmlIgnore]
        //List<Setupparameters> listparameters;
        [XmlElement("Quicktest")]
        public List<string> Quicktest;
        [XmlElement("Temperature")]
        public List<string> Temperature;
        [XmlElement("Setupparameters")]
        public List<Setupparameters> listparameters;
        [XmlElement("Test")]
        public List<Test> tests;
        // This is the class that will be deserialized.
        //Quicktest: This setupparameters is used for quicktests
        public class Setupparameters : Test
        {
            //using the new keyword to heide the original Test variables
            //public new string id;//Inverter nickname
           // public new string filename;//"for ex. pyhton.exe"
            //public new string workingdirectory;
            //public new string arguments;//"path of to .py"
            //[XmlElement("Parsstring")]
           // public new List<string> Parsstring;//5 for smoke test
            public Setupparameters()
            {
                Parsstring = new List<string>();
            }
            //copy constructor
            public Setupparameters(Setupparameters S)
            {
                id = S.id;
                filename = S.filename;
                workingdirectory = S.workingdirectory;
                arguments = S.arguments;
                //copy(constructor) the list
                Parsstring = new List<string>(S.Parsstring);
            }
            //used to change the genereic "TESTNAME" to a real test name
            public void Inserttestname(string realtestname)
            {
                int i = -1;
                i = Parsstring.FindIndex(x => x == "TESTNAME");
                if (1 != -1)
                {
                    //I should always be the same. 3
                    Parsstring[i] = realtestname;
                }
            }
            //used to change the "TEMPERATURE"
            public void Inserttemperature(string temperature)
            {
                int i = -1;
                i = Parsstring.FindIndex(x => x == "TEMPERATURE");
                if (1 != -1)
                {
                    //I should always be the same. 3
                    Parsstring[i] = temperature;
                }
            }
        }
        // This is the class that will be deserialized.
        public class Test
        {
            public string id;//test name
            public string filename;//"for ex. pyhton.exe"
            public string workingdirectory;
            public string arguments;//"path to .py"
            [XmlElement("Parsstring")]
            public List<string> Parsstring;//5 for smoke test
            public string interceptio;

            public Test()
            {
                Parsstring = new List<string>();
            }
        }
   
        public Parameter()
        {
            Quicktest = new List<string>();
            Temperature = new List<string>();
            //Setupgeneral = new Setupparameters();
            tests = new List<Test>();
            listparameters = new List<Setupparameters>();
        }
        /*
        Test builder does the final finishing off on quicktest, it then add it to
        the tests list ready for the Form1 to acces and run
        */
        public List<Test> Tests()
        {
            //listparameters.Add(new Setupparameters(Setupgeneral));
            //For each string of quicktest create a new test in a list
            foreach (string s in Quicktest)
            {
                foreach (Setupparameters sp in listparameters)
                {
                    foreach (string temperature in Temperature)
                    {
                        //use the copy constructor here
                        tests.Add(new Setupparameters(sp));
                        //find the last elemnent in the list and insert the right test name. cast
                        (tests[tests.Count - 1] as Setupparameters).Inserttestname(s);
                        (tests[tests.Count - 1] as Setupparameters).Inserttemperature(temperature);
                        //change the test id so that it includes the test name
                        tests[tests.Count - 1].id += s + temperature;
                        //tests.Add(listparameters[listparameters.Count - 1]);
                    }
                }
            }
            return tests;
        }
        //public List<Setupparameters> Listsetupparameters()
        //{
        //    //For each string of quicktest create a new test in a list
        //    foreach (string s in Quicktest)
        //    {
        //        //use the copy constructor here
        //        listparameters.Add(new Setupparameters(Setupgeneral));
        //        //find the last elemnent in the list and insert the right test name
        //        listparameters[listparameters.Count - 1].Inserttestname(s);
        //        //change the test id so that it includes the test name
        //        listparameters[listparameters.Count - 1].id += " " + s;
        //        tests.Add(listparameters[listparameters.Count - 1]);
        //    }
        //    return listparameters;
        //}
    }
}
