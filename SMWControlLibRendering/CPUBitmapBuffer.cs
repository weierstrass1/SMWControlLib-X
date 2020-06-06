using SMWControlLibRendering.Enumerator;
using System;

namespace SMWControlLibRendering
{
    public class CPUBitmapBuffer : BitmapBuffer
    {
        public CPUBitmapBuffer(int width, int height, BytesPerPixel bpp) : base(width, height, bpp)
        {
        }
        protected byte[] pixels;
        /*
        public override void DrawGrid(int zoom, int cellsize, int type, byte colorR, byte colorG, byte colorB)
        {
            int zs = zoom * cellsize;
            int lateralCount = Width / zs;
            if (Width % zs != 0) lateralCount++;
            int verticalCount = Height / zs;
            if (Height % zs != 0) verticalCount++;
            int z2 = zoom << 1;
            switch (type)
            {
                case 0:
                    _ = Parallel.For(1, verticalCount, j =>
                    {
                        int jw = j * zs * Width;
                        _ = Parallel.For(0, Width, i =>
                        {
                            int ind = (jw + i) * 3;
                            pixels[ind] = colorR;
                            pixels[ind + 1] = colorG;
                            pixels[ind + 2] = colorB;
                        });
                    });

                    _ = Parallel.For(1, Height, j =>
                    {
                        int jw = j * Width;
                        _ = Parallel.For(1, lateralCount, i =>
                        {
                            int ind = (jw + (i * zs)) * 3;
                            pixels[ind] = colorR;
                            pixels[ind + 1] = colorG;
                            pixels[ind + 2] = colorB;
                        });
                    });
                    break;

                case 1:
                    int wmz = Width - zoom;
                    _ = Parallel.For(1, verticalCount, j =>
                    {
                        int jw = j * zs * Width;
                        _ = Parallel.For(0, wmz, i =>
                        {
                            int drawer = ((i / z2) % 2);
                            if (drawer == 1 || i % z2 == 0)
                            {
                                int ind = (jw + i + zoom) * 3;
                                pixels[ind] = colorR;
                                pixels[ind + 1] = colorG;
                                pixels[ind + 2] = colorB;
                            }
                        });
                    });

                    _ = Parallel.For(0, Height - zoom, j =>
                    {
                        int drawer = (j / z2) % 2;
                        if (drawer == 1 || j % z2 == 0)
                        {
                            int jw = (j + zoom) * Width;
                            _ = Parallel.For(1, lateralCount, i =>
                            {
                                int ind = (jw + (i * zs)) * 3;
                                pixels[ind] = colorR;
                                pixels[ind + 1] = colorG;
                                pixels[ind + 2] = colorB;
                            });
                        }
                    });
                    break;

                default:
                    int dist = Math.Min(4, z2);
                    if (dist == 1) dist = 2;
                    _ = Parallel.For(1, verticalCount, j =>
                    {
                        int jw = j * zs * Width;
                        _ = Parallel.For(0, Width, i =>
                        {
                            if ((i % dist) == 0)
                            {
                                int ind = (jw + i) * 3;
                                pixels[ind] = colorR;
                                pixels[ind + 1] = colorG;
                                pixels[ind + 2] = colorB;
                            }
                        });
                    });

                    _ = Parallel.For(1, Height, j =>
                    {
                        int jw = j * Width;
                        if ((j % dist) == 0)
                            _ = Parallel.For(1, lateralCount, i =>
                            {
                                int ind = (jw + (i * zs)) * 3;
                                pixels[ind] = colorR;
                                pixels[ind + 1] = colorG;
                                pixels[ind + 2] = colorB;
                            });
                    });
                    break;
            }
        }
        
        public override void DrawLine(int x1, int y1, int x2, int y2, byte colorR, byte colorG, byte colorB)
        {
            int dX = x2 - x1;

            int dY = y2 - y1;

            if (dY == 0 && dX == 0) return;

            if (dX == 0)
            {
                _ = Parallel.For(y1, Math.Min(y2 + 1, Height), j =>
                {
                    int ind = ((j * Width) + x1) * 3;
                    pixels[ind] = colorR;
                    pixels[ind + 1] = colorG;
                    pixels[ind + 2] = colorB;
                });
            }
            else if (dY == 0)
            {
                int jw = y1 * Width;
                _ = Parallel.For(x1, Math.Min(x2 + 1, Width), i =>
                {
                    int ind = (jw + i) * 3;
                    pixels[ind] = colorR;
                    pixels[ind + 1] = colorG;
                    pixels[ind + 2] = colorB;
                });
            }
            else
            {
                float props = dY / (float)dX;

                if (Math.Abs(dX) >= Math.Abs(dY))
                {
                    int minX = Math.Min(x2, x1);
                    int maxX = Math.Max(x2, x1);
                    _ = Parallel.For(minX, maxX + 1, i =>
                    {
                        float y = (((i - x1) * props) + y1);
                        int ind = ((int)(Math.Round(y, 0) * Width) + i) * 3;
                        pixels[ind] = colorR;
                        pixels[ind + 1] = colorG;
                        pixels[ind + 2] = colorB;
                    });
                }
                else
                {
                    int minY = Math.Min(y2, y1);
                    int maxY = Math.Max(y2, y1);
                    _ = Parallel.For(minY, maxY + 1, i =>
                    {
                        float x = ((i - y1) / props) + x1;
                        int ind = ((int)Math.Round(x, 0) + i * Width) * 3;
                        pixels[ind] = colorR;
                        pixels[ind + 1] = colorG;
                        pixels[ind + 2] = colorB;
                    });
                }
            }
        }*/
        public override void Initialize(int width, int height, BytesPerPixel bpp)
        {
            base.Initialize(width, height, bpp);
            pixels = new byte[Length];
        }
        public override void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, int srcXOffset, int srcYOffset, int zoom, byte[] backgroundColor)
        {
            throw new NotImplementedException();
        }
        public override void DrawRectangleBorder(int x, int y, int width, int height, byte[] color)
        {
            throw new NotImplementedException();
        }
        public override void DrawLine(int x1, int y1, int x2, int y2, byte[] color)
        {
            throw new NotImplementedException();
        }
        public override void DrawGrid(int zoom, int cellsize, int type, byte[] color)
        {
            throw new NotImplementedException();
        }
        public override void FillWithColor(byte[] color)
        {
            throw new NotImplementedException();
        }
        public override void DrawRectangle(int x, int y, int width, int height, byte[] color)
        {
            throw new NotImplementedException();
        }
        public override unsafe void CopyTo(byte* target, int subImageLeft, int subImageRight, int subImageTop, int subImageBottom, int dirtyLeft, int dirtyRight, int dirtyTop, int dirtyBottom)
        {
            throw new NotImplementedException();
        }
    }
}