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

        Microsoft.Msagl.Drawing.Graph graph; // The graph that MSAGL accepts
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        // Graph viewer engine
        private Graph graf;
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

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

        private void DrawGraph(Graph graf)
        {

            List<Tuple<string, string>> visited = new List<Tuple<string, string>>();
            graph = new Microsoft.Msagl.Drawing.Graph("graph"); // Initialize new MSAGL graph                

            foreach (KeyValuePair<string, List<string>> entry1 in graf.getAdjacent())
            {
                // do something with entry.Value or entry.Key
                foreach (var entry2 in entry1.Value)
                {
                    if (!visited.Contains(Tuple.Create(entry1.Key, entry2)) && !visited.Contains(Tuple.Create(entry2, entry1.Key)))
                    {
                        Microsoft.Msagl.Drawing.Edge e;
                        e = graph.AddEdge(entry1.Key, entry2);
                        e.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        e.Attr.Id = entry1.Key + entry2;
                    }
                    visited.Add(Tuple.Create(entry1.Key, entry2));
                }
            }
            drawContainer(graph);
        }
        public void drawContainer(Microsoft.Msagl.Drawing.Graph graph)
        {
            graph.LayoutAlgorithmSettings = new Microsoft.Msagl.Layout.MDS.MdsLayoutSettings();
            viewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.UseSettingsOfTheGraph;
            viewer.Graph = graph;
            // Bind graph to viewer engine
            viewer.Graph = graph;
            // Bind viewer engine to the panel
            panel_DrawGraph.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel_DrawGraph.Controls.Add(viewer);
            panel_DrawGraph.ResumeLayout();
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Bikin fungsi dfs bfs, trs panggil disini. Ini button submit ceunah wkwk 
            if (radioButton1.Checked)
            
            {
                
                if (comboBox3.Text == "Show Graph")
                {
                    string x = "DFS1";
                    DrawGraph(this.graf);
                    textBox1.Text = x;
                }
                else if (comboBox3.Text == "Friend Recommendation")
                {
                    Graph.DFS d;
                    d = new Graph.DFS(this.graf, ref graph, ref panel_DrawGraph, ref viewer);
                    List<string> recommendation = new List<string>();
                    List<string> node = new List<string>(d.friendsRecommendation(comboBox1.Text));
                    string x = "Daftar rekomendasi teman untuk akun " + comboBox1.Text +":\r\n";
                    x = x + d.friendRecommendationText(comboBox1.Text,node);

                    textBox1.Text = x;
                }
                else if (comboBox3.Text == "Explore Friends")
                {
                    Graph.DFS d;
                    d = new Graph.DFS(this.graf, ref graph, ref panel_DrawGraph, ref viewer);
                    List<string> answer = new List<string>(d.exploreFriends(comboBox1.Text, comboBox2.Text));
                    string x = "Nama akun : " + comboBox1.Text + " dan " + comboBox2.Text + "\r\n";
                    x = x + d.exploreFriendsText(answer);
                    textBox1.Text = x;
                }

            }
            if (radioButton2.Checked)
            {
                if (comboBox3.Text == "Show Graph")
                {
                    string x = "BFS1";
                    textBox1.Text = x;
                }
                else if (comboBox3.Text == "Friend Recommendation")
                {
                    Graph.BFS b;
                    b = new Graph.BFS(this.graf, ref graph, ref panel_DrawGraph, ref viewer);
                    List<string> recommendation = new List<string>();
                    List<string> node = new List<string>(b.friendsRecommendation(comboBox1.Text));
                    string x = "Daftar rekomendasi teman untuk akun " + comboBox1.Text +":\r\n";
                    x = x + b.friendRecommendationText(comboBox1.Text,node);

                    textBox1.Text = x;

                }
                else if (comboBox3.Text == "Explore Friends")
                {
                    Graph.BFS b;
                    b = new Graph.BFS(this.graf, ref graph, ref panel_DrawGraph, ref viewer);
                    List<string> answer = new List<string>(b.exploreFriends(this.currentAccount, this.targetAccount));
                    string x = "Nama akun : " + comboBox1.Text + " dan " + comboBox2.Text + "\r\n";
                    x = x + b.exploreFriendsText(answer);
                    textBox1.Text = x;
                }
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
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