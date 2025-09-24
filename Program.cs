// File: Program.cs

using System;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Console.InputEncoding = System.Text.Encoding.UTF8;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}