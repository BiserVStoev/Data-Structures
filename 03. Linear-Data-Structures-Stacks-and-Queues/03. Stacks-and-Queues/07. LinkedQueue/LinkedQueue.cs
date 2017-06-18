namespace _07.LinkedQueue
{
    using System;

    public class LinkedQueue<T>
    {
        private QueueNode<T> head;
        private QueueNode<T> tail;
         
        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            var node = new QueueNode<T>(element);

            if (this.Count == 0)
            {
                this.head = this.tail = node;
            }
            else
            {
                this.tail.NextNode = node;
                node.PrevNode = this.tail;
                this.tail = node;
            }

            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty!");
            }

            var node = this.head;

            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.head = this.head.NextNode;
            }

            this.Count--;

            return node.Value;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];
            var node = this.head;
            int index = 0;

            while (node != null)
            {
                array[index] = node.Value;
                index++;
                node = node.NextNode;
            }

            return array;
        }

        private class QueueNode<T>
        {
            public QueueNode(T value)
            {
                this.Value = value;
            }

            public T Value { get; private set; }

            public QueueNode<T> NextNode { get; set; }

            public QueueNode<T> PrevNode { get; set; }
        }
    }
}
