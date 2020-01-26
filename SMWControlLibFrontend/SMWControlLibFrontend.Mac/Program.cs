using System;
using Eto.Forms;

namespace SMWControlLibFrontend.Mac
{
    /// <summary>
    /// The main class.
    /// </summary>
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Eto.Platforms.Mac64).Run(new MainForm());
        }
    }
}
