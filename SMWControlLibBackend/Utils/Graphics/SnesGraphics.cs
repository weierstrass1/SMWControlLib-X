using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Graphics;
using System.IO;
using System.Threading.Tasks;

namespace SMWControlLibBackend.Utils.Graphics
{
    /// <summary>
    /// The snes graphics.
    /// </summary>
    public static class SnesGraphics
    {
        /// <summary>
        /// Obtain a matrix that have the colors used in each pixel.
        /// Only works for 4bpp gfx.
        /// </summary>
        /// <param name="path">Path of the gfx that you want to load.</param>
        /// <returns></returns>
        public static byte[,] GenerateGFX(string path)
        {

            byte[] gfx = File.ReadAllBytes(path);
            return GenerateGFX(gfx);
        }

        /// <summary>
        /// Obtain a matrix that have the colors used in each pixel.
        /// Only works for 4bpp gfx.
        /// </summary>
        /// <param name="gfx">An array of bytes that was read from .bin file</param>
        /// <returns></returns>
        public static byte[,] GenerateGFX(byte[] gfx)
        {
            byte[,] bits = GetBits(gfx);

            byte[] colors = new byte[gfx.Length * 2];

            Parallel.For(0, gfx.Length / 32, i =>
            {
                int tileOffset = i << 5;
                int tileOffset2 = tileOffset << 1;
                Parallel.For(0, 8, w =>
                {
                    int rowOffset = w << 1;
                    int rowOffset4 = rowOffset << 2;
                    int tilerowOffset = tileOffset + rowOffset;
                    int otherOffset = tileOffset2 + rowOffset4;
                    Parallel.For(0, 8, bit =>
                    {
                        colors[otherOffset + (7 - bit)] = (byte)((bits[bit, tilerowOffset]) +
                            (bits[bit, tilerowOffset + 1] << 1) +
                            (bits[bit, tilerowOffset + 16] << 2) +
                            (bits[bit, tilerowOffset + 17] << 3));
                    });
                });
            });

            return generateColorMatrixFromColorArray(colors);
        }

        /// <summary>
        /// generates the color matrix from color array.
        /// </summary>
        /// <param name="colors">The colors.</param>
        /// <returns>An array of byte.</returns>
        private static byte[,] generateColorMatrixFromColorArray(byte[] colors)
        {
            int height = colors.Length / 128;
            if (colors.Length % 1024 != 0) height = ((colors.Length / 1024) + 1) * 1024;
            byte[,] cols = new byte[128, height];

            for (int i = 0, j, k; i < colors.Length; i++)
            {
                j = ((i % 8) + 8 * (i / 64)) % 128;
                k = ((i / 8) % 8) + 8 * (i / 1024);

                cols[j, k] = colors[i];
            }
            return cols;
        }

        /// <summary>
        /// Obtain a GFX from a color matrix.
        /// Only work for 4bpp gfxs.
        /// </summary>
        /// <param name="colors">The Color Matrix obtained from GenerateGFX</param>
        /// <returns></returns>
        public static byte[] GetGFXFromColorMatrix(byte[,] colors)
        {
            byte[] gfx = new byte[(colors.GetLength(0) * colors.GetLength(1)) / 2];
            int k;
            byte b0, b1, b2, b3;

            for (int i = 0, x = 0, y; i < gfx.Length; i += 32, x = (x + 8) % 128)
            {
                y = i / 512;
                y *= 8;
                for (int j = 0; j < 16; j += 2, y++)
                {
                    k = i + j;
                    gfx[k] = 0;
                    gfx[k + 1] = 0;
                    gfx[k + 16] = 0;
                    gfx[k + 17] = 0;

                    for (int p = 0, m = 128; p < 8; p++, m /= 2)
                    {
                        b0 = (byte)(colors[x + p, y] & 1);
                        b1 = (byte)((colors[x + p, y] & 2) >> 1);
                        b2 = (byte)((colors[x + p, y] & 4) >> 2);
                        b3 = (byte)((colors[x + p, y] & 8) >> 3);

                        gfx[k] += (byte)(b0 * m);
                        gfx[k + 1] += (byte)(b1 * m);
                        gfx[k + 16] += (byte)(b2 * m);
                        gfx[k + 17] += (byte)(b3 * m);
                    }
                }
            }

            return gfx;
        }

        /// <summary>
        /// Separate each bit of each byte on a byte array
        /// </summary>
        /// <param name="bytes">Byte array that you want to separate each bit</param>
        /// <returns></returns>
        public static byte[,] GetBits(byte[] bytes)
        {
            byte[,] bits = new byte[8, bytes.Length];

            Parallel.For(0, bytes.Length, i =>
            {
                bits[0, i] = (byte)(bytes[i] & 0x00000001);
                bits[1, i] = (byte)((bytes[i] & 0x00000002) >> 1);
                bits[2, i] = (byte)((bytes[i] & 0x00000004) >> 2);
                bits[3, i] = (byte)((bytes[i] & 0x00000008) >> 3);
                bits[4, i] = (byte)((bytes[i] & 0x00000010) >> 4);
                bits[5, i] = (byte)((bytes[i] & 0x00000020) >> 5);
                bits[6, i] = (byte)((bytes[i] & 0x00000040) >> 6);
                bits[7, i] = (byte)((bytes[i] & 0x00000080) >> 7);
            });

            return bits;
        }

        /// <summary>
        /// Gets the create bitmap int pointer.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <param name="graphicsMap">The graphics map.</param>
        /// <returns>An array of uint.</returns>
        public static uint[] GetCreateBitmapIntPointer(SNESColorPalette cp, byte[,] graphicsMap)
        {
            uint[] c2 = cp.Colors;

            int srcW = graphicsMap.GetLength(0);
            int srcH = graphicsMap.GetLength(1);
            uint[] res2 = new uint[srcW * srcH];

            Parallel.For(0, srcH, j =>
            {
                int wj = j * srcW;
                Parallel.For(0, srcW, i =>
                {
                    byte c = graphicsMap[i, j];
                    if (c != 0)
                    {
                        int ipwj = wj + i;
                        res2[ipwj] = c2[c];
                    }
                });
            });

            return res2;
        }

        /// <summary>
        /// Gets the create bitmap int pointer.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <param name="graphicsMap">The graphics map.</param>
        /// <param name="zoom">The zoom.</param>
        /// <returns>An array of uint.</returns>
        public static uint[] GetCreateBitmapIntPointer(SNESColorPalette cp, byte[,] graphicsMap, Zoom zoom)
        {
            uint[] c2 = cp.Colors;

            int srcW = graphicsMap.GetLength(0);
            int srcH = graphicsMap.GetLength(1);
            int zoom2 = zoom * zoom;
            int srcWz = srcW * zoom;
            uint[] res2 = new uint[srcH * srcW * zoom2];

            _ = Parallel.For(0, srcH, j =>
              {
                  int wj = j * srcW * zoom2;
                  _ = Parallel.For(0, srcW, i =>
                    {
                        byte c = graphicsMap[i, j];
                        if (c != 0)
                        {
                            int ipwj = wj + (i * zoom);
                            int i4py = 0;
                            for (int y = 0; y < zoom; y++)
                            {
                                i4py = ipwj + (y * srcWz);
                                for (int x = 0; x < zoom; x++)
                                {
                                    res2[i4py + x] = c2[c];
                                }
                            }
                        }
                    });
              });
            return res2;
        }
    }
}
