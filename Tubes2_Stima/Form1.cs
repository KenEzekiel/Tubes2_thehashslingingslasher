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
using Players;
using Matrices;
using Blocks;
using Tubes2_Stima.src;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SkiaSharp;

namespace Tubes2_stima
{

    public partial class Form1 : Form
    {

        public Matrices.Matrix map;
        public Player p;

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


        public Form1()
        {
            InitializeComponent();
            button1.Enabled = true;
        }

        private void panel_DrawGraph_Paint(object sender, PaintEventArgs e)
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


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button_LoadFile_Click(object sender, EventArgs e)
        {
            Player player = new Player(0, 0);
            String path = "./config/test.txt";
            Matrices.Matrix matrix = new Matrices.Matrix (path, player);

            this.p = player;
            this.map = matrix;

            button1.Enabled = true;
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            // Steps
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Start search
            // debug DFS
            String steps = "";

            Block start = map.GetBlock(p.getX(), p.getY());

            DFS dfs = new DFS(Treasure.getTreasureCount());
            steps = dfs.startSearch(start);


            label7.Text = steps;
            if (radioButton1.Checked)
            {
                if (comboBox3.Text == "With TSP")
                {
                    string x = "DFS1";
                    // visualisasiin
                    textBox1.Text = x;
                }
                else if (comboBox3.Text == "Without TSP")
                {
                    string x = "DFS2";
                    // visualisasiin
                    textBox1.Text = x;
                }
            }
            if (radioButton2.Checked)
            {
                if (comboBox3.Text == "With TSP")
                {
                    string x = "BFS1";
                    // visualisasiin
                    textBox1.Text = x;
                }
                else if (comboBox3.Text == "Without TSP")
                {
                    string x = "BFS2";
                    // visualisasiin
                    textBox1.Text = x;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}