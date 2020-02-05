using Eto.Forms;
using System;

namespace SMWControlLibFrontend.Gtk
{
    /// <summary>
    /// The main class.
    /// </summary>
    class MainClass
    {
        /// <summary>
        /// Mains the.
        /// </summary>
        /// <param name="args">The args.</param>
        [STAThread]
        public static void Main()
        {
            new Application(Eto.Platforms.Gtk).Run(new MainForm());
        }
    }
}
