namespace SMWControlLibSNES.Enumerators.Graphics
{
    /// <summary>
    /// The b g page.
    /// </summary>
    public class BGPage : GraphicPage
    {
        public static BGPage BG12 = new BGPage(0);
        public static BGPage BG34 = new BGPage(1);
        public static BGPage BG56 = new BGPage(2);
        public static BGPage BG78 = new BGPage(3);
        /// <summary>
        /// Gets the s m w f g12.
        /// </summary>
        public static BGPage SMWFG12 { get { return BG12; } }
        /// <summary>
        /// Gets the s m w f g3 b g1.
        /// </summary>
        public static BGPage SMWFG3BG1 { get { return BG34; } }
        /// <summary>
        /// Gets the s m w b g23.
        /// </summary>
        public static BGPage SMWBG23 { get { return BG56; } }
        /// <summary>
        /// Prevents a default instance of the <see cref="BGPage"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        private BGPage(int value) : base(value)
        {

        }
    }
}
