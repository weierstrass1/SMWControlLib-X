using System;
using System.Collections.Generic;

namespace SMWControlLibOptimization.Astar
{
    public abstract class AstarNode<T> : IComparable<AstarNode<T>>
    {
        public static float WeightCost = 0, WeightHeuristic = 1;
        public T Content { get; protected set; }
        public AstarNode<T> Parent { get; protected set; }
        public List<AstarNode<T>> Children { get; protected set; }
        private int heuristic;
        public int Heuristic
        {
            get => heuristic;
            private set
            {
                heuristic = value;
                setValue();
            }
        }
        private int cost;
        public int Cost
        {
            get => cost;
            private set
            {
                cost = value;
                setValue();
            }
        }
        public float Value { get; private set; }
        protected abstract int setCost();
        public abstract bool CanAdd();
        protected void updateCost()
        {
            Cost = setCost();
        }
        protected abstract int setHeuristic();
        protected void updateHeuristic()
        {
            Heuristic = setHeuristic();
        }

        private void setValue()
        {
            Value = (WeightCost * cost) + (WeightHeuristic * heuristic);
        }

        public AstarNode()
        {
            cost = 0;
            heuristic = 0;
            Value = 0;
            Children = new List<AstarNode<T>>();
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
