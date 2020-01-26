using SMWControlLibBackend.DataStructs;
using SMWControlLibBackend.Enumerators.Graphics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite tile section.
    /// </summary>
    public class SpriteTileSection : ExchangeableDynamicList<SpriteTileMaskCollection>
    {
        private int left;
        private bool requireUpdateLeft = true;
        /// <summary>
        /// Gets the left.
        /// </summary>
        public int Left 
        { 
            get
            {
                if (requireUpdateLeft)
                    left = elements.Min(e => { if (e == null) return 100000; return e.Left; });

                requireUpdateLeft = false;
                return left;
            }
            private set 
            {
                int x = Left;
                _ = Parallel.ForEach(elements, e =>
                  {
                      int delta = e.Left - x;
                      e.MoveTo(value + delta, e.Top);
                  });
                x = value;
            }
        }
        public int top;
        private bool requireUpdateTop = true;
        /// <summary>
        /// Gets the top.
        /// </summary>
        public int Top
        {
            get
            {
                if (requireUpdateTop)
                    top = elements.Min(e => { if (e == null) return 100000; return e.Top; });

                requireUpdateTop = false;
                return top;
            }
            private set
            {
                int y = Top;
                _ = Parallel.ForEach(elements, e =>
                {
                    int delta = e.Top - y;
                    e.MoveTo(e.Left, value + delta);
                });
                y = value;
            }
        }
        private int right;
        private bool requireUpdateRight = true;
        /// <summary>
        /// Gets the right.
        /// </summary>
        public int Right
        {
            get
            {
                if (requireUpdateRight)
                    right = elements.Max(e => { if (e == null) return -1; return e.Right; });

                requireUpdateRight = false;
                return right;
            }
        }
        private int bottom;
        private bool requireUpdateBottom = true;
        /// <summary>
        /// Gets the bottom.
        /// </summary>
        public int Bottom
        {
            get
            {
                if (requireUpdateBottom)
                    bottom = elements.Max(e => { if (e == null) return -1; return e.Bottom; });

                requireUpdateBottom = false;
                return bottom;
            }
        }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width => Right - Left;
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height => Bottom - Top;
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileSection"/> class.
        /// </summary>
        public SpriteTileSection() : base()
        {
            OnFrameAdded += onFrameAdded;
            OnFrameRemoved += onFrameRemoved;
        }
        /// <summary>
        /// ons the frame removed.
        /// </summary>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        private void onFrameRemoved(ExchangeableDynamicList<SpriteTileMaskCollection> arg1, int arg2)
        {
            elements[arg2].OnCollectionAdded -= onCollectionAdded;
            elements[arg2].OnMoveTo -= onMoveTo;
            elements[arg2].OnSelectionChanged -= onCollectionAdded;
            elements[arg2].OnTileAdded -= onTileAdded;
            requireUpdateLeft = true;
            requireUpdateTop = true;
            requireUpdateBottom = true;
            requireUpdateRight = true;
        }
        /// <summary>
        /// ons the frame added.
        /// </summary>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        private void onFrameAdded(ExchangeableDynamicList<SpriteTileMaskCollection> arg1, int arg2)
        {
            elements[arg2].OnCollectionAdded += onCollectionAdded;
            elements[arg2].OnMoveTo += onMoveTo;
            elements[arg2].OnSelectionChanged += onCollectionAdded;
            elements[arg2].OnTileAdded += onTileAdded;
        }
        /// <summary>
        /// ons the tile added.
        /// </summary>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        private void onTileAdded(SpriteTileMaskCollection arg1, SpriteTileMask arg2)
        {
            requireUpdateLeft = true;
            requireUpdateTop = true;
            requireUpdateBottom = true;
            requireUpdateRight = true;
        }
        /// <summary>
        /// ons the move to.
        /// </summary>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <param name="arg3">The arg3.</param>
        private void onMoveTo(SpriteTileMaskCollection arg1, int arg2, int arg3)
        {
            requireUpdateLeft = true;
            requireUpdateTop = true;
            requireUpdateBottom = true;
            requireUpdateRight = true;
        }
        /// <summary>
        /// ons the collection added.
        /// </summary>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        private void onCollectionAdded(SpriteTileMaskCollection arg1, SpriteTileMaskCollection arg2)
        {
            requireUpdateLeft = true;
            requireUpdateTop = true;
            requireUpdateBottom = true;
            requireUpdateRight = true;
        }
        /// <summary>
        /// Moves the to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void MoveTo(int x, int y)
        {
            int cx = Left;
            int cy = Top;
            _ = Parallel.ForEach(elements, e =>
            {
                int deltaX = e.Left - cx;
                int deltaY = e.Top - cy;
                e.MoveTo(x + deltaX, y + deltaY);
            });
            left = x;
            top = y;
        }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        public uint[] GetGraphics(int index, Zoom z)
        {
            if (Lenght == 0) return null;
            return elements[index].GetGraphics(z);
        }
    }
}
