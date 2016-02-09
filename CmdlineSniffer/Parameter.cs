﻿using System.Collections.Generic;

using System.Xml.Serialization;

namespace PyLauncher
{
    // This is the class that will be deserialized.
    public class Parameter
    {
        [XmlIgnore]
        List<Setupparameters> listparameters;
        [XmlElement("Quicktest")]
        public List<string> Quicktest;
        // This is the class that will be deserialized.
        public class Setupparameters
        {
            public string id;//Inverter nickname
            public string filename;//"for ex. pyhton.exe"
            public string workingdirectory;
            public string arguments;//"path of to .py"
            [XmlElement("Parsstring")]
            public List<string> Parsstring;//5 for smoke test
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
            //use to change the genereic "TESTNAME" to a real test name
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
        [XmlElement("Setupparameters")]
        public Setupparameters Setupgeneral;
        [XmlElement("Test")]
        public List<Test> tests;

        public Parameter()
        {
            Quicktest = new List<string>();
            Setupgeneral = new Setupparameters();
            tests = new List<Test>();
            listparameters = new List<Setupparameters>();
        }

        public List<Setupparameters> Listsetupparameters()
        {
            foreach (string s in Quicktest)
            {
                //use the copy constructor here
                listparameters.Add(new Setupparameters(Setupgeneral));
                //find the last elemnent in the list and insert the right test name
                listparameters[listparameters.Count - 1].Inserttestname(s);
                
            }

            return listparameters;
        }
    }
}
