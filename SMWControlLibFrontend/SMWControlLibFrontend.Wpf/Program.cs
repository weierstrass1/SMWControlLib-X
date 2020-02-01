using Eto.Forms;
using SMWControlLibRendering;
using System;

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
            new Application(Eto.Platforms.Wpf).Run(MainForm<CPUBitmapBuffer>.CreateInstance());
        }
    }
}
