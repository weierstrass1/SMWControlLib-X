﻿using SMWControlLibUtils;

namespace SMWControlLibRendering.Enumerators
{
    /// <summary>
    /// The flip.
    /// </summary>
    public class Flip : FakeEnumerator
    {
        public static Flip NotFlipped = new Flip(false, false, 0);
        public static Flip HorizontalFlip = new Flip(true, false, 1);
        public static Flip VerticalFlip = new Flip(false, true, 2);
        public static Flip HorizontalVerticalFlip = new Flip(true, true, 3);
        /// <summary>
        /// Gets a value indicating whether flip x.
        /// </summary>
        public bool FlipX { get; private set; }
        /// <summary>
        /// Gets a value indicating whether flip y.
        /// </summary>
        public bool FlipY { get; private set; }
        /// <summary>
        /// Prevents a default instance of the <see cref="Flip"/> class from being created.
        /// </summary>
        /// <param name="fx">If true, fx.</param>
        /// <param name="fy">If true, fy.</param>
        /// <param name="value">The value.</param>
        private Flip(bool fx, bool fy, int value) : base(value)
        {
            FlipX = fx;
            FlipY = fy;
            Value = value;
        }
    }
}