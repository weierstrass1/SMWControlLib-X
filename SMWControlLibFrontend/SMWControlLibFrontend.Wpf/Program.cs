using System;
using Eto.Forms;

namespace SMWControlLibFrontend.Wpf
{
    /// <summary>
    /// The main class.
    /// </summary>
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Eto.Platforms.Wpf).Run(new MainForm());
        }
    }
}
