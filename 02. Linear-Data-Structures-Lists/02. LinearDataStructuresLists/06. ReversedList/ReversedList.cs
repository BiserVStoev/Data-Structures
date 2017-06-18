namespace _06ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private const int DefaultCapacity = 8;
        private T[] array;

        public ReversedList() : this(DefaultCapacity)
        {
        }

        public ReversedList(int capacity)
        {
            this.Capacity = capacity;
            this.array = new T[this.Capacity];
            this.Count = 0;
        }

        public int Capacity { get; }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (this.Count == this.Capacity)
            {
                this.Resize();
            }
            this.array[this.Count] = item;
            this.Count++;
        }

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this.array[(this.Count-1) - index];
            }
            set
            {
                this.ValidateIndex(index);
                this.array[(this.Count - 1) - index] = value;
            }
        }

        public void Remove(int index)
        {
            this.ValidateIndex(index);
            var realIndex = (this.Count - 1) - index;
            for (int i = realIndex; i < this.Count - 1; i++)
            {
                this.array[i] = this.array[i + 1];
            }
            this.Count--;
        }

        private void Resize()
        {
            var newArray = new T[this.Capacity * 2];
            Array.Copy(this.array, newArray, this.array.Length);
            this.array = newArray;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Given index does not exist!");
            }
        }


        public IEnumerator<T> GetEnumerator()
        {
            var start = this.Count - 1;
            for (int i = start; i >= 0; i--)
            {
                yield return this.array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
