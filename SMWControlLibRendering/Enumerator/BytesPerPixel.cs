using SMWControlLibUtils;

namespace SMWControlLibRendering.Enumerator
{
    public class BytesPerPixel : FakeEnumerator
    {
        public static readonly BytesPerPixel RGB555 = new BytesPerPixel(3, 0x00FFFFFF, 0x07, 0x07, 0x07);
        public static readonly BytesPerPixel ARGB8888 = new BytesPerPixel(4, 0xFFFFFFFF, 0x00, 0x00, 0x00, 0x00);

        private uint shorter;
        public byte[] BitClearer;
        private BytesPerPixel(int Bpp, uint s, params byte[] bitclearer) : base(Bpp)
        {
            shorter = s;
            BitClearer = bitclearer;
        }

        public int ShortColor(int color)
        {
            return (int)((color & shorter) & 0x00000000FFFFFFFF);
        }
    }
}
