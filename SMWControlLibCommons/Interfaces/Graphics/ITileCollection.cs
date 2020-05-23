using SMWControlLibCommons.DataStructs;
using System.Collections.Generic;

namespace SMWControlLibCommons.Interfaces.Graphics
{
    public interface ITileCollection
    {
        /// <summary>
        /// Gets the left.
        /// </summary>
        int Left { get; }

        /// <summary>
        /// Gets the top.
        /// </summary>
        int Top { get; }

        /// <summary>
        /// Gets the right.
        /// </summary>
        int Right { get; }

        /// <summary>
        /// Gets the bottom.
        /// </summary>
        int Bottom { get; }

        /// <summary>
        /// Adds the tiles.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        void AddTiles(ITileCollection tiles);

        /// <summary>
        /// Removes the tiles.
        /// </summary>
        void RemoveTiles();

        /// <summary>
        /// Gets the tile borders.
        /// </summary>
        /// <returns>A list of TileBorders.</returns>
        List<TileBorder> GetTileBorders();

        /// <summary>
        /// Are the empty.
        /// </summary>
        /// <returns>A bool.</returns>
        bool IsEmpty();

        /// <summary>
        /// Gets the enumerable.
        /// </summary>
        /// <returns>A list of TS.</returns>
        IEnumerable<ITile> GetEnumerable();
    }
}