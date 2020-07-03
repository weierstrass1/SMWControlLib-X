using SMWControlLibUtils.DataStruct;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibUtils
{
    public class CommandManager : DoubleLinkedList<ICommand>
    {
        public CommandManager() : base()
        {
        }
        public CommandInvoker GetInvoker()
        {
            return (CommandInvoker)GetEnumerator();
        }
        public override ListEnumerator<ICommand> GetEnumerator()
        {
            return new CommandInvoker(first, last);
        }
    }
}
