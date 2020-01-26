using System;
using Eto.Forms;

namespace SMWControlLibFrontend.Gtk
{
    /// <summary>
    /// The main class.
    /// </summary>
    class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application(Eto.Platforms.Gtk).Run(new MainForm());
        }
    }
}
