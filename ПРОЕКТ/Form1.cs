using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ПРОЕКТ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] mas = null;
        string[] first_lvl = null;
        string[] second_lvl = null;
        string[] third_lvl = null;

        private void ОткрытьДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName.Length > 0)
            {
                try
                {
                    mas = File.ReadAllLines(openFileDialog1.FileName);
                    richTextBox1.Lines = mas;
                    OnScreen();
                    

                }
                catch (Exception)
                { }
            }

        }

        void OnScreen()
        {
            bool ok = true;
            string[] splitted = richTextBox1.Text.Split(new char[] { '\n', '-' }, StringSplitOptions.RemoveEmptyEntries);
            first_lvl = new string[splitted.Length/2];
            string[]second_lvl_dop = new string[splitted.Length / 2];
            second_lvl = new string[4];
            third_lvl = new string[4];
            for(int x=0,i=0,j=0;x<splitted.Length;x++)
            {
                if (ok == true)
                {
                    first_lvl[i] = splitted[x];
                    i++;
                    ok = !ok;
                }
                else
                {
                    second_lvl_dop[j] = splitted[x];
                    j++;
                    ok = !ok;
                }
                
            }
            bool ok1 = true;
            int t = 0;int k = 0;
            for(int ii=0;ii<second_lvl_dop.Length;ii++)
            {
                
                splitted = second_lvl_dop[ii].Split(new char[] { '|', ';' }, StringSplitOptions.RemoveEmptyEntries);
                for(int p=0;p<splitted.Length;p++)
                {
                    if(ok1==true)
                    {
                        second_lvl[t] = splitted[p];
                        t++;
                        ok1 = !ok1;
                    }
                    else
                    {
                        third_lvl[k] = splitted[p];
                        k++;
                        ok1 = !ok1;
                    }
                }
            }
        }
        

        void DrawLine()
        {

        }
        List<Label> lblList = new List<Label>();
        void CreateLabel()
        {
            for(int i=0;i<first_lvl.Length;i++)
            {
                Label lbl = new Label();
                lbl.Text = first_lvl[i];
                lbl.Location = new Point(300*(i+1), 100);
                lblList.Add(lbl);
                this.Controls.Add(lbl);

            }
            for(int j=0;j<second_lvl.Length;j++)
            {
                Label lbl = new Label();
                lbl.Text = second_lvl[j];
                lbl.Location = new Point(300+100*(j+1), 120);
                lblList.Add(lbl);
                this.Controls.Add(lbl);
            }
                   
        }
      
        
        private void button1_Click(object sender, EventArgs e)
        {
            CreateLabel();
        }
       
    }
}
