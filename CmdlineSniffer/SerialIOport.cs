using System.IO.Ports;

namespace PyLauncher
{
    public class SerialIOport
    {
        string[] serialitems;

        public void Getserialportnames()
        {
            serialitems = SerialPort.GetPortNames();
        }

        public bool CheckPortName(string value)
        {
            foreach(var v in serialitems)
            {
                if (v.Contains(value) && value != "")
                    return true;
            }
            return false;
        }

        public string Getportstring()
        {
            string s = "";
            foreach(var v in serialitems)
            {
                s += " " + v;
            }
            return s;
        }
    }
}
