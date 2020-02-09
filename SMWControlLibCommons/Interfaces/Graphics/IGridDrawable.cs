﻿using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics;
using SMWControlLibRendering;

namespace SMWControlLibCommons.Interfaces.Graphics
{
    public interface IGridDrawable<T> : ITileCollection<T>
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        int X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        int Y { get; set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        BitmapBuffer GetGraphics(Zoom z);
        /// <summary>
        /// Selects the.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        ITileCollection<T> SelectTiles(int x, int y, int width, int height);
        /// <summary>
        /// Tiles the on area.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>An ITileCollection.</returns>
        ITileCollection<T> TilesOnArea(int x, int y, int width, int height);
        /// <summary>
        /// Moves the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        bool MoveTiles(int x, int y);
        /// <summary>
        /// Increases the z index.
        /// </summary>
        bool IncreaseZIndex();
        /// <summary>
        /// Decreases the z index.
        /// </summary>
        bool DecreaseZIndex();
    }
}
