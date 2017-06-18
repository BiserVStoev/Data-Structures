namespace _07SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IEnumerable<T> where T: IComparable<T>
    {
        private class ListNode<T>
        {
            public ListNode(T value)
            {
                this.Value = value;
            }
            public T Value { get; }

            public ListNode<T> Next { get; set; }

        }

        public int Count { get; private set; }

        private ListNode<T> head;

        public void Add(T element)
        {
            if (this.Count == 0)
            {
                this.head = new ListNode<T>(element);
            }
            else
            {
                var current = this.head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new ListNode<T>(element);
            }
            this.Count++;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Given index does not exist!");
            }

            if (index == 0)
            {
                this.head = this.head.Next;
            }
            else
            {
                var node = this.head;
                for (int i = 1; i < index; i++)
                {
                    node = node.Next;
                }

                var previous = node;
                node = node.Next;
                var next = node.Next;
                previous.Next = next;
            }
            
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int FirstIndexOf(T item)
        {
            var index = -1;
            var count = 0;
            var start = this.head;
            while (start != null)
            {
                if (start.Value.CompareTo(item) == 0)
                {
                    return count;
                }
                count++;
                start = start.Next;
            }
            return index;
        }

        public int LastIndexOf(T item)
        {
            var index = -1;
            var count = 0;
            var start = this.head;
            while (start != null)
            {
                if (start.Value.CompareTo(item) == 0)
                {
                    index = count;
                }
                count++;
                start = start.Next;
            }
            return index;
        }
    }


    public class Example
    {
        public static void Main()
        {
            var list = new SinglyLinkedList<int>();

            Console.WriteLine("--------------------");

            list.Add(5);
            list.Add(3);
            list.Add(2);
            list.Add(10);
            list.Add(8);
            list.Add(2);
            Console.WriteLine("Count = {0}", list.Count);

            Console.WriteLine(string.Join(", ",list));
            Console.WriteLine("--------------------");

            list.Remove(1);

            Console.WriteLine(string.Join(", ", list));
            Console.WriteLine("--------------------");

            Console.WriteLine(list.FirstIndexOf(2));
            Console.WriteLine(list.LastIndexOf(2));
            
            Console.WriteLine("--------------------");

            foreach (var element in list)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }
    }
}
