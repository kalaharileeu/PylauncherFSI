using System;
using System.Windows.Forms;
/*
This file consists of all the interrupts for buttons and clicks etc.
*/
namespace PyLauncher
{
    public partial class Form1 : Form
    {
        //only populate vogel test items in the listbox 1
        public void button1_Click(object sender, EventArgs e)
        {
            for (int k = listBox1.Items.Count - 1; k >= 0; --k)
            {
                listBox1.Items.RemoveAt(k);
            }
            //storedvalues = new List<string>();
            for (int n = storedvalues.Count - 1; n >= 0; --n)
            {
                string addlistitem = "Vogel";
                if (storedvalues[n].Contains(addlistitem))
                {
                    listBox1.Items.Add(storedvalues[n]);
                }
                /*   if (listBox1.Items[n].ToString().Contains(removelistitem))
                    {
                        storedvalues.Add((string)listBox1.Items[n]);
                        listBox1.Items.RemoveAt(n);
                    }*/
            }
            /*            string buttonid = "Testtime";
                        Parameter.Test Testtime = new Parameter.Test();
                        Testtime = tests.tests.Find(x => x.id.Equals(buttonid));//workds to find test id
                        if (Testtime != null)
                            Console.WriteLine("Found it");
                        else
                            Console.WriteLine("Did not find it");
             */

        }
        //run the main process loop 
        private void button2_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;//cannot run rack unit diagnostics
            keepreporting = true;//start reporting
            runmainprocess();
            Reporting();
        }
        //only populate diogenes test items in the listbox 1
        private void button3_Click(object sender, EventArgs e)
        {
            for (int k = listBox1.Items.Count - 1; k >= 0; --k)
            {
                listBox1.Items.RemoveAt(k);
            }

            for (int n = storedvalues.Count - 1; n >= 0; --n)
            {
                string addlistitem = "Diogenes";
                if (storedvalues[n].Contains(addlistitem))
                {
                    listBox1.Items.Add(storedvalues[n]);
                }
            }
        }
        //only populate corvus test items in the listbox 1
        private void button4_Click(object sender, EventArgs e)
        {
            for (int k = listBox1.Items.Count - 1; k >= 0; --k)
            {
                listBox1.Items.RemoveAt(k);
            }

            for (int n = storedvalues.Count - 1; n >= 0; --n)
            {
                string addlistitem = "Corvus";
                if (storedvalues[n].Contains(addlistitem))
                {
                    listBox1.Items.Add(storedvalues[n]);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void list_Box1DoubleClick(object sender, MouseEventArgs e)//list box 1 double cllick
        {
            if (textBox1.Text.Length < 12)
            {
                MessageBox.Show("invalid SN");
                return;
            }
            else
                textBox1.Text = textBox1.Text.Substring(textBox1.Text.Length - 12);

            textBox1.Refresh();

            int index = this.listBox1.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)//check if item is there
            {
                string item = (string)(this.listBox1.Items[index]);//get cliked it fom listbox 1
                listBox2.Items.Add(item);//add item to litbox 2
                //MessageBox.Show(index.ToString());
            }
        }

        private void list_box2DoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox2.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)//check if item is there
            {
                object item = this.listBox2.Items[index];//get clicked it fom listbox 1

                if (index >= 0)
                {
                    this.listBox2.Items.RemoveAt(index);//remove item
                }
            }
        }

        private void list_box2Click(object sender, MouseEventArgs e)
        {
            int index = this.listBox2.IndexFromPoint(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (index != ListBox.NoMatches)//check if item is there
                {
                    object item = this.listBox2.Items[index];//get clicked it fom listbox 1

                    if (index > 0)
                    {
                        this.listBox2.Items.RemoveAt(index);//remove item
                        this.listBox2.Items.Insert(index - 1, item);
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                if (index != ListBox.NoMatches)//check if item is there
                {
                    object item = this.listBox2.Items[index];//get cliked it fom listbox 1

                    if (index >= 0)
                    {
                        this.listBox2.Items.RemoveAt(index);//remove item
                    }
                }
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            // textBox1.Text = textBox1.Text.Substring(textBox1.Text.Length - 12);
            // textBox1.Refresh();
        }

        //       /* public async void reporttoserver()
        //        {
        //          /* if(listBox2.Items.Count != 0)
        //            {
        //                string value = await Task.Run(() => report1.Reporttoserver(status, test, ref error));
        //            }
        //            */
        //    }
    }
}
