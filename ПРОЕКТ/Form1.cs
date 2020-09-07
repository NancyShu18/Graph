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
        int ilabelX, ilabelY, iMouseX, iMouseY;


        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        string[] mas = null;
        string[] first_lvl = null;
        int[] fNs = null;
        string[] sNt = null;
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
                    button1.Enabled = true;

                }
                catch (Exception)
                {
                    MessageBox.Show("Откройте документ согласно синтакису");
                    richTextBox1.Clear();
                }
            }
        }

        void OnScreen()
        {
            bool ok = true;
            string[] splitted = richTextBox1.Text.Split(new char[] { '\n', '-' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in splitted) s.Trim();
            first_lvl = new string[splitted.Length / 2];
            fNs = new int[first_lvl.Length];
            string[] second_lvl_dop = new string[splitted.Length / 2];

            for (int x = 0, i = 0, j = 0; x < splitted.Length; x++)
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
            for (int p = 0; p < second_lvl_dop.Length; p++)
            {
                sNt = second_lvl_dop[p].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in sNt)
                {
                    second_third.Add(s);
                }
            }
            second_lvl = new string[second_third.Count];
            third_lvl = new string[second_third.Count];
            for (int z = 0; z < second_lvl_dop.Length; z++)
            {
                string[] s = second_lvl_dop[z].Split(new char[] { '|', ';' }, StringSplitOptions.RemoveEmptyEntries);
                fNs[z] = s.Length / 2;
            }
            bool ok1 = true;
            int t = 0; int k = 0;
            for (int ii = 0; ii < second_lvl_dop.Length; ii++)
            {
                splitted = second_lvl_dop[ii].Split(new char[] { '|', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in splitted) s.Trim();
                for (int p = 0; p < splitted.Length; p++)
                {
                    if (ok1 == true)
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

        List<Label> lbl1 = new List<Label>();
        List<Label> lbl2 = new List<Label>();
        List<Label> lbl3 = new List<Label>();
        List<string> second_third = new List<string>();
        void CreateLabel()
        {
            for (int i = 0; i < first_lvl.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = first_lvl[i];
                lbl.AutoSize = true;
                lbl.Location = new Point(210 + 100 * (i + 1), 100);
                lbl.Name = lbl.Text;
                lbl.AllowDrop = true;
                lbl.MouseDown += new MouseEventHandler(this.lbl_MouseDown);
                lbl.MouseMove += new MouseEventHandler(this.lbl_MouseMove);
                lbl1.Add(lbl);
                this.Controls.Add(lbl);

            }
            for (int j = 0; j < second_lvl.Length; j++)
            {
                Label lbl = new Label();
                lbl.Text = second_lvl[j];
                lbl.Location = new Point(210 + 100 * (j + 1), 170);
                lbl.Name = lbl.Text;
                lbl.AutoSize = true;
                lbl.AllowDrop = true;
                lbl.MouseDown += new MouseEventHandler(this.lbl_MouseDown);
                lbl.MouseMove += new MouseEventHandler(this.lbl_MouseMove);
                lbl2.Add(lbl);
                this.Controls.Add(lbl);
            }
            for (int k = 0; k < third_lvl.Length; k++)
            {
                bool found = false;
                Label lbl = new Label();
                lbl.Text = third_lvl[k];
                lbl.Name = lbl.Text;
                foreach (Label labe in lbl3)
                {
                    if (labe.Name == lbl.Name)
                    {
                        found = true;
                        break;
                    }

                }
                if (!found)
                {
                    lbl.Location = new Point(210 + 100 * (k + 1), 240);
                    lbl.AllowDrop = true;
                    lbl.AutoSize = true;
                    lbl.MouseDown += new MouseEventHandler(this.lbl_MouseDown);
                    lbl.MouseMove += new MouseEventHandler(this.lbl_MouseMove);
                    lbl3.Add(lbl);
                    this.Controls.Add(lbl);
                }
            }
            //DrawLine();
            this.Paint += Form_Paint;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateLabel();
        }

        private void lbl_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            ilabelX = label.Location.X;
            ilabelY = label.Location.Y;
            iMouseX = MousePosition.X;
            iMouseY = MousePosition.Y;
        }
        private void lbl_MouseMove(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            int iMouseX2 = MousePosition.X;
            int iMouseY2 = MousePosition.Y;
            if (e.Button == MouseButtons.Left)
                label.Location = new Point(ilabelX + (iMouseX2 - iMouseX), ilabelY + (iMouseY2 - iMouseY));
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Black);
            int curr = 0;
            for (int i = 0; i < first_lvl.Length; i++)
            {

                for (int j = 0; j < fNs[i]; j++)
                {
                    e.Graphics.DrawLine(p, lbl1[i].Location, lbl2[curr++].Location);
                }
            }
            for (int i = 0; i < lbl2.Count; i++)
            {
                for (int j = 0; j < lbl3.Count; j++)
                {
                    for (int k = 0; k < second_third.Count; k++)
                    {
                        if (second_third[k].Contains(lbl2[i].Name) && second_third[k].Contains(lbl3[j].Name))
                        {
                            e.Graphics.DrawLine(p, lbl2[i].Location, lbl3[j].Location);
                            break;
                        }

                    }
                }
            }
            this.Refresh();
        }
    }
}
