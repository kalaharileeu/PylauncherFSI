using System.Collections.Generic;

using System.Xml.Serialization;

namespace PyLauncher
{
    // This is the class that will be deserialized.
    public class Parameter
    {
        [XmlElement("Quicktest")]
        public List<string> Quicktest;
        // This is the class that will be deserialized.
        public class Setup
        {
            public string id;//Inverter nickname
            public string filename;//"for ex. pyhton.exe"
            public string workingdirectory;
            public string arguments;//"path of to .py"
            [XmlElement("Parsstring")]
            public List<string> Parsstring;//5 for smoke test
            public Setup()
            {
                Parsstring = new List<string>();
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
        [XmlElement("Setup")]
        public Setup Setupgeneral;
        [XmlElement("Test")]
        public List<Test> tests;

        public Parameter()
        {
            Quicktest = new List<string>();
            Setupgeneral = new Setup();
            tests = new List<Test>();
        }
    }
}
