using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics.DirtyClasses;
using SMWControlLibRendering.Disguise;
using SMWControlLibCommons.Disguise;
using SMWControlLibCommons.Keys.Graphics;
using SMWControlLibCommons.Factory;
using SMWControlLibRendering.Interfaces;
using System.IO;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The sprite tile g f x box.
    /// </summary>
    public abstract class GraphicBox<TD, TT, TF> : IndexedBitmapBufferDisguise, ICanLoad    where TD : DirtyTile
                                                                                            where TT : Tile
                                                                                            where TF : DirtyTileFactory, new()
    {
        protected TF tileFactory = new TF();
        protected TileDirtyCollectionDisguise tiles = new TileDirtyCollectionDisguise();
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicBox"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public GraphicBox(int width, int height) : base(width, height)
        {
        }
        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="hIndex">The h index.</param>
        /// <param name="vIndex">The v index.</param>
        /// <returns>A SpriteTile.</returns>
        public virtual TT GetTile(TileSize size, TileIndex index)
        {
            TileSizeIndexKey key = new TileSizeIndexKey(size, index);
            return (TT)tiles.RealObject.DirtyAction(key,
                () => (TD)tileFactory.GenerateObject(size, index),
                (e) =>
                {
                    e.Tile.RealObject.DrawIndexedBitmap(RealObject, 0, 0, index.X, index.Y);
                }).Tile;
        }
        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="path">The path.</param>
        public virtual void Load(string path)
        {
            Load(path, 0);
        }

        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="bin">The bin.</param>
        public virtual void Load(byte[] bin)
        {
            Load(bin, 0);
        }
        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="offset">The offset.</param>
        public virtual void Load(string path, int offset)
        {
            byte[] bin = File.ReadAllBytes(path);
            Load(bin, 0);
        }
        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="bin">The bin.</param>
        /// <param name="offset">The offset.</param>
        public abstract void Load(byte[] bin, int offset);
    }
}
