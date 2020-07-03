using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibUtils.DataStruct
{
    public class DoubleLinkedList<T>
    {
        internal ListNode<T> first, last;
        public int Length { get; private set; }

        public DoubleLinkedList()
        {
            Clear();
        }
        public virtual ListEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T>(first, last);
        }
        public void Clear()
        {
            first = null;
            last = null;
            Length = 0;
        }

        public T ElementAt(int i)
        {
            return elementAt(i).Content;
        }

        internal ListNode<T> elementAt(int i)
        {
            if (i < 0 || i >= Length)
                throw new IndexOutOfRangeException(nameof(i));

            if (i == 0)
                return first;
            if (i == Length - 1)
                return last;

            ListNode<T> aux;

            if (i <= Length / 2)
            {
                aux = first;
                for (int j = 0; j != i; j++)
                {
                    aux = aux.Next;
                }
            }
            else
            {
                aux = last;
                for (int j = Length - 1; j != i; j--)
                {
                    aux = aux.Preview;
                }
            }
            return aux;
        }

        public void AddAt(int i, T newElement)
        {
            if (i < 0 || i > Length) 
                throw new IndexOutOfRangeException(nameof(i));

            if (i == 0)
                AddFirst(newElement);
            if (i == Length)
                AddLast(newElement);

            ListNode<T> cmd = new ListNode<T>(newElement);

            ListNode<T> aux = elementAt(i);

            aux.Preview.Next = cmd;
            cmd.Preview = aux.Preview;

            aux.Preview = cmd;
            cmd.Next = aux;
        }

        public T RemoveAt(int i)
        {
            if (Length == 0)
                return default;

            if (i == 0)
                return RemoveFirst();
            if (i == Length - 1)
                return RemoveLast();

            ListNode<T> aux = elementAt(i);

            aux.Preview.Next = aux.Next;
            aux.Next.Preview = aux.Preview;

            aux.Next = null;
            aux.Preview = null;

            Length--;
            return aux.Content;
        }


        public void AddFirst(T newElement)
        {
            ListNode<T> cmd = new ListNode<T>(newElement);
            if (Length == 0)
            {
                first = cmd;
                last = cmd;
            }
            else
            {
                first.Preview = cmd;
                cmd.Next = first;
                first = cmd;
                first.Preview = null;
            }
            Length++;
        }

        public T RemoveFirst()
        {
            if (Length == 0)
                return default;

            ListNode<T> cmd = first;

            if (Length == 1)
            {
                first = null;
                last = null;
            }
            else
            {
                first = first.Next;
                first.Preview = null;
            }

            cmd.Next = null;
            Length--;
            return cmd.Content;
        }

        public void AddLast(T newElement)
        {
            ListNode<T> cmd = new ListNode<T>(newElement);

            if (Length == 0)
            {
                first = cmd;
                last = cmd;
            }
            else
            {
                last.Next = cmd;
                cmd.Preview = last;
                last = cmd;
                last.Next = null;
            }
            Length++;
        }

        public T RemoveLast()
        {
            if (Length == 0)
                return default;

            ListNode<T> cmd = last;

            if (Length == 1) 
            {
                first = null;
                last = null;
            }
            else
            {
                last = last.Preview;
                last.Next = null;
            }

            cmd.Preview = null;
            Length--;
            return cmd.Content;
        }
        public void UpdateFirst(T cmd)
        {
            if (first != null) 
                first.Content = cmd;
        }
        public void UpdateLast(T cmd)
        {
            if (first != null)
                first.Content = cmd;
        }
        public void UpdateAt(int i, T cmd)
        {
            if (i == 0)
                UpdateFirst(cmd);
            else if (i == Length - 1)
                UpdateLast(cmd);
            else
            {
                ListNode<T> aux = elementAt(i);
                aux.Content = cmd;
            }
        }
    }
}
