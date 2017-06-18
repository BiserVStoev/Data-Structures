using System;

namespace _03.ImplementAnArrayBasedStack
{
    public class ArrayStack<T>
    {
        private const int InitialCapacity = 16;
        private T[] elements;

        public ArrayStack(int capacity = InitialCapacity)
        {
            this.elements = new T[capacity];
        }

        public int Capacity
        {
            get
            {
                return this.elements.Length;
            }
        }

        public int Count { get; private set; }

        public void Push(T element)
        {
            if (this.Count == this.Capacity)
            {
                this.Resize();
            }

            this.elements[Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            this.Count--;
            var element = this.elements[this.Count];
            this.elements[this.Count] = default(T);

            return element;
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];

            for (int index = 0; index < this.Count; index++)
            {
                array[index] = this.elements[index];
            }

            Array.Reverse(array);

            return array;
        }

        private void Resize()
        {
            var doubleSizedArray = new T[this.elements.Length * 2];

            this.CopyAllElementsTo(doubleSizedArray);

            this.elements = doubleSizedArray;
        }

        private void CopyAllElementsTo(T[] doubleSizedArray)
        {
            for (int index = 0; index < this.Count; index++)
            {
                doubleSizedArray[index] = this.elements[index];
            }
        }
    }

}
