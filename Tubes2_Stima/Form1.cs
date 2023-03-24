using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Blocks;
using Tubes2_Stima.src;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using System.Threading.Tasks;

namespace Tubes2_stima
{

    public partial class Form1 : Form
    {
        private bool isDone = false;
        private string filePath;
        //timer buat slider
        private Timer timer = new Timer();
        //private int speed = 200;

        public Matrices.Matrix map;

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
            button1.Enabled = false;

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kelompok Thehashslingingslasher\nKenneth Ezekiel\nChiquita Ahsanunnisa\nVanessa Rebecca");
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

        private void label7_Click_1(object sender, EventArgs e)
        {
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        bool stopVisualize = true;
        private void button1_Click(object sender, EventArgs e)
        {
            // Start search
            // debug DFS
            
            if (!stopVisualize)
            {
                MessageBox.Show("Wait for the visualization to finish!");
                return;
            } 
            

            String steps = "";

            Matrices.Matrix matrix = new Matrices.Matrix(filePath);



            Console.WriteLine(matrix.ToString());

            Block start = matrix.GetStart();



            if (radioButton1.Checked)
            {
                DFS dfs = new DFS(Treasure.getTreasureCount());

                if (comboBox3.Text == "With TSP")
                {
                    //
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    steps = dfs.startSearch(start, true);
                    stopwatch.Stop();
                    string x = "DFS1";
                    Console.WriteLine(steps);
                    // visualisasi disimpen di folder visualization
                    // urutan: belom diapa2in, search (animasi), route
                    matrix.visualizeAll("../../visualization/", steps, steps);
                    textBox1.Text = x;
                    textBox2.Text = steps;
                    textBox3.Text = stopwatch.ElapsedMilliseconds.ToString();
                    isDone = true;

                }
                else if (comboBox3.Text == "Without TSP")
                {
                    string x = "DFS2";
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    steps = dfs.startSearch(start, false);
                    stopwatch.Stop();
                    Console.WriteLine(steps);
                    // visualisasi disimpen di folder visualization
                    // urutan: belom diapa2in, search (animasi), route
                    matrix.visualizeAll("../../visualization/", steps, steps);
                    textBox1.Text = x;
                    textBox2.Text = steps;
                    textBox3.Text = stopwatch.ElapsedMilliseconds.ToString();
                    isDone = true;


                }
            }
            if (radioButton2.Checked)
            {
                BFS bfs = new BFS(matrix);

                if (comboBox3.Text == "With TSP")
                {
                    string x = "BFS1";
                    string search = "";
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    string step = bfs.Search(true, ref search);
                    stopwatch.Stop();
                    // visualisasi disimpen di folder visualization
                    // urutan: belom diapa2in, search (animasi), route
                    matrix.visualizeAll("../../visualization/", step, search);
                    textBox1.Text = x;
                    textBox2.Text = step;
                    textBox3.Text = stopwatch.ElapsedMilliseconds.ToString();
                    isDone = true;

                }
                else if (comboBox3.Text == "Without TSP")
                {
                    string x = "BFS2";
                    string search = "";
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    string step = bfs.Search(false, ref search);
                    stopwatch.Stop();
                    // visualisasi disimpen di folder visualization
                    // urutan: belom diapa2in, search (animasi), route
                    matrix.visualizeAll("../../visualization/", step, search);
                    textBox1.Text = x;
                    textBox2.Text = step;
                    textBox3.Text = stopwatch.ElapsedMilliseconds.ToString();
                    isDone = true;

                }
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button_LoadFile_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    Matrices.Matrix matrix = new Matrices.Matrix(filePath);
                    this.map = matrix;
                    button1.Enabled = true;
                    button_LoadFile.Text = Path.GetFileName(filePath); // ganti text jd nama chosen file
                }
            }
        }



        //berhub sm speed"an
        private int currentSpeed = 80000;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int[] speedValues = { 12000, 1000, 8000, 6000, 4000, 3000, 2000, 1000, 500 };

            // nyocokin index sm speed
            int selectedIndex = trackBar1.Value - 1;
            if (selectedIndex >= 0 && selectedIndex < speedValues.Length)
            {
                currentSpeed = speedValues[selectedIndex];
            }
        }

        // buat nampilin gbr" otomatis keganti dlm 1 folder
        static string folderPath = "../../visualization/";
        

        private int currentIndex = 0; // index current image


        private async void displayImage()
        {
            if (isDone)
            {
                string[] imagePaths = System.IO.Directory.GetFiles(folderPath, "*.png").OrderBy(f => File.GetLastWriteTime(f)).ToArray();
                // ada sedikit masalah minor di sini, jadi kalo mo animasi, pastiin gambarnya udah selsai dibikin semua baru klik slider
                // karna kalo ngga nanti gambarnya kepotong / keluar error out of bounds
                if (imagePaths == null || imagePaths.Length == 0)
                {
                    MessageBox.Show("Tidak ada gambar :(");
                    return;
                } else
                {
                    Console.WriteLine(imagePaths);
                    foreach(string img in imagePaths)
                    {
                        // Load image at curr index

                        Image image = Image.FromFile(img);

                        pictureBox2.Image = image;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                        await Task.Delay(TimeSpan.FromSeconds(currentSpeed / 1000));
                        if (stopVisualize)
                        {
                            pictureBox2.Image = null;
                            break;
                        }
                    }
                    
                }
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Visualize

            Console.WriteLine("Visualizer");
            stopVisualize = !stopVisualize;

            if (!stopVisualize)
            {
                displayImage();
                pictureBox2.Image = null;
            }
            

        }
    }
}