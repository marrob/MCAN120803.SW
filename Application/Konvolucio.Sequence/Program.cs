using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Konvolucio.Sequence
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Engine en = new Engine();
            


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
