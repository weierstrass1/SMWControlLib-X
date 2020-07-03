using SMWControlLibOptimization.Clustering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.ColorReduction
{
    /// <summary>
    /// The color reduction cluster node.
    /// </summary>
    public class ColorReductionClusterNode : ClusterNode<ColorGroup>
    {
        public ColorReductionClusterNode() : base(1)
        {

        }
        public override float BreakDraw(ColorGroup cont)
        {
            return MergeSize(cont);
        }

        public override bool Contains(ColorGroup cont)
        {
            return Content.Contains(cont);
        }

        public override int Distance(ColorGroup cont)
        {
            return Content.Distance(cont);
        }

        public override ClusterNode<ColorGroup> Merge(ColorGroup cont)
        {
            if (Content == null)
            {
                Content = cont;
                return this;
            }

            ColorReductionClusterNode node = new ColorReductionClusterNode();
            node.Content = Content.Merge(cont);

            return node;
        }

        public override int MergeSize(ColorGroup cont)
        {
            return Content.Length + cont.Length;
        }
    }
}
