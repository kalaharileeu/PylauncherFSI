using System;
using System.Windows.Forms;
/*
This file consists of all the interrupts for buttons and clicks etc.
*/
namespace PyLauncher
{
    public partial class Form1 : Form
    {
        //only populate vog test items in the listbox 1
        //use the storedvalue variable: data xml deserialization
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
            }
        }
        //run the main process loop 
        private void button2_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;//cannot run rack unit diagnostics
            keepreporting = true;//start reporting
            runmainprocess();
            Reporting();
        }
        //only populate dioge test items in the listbox 1
        //use the storedvalue variable: data xml deserialization
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
        //only populate corv test items in the listbox 1
        //use the storedvalue variable: data xml deserialization
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
        //list box 1 double cllick
        private void list_Box1DoubleClick(object sender, MouseEventArgs e)
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
                //get clicked it from listbox 1
                string item = (string)(this.listBox1.Items[index]);
                listBox2.Items.Add(item);//add item to litbox 2
                //MessageBox.Show(index.ToString());
            }
        }

        private void list_box2DoubleClick(object sender, MouseEventArgs e)
        {
            //int index = this.listBox2.IndexFromPoint(e.Location);

            //if (index != ListBox.NoMatches)//check if item is there
            //{
            //    object item = this.listBox2.Items[index];//get clicked it fom listbox 1

            //    if (index >= 0)
            //    {
            //        this.listBox2.Items.RemoveAt(index);//remove item
            //    }
            //}
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

        private void list2_rightclick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
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
