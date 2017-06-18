using System;

namespace _05.LinkedStack
{
    public class LinkedStack<T>
    {
        private Node<T> firstNode;

        public int Count { get; private set; }

        public void Push(T element)
        {
            var node = new Node<T>(element);
            if (this.Count == 0)
            {
                this.firstNode = node;
            }
            else
            {
                node.NextNode = this.firstNode;
                this.firstNode = node;
            }

            this.Count++;
        }

        public T Pop()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            var first = this.firstNode;
            this.firstNode = this.firstNode.NextNode;

            this.Count--;

            return first.Value;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];

            var node = this.firstNode;
            for (int i = 0; i < this.Count; i++)
            {
                array[i] = node.Value;
                node = node.NextNode;
            }

            return array;
        }

        private class Node<T>
        {
            private T value;

            public Node(T value, Node<T> nextNode = null)
            {
                this.Value = value;
                this.NextNode = nextNode;
            }

            public Node<T> NextNode { get; set; }

            public T Value
            {
                get
                {
                    return this.value; 
                }

                private set
                {
                    this.value = value;
                }
            }
        }
    }
}
