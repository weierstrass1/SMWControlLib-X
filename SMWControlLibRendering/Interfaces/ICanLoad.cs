namespace SMWControlLibRendering.Interfaces
{
    public interface ICanLoad
    {
        /// <summary>
        /// Loads the graphics.
        /// </summary>
        /// <param name="path">The path.</param>
        void Load(string path);
        /// <summary>
        /// Loads the graphics.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        void Load(byte[] bin);
        /// <summary>
        /// Loads the graphics.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="offset">The offset.</param>
        void Load(string path, int offset);
        /// <summary>
        /// Loads the graphics.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="offset">The offset.</param>
        void Load(byte[] bin, int offset);
    }
}
