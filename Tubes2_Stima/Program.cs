﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Players;
using Matrices;

namespace Tubes2_Stima
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Player p = new Player(0, 0);
            Matrix b = new Matrix("../../config/test.txt", ref p);
            b.visualize("../../config/testSysDrawBefore.png");

            // kalo mo liat map + berapa kali step suatu kotak
            // Console.WriteLine(b);

            // isi solusi dari bfs/dfs
            string walkPath = "RUUUDDRL";

            b.walk(p, walkPath);

            // kalo mo liat map + berapa kali step suatu kotak
            // Console.WriteLine(b);
            b.visualize("../../config/testSysDrawAfter.png");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Tubes2_stima.Form1());
        }
    }
}
