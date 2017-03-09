using System;
using System.Windows.Forms;

namespace Hotel
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //Inicio inicio = new Inicio();
            //if (inicio.ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new Form1());
            //}
            //else
            //{
            //    //Application.Exit();
            //}
        }
    }
}
