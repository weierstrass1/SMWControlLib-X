using SMWControlLibUtils.DataStruct;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibUtils
{
    public class CommandInvoker : ListEnumerator<ICommand>
    {
        internal CommandInvoker(ListNode<ICommand> node) : base(node)
        {
        }
        internal CommandInvoker(ListNode<ICommand> f, ListNode<ICommand> l) : base(f, l)
        {
        }
        public void Invoke()
        {
            if (current != null)
                current.Content.Execute();
        }
    }
}
