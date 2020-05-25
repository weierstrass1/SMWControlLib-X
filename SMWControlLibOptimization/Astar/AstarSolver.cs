using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C5;

namespace SMWControlLibOptimization.Astar
{
    public class AstarSolver<T>
    {
        public virtual AstarNode<T> Solve(AstarNode<T> Root, params object[] args)
        {
            IntervalHeap<AstarNode<T>> ih = new IntervalHeap<AstarNode<T>>();
            ih.Add(Root);
            List<AstarNode<T>> close = new List<AstarNode<T>>();
            AstarNode<T> curNode;
            bool add;

            while (ih.Count > 0)
            {
                curNode = ih.DeleteMin();
                curNode.Expand(args);
                close.Add(curNode);

                if (curNode.Completed(args)) 
                    return curNode;

                if (curNode.Children.Count > 0)
                {
                    foreach(var node in curNode.Children)
                    {
                        add = true;
                        foreach(var n in close)
                        {
                            if (n.Equals(node)) 
                            {
                                add = false;
                                break;
                            }
                        }

                        if (add)
                        {
                            foreach (var n in ih)
                            {
                                if (n.Equals(node))
                                {
                                    add = false;
                                    break;
                                }
                            }
                        }

                        if(add)
                        {
                            ih.Add(node);
                        }
                    }
                }
            }
            return null;
        }
    }
}
