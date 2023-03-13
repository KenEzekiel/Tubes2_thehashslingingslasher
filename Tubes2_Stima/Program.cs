using System;
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
            Matrix b = new Matrix("../../test/tc.txt", p);
            b.visualize("../../../test/before.png");
            Console.WriteLine(b);
            char[] walkPath = { 'R', 'U', 'U', 'U', 'D', 'D', 'R', 'L' };
            b.walk(p, walkPath);
            Console.WriteLine(b);
            b.visualize("../../test/after.png");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
