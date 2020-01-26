namespace SMWControlLibBackend.Interfaces.Graphics
{
    public interface ITileCollection
    {
        /// <summary>
        /// Adds the tiles.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        void AddTiles(ITileCollection tiles);
        /// <summary>
        /// Removes the tiles.
        /// </summary>
        void RemoveTiles();
    }
}
