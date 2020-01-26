using System;
using Eto.Forms;

namespace SMWControlLibFrontend.Wpf
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
            new Application(Eto.Platforms.Wpf).Run(new MainForm());
        }
    }
}
