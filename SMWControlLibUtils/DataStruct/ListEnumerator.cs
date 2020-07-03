using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibUtils.DataStruct
{
    public class ListEnumerator<T>
    {
        protected ListNode<T> first { get; set; }
        protected ListNode<T> last { get; set; }
        protected ListNode<T> current { get; set; }
        protected T Current { get => current.Content; }
        public ListEnumerator(ListNode<T> node)
        {
            current = null;
            first = node;
        }
        public ListEnumerator(ListNode<T> f, ListNode<T> l)
        {
            current = null;
            first = f;
            last = l;
        }
        public void Reset()
        {
            current = null;
        }
        public bool MovePreview()
        {
            if (last == null)
                return false;
            if (last != null && current == null)
            {
                current = last;
                return true;
            }
            if (current.Preview == null)
                return false;

            current = current.Preview;
            return true;
        }
        public bool MoveNext()
        {
            if (first == null)
                return false;
            if (first != null && current == null)
            {
                current = first;
                return true;
            }
            if (current.Next == null)
                return false;

            current = current.Next;
            return true;
        }
        public bool HasNext()
        {
            if (first == null)
                return false;

            if (first != null && current == null)
                return true;

            return current.Next != null;
        }
        public bool HasPreview()
        {
            if (last == null)
                return false;

            if (last != null && current == null)
                return true;

            return current.Preview != null;
        }
    }
}
