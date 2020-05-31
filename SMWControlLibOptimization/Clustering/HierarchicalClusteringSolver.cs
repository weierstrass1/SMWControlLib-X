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
            List<ClusterNode<T>> clustersAux, candidatesAux, remlist = new List<ClusterNode<T>>(),
                addlist = new List<ClusterNode<T>>();
            ClusterNode<T> cux, c1, c2;
            bool change = true, replace;
            int l;
            int dist, curdist;
            float breakDraw, curbd;

            while(change)
            {
                change = false;
                dist = int.MaxValue;
                c1 = null;
                c2 = null;
                clustersAux = new List<ClusterNode<T>>();
                l = clusters.Count;
                breakDraw = float.MaxValue;
                candidatesAux = new List<ClusterNode<T>>();

                for (int i = 0; i < l; i++)
                {
                    cux = clusters.First();
                    clusters.RemoveAt(0);
                    clustersAux.Add(cux);

                    foreach (var c in clusters)
                    {
                        if (cux.MergeSize(c) <= ClusterMaxSize)
                        {
                            curdist = cux.Distance(c);
                            if (curdist <= dist)
                            {
                                replace = curdist < dist;

                                if(replace)
                                {
                                    breakDraw = float.MaxValue;
                                }
                                else if (!replace && curdist == dist)
                                {
                                    if (breakDraw == float.MaxValue)
                                        breakDraw = c1.BreakDraw(c2);
                                    
                                    curbd = cux.BreakDraw(c);

                                    replace = curbd < breakDraw;
                                    if(replace)
                                    {
                                        breakDraw = curbd;
                                    }
                                    else if (curbd == breakDraw)
                                    {
                                        candidatesAux.Add(cux.Merge(c));
                                    }
                                }

                                if (replace)
                                {
                                    candidatesAux.Clear();
                                    dist = curdist;
                                    c1 = cux;
                                    c2 = c;
                                    candidatesAux.Add(c1.Merge(c2));
                                    if (dist == 0)
                                        break;
                                }
                            }
                        }
                    }
                }

                if (c1 != null && c2 != null)
                {
                    change = true;
                    if (candidatesAux.Count > 1)
                    {
                        dist = 0;

                        foreach (var cx in candidatesAux)
                        {
                            curdist = 0;
                            foreach (var cy in clustersAux)
                            {
                                if (cx.Contains(cy))
                                {
                                    curdist++;
                                }
                            }
                            if (dist < curdist)
                            {
                                dist = curdist;
                                addlist.Clear();
                            }
                            if (dist == curdist)
                            {
                                addlist.Add(cx);
                            }
                        }
                        if (addlist.Count > 1)
                        {
                            int b;
                            int checks = 0;
                            int[] uniques = new int[addlist.Count];
                            int uniqid;
                            int iter;
                            int maxUn = 0;

                            foreach (var c in clustersAux)
                            {
                                b = 0;
                                iter = 0;
                                uniqid = -1;
                                foreach (var cx in addlist)
                                {
                                    if (cx.Contains(c))
                                    {
                                        uniqid = iter;
                                        b++;
                                    }
                                    iter++;
                                }
                                if (b == 1)
                                {
                                    uniques[uniqid]++;
                                    if (uniques[uniqid] > maxUn)
                                    {
                                        maxUn = uniques[uniqid];
                                    }
                                    checks++;
                                }
                            }

                            int minSize = int.MaxValue;
                            iter = 0;
                            foreach (var c in addlist) 
                            {
                                if (uniques[iter] != dist)
                                {
                                    remlist.Add(c);
                                    if (uniques[iter] == maxUn && minSize > c.Size) 
                                    {
                                        c1 = c;
                                        minSize = c.Size;
                                    }
                                }
                                iter++;
                            }

                            if (remlist.Count == addlist.Count && minSize != int.MaxValue) 
                            {
                                remlist.Remove(c1);
                            }

                            foreach (var c in remlist)
                            {
                                addlist.Remove(c);
                            }
                            remlist.Clear();
                        }
                    }
                    else
                    {
                        addlist.Add(candidatesAux.First());
                    }

                    foreach (var cx in addlist)
                    {
                        foreach (var c in clustersAux)
                        {
                            if (cx.Contains(c))
                            {
                                remlist.Add(c);
                            }
                        }

                        foreach (var c in remlist)
                        {
                            clustersAux.Remove(c);
                        }
                        remlist.Clear();

                        clustersAux.Add(cx);
                    }
                    addlist.Clear();
                    candidatesAux.Clear();
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
