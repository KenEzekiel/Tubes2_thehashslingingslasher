using Matrices;
using System;
using System.Windows.Forms;
using Tubes2_Stima.src;

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
            // Matrix b = new Matrix("../../config/test.txt");
            // Console.WriteLine(b);
            
            // b.walk("RUU");
            // Console.WriteLine(b);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Tubes2_stima.Form1());
        }
    }
}
