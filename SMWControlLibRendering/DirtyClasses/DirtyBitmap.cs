using SMWControlLibRendering.Enumerator;
using SMWControlLibUtils;
using System;

namespace SMWControlLibRendering.DirtyClasses
{
    public class DirtyBitmap : DirtyClass<BitmapBuffer>, IDisposable
    {
        public BitmapBuffer Bitmap
        {
            get => Object;
            set
            {
                Object.Dispose();
                Object = value;
            }
        }
        public DirtyBitmap() : base(null)
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "This object Manage Dispose.")]
        public DirtyBitmap(int width, int height, BytesPerPixel bpp) : base(BitmapBuffer.CreateInstance(width, height, bpp))
        {

        }
        public DirtyBitmap(BitmapBuffer bitmap) : base(bitmap)
        {

        }
        public void Dispose()
        {
            Bitmap.Dispose();
        }
    }
}
