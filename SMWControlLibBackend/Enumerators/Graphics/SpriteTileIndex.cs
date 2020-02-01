namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// The sprite tile index.
    /// </summary>
    public class SpriteTileIndex : FakeEnumerator
    {
        /// <summary>
        /// Gets the x.
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Gets the y.
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// Gets the s n e s value.
        /// </summary>
        public string SNESValue { get; private set; }

        #region Values
        /// <summary>
        /// Gets the index00.
        /// </summary>
        public static SpriteTileIndex Index00 { get { return GetIndex(0, 0); } }
        /// <summary>
        /// Gets the index10.
        /// </summary>
        public static SpriteTileIndex Index10 { get { return GetIndex(0, 1); } }
        /// <summary>
        /// Gets the index20.
        /// </summary>
        public static SpriteTileIndex Index20 { get { return GetIndex(0, 2); } }
        /// <summary>
        /// Gets the index30.
        /// </summary>
        public static SpriteTileIndex Index30 { get { return GetIndex(0, 3); } }
        /// <summary>
        /// Gets the index40.
        /// </summary>
        public static SpriteTileIndex Index40 { get { return GetIndex(0, 4); } }
        /// <summary>
        /// Gets the index50.
        /// </summary>
        public static SpriteTileIndex Index50 { get { return GetIndex(0, 5); } }
        /// <summary>
        /// Gets the index60.
        /// </summary>
        public static SpriteTileIndex Index60 { get { return GetIndex(0, 6); } }
        /// <summary>
        /// Gets the index70.
        /// </summary>
        public static SpriteTileIndex Index70 { get { return GetIndex(0, 7); } }
        /// <summary>
        /// Gets the index80.
        /// </summary>
        public static SpriteTileIndex Index80 { get { return GetIndex(0, 8); } }
        /// <summary>
        /// Gets the index90.
        /// </summary>
        public static SpriteTileIndex Index90 { get { return GetIndex(0, 9); } }
        /// <summary>
        /// Gets the index a0.
        /// </summary>
        public static SpriteTileIndex IndexA0 { get { return GetIndex(0, 10); } }
        /// <summary>
        /// Gets the index b0.
        /// </summary>
        public static SpriteTileIndex IndexB0 { get { return GetIndex(0, 11); } }
        /// <summary>
        /// Gets the index c0.
        /// </summary>
        public static SpriteTileIndex IndexC0 { get { return GetIndex(0, 12); } }
        /// <summary>
        /// Gets the index d0.
        /// </summary>
        public static SpriteTileIndex IndexD0 { get { return GetIndex(0, 13); } }
        /// <summary>
        /// Gets the index e0.
        /// </summary>
        public static SpriteTileIndex IndexE0 { get { return GetIndex(0, 14); } }
        /// <summary>
        /// Gets the index f0.
        /// </summary>
        public static SpriteTileIndex IndexF0 { get { return GetIndex(0, 15); } }
        /// <summary>
        /// Gets the index01.
        /// </summary>
        public static SpriteTileIndex Index01 { get { return GetIndex(1, 0); } }
        /// <summary>
        /// Gets the index11.
        /// </summary>
        public static SpriteTileIndex Index11 { get { return GetIndex(1, 1); } }
        /// <summary>
        /// Gets the index21.
        /// </summary>
        public static SpriteTileIndex Index21 { get { return GetIndex(1, 2); } }
        /// <summary>
        /// Gets the index31.
        /// </summary>
        public static SpriteTileIndex Index31 { get { return GetIndex(1, 3); } }
        /// <summary>
        /// Gets the index41.
        /// </summary>
        public static SpriteTileIndex Index41 { get { return GetIndex(1, 4); } }
        /// <summary>
        /// Gets the index51.
        /// </summary>
        public static SpriteTileIndex Index51 { get { return GetIndex(1, 5); } }
        /// <summary>
        /// Gets the index61.
        /// </summary>
        public static SpriteTileIndex Index61 { get { return GetIndex(1, 6); } }
        /// <summary>
        /// Gets the index71.
        /// </summary>
        public static SpriteTileIndex Index71 { get { return GetIndex(1, 7); } }
        /// <summary>
        /// Gets the index81.
        /// </summary>
        public static SpriteTileIndex Index81 { get { return GetIndex(1, 8); } }
        /// <summary>
        /// Gets the index91.
        /// </summary>
        public static SpriteTileIndex Index91 { get { return GetIndex(1, 9); } }
        /// <summary>
        /// Gets the index a1.
        /// </summary>
        public static SpriteTileIndex IndexA1 { get { return GetIndex(1, 10); } }
        /// <summary>
        /// Gets the index b1.
        /// </summary>
        public static SpriteTileIndex IndexB1 { get { return GetIndex(1, 11); } }
        /// <summary>
        /// Gets the index c1.
        /// </summary>
        public static SpriteTileIndex IndexC1 { get { return GetIndex(1, 12); } }
        /// <summary>
        /// Gets the index d1.
        /// </summary>
        public static SpriteTileIndex IndexD1 { get { return GetIndex(1, 13); } }
        /// <summary>
        /// Gets the index e1.
        /// </summary>
        public static SpriteTileIndex IndexE1 { get { return GetIndex(1, 14); } }
        /// <summary>
        /// Gets the index f1.
        /// </summary>
        public static SpriteTileIndex IndexF1 { get { return GetIndex(1, 15); } }
        /// <summary>
        /// Gets the index02.
        /// </summary>
        public static SpriteTileIndex Index02 { get { return GetIndex(2, 0); } }
        /// <summary>
        /// Gets the index12.
        /// </summary>
        public static SpriteTileIndex Index12 { get { return GetIndex(2, 1); } }
        /// <summary>
        /// Gets the index22.
        /// </summary>
        public static SpriteTileIndex Index22 { get { return GetIndex(2, 2); } }
        /// <summary>
        /// Gets the index32.
        /// </summary>
        public static SpriteTileIndex Index32 { get { return GetIndex(2, 3); } }
        /// <summary>
        /// Gets the index42.
        /// </summary>
        public static SpriteTileIndex Index42 { get { return GetIndex(2, 4); } }
        /// <summary>
        /// Gets the index52.
        /// </summary>
        public static SpriteTileIndex Index52 { get { return GetIndex(2, 5); } }
        /// <summary>
        /// Gets the index62.
        /// </summary>
        public static SpriteTileIndex Index62 { get { return GetIndex(2, 6); } }
        /// <summary>
        /// Gets the index72.
        /// </summary>
        public static SpriteTileIndex Index72 { get { return GetIndex(2, 7); } }
        /// <summary>
        /// Gets the index82.
        /// </summary>
        public static SpriteTileIndex Index82 { get { return GetIndex(2, 8); } }
        /// <summary>
        /// Gets the index92.
        /// </summary>
        public static SpriteTileIndex Index92 { get { return GetIndex(2, 9); } }
        /// <summary>
        /// Gets the index a2.
        /// </summary>
        public static SpriteTileIndex IndexA2 { get { return GetIndex(2, 10); } }
        /// <summary>
        /// Gets the index b2.
        /// </summary>
        public static SpriteTileIndex IndexB2 { get { return GetIndex(2, 11); } }
        /// <summary>
        /// Gets the index c2.
        /// </summary>
        public static SpriteTileIndex IndexC2 { get { return GetIndex(2, 12); } }
        /// <summary>
        /// Gets the index d2.
        /// </summary>
        public static SpriteTileIndex IndexD2 { get { return GetIndex(2, 13); } }
        /// <summary>
        /// Gets the index e2.
        /// </summary>
        public static SpriteTileIndex IndexE2 { get { return GetIndex(2, 14); } }
        /// <summary>
        /// Gets the index f2.
        /// </summary>
        public static SpriteTileIndex IndexF2 { get { return GetIndex(2, 15); } }
        /// <summary>
        /// Gets the index03.
        /// </summary>
        public static SpriteTileIndex Index03 { get { return GetIndex(3, 0); } }
        /// <summary>
        /// Gets the index13.
        /// </summary>
        public static SpriteTileIndex Index13 { get { return GetIndex(3, 1); } }
        /// <summary>
        /// Gets the index23.
        /// </summary>
        public static SpriteTileIndex Index23 { get { return GetIndex(3, 2); } }
        /// <summary>
        /// Gets the index33.
        /// </summary>
        public static SpriteTileIndex Index33 { get { return GetIndex(3, 3); } }
        /// <summary>
        /// Gets the index43.
        /// </summary>
        public static SpriteTileIndex Index43 { get { return GetIndex(3, 4); } }
        /// <summary>
        /// Gets the index53.
        /// </summary>
        public static SpriteTileIndex Index53 { get { return GetIndex(3, 5); } }
        /// <summary>
        /// Gets the index63.
        /// </summary>
        public static SpriteTileIndex Index63 { get { return GetIndex(3, 6); } }
        /// <summary>
        /// Gets the index73.
        /// </summary>
        public static SpriteTileIndex Index73 { get { return GetIndex(3, 7); } }
        /// <summary>
        /// Gets the index83.
        /// </summary>
        public static SpriteTileIndex Index83 { get { return GetIndex(3, 8); } }
        /// <summary>
        /// Gets the index93.
        /// </summary>
        public static SpriteTileIndex Index93 { get { return GetIndex(3, 9); } }
        /// <summary>
        /// Gets the index a3.
        /// </summary>
        public static SpriteTileIndex IndexA3 { get { return GetIndex(3, 10); } }
        /// <summary>
        /// Gets the index b3.
        /// </summary>
        public static SpriteTileIndex IndexB3 { get { return GetIndex(3, 11); } }
        /// <summary>
        /// Gets the index c3.
        /// </summary>
        public static SpriteTileIndex IndexC3 { get { return GetIndex(3, 12); } }
        /// <summary>
        /// Gets the index d3.
        /// </summary>
        public static SpriteTileIndex IndexD3 { get { return GetIndex(3, 13); } }
        /// <summary>
        /// Gets the index e3.
        /// </summary>
        public static SpriteTileIndex IndexE3 { get { return GetIndex(3, 14); } }
        /// <summary>
        /// Gets the index f3.
        /// </summary>
        public static SpriteTileIndex IndexF3 { get { return GetIndex(3, 15); } }
        /// <summary>
        /// Gets the index04.
        /// </summary>
        public static SpriteTileIndex Index04 { get { return GetIndex(4, 0); } }
        /// <summary>
        /// Gets the index14.
        /// </summary>
        public static SpriteTileIndex Index14 { get { return GetIndex(4, 1); } }
        /// <summary>
        /// Gets the index24.
        /// </summary>
        public static SpriteTileIndex Index24 { get { return GetIndex(4, 2); } }
        /// <summary>
        /// Gets the index34.
        /// </summary>
        public static SpriteTileIndex Index34 { get { return GetIndex(4, 3); } }
        /// <summary>
        /// Gets the index44.
        /// </summary>
        public static SpriteTileIndex Index44 { get { return GetIndex(4, 4); } }
        /// <summary>
        /// Gets the index54.
        /// </summary>
        public static SpriteTileIndex Index54 { get { return GetIndex(4, 5); } }
        /// <summary>
        /// Gets the index64.
        /// </summary>
        public static SpriteTileIndex Index64 { get { return GetIndex(4, 6); } }
        /// <summary>
        /// Gets the index74.
        /// </summary>
        public static SpriteTileIndex Index74 { get { return GetIndex(4, 7); } }
        /// <summary>
        /// Gets the index84.
        /// </summary>
        public static SpriteTileIndex Index84 { get { return GetIndex(4, 8); } }
        /// <summary>
        /// Gets the index94.
        /// </summary>
        public static SpriteTileIndex Index94 { get { return GetIndex(4, 9); } }
        /// <summary>
        /// Gets the index a4.
        /// </summary>
        public static SpriteTileIndex IndexA4 { get { return GetIndex(4, 10); } }
        /// <summary>
        /// Gets the index b4.
        /// </summary>
        public static SpriteTileIndex IndexB4 { get { return GetIndex(4, 11); } }
        /// <summary>
        /// Gets the index c4.
        /// </summary>
        public static SpriteTileIndex IndexC4 { get { return GetIndex(4, 12); } }
        /// <summary>
        /// Gets the index d4.
        /// </summary>
        public static SpriteTileIndex IndexD4 { get { return GetIndex(4, 13); } }
        /// <summary>
        /// Gets the index e4.
        /// </summary>
        public static SpriteTileIndex IndexE4 { get { return GetIndex(4, 14); } }
        /// <summary>
        /// Gets the index f4.
        /// </summary>
        public static SpriteTileIndex IndexF4 { get { return GetIndex(4, 15); } }
        /// <summary>
        /// Gets the index05.
        /// </summary>
        public static SpriteTileIndex Index05 { get { return GetIndex(5, 0); } }
        /// <summary>
        /// Gets the index15.
        /// </summary>
        public static SpriteTileIndex Index15 { get { return GetIndex(5, 1); } }
        /// <summary>
        /// Gets the index25.
        /// </summary>
        public static SpriteTileIndex Index25 { get { return GetIndex(5, 2); } }
        /// <summary>
        /// Gets the index35.
        /// </summary>
        public static SpriteTileIndex Index35 { get { return GetIndex(5, 3); } }
        /// <summary>
        /// Gets the index45.
        /// </summary>
        public static SpriteTileIndex Index45 { get { return GetIndex(5, 4); } }
        /// <summary>
        /// Gets the index55.
        /// </summary>
        public static SpriteTileIndex Index55 { get { return GetIndex(5, 5); } }
        /// <summary>
        /// Gets the index65.
        /// </summary>
        public static SpriteTileIndex Index65 { get { return GetIndex(5, 6); } }
        /// <summary>
        /// Gets the index75.
        /// </summary>
        public static SpriteTileIndex Index75 { get { return GetIndex(5, 7); } }
        /// <summary>
        /// Gets the index85.
        /// </summary>
        public static SpriteTileIndex Index85 { get { return GetIndex(5, 8); } }
        /// <summary>
        /// Gets the index95.
        /// </summary>
        public static SpriteTileIndex Index95 { get { return GetIndex(5, 9); } }
        /// <summary>
        /// Gets the index a5.
        /// </summary>
        public static SpriteTileIndex IndexA5 { get { return GetIndex(5, 10); } }
        /// <summary>
        /// Gets the index b5.
        /// </summary>
        public static SpriteTileIndex IndexB5 { get { return GetIndex(5, 11); } }
        /// <summary>
        /// Gets the index c5.
        /// </summary>
        public static SpriteTileIndex IndexC5 { get { return GetIndex(5, 12); } }
        /// <summary>
        /// Gets the index d5.
        /// </summary>
        public static SpriteTileIndex IndexD5 { get { return GetIndex(5, 13); } }
        /// <summary>
        /// Gets the index e5.
        /// </summary>
        public static SpriteTileIndex IndexE5 { get { return GetIndex(5, 14); } }
        /// <summary>
        /// Gets the index f5.
        /// </summary>
        public static SpriteTileIndex IndexF5 { get { return GetIndex(5, 15); } }
        /// <summary>
        /// Gets the index06.
        /// </summary>
        public static SpriteTileIndex Index06 { get { return GetIndex(6, 0); } }
        /// <summary>
        /// Gets the index16.
        /// </summary>
        public static SpriteTileIndex Index16 { get { return GetIndex(6, 1); } }
        /// <summary>
        /// Gets the index26.
        /// </summary>
        public static SpriteTileIndex Index26 { get { return GetIndex(6, 2); } }
        /// <summary>
        /// Gets the index36.
        /// </summary>
        public static SpriteTileIndex Index36 { get { return GetIndex(6, 3); } }
        /// <summary>
        /// Gets the index46.
        /// </summary>
        public static SpriteTileIndex Index46 { get { return GetIndex(6, 4); } }
        /// <summary>
        /// Gets the index56.
        /// </summary>
        public static SpriteTileIndex Index56 { get { return GetIndex(6, 5); } }
        /// <summary>
        /// Gets the index66.
        /// </summary>
        public static SpriteTileIndex Index66 { get { return GetIndex(6, 6); } }
        /// <summary>
        /// Gets the index76.
        /// </summary>
        public static SpriteTileIndex Index76 { get { return GetIndex(6, 7); } }
        /// <summary>
        /// Gets the index86.
        /// </summary>
        public static SpriteTileIndex Index86 { get { return GetIndex(6, 8); } }
        /// <summary>
        /// Gets the index96.
        /// </summary>
        public static SpriteTileIndex Index96 { get { return GetIndex(6, 9); } }
        /// <summary>
        /// Gets the index a6.
        /// </summary>
        public static SpriteTileIndex IndexA6 { get { return GetIndex(6, 10); } }
        /// <summary>
        /// Gets the index b6.
        /// </summary>
        public static SpriteTileIndex IndexB6 { get { return GetIndex(6, 11); } }
        /// <summary>
        /// Gets the index c6.
        /// </summary>
        public static SpriteTileIndex IndexC6 { get { return GetIndex(6, 12); } }
        /// <summary>
        /// Gets the index d6.
        /// </summary>
        public static SpriteTileIndex IndexD6 { get { return GetIndex(6, 13); } }
        /// <summary>
        /// Gets the index e6.
        /// </summary>
        public static SpriteTileIndex IndexE6 { get { return GetIndex(6, 14); } }
        /// <summary>
        /// Gets the index f6.
        /// </summary>
        public static SpriteTileIndex IndexF6 { get { return GetIndex(6, 15); } }
        /// <summary>
        /// Gets the index07.
        /// </summary>
        public static SpriteTileIndex Index07 { get { return GetIndex(7, 0); } }
        /// <summary>
        /// Gets the index17.
        /// </summary>
        public static SpriteTileIndex Index17 { get { return GetIndex(7, 1); } }
        /// <summary>
        /// Gets the index27.
        /// </summary>
        public static SpriteTileIndex Index27 { get { return GetIndex(7, 2); } }
        /// <summary>
        /// Gets the index37.
        /// </summary>
        public static SpriteTileIndex Index37 { get { return GetIndex(7, 3); } }
        /// <summary>
        /// Gets the index47.
        /// </summary>
        public static SpriteTileIndex Index47 { get { return GetIndex(7, 4); } }
        /// <summary>
        /// Gets the index57.
        /// </summary>
        public static SpriteTileIndex Index57 { get { return GetIndex(7, 5); } }
        /// <summary>
        /// Gets the index67.
        /// </summary>
        public static SpriteTileIndex Index67 { get { return GetIndex(7, 6); } }
        /// <summary>
        /// Gets the index77.
        /// </summary>
        public static SpriteTileIndex Index77 { get { return GetIndex(7, 7); } }
        /// <summary>
        /// Gets the index87.
        /// </summary>
        public static SpriteTileIndex Index87 { get { return GetIndex(7, 8); } }
        /// <summary>
        /// Gets the index97.
        /// </summary>
        public static SpriteTileIndex Index97 { get { return GetIndex(7, 9); } }
        /// <summary>
        /// Gets the index a7.
        /// </summary>
        public static SpriteTileIndex IndexA7 { get { return GetIndex(7, 10); } }
        /// <summary>
        /// Gets the index b7.
        /// </summary>
        public static SpriteTileIndex IndexB7 { get { return GetIndex(7, 11); } }
        /// <summary>
        /// Gets the index c7.
        /// </summary>
        public static SpriteTileIndex IndexC7 { get { return GetIndex(7, 12); } }
        /// <summary>
        /// Gets the index d7.
        /// </summary>
        public static SpriteTileIndex IndexD7 { get { return GetIndex(7, 13); } }
        /// <summary>
        /// Gets the index e7.
        /// </summary>
        public static SpriteTileIndex IndexE7 { get { return GetIndex(7, 14); } }
        /// <summary>
        /// Gets the index f7.
        /// </summary>
        public static SpriteTileIndex IndexF7 { get { return GetIndex(7, 15); } }
        /// <summary>
        /// Gets the index08.
        /// </summary>
        public static SpriteTileIndex Index08 { get { return GetIndex(8, 0); } }
        /// <summary>
        /// Gets the index18.
        /// </summary>
        public static SpriteTileIndex Index18 { get { return GetIndex(8, 1); } }
        /// <summary>
        /// Gets the index28.
        /// </summary>
        public static SpriteTileIndex Index28 { get { return GetIndex(8, 2); } }
        /// <summary>
        /// Gets the index38.
        /// </summary>
        public static SpriteTileIndex Index38 { get { return GetIndex(8, 3); } }
        /// <summary>
        /// Gets the index48.
        /// </summary>
        public static SpriteTileIndex Index48 { get { return GetIndex(8, 4); } }
        /// <summary>
        /// Gets the index58.
        /// </summary>
        public static SpriteTileIndex Index58 { get { return GetIndex(8, 5); } }
        /// <summary>
        /// Gets the index68.
        /// </summary>
        public static SpriteTileIndex Index68 { get { return GetIndex(8, 6); } }
        /// <summary>
        /// Gets the index78.
        /// </summary>
        public static SpriteTileIndex Index78 { get { return GetIndex(8, 7); } }
        /// <summary>
        /// Gets the index88.
        /// </summary>
        public static SpriteTileIndex Index88 { get { return GetIndex(8, 8); } }
        /// <summary>
        /// Gets the index98.
        /// </summary>
        public static SpriteTileIndex Index98 { get { return GetIndex(8, 9); } }
        /// <summary>
        /// Gets the index a8.
        /// </summary>
        public static SpriteTileIndex IndexA8 { get { return GetIndex(8, 10); } }
        /// <summary>
        /// Gets the index b8.
        /// </summary>
        public static SpriteTileIndex IndexB8 { get { return GetIndex(8, 11); } }
        /// <summary>
        /// Gets the index c8.
        /// </summary>
        public static SpriteTileIndex IndexC8 { get { return GetIndex(8, 12); } }
        /// <summary>
        /// Gets the index d8.
        /// </summary>
        public static SpriteTileIndex IndexD8 { get { return GetIndex(8, 13); } }
        /// <summary>
        /// Gets the index e8.
        /// </summary>
        public static SpriteTileIndex IndexE8 { get { return GetIndex(8, 14); } }
        /// <summary>
        /// Gets the index f8.
        /// </summary>
        public static SpriteTileIndex IndexF8 { get { return GetIndex(8, 15); } }
        /// <summary>
        /// Gets the index09.
        /// </summary>
        public static SpriteTileIndex Index09 { get { return GetIndex(9, 0); } }
        /// <summary>
        /// Gets the index19.
        /// </summary>
        public static SpriteTileIndex Index19 { get { return GetIndex(9, 1); } }
        /// <summary>
        /// Gets the index29.
        /// </summary>
        public static SpriteTileIndex Index29 { get { return GetIndex(9, 2); } }
        /// <summary>
        /// Gets the index39.
        /// </summary>
        public static SpriteTileIndex Index39 { get { return GetIndex(9, 3); } }
        /// <summary>
        /// Gets the index49.
        /// </summary>
        public static SpriteTileIndex Index49 { get { return GetIndex(9, 4); } }
        /// <summary>
        /// Gets the index59.
        /// </summary>
        public static SpriteTileIndex Index59 { get { return GetIndex(9, 5); } }
        /// <summary>
        /// Gets the index69.
        /// </summary>
        public static SpriteTileIndex Index69 { get { return GetIndex(9, 6); } }
        /// <summary>
        /// Gets the index79.
        /// </summary>
        public static SpriteTileIndex Index79 { get { return GetIndex(9, 7); } }
        /// <summary>
        /// Gets the index89.
        /// </summary>
        public static SpriteTileIndex Index89 { get { return GetIndex(9, 8); } }
        /// <summary>
        /// Gets the index99.
        /// </summary>
        public static SpriteTileIndex Index99 { get { return GetIndex(9, 9); } }
        /// <summary>
        /// Gets the index a9.
        /// </summary>
        public static SpriteTileIndex IndexA9 { get { return GetIndex(9, 10); } }
        /// <summary>
        /// Gets the index b9.
        /// </summary>
        public static SpriteTileIndex IndexB9 { get { return GetIndex(9, 11); } }
        /// <summary>
        /// Gets the index c9.
        /// </summary>
        public static SpriteTileIndex IndexC9 { get { return GetIndex(9, 12); } }
        /// <summary>
        /// Gets the index d9.
        /// </summary>
        public static SpriteTileIndex IndexD9 { get { return GetIndex(9, 13); } }
        /// <summary>
        /// Gets the index e9.
        /// </summary>
        public static SpriteTileIndex IndexE9 { get { return GetIndex(9, 14); } }
        /// <summary>
        /// Gets the index f9.
        /// </summary>
        public static SpriteTileIndex IndexF9 { get { return GetIndex(9, 15); } }
        /// <summary>
        /// Gets the index0 a.
        /// </summary>
        public static SpriteTileIndex Index0A { get { return GetIndex(10, 0); } }
        /// <summary>
        /// Gets the index1 a.
        /// </summary>
        public static SpriteTileIndex Index1A { get { return GetIndex(10, 1); } }
        /// <summary>
        /// Gets the index2 a.
        /// </summary>
        public static SpriteTileIndex Index2A { get { return GetIndex(10, 2); } }
        /// <summary>
        /// Gets the index3 a.
        /// </summary>
        public static SpriteTileIndex Index3A { get { return GetIndex(10, 3); } }
        /// <summary>
        /// Gets the index4 a.
        /// </summary>
        public static SpriteTileIndex Index4A { get { return GetIndex(10, 4); } }
        /// <summary>
        /// Gets the index5 a.
        /// </summary>
        public static SpriteTileIndex Index5A { get { return GetIndex(10, 5); } }
        /// <summary>
        /// Gets the index6 a.
        /// </summary>
        public static SpriteTileIndex Index6A { get { return GetIndex(10, 6); } }
        /// <summary>
        /// Gets the index7 a.
        /// </summary>
        public static SpriteTileIndex Index7A { get { return GetIndex(10, 7); } }
        /// <summary>
        /// Gets the index8 a.
        /// </summary>
        public static SpriteTileIndex Index8A { get { return GetIndex(10, 8); } }
        /// <summary>
        /// Gets the index9 a.
        /// </summary>
        public static SpriteTileIndex Index9A { get { return GetIndex(10, 9); } }
        /// <summary>
        /// Gets the index a a.
        /// </summary>
        public static SpriteTileIndex IndexAA { get { return GetIndex(10, 10); } }
        /// <summary>
        /// Gets the index b a.
        /// </summary>
        public static SpriteTileIndex IndexBA { get { return GetIndex(10, 11); } }
        /// <summary>
        /// Gets the index c a.
        /// </summary>
        public static SpriteTileIndex IndexCA { get { return GetIndex(10, 12); } }
        /// <summary>
        /// Gets the index d a.
        /// </summary>
        public static SpriteTileIndex IndexDA { get { return GetIndex(10, 13); } }
        /// <summary>
        /// Gets the index e a.
        /// </summary>
        public static SpriteTileIndex IndexEA { get { return GetIndex(10, 14); } }
        /// <summary>
        /// Gets the index f a.
        /// </summary>
        public static SpriteTileIndex IndexFA { get { return GetIndex(10, 15); } }
        /// <summary>
        /// Gets the index0 b.
        /// </summary>
        public static SpriteTileIndex Index0B { get { return GetIndex(11, 0); } }
        /// <summary>
        /// Gets the index1 b.
        /// </summary>
        public static SpriteTileIndex Index1B { get { return GetIndex(11, 1); } }
        /// <summary>
        /// Gets the index2 b.
        /// </summary>
        public static SpriteTileIndex Index2B { get { return GetIndex(11, 2); } }
        /// <summary>
        /// Gets the index3 b.
        /// </summary>
        public static SpriteTileIndex Index3B { get { return GetIndex(11, 3); } }
        /// <summary>
        /// Gets the index4 b.
        /// </summary>
        public static SpriteTileIndex Index4B { get { return GetIndex(11, 4); } }
        /// <summary>
        /// Gets the index5 b.
        /// </summary>
        public static SpriteTileIndex Index5B { get { return GetIndex(11, 5); } }
        /// <summary>
        /// Gets the index6 b.
        /// </summary>
        public static SpriteTileIndex Index6B { get { return GetIndex(11, 6); } }
        /// <summary>
        /// Gets the index7 b.
        /// </summary>
        public static SpriteTileIndex Index7B { get { return GetIndex(11, 7); } }
        /// <summary>
        /// Gets the index8 b.
        /// </summary>
        public static SpriteTileIndex Index8B { get { return GetIndex(11, 8); } }
        /// <summary>
        /// Gets the index9 b.
        /// </summary>
        public static SpriteTileIndex Index9B { get { return GetIndex(11, 9); } }
        /// <summary>
        /// Gets the index a b.
        /// </summary>
        public static SpriteTileIndex IndexAB { get { return GetIndex(11, 10); } }
        /// <summary>
        /// Gets the index b b.
        /// </summary>
        public static SpriteTileIndex IndexBB { get { return GetIndex(11, 11); } }
        /// <summary>
        /// Gets the index c b.
        /// </summary>
        public static SpriteTileIndex IndexCB { get { return GetIndex(11, 12); } }
        /// <summary>
        /// Gets the index d b.
        /// </summary>
        public static SpriteTileIndex IndexDB { get { return GetIndex(11, 13); } }
        /// <summary>
        /// Gets the index e b.
        /// </summary>
        public static SpriteTileIndex IndexEB { get { return GetIndex(11, 14); } }
        /// <summary>
        /// Gets the index f b.
        /// </summary>
        public static SpriteTileIndex IndexFB { get { return GetIndex(11, 15); } }
        /// <summary>
        /// Gets the index0 c.
        /// </summary>
        public static SpriteTileIndex Index0C { get { return GetIndex(12, 0); } }
        /// <summary>
        /// Gets the index1 c.
        /// </summary>
        public static SpriteTileIndex Index1C { get { return GetIndex(12, 1); } }
        /// <summary>
        /// Gets the index2 c.
        /// </summary>
        public static SpriteTileIndex Index2C { get { return GetIndex(12, 2); } }
        /// <summary>
        /// Gets the index3 c.
        /// </summary>
        public static SpriteTileIndex Index3C { get { return GetIndex(12, 3); } }
        /// <summary>
        /// Gets the index4 c.
        /// </summary>
        public static SpriteTileIndex Index4C { get { return GetIndex(12, 4); } }
        /// <summary>
        /// Gets the index5 c.
        /// </summary>
        public static SpriteTileIndex Index5C { get { return GetIndex(12, 5); } }
        /// <summary>
        /// Gets the index6 c.
        /// </summary>
        public static SpriteTileIndex Index6C { get { return GetIndex(12, 6); } }
        /// <summary>
        /// Gets the index7 c.
        /// </summary>
        public static SpriteTileIndex Index7C { get { return GetIndex(12, 7); } }
        /// <summary>
        /// Gets the index8 c.
        /// </summary>
        public static SpriteTileIndex Index8C { get { return GetIndex(12, 8); } }
        /// <summary>
        /// Gets the index9 c.
        /// </summary>
        public static SpriteTileIndex Index9C { get { return GetIndex(12, 9); } }
        /// <summary>
        /// Gets the index a c.
        /// </summary>
        public static SpriteTileIndex IndexAC { get { return GetIndex(12, 10); } }
        /// <summary>
        /// Gets the index b c.
        /// </summary>
        public static SpriteTileIndex IndexBC { get { return GetIndex(12, 11); } }
        /// <summary>
        /// Gets the index c c.
        /// </summary>
        public static SpriteTileIndex IndexCC { get { return GetIndex(12, 12); } }
        /// <summary>
        /// Gets the index d c.
        /// </summary>
        public static SpriteTileIndex IndexDC { get { return GetIndex(12, 13); } }
        /// <summary>
        /// Gets the index e c.
        /// </summary>
        public static SpriteTileIndex IndexEC { get { return GetIndex(12, 14); } }
        /// <summary>
        /// Gets the index f c.
        /// </summary>
        public static SpriteTileIndex IndexFC { get { return GetIndex(12, 15); } }
        /// <summary>
        /// Gets the index0 d.
        /// </summary>
        public static SpriteTileIndex Index0D { get { return GetIndex(13, 0); } }
        /// <summary>
        /// Gets the index1 d.
        /// </summary>
        public static SpriteTileIndex Index1D { get { return GetIndex(13, 1); } }
        /// <summary>
        /// Gets the index2 d.
        /// </summary>
        public static SpriteTileIndex Index2D { get { return GetIndex(13, 2); } }
        /// <summary>
        /// Gets the index3 d.
        /// </summary>
        public static SpriteTileIndex Index3D { get { return GetIndex(13, 3); } }
        /// <summary>
        /// Gets the index4 d.
        /// </summary>
        public static SpriteTileIndex Index4D { get { return GetIndex(13, 4); } }
        /// <summary>
        /// Gets the index5 d.
        /// </summary>
        public static SpriteTileIndex Index5D { get { return GetIndex(13, 5); } }
        /// <summary>
        /// Gets the index6 d.
        /// </summary>
        public static SpriteTileIndex Index6D { get { return GetIndex(13, 6); } }
        /// <summary>
        /// Gets the index7 d.
        /// </summary>
        public static SpriteTileIndex Index7D { get { return GetIndex(13, 7); } }
        /// <summary>
        /// Gets the index8 d.
        /// </summary>
        public static SpriteTileIndex Index8D { get { return GetIndex(13, 8); } }
        /// <summary>
        /// Gets the index9 d.
        /// </summary>
        public static SpriteTileIndex Index9D { get { return GetIndex(13, 9); } }
        /// <summary>
        /// Gets the index a d.
        /// </summary>
        public static SpriteTileIndex IndexAD { get { return GetIndex(13, 10); } }
        /// <summary>
        /// Gets the index b d.
        /// </summary>
        public static SpriteTileIndex IndexBD { get { return GetIndex(13, 11); } }
        /// <summary>
        /// Gets the index c d.
        /// </summary>
        public static SpriteTileIndex IndexCD { get { return GetIndex(13, 12); } }
        /// <summary>
        /// Gets the index d d.
        /// </summary>
        public static SpriteTileIndex IndexDD { get { return GetIndex(13, 13); } }
        /// <summary>
        /// Gets the index e d.
        /// </summary>
        public static SpriteTileIndex IndexED { get { return GetIndex(13, 14); } }
        /// <summary>
        /// Gets the index f d.
        /// </summary>
        public static SpriteTileIndex IndexFD { get { return GetIndex(13, 15); } }
        /// <summary>
        /// Gets the index0 e.
        /// </summary>
        public static SpriteTileIndex Index0E { get { return GetIndex(14, 0); } }
        /// <summary>
        /// Gets the index1 e.
        /// </summary>
        public static SpriteTileIndex Index1E { get { return GetIndex(14, 1); } }
        /// <summary>
        /// Gets the index2 e.
        /// </summary>
        public static SpriteTileIndex Index2E { get { return GetIndex(14, 2); } }
        /// <summary>
        /// Gets the index3 e.
        /// </summary>
        public static SpriteTileIndex Index3E { get { return GetIndex(14, 3); } }
        /// <summary>
        /// Gets the index4 e.
        /// </summary>
        public static SpriteTileIndex Index4E { get { return GetIndex(14, 4); } }
        /// <summary>
        /// Gets the index5 e.
        /// </summary>
        public static SpriteTileIndex Index5E { get { return GetIndex(14, 5); } }
        /// <summary>
        /// Gets the index6 e.
        /// </summary>
        public static SpriteTileIndex Index6E { get { return GetIndex(14, 6); } }
        /// <summary>
        /// Gets the index7 e.
        /// </summary>
        public static SpriteTileIndex Index7E { get { return GetIndex(14, 7); } }
        /// <summary>
        /// Gets the index8 e.
        /// </summary>
        public static SpriteTileIndex Index8E { get { return GetIndex(14, 8); } }
        /// <summary>
        /// Gets the index9 e.
        /// </summary>
        public static SpriteTileIndex Index9E { get { return GetIndex(14, 9); } }
        /// <summary>
        /// Gets the index a e.
        /// </summary>
        public static SpriteTileIndex IndexAE { get { return GetIndex(14, 10); } }
        /// <summary>
        /// Gets the index b e.
        /// </summary>
        public static SpriteTileIndex IndexBE { get { return GetIndex(14, 11); } }
        /// <summary>
        /// Gets the index c e.
        /// </summary>
        public static SpriteTileIndex IndexCE { get { return GetIndex(14, 12); } }
        /// <summary>
        /// Gets the index d e.
        /// </summary>
        public static SpriteTileIndex IndexDE { get { return GetIndex(14, 13); } }
        /// <summary>
        /// Gets the index e e.
        /// </summary>
        public static SpriteTileIndex IndexEE { get { return GetIndex(14, 14); } }
        /// <summary>
        /// Gets the index f e.
        /// </summary>
        public static SpriteTileIndex IndexFE { get { return GetIndex(14, 15); } }
        /// <summary>
        /// Gets the index0 f.
        /// </summary>
        public static SpriteTileIndex Index0F { get { return GetIndex(15, 0); } }
        /// <summary>
        /// Gets the index1 f.
        /// </summary>
        public static SpriteTileIndex Index1F { get { return GetIndex(15, 1); } }
        /// <summary>
        /// Gets the index2 f.
        /// </summary>
        public static SpriteTileIndex Index2F { get { return GetIndex(15, 2); } }
        /// <summary>
        /// Gets the index3 f.
        /// </summary>
        public static SpriteTileIndex Index3F { get { return GetIndex(15, 3); } }
        /// <summary>
        /// Gets the index4 f.
        /// </summary>
        public static SpriteTileIndex Index4F { get { return GetIndex(15, 4); } }
        /// <summary>
        /// Gets the index5 f.
        /// </summary>
        public static SpriteTileIndex Index5F { get { return GetIndex(15, 5); } }
        /// <summary>
        /// Gets the index6 f.
        /// </summary>
        public static SpriteTileIndex Index6F { get { return GetIndex(15, 6); } }
        /// <summary>
        /// Gets the index7 f.
        /// </summary>
        public static SpriteTileIndex Index7F { get { return GetIndex(15, 7); } }
        /// <summary>
        /// Gets the index8 f.
        /// </summary>
        public static SpriteTileIndex Index8F { get { return GetIndex(15, 8); } }
        /// <summary>
        /// Gets the index9 f.
        /// </summary>
        public static SpriteTileIndex Index9F { get { return GetIndex(15, 9); } }
        /// <summary>
        /// Gets the index a f.
        /// </summary>
        public static SpriteTileIndex IndexAF { get { return GetIndex(15, 10); } }
        /// <summary>
        /// Gets the index b f.
        /// </summary>
        public static SpriteTileIndex IndexBF { get { return GetIndex(15, 11); } }
        /// <summary>
        /// Gets the index c f.
        /// </summary>
        public static SpriteTileIndex IndexCF { get { return GetIndex(15, 12); } }
        /// <summary>
        /// Gets the index d f.
        /// </summary>
        public static SpriteTileIndex IndexDF { get { return GetIndex(15, 13); } }
        /// <summary>
        /// Gets the index e f.
        /// </summary>
        public static SpriteTileIndex IndexEF { get { return GetIndex(15, 14); } }
        /// <summary>
        /// Gets the index f f.
        /// </summary>
        public static SpriteTileIndex IndexFF { get { return GetIndex(15, 15); } }
        private static SpriteTileIndex[,] indexes;
        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="SpriteTileIndex"/> class from being created.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        private SpriteTileIndex(int x, int y) : base(x + y * 16)
        {
            X = x;
            Y = y;
            SNESValue = Value.ToString("X2");
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>A SpriteTileIndex.</returns>
        internal static SpriteTileIndex GetIndex(int x, int y)
        {
            if (indexes == null)
            {
                indexes = new SpriteTileIndex[16, 16];
            }
            if (indexes[x, y] == null)
            {
                indexes[x, y] = new SpriteTileIndex(x, y);
            }
            return indexes[x, y];
        }
    }
}
