using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Interfaces.Graphics;
namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite tile section mask.
    /// </summary>
    public class SpriteTileSectionMask : IGridDrawable
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
        public SpriteTileSection Section { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileSectionMask"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public SpriteTileSectionMask(SpriteTileSection s)
        {
            Section = s;
            X = -1;
            Y = -1;
        }
        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <returns>A SpriteTileMaskCollection.</returns>
        public SpriteTileMaskCollection GetCollection()
        {
            return Section[Index];
        }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        public uint[] GetGraphics(Zoom z)
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
        public void Select(int x, int y, int width, int height)
        {
            GetCollection().FindByArea(x, y, width, height);
        }

        /// <summary>
        /// Adds the tiles.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        public void AddTiles(ITileCollection tiles)
        {
            GetCollection().AddCollection((SpriteTileMaskCollection)tiles);
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
        public void MoveTiles(int x, int y)
        {
            GetCollection().MoveSelection(x, y);
        }

        /// <summary>
        /// Increases the z index.
        /// </summary>
        public void IncreaseZIndex()
        {
            GetCollection().IncreaseSelectionZIndex();
        }

        /// <summary>
        /// Decreases the z index.
        /// </summary>
        public void DecreaseZIndex()
        {
            GetCollection().DecreaseSelectionZIndex();
        }
    }
}
