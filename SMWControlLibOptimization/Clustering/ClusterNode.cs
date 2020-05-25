﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibOptimization.Clustering
{
    public abstract class ClusterNode<T>
    {
        public virtual int Size { get; }
        public int MaxClusterSize { get; internal set; }
        public T Content { get; protected set; }

        public ClusterNode(int maxSize)
        {
            MaxClusterSize = maxSize;
        }
        public abstract ClusterNode<T> Merge(T cont);
        public virtual ClusterNode<T> Merge(ClusterNode<T> cont)
        {
            return Merge(cont.Content);
        }
        public abstract int MergeSize(T cont);
        public virtual int MergeSize(ClusterNode<T> cont)
        {
            return MergeSize(cont.Content);
        }
        public abstract int Distance(T cont);
        public virtual int Distance(ClusterNode<T> cont)
        {
            return Distance(cont.Content);
        }
    }
}
