namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public SinglyLinkedList()
        {
            this._head = null;
            this.Count = 0;
        }

        public SinglyLinkedList(Node<T> head)
        {
            this._head = head;
            this.Count = 1;
        }

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> toInsert = new Node<T>(item, this._head);
            this._head = toInsert;
          
            this.Count++;
        }

        public void AddLast(T item)
        {
            Node<T> current = this._head;
            Node<T> toInsert = new Node<T>(item);

            if (current == null)
            {
                this._head = toInsert;
            }
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = toInsert;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.ValidateIfEmpty();
            return this._head.Value;
        }
        

        public T GetLast()
        {
            this.ValidateIfEmpty();

            Node<T> current = this._head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            return current.Value;
        }

        public T RemoveFirst()
        {
            this.ValidateIfEmpty();
            Node<T> current = this._head;
            this._head = this._head.Next;
            this.Count--;

            return current.Value;
        }

        public T RemoveLast()
        {
            this.ValidateIfEmpty();
            Node<T> current = this._head;
            Node<T> toReturn;

            if (this.Count == 1)
            {
                toReturn = current;
                current = null;
            }
            else
            {
                while (current.Next.Next != null)
                {
                    current = current.Next;
                }

                toReturn = current.Next;
                current.Next = null;
            }
            
            this.Count--;

            return toReturn.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = this._head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidateIfEmpty()
        {
            if (this._head == null)
            {
                throw new InvalidOperationException("The Linked list is empty!");
            }
        }
    }
}