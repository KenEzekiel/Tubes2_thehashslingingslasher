using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Tubes2_stima
{
    
    public partial class Form1 : Form
    {
        
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern System.IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        private string currentAccount;
        private string targetAccount;



        private void button_LoadFile_Click(object sender, EventArgs e)
        {
            // Browse Document
            openFileGraph.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileGraph.InitialDirectory = Directory.GetCurrentDirectory();

            // Show file dialog
            DialogResult result = openFileGraph.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Read input file
                using (StreamReader bacafile = new StreamReader(openFileGraph.OpenFile()))
                {
                    string baca = bacafile.ReadLine();
                    if (baca == null || baca == "0")
                    {
                        MessageBox.Show("File Kosong");
                    }
                    else
                    {
                        this.graf = new Graph(bacafile);
                        comboBox1.Items.Clear();
                        comboBox2.Items.Clear();
                        foreach (KeyValuePair<string, List<string>> entry1 in graf.getAdjacent())
                        {
                            comboBox1.Items.Add(entry1.Key);
                            comboBox2.Items.Add(entry1.Key);
                        }
                            DrawGraph(this.graf);
                            button1.Enabled = true;
                            comboBox3.SelectedItem = "Show Graph";
                    }
                }
            }
        }

        private void panel_DrawGraph_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentAccount = comboBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // kalo mau disable
            //button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 30, 30));
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.targetAccount = comboBox2.Text;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}