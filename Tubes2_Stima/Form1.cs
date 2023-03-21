using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Players;
using Matrices;
using Blocks;
using Tubes2_Stima.src;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace Tubes2_stima
{

    public partial class Form1 : Form
    {
        //timer buat slider
        private Timer timer = new Timer();
        //private int speed = 200;

        public Matrices.Matrix map;
        public Player p = new Player(0,0);

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
            Matrices.Matrix matrix = new Matrices.Matrix (path, ref player);

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

            Player player = new Player(3, 0);
            String path = "./config/test.txt";
            Matrices.Matrix matrix = new Matrices.Matrix(path, ref player);



            Console.WriteLine(matrix.ToString());

            Console.WriteLine(p);
            Block start = matrix.GetBlock(player.getY(), player.getX());


          
            if (radioButton1.Checked)
            {
                DFS dfs = new DFS(Treasure.getTreasureCount());
                Console.WriteLine("bangke");
                if (comboBox3.Text == "With TSP")
                {
                    //
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    steps = dfs.startSearch(start, true);
                    stopwatch.Stop();
                    // 
                    string x = "DFS1";
                    Console.WriteLine("tai");
                    Console.WriteLine(steps);
                    matrix.walk(player, steps);
                    matrix.visualize("./test.png");
                    textBox1.Text = x;
                    textBox2.Text = steps;
                    textBox3.Text = stopwatch.ElapsedMilliseconds.ToString();

                    //timer.Interval = currentSpeed / speed;
                    timer.Interval = currentSpeed;
                    timer.Tick += new EventHandler(DisplayNextImage);
                    //DisplayNextImage(sender, e);
                    timer.Start();

                    //pictureBox2.Image = Image.FromFile("./test.png");
                    //pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button_LoadFile_Click_1(object sender, EventArgs e)
        {

        }

        //berhub sm speed"an
        private int currentSpeed = 80000;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int[] speedValues = {12800, 6400, 3200, 1600, 800, 400, 200, 100, 50};
    
            // nyocokin index sm speed
            int selectedIndex = trackBar1.Value - 1;
            if (selectedIndex >= 0 && selectedIndex < speedValues.Length)
            {
                currentSpeed = speedValues[selectedIndex];
            }
            timer.Interval = currentSpeed;
        }

        // buat nampilin gbr" otomatis keganti dlm 1 folder
        static string folderPath = "./config"; 
        string[] imagePaths = System.IO.Directory.GetFiles(folderPath, "*.png").OrderBy(f => File.GetLastWriteTime(f)).ToArray();
        private int currentIndex = 0; // index current image


        private void DisplayNextImage(object sender, EventArgs e)
        {
           
            if (imagePaths == null || imagePaths.Length == 0)
            {
                MessageBox.Show("Tidak ada gambar :(");
                return;
            }

            // Load image at curr index
            string imagePath = imagePaths[currentIndex];
            Image image = Image.FromFile(imagePath);

            pictureBox2.Image = image;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            currentIndex = (currentIndex + 1) % imagePaths.Length;

            // Reset timer with new speed
            timer.Interval = currentSpeed;
            //timer.Start();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}