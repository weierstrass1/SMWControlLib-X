using SMWControlLibRendering;
using SMWControlLibCommons.DataStructs;
using SMWControlLibCommons.Enumerators.Graphics;
using System.Collections.Generic;
using SMWControlLibCommons.Interfaces.Graphics;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The sprite tile section mask.
    /// </summary>
    public class TileSectionMask : IGridDrawable
    {
        private int index;
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                if (value >= 0 && value < Section.Lenght)
                    index = value;
            }
        }
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Gets the left.
        /// </summary>
        public int Left => GetCollection().Left;
        /// <summary>
        /// Gets the top.
        /// </summary>
        public int Top => GetCollection().Top;
        /// <summary>
        /// Gets the right.
        /// </summary>
        public int Right => Left + Width;
        /// <summary>
        /// Gets the bottom.
        /// </summary>
        public int Bottom => Top + Height;
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width => Section.Width;
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height => Section.Height;
        /// <summary>
        /// Gets a value indicating whether require refresh.
        /// </summary>
        public bool RequireRefresh => Section[Index].RequireRefresh;
        /// <summary>
        /// Gets the section.
        /// </summary>
        public TileSection Section { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TileSectionMask"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public TileSectionMask(TileSection s)
        {
            Section = s;
            X = -1;
            Y = -1;
        }
        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <returns>A SpriteTileMaskCollection.</returns>
        public TileMaskCollection GetCollection()
        {
            return Section[Index];
        }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        public BitmapBuffer GetGraphics(Zoom z)
        {
            return Section.GetGraphics(Index, z);
        }
        /// <summary>
        /// Selects the.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ITileCollection SelectTiles(int x, int y, int width, int height)
        {
            TileMaskCollection sel = GetCollection();

            if (width < 2 || height < 2)
                return sel.FindByPosition(x, y);
            else
                return sel.FindByArea(x, y, width, height);
        }
        /// <summary>
        /// Adds the tiles.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        public void AddTiles(ITileCollection tiles)
        {
            
            GetCollection().AddCollection((TileMaskCollection)tiles);
            X = Section.Left;
            Y = Section.Top;
        }
        /// <summary>
        /// Removes the tiles.
        /// </summary>
        public void RemoveTiles()
        {
            GetCollection().RemoveSelection();
        }

        /// <summary>
        /// Moves the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public bool MoveTiles(int x, int y)
        {
            return GetCollection().MoveSelection(x, y);
        }
        /// <summary>
        /// Increases the z index.
        /// </summary>
        public bool IncreaseZIndex()
        {
            return GetCollection().IncreaseSelectionZIndex();
        }
        /// <summary>
        /// Decreases the z index.
        /// </summary>
        public bool DecreaseZIndex()
        {
            return GetCollection().DecreaseSelectionZIndex();
        }

        /// <summary>
        /// Gets the tile borders.
        /// </summary>
        /// <returns>A list of TileBorders.</returns>
        public List<TileBorder> GetTileBorders()
        {
            return GetCollection().GetTileBorders();
        }
        /// <summary>
        /// Are the empty.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IsEmpty()
        {
            return Section.Lenght > 0;
        }
    }
}
