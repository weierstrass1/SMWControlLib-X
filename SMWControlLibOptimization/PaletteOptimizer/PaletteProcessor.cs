using SMWControlLibOptimization.Astar;
using SMWControlLibOptimization.Clustering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.PaletteOptimizer
{
    public class PaletteProcessor
    {
        public static List<ConcurrentDictionary<Int32, int>> OptimizePalettes(List<ConcurrentDictionary<Int32, int>> allPals, int maxPal)
        {
            List< ConcurrentDictionary<Int32, int>> res = 
                HierarchicalClusteringSolver<ConcurrentDictionary<Int32, int>, PalettesClusterNode>.Solve(allPals, maxPal);
            return res;
            /*PalletteGroupNode root = new PalletteGroupNode(res, maxPal);
            AstarSolver<ConcurrentDictionary<Int32, int>> astarSolver = new AstarSolver<ConcurrentDictionary<int, int>>();

            PalletteGroupNode res1 = (PalletteGroupNode)astarSolver.Solve(root);

            return res1.FinishedPalettes;*/
        }
        public static int CountEquals(ConcurrentDictionary<Int32, int> p1, ConcurrentDictionary<Int32, int> p2)
        {
            ConcurrentDictionary<Int32, int> res = p1;
            ConcurrentDictionary<Int32, int> rev = p2;
            if (p2.Count > p1.Count)
            {
                res = p2;
                rev = p1;
            }

            int delta = 0;

            Parallel.ForEach(rev, kvp =>
            {
                if (res.ContainsKey(kvp.Key))
                    delta++;
            });

            return delta;
        }

        public static int CountDiffs(ConcurrentDictionary<Int32, int> p1, ConcurrentDictionary<Int32, int> p2)
        {
            ConcurrentDictionary<Int32, int> res = p1;
            ConcurrentDictionary<Int32, int> rev = p2;
            if (p2.Count > p1.Count)
            {
                res = p2;
                rev = p1;
            }

            int delta = 0;

            Parallel.ForEach(rev, kvp =>
            {
                if (!res.ContainsKey(kvp.Key))
                    delta++;
            });

            return delta;
        }
    }
}
