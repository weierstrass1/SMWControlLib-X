using SMWControlLibRendering.Enumerator;
using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Disguise
{
    public class BitmapBufferDisguise : Disguise<BitmapBufferFactory, BitmapBuffer, int, int, BytesPerPixel>
    {
        public BitmapBufferDisguise(int param1, int param2, BytesPerPixel param3) : base(param1, param2, param3)
        {
        }
    }
}
