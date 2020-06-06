using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.Clustering
{
    public class GPUHierarchicalClusteringSolver
    {
        public static List<ConcurrentDictionary<Int32, int>> Solve(List<ConcurrentDictionary<Int32, int>> clustersDic, int maxPal)
        {
            int[,] clusters = new int[clustersDic.Count, maxPal];
            int j, k = 0;
            foreach (var pal in clustersDic)
            {
                if (pal.Count <= maxPal)
                {
                    j = 0;
                    foreach (var kvp in pal)
                    {
                        clusters[k, j] = kvp.Key;
                        j++;
                    }
                }
                k++;
            }

            GetNearestClusterKernel.Execute(clusters);
            List<ConcurrentDictionary<Int32, int>> l = new List<ConcurrentDictionary<int, int>>();
            ConcurrentDictionary<int, int> curp;
            for (int i = 0; i < clusters.GetLength(0); i++)
            {
                if(clusters[i,0] != 0)
                {
                    curp = new ConcurrentDictionary<int, int>();
                    for (j = 0; j < clusters.GetLength(1); j++)
                    {
                        if (clusters[i, j] != 0)
                            curp.TryAdd(clusters[i, j], 0);
                    }
                    l.Add(curp);
                }
            }
            return l;
        }
    }
}
