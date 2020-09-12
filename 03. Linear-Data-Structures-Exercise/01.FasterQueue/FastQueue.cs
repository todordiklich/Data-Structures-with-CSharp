namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FastQueue<T> : IAbstractQueue<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public FastQueue()
        {
            this.head = this.tail = null;
            this.Count = 0;
        }

        public FastQueue(Node<T> headNode)
        {
            this.head = this.tail = headNode;
            this.Count = 1;
        }
        public int Count { get; private set; }

        public bool Contains(T item)
        {
            this.EnsureNotEmpty();
            Node<T> current = this.head;

            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.EnsureNotEmpty();
            Node<T> current = this.head;

            if (this.Count == 1)
            {
                this.head = this.tail = null;
            }
            else
            {
                this.head = this.head.Next;
            }

            this.Count--;
            return current.Item;
        }

        public void Enqueue(T item)
        {
            Node<T> toInsert = new Node<T>
            {
                Item = item
            };

            if (this.Count == 0)
            {
                this.head = this.tail = toInsert;
            }
            else
            {
                this.tail.Next = toInsert;
                this.tail = toInsert;
            }

            this.Count++;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this.head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this.head;

            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }
    }
}