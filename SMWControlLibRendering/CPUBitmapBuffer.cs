using System;
using System.Threading.Tasks;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The c p u bitmap buffer.
    /// </summary>
    public class CPUBitmapBuffer : BitmapBuffer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CPUBitmapBuffer"/> class.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public CPUBitmapBuffer(byte[] pixels, int width) : base(pixels, width)
        {
        }
        /// <summary>
        /// Gets or sets the pixels.
        /// </summary>
        public override byte[] Pixels { get; protected set; }
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer Clone()
        {
            CPUBitmapBuffer clone = new CPUBitmapBuffer(new byte[Length], Width);
            clone.DrawBitmapBuffer(this, 0, 0);
            return clone;
        }

        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int x, int y)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            int w = Math.Min(x + src.Width, Width) - x;
            if (w <= 0) return;
            int h = Math.Min(y + src.Height, Height) - y;
            if (h <= 0) return;

            _ = Parallel.For(0, h, j =>
            {
                int srcjw = j * src.Width;
                int dstjw = ((j + y) * Width) + x;
                _ = Parallel.For(0, w, i =>
                {
                    int indsrc = (srcjw + i) * 3;
                    byte cR = src.Pixels[indsrc];
                    byte cG = src.Pixels[indsrc + 1];
                    byte cB = src.Pixels[indsrc + 2];

                    if (!((cR & 0x7) != 0 || (cG & 0x7) != 0 || (cB & 0x7) != 0))
                    {
                        int ind = (dstjw + i) * 3;
                        Pixels[ind] = cR;
                        Pixels[ind + 1] = cG;
                        Pixels[ind + 2] = cB;
                    }
                });
            });
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int x, int y, int zoom)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if(zoom == 1)
            {
                DrawBitmapBuffer(src, x, y);
            }
            else
            {
                int w = Math.Min(x + (src.Width * zoom), Width) - x;
                if (w <= 0) return;

                int h = Math.Min(y + (src.Height * zoom), Height) - y;
                if (h <= 0) return;

                _ = Parallel.For(0, src.Height, j =>
                {
                    int srcjw = j * src.Width;
                    int dstjw = (((j * zoom) + y) * Width) + x;
                    _ = Parallel.For(0, src.Width, i =>
                    {
                        int indsrc = (srcjw + i) * 3;
                        byte cR = src.Pixels[indsrc];
                        byte cG = src.Pixels[indsrc + 1];
                        byte cB = src.Pixels[indsrc + 2];
                        int dstjwi = dstjw + (i * zoom);

                        if (!((cR & 0x7) != 0 || (cG & 0x7) != 0 || (cB & 0x7) != 0))
                        {
                            int dstjwiq;
                            for (int q = 0; q < zoom; q++)
                            {
                                dstjwiq = dstjwi + (q * Width);
                                for (int p = 0; p < zoom; p++)
                                {
                                    int ind = (dstjwiq + p) * 3;
                                    Pixels[ind] = cR;
                                    Pixels[ind + 1] = cG;
                                    Pixels[ind + 2] = cB;
                                }
                            }
                        }
                    });
                });
            }
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int x, int y, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            int w = Math.Min(x + src.Width, Width) - x;
            if (w <= 0) return;
            int h = Math.Min(y + src.Height, Height) - y;
            if (h <= 0) return;

            _ = Parallel.For(0, h, j =>
            {
                int srcjw = j * src.Width;
                int dstjw = ((j + y) * Width) + x;
                _ = Parallel.For(0, w, i =>
                {
                    int indsrc = (srcjw + i) * 3;
                    byte cR = src.Pixels[indsrc];
                    byte cG = src.Pixels[indsrc + 1];
                    byte cB = src.Pixels[indsrc + 2];

                    int ind = (dstjw + i) * 3;

                    if ((cR & 0x7) != 0 || (cG & 0x7) != 0 || (cB & 0x7) != 0)
                    {
                        Pixels[ind] = cR;
                        Pixels[ind + 1] = cG;
                        Pixels[ind + 2] = cB;
                    }
                    else
                    {
                        Pixels[ind] = backgroundColorR;
                        Pixels[ind + 1] = backgroundColorG;
                        Pixels[ind + 2] = backgroundColorB;
                    }
                });
            });
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int x, int y, int zoom, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (zoom == 1)
            {
                DrawBitmapBuffer(src, x, y, backgroundColorR, backgroundColorG, backgroundColorB);
            }
            else
            {
                int srcwz = src.Width * zoom;
                int w = Math.Min(x + srcwz, Width) - x;
                if (w <= 0) return;

                int srchz = src.Height * zoom;
                int h = Math.Min(y + srchz, Height) - y;
                if (h <= 0) return;

                _ = Parallel.For(0, h, j =>
                {
                    int srcjw = j * srcwz;
                    int dstjw = (((j * zoom) + y) * Width) + x;
                    _ = Parallel.For(0, w, i =>
                    {
                        int indsrc = (srcjw + i) * 3;
                        byte cR = src.Pixels[indsrc];
                        byte cG = src.Pixels[indsrc + 1];
                        byte cB = src.Pixels[indsrc + 2];
                        int dstjwi = dstjw + (i * zoom);
                        if ((cR & 0x7) != 0 || (cG & 0x7) != 0 || (cB & 0x7) != 0) 
                        {
                            cR = backgroundColorR;
                            cG = backgroundColorG;
                            cB = backgroundColorB;
                        }

                        int dstjwiq;
                        for (int q = 0; q < zoom; q++)
                        {
                            dstjwiq = dstjwi + (q * Width);
                            for (int p = 0; p < zoom; p++)
                            {
                                int ind = (dstjwiq + p) * 3;
                                Pixels[ind] = cR;
                                Pixels[ind + 1] = cG;
                                Pixels[ind + 2] = cB;
                            }
                        }
                    });
                });
            }
        }
        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="cellsize">The cellsize.</param>
        /// <param name="type">The type.</param>
        /// <param name="gridColor">The grid color.</param>
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
                            Pixels[ind] = colorR;
                            Pixels[ind + 1] = colorG;
                            Pixels[ind + 2] = colorB;
                        });
                    });
                    
                    _ = Parallel.For(1, Height, j =>
                    {
                        int jw = j * Width;
                        _ = Parallel.For(1, lateralCount, i =>
                        {
                            int ind = (jw + (i * zs)) * 3;
                            Pixels[ind] = colorR;
                            Pixels[ind + 1] = colorG;
                            Pixels[ind + 2] = colorB;
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
                                Pixels[ind] = colorR;
                                Pixels[ind + 1] = colorG;
                                Pixels[ind + 2] = colorB;
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
                                Pixels[ind] = colorR;
                                Pixels[ind + 1] = colorG;
                                Pixels[ind + 2] = colorB;
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
                                Pixels[ind] = colorR;
                                Pixels[ind + 1] = colorG;
                                Pixels[ind + 2] = colorB;
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
                                Pixels[ind] = colorR;
                                Pixels[ind + 1] = colorG;
                                Pixels[ind + 2] = colorB;
                            });
                    });
                    break;
            }
        }
        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="lineColor">The line color.</param>
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
                    Pixels[ind] = colorR;
                    Pixels[ind + 1] = colorG;
                    Pixels[ind + 2] = colorB;
                });
            }
            else if (dY == 0)
            {
                int jw = y1 * Width;
                _ = Parallel.For(x1, Math.Min(x2 + 1, Width), i =>
                {
                    int ind = (jw + i) * 3;
                    Pixels[ind] = colorR;
                    Pixels[ind + 1] = colorG;
                    Pixels[ind + 2] = colorB;
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
                        Pixels[ind] = colorR;
                        Pixels[ind + 1] = colorG;
                        Pixels[ind + 2] = colorB;
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
                        Pixels[ind] = colorR;
                        Pixels[ind + 1] = colorG;
                        Pixels[ind + 2] = colorB;
                    });
                }
            }
        }
        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="rectangleColor">The rectangle color.</param>
        public override void DrawRectangleBorder(int x, int y, int width, int height, byte colorR, byte colorG, byte colorB)
        {
            if (y < 0) y = 0;
            if (x < 0) x = 0;
            if (y >= Height) y = Height - 1;
            if (x >= Width) x = Width - 1;

            int w = Math.Min(width + x, Width) - x;
            int h = Math.Min(height + y, Height) - y;

            int xw = w + x - 1;
            _ = Parallel.For(0, h, j =>
            {
                int jy = (j + y) * Width;
                int ind1 = (jy + x) * 3;
                int ind2 = (jy + xw) * 3;
                Pixels[ind1] = colorR;
                Pixels[ind1 + 1] = colorG;
                Pixels[ind1 + 2] = colorB;

                Pixels[ind2] = colorR;
                Pixels[ind2 + 1] = colorG;
                Pixels[ind2 + 2] = colorB;
            });

            int yw = (y * Width) + x;
            int ywh = ((y + h - 1) * Width) + x;
            _ = Parallel.For(0, w, i =>
            {
                int ind1 = (yw + i) * 3;
                int ind2 = (ywh + i) * 3;
                Pixels[ind1] = colorR;
                Pixels[ind1 + 1] = colorG;
                Pixels[ind1 + 2] = colorB;

                Pixels[ind2] = colorR;
                Pixels[ind2 + 1] = colorG;
                Pixels[ind2 + 2] = colorB;
            });
        }
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public override void FillWithColor(byte colorR, byte colorG, byte colorB)
        {
            _ = Parallel.For(0, Pixels.Length, i =>
            {
                int ind = i * 3;
                Pixels[ind] = colorR;
                Pixels[ind + 1] = colorG;
                Pixels[ind + 2] = colorB;
            });
        }

        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void DrawRectangle(int x, int y, int width, int height, byte colorR, byte colorG, byte colorB)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;

            int h = Math.Min(height + y, Height) - y;
            int w = Math.Min(width + x, Width) - x;

            _ = Parallel.For(0, h, j =>
            {
                int jw = ((j + y) * Width) + x;
                _ = Parallel.For(0, w, i =>
                {
                    int ind = (jw + i) * 3;
                    Pixels[ind] = colorR;
                    Pixels[ind + 1] = colorG;
                    Pixels[ind + 2] = colorB;
                });
            });
        }

        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        public override void ZoomIn(int zoom)
        {
            CPUBitmapBuffer b = new CPUBitmapBuffer(Pixels, Width);

            int wz = Width * zoom;
            Initialize(new byte[wz * Height * zoom], wz);

            DrawBitmapBuffer(b, 0, 0, zoom);
        }
        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void ZoomIn(int zoom, byte colorR, byte colorG, byte colorB)
        {
            CPUBitmapBuffer b = new CPUBitmapBuffer(Pixels, Width);

            int wz = Width * zoom;
            Initialize(new byte[wz * Height * zoom], wz);

            DrawBitmapBuffer(b, 0, 0, zoom, colorR, colorG, colorB);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="pixls">The pixls.</param>
        /// <param name="width">The width.</param>
        public override void Initialize(byte[] pixls, int width)
        {
            BytesPerColor = 3;
            base.Initialize(pixls, width);
        }
    }
}
