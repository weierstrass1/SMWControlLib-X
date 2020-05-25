using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMWControlLibOptimization.Clustering
{
    public class HierarchicalClusteringSolver<T,K> where K : ClusterNode<T>, new()
    {
        public static List<T> Solve(List<T> input, int ClusterMaxSize, params object[] args)
        {
            List<ClusterNode<T>> clusters = new List<ClusterNode<T>>();
            K aux;

            foreach (var i in input)
            {
                aux = new K();
                aux.MaxClusterSize = ClusterMaxSize;
                aux = (K)aux.Merge(i);
                clusters.Add(aux);
            }
            List<ClusterNode<T>> clustersAux;
            ClusterNode<T> cux, c1, c2;
            bool change = true;
            int l;
            int dist, curdist, totalSize, cursize;

            while(change)
            {
                change = false;
                dist = 100000;
                c1 = null;
                c2 = null;
                clustersAux = new List<ClusterNode<T>>();
                l = clusters.Count;
                totalSize = -100000;

                for (int i = 0; i < l; i++)
                {
                    cux = clusters.First();
                    clusters.RemoveAt(0);
                    clustersAux.Add(cux);
                    foreach (var c in clusters)
                    {
                        curdist = cux.Distance(c);
                        cursize = cux.MergeSize(c);
                        if ((curdist < dist || (curdist == dist && (cursize < totalSize || cursize==ClusterMaxSize))) && curdist != 100000)
                        {
                            dist = curdist;
                            c1 = cux;
                            c2 = c;
                            totalSize = cursize;
                            if (dist == 0)
                                break;
                        }
                    }
                }

                if (c1 != null && c2 != null && dist != 100000)
                {
                    change = true;
                    clustersAux.Remove(c1);
                    clustersAux.Remove(c2);
                    clustersAux.Add(c1.Merge(c2));
                }

                clusters = clustersAux;
            }

            List<T> ret = new List<T>();

            foreach (var c in clusters)
            {
                ret.Add(c.Content);
            }

            return ret;
        }
    }
}
