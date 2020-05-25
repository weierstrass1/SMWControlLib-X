using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibOptimization.Astar
{
    public abstract class AstarNode<T> : IComparable<AstarNode<T>>
    {
        public T Content { get; protected set; }
        public AstarNode<T> Parent { get; protected set; }
        public List<AstarNode<T>> Children { get; protected set; }
        public int Heuristic { get; protected set; }
        public int Cost { get; protected set; }
        public int Value { get; protected set; }

        public AstarNode()
        {
            Cost = 0;
            Heuristic = 0;
            Value = 0;
        }

        public virtual int CompareTo(AstarNode<T> other)
        {
            if (Value < other.Value) return -1;
            if (Value > other.Value) return 1;
            return 0;
        }

        public abstract void Expand(params object[] args);
        public abstract bool Completed(params object[] args);
    }
}
