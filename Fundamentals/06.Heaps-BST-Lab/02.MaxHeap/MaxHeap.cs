namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MaxHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp(element);
        }

        public T Peek()
        {
            EnsureNotEmpty();

            return this._elements[0];
        }        

        private void HeapifyUp(T element)
        {
            int elementIndex = this.Size - 1;
            int parentIndex = this.GetParentIndex(elementIndex);

            while (elementIndex > 0 && this.IsGreater(elementIndex, parentIndex))
            {
                var temp = this._elements[parentIndex];
                this._elements[parentIndex] = element;
                this._elements[elementIndex] = temp;

                elementIndex = parentIndex;
                parentIndex = this.GetParentIndex(elementIndex);
            }
        }

        private bool IsGreater(int childIndex, int parentIndex)
        {
            int result = this._elements[childIndex].CompareTo(this._elements[parentIndex]);

            return result > 0;
        }

        private int GetParentIndex(int elementIndex)
        {
            return (elementIndex - 1) / 2;
        }

        private void EnsureNotEmpty()
        {
            if (this._elements.Count == 0)
            {
                throw new InvalidOperationException("Max Heap is empty!");
            }
        }
    }
}
