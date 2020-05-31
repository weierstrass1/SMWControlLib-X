using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Interfaces.Graphics;
using SMWControlLibRendering.Enumerator;
using System.Collections.Generic;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The tilemap grid.
    /// </summary>
    public class TilemapGrid : TileGrid
    {
        protected List<Layer> layers;
        public int SelectedLayer { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TilemapGrid"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="z">The z.</param>
        /// <param name="bgR">The bg r.</param>
        /// <param name="bgG">The bg g.</param>
        /// <param name="bgB">The bg b.</param>
        public TilemapGrid(int width, int height, Zoom z, BytesPerPixel bpp, params byte[] color) : base(width, height, z, bpp, color)
        {
            layers = new List<Layer>();
        }

        public override void AddingAtPosition(int x, int y, TileMaskCollection selection)
        {
            if (selection == null) return;

            selection.MoveTo(x, y);

            int drx = selection.Left;
            int dry = selection.Top;
            int drr = drx + selection.Width;
            int drb = dry + selection.Height;
            int cz = CellSize;
            drx /= cz;
            drx *= cz;
            dry /= cz;
            dry *= cz;
            drr += (drr % cz);
            drb += (drb % cz);
            int w = drr - drx;
            int h = drb - dry;

            layer1.DrawRectangle(drx, dry, w, h, BackgroundColor);
            layers[SelectedLayer].AddTiles(selection);

            ITileCollection col = layers[SelectedLayer].TilesOnArea(drx, dry, w, h);
            drawTileMaskCollection(col);
            UpdateLayer2();
            updateOffsetAndCopyLenght(drx, dry, drr, drb);
        }

        public override void ClearTileSelection()
        {
            base.ClearTileSelection();
        }

        public override unsafe void CopyTo(byte* b, int left, int right, int top, int bottom)
        {
            base.CopyTo(b, left, right, top, bottom);
        }

        public override void DecreaseZIndex()
        {
            base.DecreaseZIndex();
        }

        public virtual void SelectionSizeChange(int x, int y)
        {

        }
    }
}