using ILGPU;
using ILGPU.Runtime;
using SMWControlLibOptimization.Keys;
using SMWControlLibRendering;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.TileOptimizer.KernelStrategies
{
    public class CheckRepeatedKernel
    {
        private static readonly Action<Index2, ArrayView2D<int>, ArrayView2D<int>, Index2, int> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<int>, ArrayView2D<int>, Index2, int>
            (strategy);

        public static Tuple<ConcurrentDictionary<TileKey, int>, ConcurrentDictionary<TileKey, TileKey>> Execute(ConcurrentDictionary<TileKey, int> tiles, ArrayView2D<int> bpBuffer, int tilewidth, int tileheight)
        {
            int[,] ts = new int[tiles.Count, 3];
            int i = 0;
            foreach (var kvp in tiles)
            {
                ts[i, 0] = 0;
                ts[i, 1] = kvp.Key.X;
                ts[i, 2] = kvp.Key.Y;
                i++;
            }
            int tilesperRow = bpBuffer.Extent.X / tilewidth;

            using (MemoryBuffer2D<int> res = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(ts.GetLength(0), ts.GetLength(1)))
            {
                res.CopyFrom(ts, Index2.Zero, Index2.Zero, res.Extent);

                kernel(new Index2(tiles.Count, tiles.Count), bpBuffer, res, new Index2(tilewidth, tileheight), tilesperRow);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();

                res.CopyTo(ts, Index2.Zero, Index2.Zero, res.Extent);
            }
            ConcurrentDictionary<TileKey, int> ret = new ConcurrentDictionary<TileKey, int>();
            ConcurrentDictionary<TileKey, TileKey> ret2 = new ConcurrentDictionary<TileKey, TileKey>();

            Parallel.For(0, tiles.Count, x =>
            {
                if (ts[x, 0] == 0)
                    ret.TryAdd(new TileKey(ts[x, 1], ts[x, 2], tilesperRow), -1);
                ret2.TryAdd(new TileKey(ts[x, 1], ts[x, 2], tilesperRow), new TileKey((ts[x, 0] - 1) % tilesperRow, (ts[x, 0] - 1) / tilesperRow, tilesperRow));
            });

            foreach(var kvp in ret2)
            {
                if (kvp.Value.X >= 0)
                {
                    TileKey aux = kvp.Value;
                    while (aux.X >= 0)
                    {
                        ret2[kvp.Key] = aux;
                        aux = ret2[aux];
                    }
                }
            }
            return new Tuple<ConcurrentDictionary<TileKey, int>, ConcurrentDictionary<TileKey, TileKey>>(ret, ret2);
        }
        private static void strategy(Index2 index, ArrayView2D<int> bpBuffer, ArrayView2D<int> result, Index2 tilesize, int tilesPerRow)
        {
            if (index.X >= index.Y)
                return;
            if (result[index.X, 0] > 0)
                return;
            if (result[index.Y, 0] > 0)
                return;

            int i1 = result[index.X, 1] * tilesize.X;
            int j1 = result[index.X, 2] * tilesize.Y;

            int i2 = result[index.Y, 1] * tilesize.X;
            int j2 = result[index.Y, 2] * tilesize.Y;

            byte d1 = 1;
            byte d2 = 1;
            byte d3 = 1;
            byte d4 = 1;

            for (int i = 0; i < tilesize.X; i++)
            {
                if (d1 == 1)
                {
                    for (int j = 0; j < tilesize.Y; j++)
                    {
                        if (bpBuffer[i1 + i, j1 + j] != bpBuffer[i2 + i, j2 + j])
                        {
                            d1 = 0;
                            break;
                        }
                    }
                }
                if (d2 == 1)
                {
                    for (int j = 0; j < tilesize.Y; j++)
                    {
                        if (bpBuffer[i1 + i, j1 + j] != bpBuffer[i2 + i, j2 + tilesize.Y - 1 - j])
                        {
                            d2 = 0;
                            break;
                        }
                    }
                }
                if (d3 == 1)
                {
                    for (int j = 0; j < tilesize.Y; j++)
                    {
                        if (bpBuffer[i1 + i, j1 + j] != bpBuffer[i2 + tilesize.X - 1 - i, j2 + j])
                        {
                            d3 = 0;
                            break;
                        }
                    }
                }
                if (d4 == 1)
                {
                    for (int j = 0; j < tilesize.Y; j++)
                    {
                        if (bpBuffer[i1 + i, j1 + j] != bpBuffer[i2 + tilesize.X - 1 - i, j2 + tilesize.Y - 1 - j])
                        {
                            d4 = 0;
                            break;
                        }
                    }
                }
                if (d1 == 0 && d2 == 0 && d3 == 0 && d4 == 0)
                {
                    return;
                }
            }
            if (d1 == 1 || d2 == 1 || d3 == 1 || d4 == 1)
                result[index.Y, 0] = 1 + (i1 / tilesize.X) + ((j1 / tilesize.Y) * tilesPerRow);
        }
    }
}
