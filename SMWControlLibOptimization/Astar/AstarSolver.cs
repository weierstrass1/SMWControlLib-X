using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C5;

namespace SMWControlLibOptimization.Astar
{
    public class AstarSolver<T>
    {
        int added = 0, total = 0;
        public virtual AstarNode<T> Solve(AstarNode<T> Root, params object[] args)
        {
            IntervalHeap<AstarNode<T>> ih = new IntervalHeap<AstarNode<T>>();
            ih.Add(Root);
            AstarNode<T> curNode;
            bool add;

            while (ih.Count > 0)
            {
                curNode = ih.DeleteMin();
                curNode.Expand(args);

                if (curNode.Completed(args)) 
                    return curNode;

                if (curNode.Children.Count > 0)
                {
                    foreach(var node in curNode.Children)
                    {
                        if (node.CanAdd())
                        {
                            ih.Add(node);
                            added++;
                        }
                        total++;
                    }
                }
            }
            return null;
        }
    }
}
