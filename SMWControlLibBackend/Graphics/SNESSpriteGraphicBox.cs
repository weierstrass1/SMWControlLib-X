using SMWControlLibSNES.Enumerators.Graphics;
using SMWControlLibSNES.Graphics.DirtyClasses;
using SMWControlLibCommons.Graphics;
using SMWControlLibRendering.Colors;
using SMWControlLibSNES.Factory.Graphics;
using ILGPU.Runtime;
using SMWControlLibRendering;
using ILGPU;
using SMWControlLibSNES.KernelStrategies.GraphicBox;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// The sprite tile g f x box.
    /// </summary>
    public class SpriteTileGFXBox : GraphicBox<byte, ColorA1R5G5B5, DirtySNESSpriteTile, SNESSpriteTile, DirtySNESSpriteTileFactory>
    {
        /// <summary>
        /// Gets the size.
        /// </summary>
        public GFXBoxSize Size { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileGFXBox"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public SpriteTileGFXBox(GFXBoxSize size) : base(size.Width, size.Height)
        {
            Size = size;
        }
        /// <summary>
        /// Selects the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="props">The props.</param>
        /// <returns>A SpriteTileMaskCollection.</returns>
        public SpriteTileMaskCollection SelectTiles(int x,
                                                    int y,
                                                    int width,
                                                    int height,
                                                    SpriteTileSizeMode mode,
                                                    SpriteTileProperties props)
        {
            SpriteTileMaskCollection Selection = new SpriteTileMaskCollection();

            bool onlySmall = width <= (mode.SmallSize.Width >> 3) &&
                                height <= (mode.SmallSize.Height >> 3);

            SpriteTileSize selected = mode.BigSize;

            if (onlySmall)
                selected = mode.SmallSize;

            int upi = selected.Width >> 3;
            int upj = selected.Height >> 3;
            int ilim = width - (width % upi);
            int jlim = height - (height % upj);
            bool extraColumn = (height % upj) != 0;
            int extraColumnI = width - upi;
            int extraColumnI3 = extraColumnI << 3;
            int j3;

            for (int j = 0; j < jlim; j += upj)
            {
                j3 = j << 3;
                for (int i = 0; i < ilim; i += upi)
                {
                    Selection.Add(new SpriteTileMask(i << 3, j3,
                            GetTile(selected, SpriteTileIndex.GetIndex(x + i, y + j)), props));
                }
                if (extraColumn)
                    Selection.Add(new SpriteTileMask(extraColumnI3, j3,
                        GetTile(selected, SpriteTileIndex.GetIndex(x + extraColumnI, y + j)), props));
            }

            bool extraRow = (width % upi) != 0;
            int extraRowJ = height - upj;
            int extraRowJ3 = extraRowJ << 3;
            if (extraRow)
            {
                for (int i = 0; i < ilim; i += upi)
                {
                    Selection.Add(new SpriteTileMask(i << 3, extraRowJ3,
                            GetTile(selected, SpriteTileIndex.GetIndex(x + i, y + extraRowJ)), props));
                }
            }

            if (extraRow && extraColumn)
            {
                if (!onlySmall && width - ilim <= mode.SmallSize.Width && height - jlim <= mode.SmallSize.Height)
                {
                    selected = mode.SmallSize;
                    upi = selected.Width >> 3;
                    upj = selected.Height >> 3;
                    extraColumnI = width - upi;
                    extraColumnI3 = extraColumnI << 3;
                    extraRowJ = height - upj;
                    extraRowJ3 = extraRowJ << 3;
                }

                Selection.Add(new SpriteTileMask(extraColumnI3, extraRowJ3,
                            GetTile(selected, SpriteTileIndex.GetIndex(x + extraColumnI, y + extraRowJ)), props));
            }

            return Selection;
        }
        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="bin">The bin.</param>
        /// <param name="offset">The offset.</param>
        public override void Load(byte[] bin, int offset)
        {
            if (RealObject is IndexedGPUBitmapBuffer<byte, ColorA1R5G5B5> b)
            {
                MemoryBuffer<byte> srcBuffer = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(bin.Length);
                srcBuffer.CopyFrom(bin, 0, Index.Zero, bin.Length);
                Load4BPP.Execute(new Index3(bin.Length / 32, 8, 8), b.Buffer, srcBuffer, offset, offset / 32);
            }
        }
    }
}
