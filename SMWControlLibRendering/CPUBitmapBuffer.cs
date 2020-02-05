using System;
using System.Threading.Tasks;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The c p u bitmap buffer.
    /// </summary>
    public class CPUBitmapBuffer<T> : BitmapBuffer<T> where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CPUBitmapBuffer"/> class.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public CPUBitmapBuffer(T[] pixels, int width) : base(pixels, width)
        {
        }
        /// <summary>
        /// Gets or sets the pixels.
        /// </summary>
        public override T[] Pixels { get; protected set; }
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer<T> Clone()
        {
            CPUBitmapBuffer<T> clone = new CPUBitmapBuffer<T>(new T[Length], Width);
            clone.DrawBitmapBuffer(this, 0, 0);
            return clone;
        }

        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y)
        {
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
                    T c = src.Pixels[srcjw + i];
                    if ((bool)(object)c)
                    {
                        Pixels[dstjw + i] = c;
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
        public override void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, int zoom)
        {
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
                        T c = src.Pixels[srcjw + i];
                        int dstjwi = dstjw + (i * zoom);
                        if ((bool)(object)c)
                        {
                            int dstjwiq;
                            for (int q = 0; q < zoom; q++)
                            {
                                dstjwiq = dstjwi + (q * Width);
                                for (int p = 0; p < zoom; p++)
                                {
                                    Pixels[dstjwiq + p] = c;
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
        public override void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, T backgroundColor)
        {
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
                    T c = src.Pixels[srcjw + i];
                    if ((bool)(object)c)
                    {
                        Pixels[dstjw + i] = c;
                    }
                    else
                    {
                        Pixels[dstjw + i] = backgroundColor;
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
        public override void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, int zoom, T backgroundColor)
        {
            if (zoom == 1)
            {
                DrawBitmapBuffer(src, x, y, backgroundColor);
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
                        T c = src.Pixels[srcjw + i];
                        int dstjwi = dstjw + (i * zoom);
                        if ((bool)(object)c)
                        {
                            c = backgroundColor;
                        }

                        int dstjwiq;
                        for (int q = 0; q < zoom; q++)
                        {
                            dstjwiq = dstjwi + (q * Width);
                            for (int p = 0; p < zoom; p++)
                            {
                                Pixels[dstjwiq + p] = c;
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
        public override void DrawGrid(int zoom, int cellsize, int type, T gridColor)
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
                            Pixels[jw + i] = gridColor;
                        });
                    });
                    
                    _ = Parallel.For(1, Height, j =>
                    {
                        int jw = j * Width;
                        _ = Parallel.For(1, lateralCount, i =>
                        {
                            Pixels[jw + (i * zs)] = gridColor;
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
                                Pixels[jw + i + zoom] = gridColor;
                        });
                    });

                    _ = Parallel.For(0, Height - zoom, j =>
                    {
                        int drawer = ((j / z2) % 2);
                        if (drawer == 1 || j % z2 == 0) 
                        {
                            int jw = (j + zoom) * Width;
                            _ = Parallel.For(1, lateralCount, i =>
                            {
                                Pixels[jw + (i * zs)] = gridColor;
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
                                Pixels[jw + i] = gridColor;
                        });
                    });

                    _ = Parallel.For(1, Height, j =>
                    {
                        int jw = j * Width;
                        if ((j % dist) == 0)
                            _ = Parallel.For(1, lateralCount, i =>
                            {
                                Pixels[jw + (i * zs)] = gridColor;
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
        public override void DrawLine(int x1, int y1, int x2, int y2, T lineColor)
        {
            int dX = x2 - x1;

            int dY = y2 - y1;

            if (dY == 0 && dX == 0) return;

            if (dX == 0)
            {
                _ = Parallel.For(y1, Math.Min(y2 + 1, Height), j =>
                {
                    Pixels[(j * Width) + x1] = lineColor;
                });
            }
            else if (dY == 0)
            {
                int jw = y1 * Width;
                _ = Parallel.For(x1, Math.Min(x2 + 1, Width), i =>
                {
                    Pixels[jw + i] = lineColor;
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
                        Pixels[(int)(Math.Round(y, 0) * Width) + i] = lineColor;
                    });
                }
                else
                {
                    int minY = Math.Min(y2, y1);
                    int maxY = Math.Max(y2, y1);
                    _ = Parallel.For(minY, maxY + 1, i =>
                    {
                        float x = ((i - y1) / props) + x1;
                        Pixels[(int)Math.Round(x, 0) + i * Width] = lineColor;
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
        public override void DrawRectangleBorder(int x, int y, int width, int height, T rectangleColor)
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
                Pixels[jy + x] = rectangleColor;
                Pixels[jy + xw] = rectangleColor;
            });

            int yw = (y * Width) + x;
            int ywh = ((y + h - 1) * Width) + x;
            _ = Parallel.For(0, w, i =>
            {
                Pixels[yw + i] = rectangleColor;
                Pixels[ywh + i] = rectangleColor;
            });
        }
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public override void FillWithColor(T backgroundColor)
        {
            _ = Parallel.For(0, Pixels.Length, i =>
            {
                Pixels[i] = backgroundColor;
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
        public override void DrawRectangle(int x, int y, int width, int height, T backgroundColor)
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
                    Pixels[jw + i] = backgroundColor;
                });
            });
        }

        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        public override void ZoomIn(int zoom)
        {
            CPUBitmapBuffer<T> b = new CPUBitmapBuffer<T>(Pixels, Width);

            int wz = Width * zoom;
            Initialize(new T[wz * Height * zoom], wz);

            DrawBitmapBuffer(b, 0, 0, zoom);
        }
        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void ZoomIn(int zoom, T backgroundColor)
        {
            CPUBitmapBuffer<T> b = new CPUBitmapBuffer<T>(Pixels, Width);

            int wz = Width * zoom;
            Initialize(new T[wz * Height * zoom], wz);

            DrawBitmapBuffer(b, 0, 0, zoom, backgroundColor);
        }
    }
}
