namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public PriorityQueue()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            var topElement = this.Peek();

            this._elements[0] = this._elements[this.Size - 1];
            this._elements.RemoveAt(this.Size - 1);

            this.HeapifyDown();

            return topElement;
        }

        private void HeapifyDown()
        {
            int index = 0;
            int leftChildIndex = this.GetLeftChild(index);

            while (leftChildIndex < this.Size && this.IsLess(index, leftChildIndex))
            {
                int toSwapWith = leftChildIndex;
                int rightChildIndex = this.GetRightChild(index);

                if (rightChildIndex < this.Size && this.IsLess(leftChildIndex, rightChildIndex))
                {
                    toSwapWith = rightChildIndex;
                }

                var temp = this._elements[toSwapWith];
                this._elements[toSwapWith] = this._elements[index];
                this._elements[index] = temp;

                index = toSwapWith;
                leftChildIndex = this.GetLeftChild(index);
            }
        }

        private bool IsLess(int childIndex, int parentIndex)
        {
            int result = this._elements[childIndex].CompareTo(this._elements[parentIndex]);

            return result <= 0;
        }

        private int GetRightChild(int elementIndex)
        {
            return elementIndex * 2 + 2;
        }

        private int GetLeftChild(int elementIndex)
        {
            return elementIndex * 2 + 1;
        }

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyUp(element);
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this._elements[0];
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("PriorityQueue is empty!");
            }
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
    }
}
