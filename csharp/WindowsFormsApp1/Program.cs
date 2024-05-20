using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) //args hozzáadni
        {
            if (args.Contains("--stat")) 
                //Consol futtatás >> Start/debug properties/command line/--stat
            { 
                Statistics.Run();
            }
            else
            {
                //Form futtatás
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
