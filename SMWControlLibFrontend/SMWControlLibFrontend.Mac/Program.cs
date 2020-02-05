using Eto.Forms;
using System;

namespace SMWControlLibFrontend.Mac
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
            new Application(Eto.Platforms.Mac64).Run(new MainForm());
        }
    }
}
