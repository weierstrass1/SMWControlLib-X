using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibUtils.DataStruct
{
    public class ListNode<T>
    {
        public ListNode<T> Preview { get; internal set; }
        public ListNode<T> Next { get; internal set; }
        public T Content { get; set; }

        public ListNode(T content)
        {
            Content = content;
        }

    }
}
