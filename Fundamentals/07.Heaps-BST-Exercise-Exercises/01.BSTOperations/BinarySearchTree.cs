namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Copy(root);
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => this.Root.Count;

        public bool Contains(T element)
        {
            var current = this.Root;

            while (current != null)
            {
                if (this.IsLess(current.Value, element))
                {
                    current = current.RightChild;
                }
                else if (this.IsGreater(current.Value, element))
                {
                    current = current.LeftChild;
                }
                else
                {
                    return true;
                }
            }


            return false;
        }

        public void Insert(T element)
        {
            Node<T> toInsert = new Node<T>(element, null, null);

            if (this.Root == null)
            {
                this.Root = toInsert;
            }
            else
            {
                this.InserDfs(this.Root, null, toInsert);
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            Node<T> current = this.Root;

            while (current != null)
            {
                if (this.IsLess(current.Value, element))
                {
                    current = current.RightChild;
                }
                else if (this.IsGreater(current.Value, element))
                {
                    current = current.LeftChild;
                }
                else
                {
                    break;
                }
            }

            return new BinarySearchTree<T>(current);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrderDfs(this.Root, action);
        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.Root);

            while (queue.Count > 0)
            {
                var currnet = queue.Dequeue();

                if (this.IsLess(lower, currnet.Value)
                    && this.IsGreater(upper, currnet.Value))
                {
                    result.Add(currnet.Value);
                }
                else if (this.AreEqual(currnet.Value, lower)
                    || this.AreEqual(currnet.Value, upper))
                {
                    result.Add(currnet.Value);
                }

                if (currnet.LeftChild != null)
                {
                    queue.Enqueue(currnet.LeftChild);
                }
                if (currnet.RightChild != null)
                {
                    queue.Enqueue(currnet.RightChild);
                }
            }


            return result;
        }

        public void DeleteMin()
        {
            this.EnsureNotEmpty();

            var current = this.Root;
            Node<T> previous = null;

            if (this.Root.LeftChild == null)
            {
                this.Root = this.Root.RightChild;
            }
            else
            {
                while (current.LeftChild != null)
                {
                    current.Count--;
                    previous = current;
                    current = current.LeftChild;
                }

                previous.LeftChild = current.RightChild;
            }
        }

        public void DeleteMax()
        {
            this.EnsureNotEmpty();

            var current = this.Root;
            Node<T> previous = null;

            if (this.Root.RightChild == null)
            {
                this.Root = this.Root.LeftChild;
            }
            else
            {
                while (current.RightChild != null)
                {
                    current.Count--;
                    previous = current;
                    current = current.RightChild;
                }

                previous.RightChild = current.LeftChild;
            }
        }

        public int GetRank(T element)
        {
            return this.GetRankDfs(this.Root, element);
        }

        private int GetRankDfs(Node<T> current, T element)
        {
            if (current == null)
            {
                return 0;
            }

            if (this.IsLess(element, current.Value))
            {
                return this.GetRankDfs(current.LeftChild, element);
            }
            else if (AreEqual(element, current.Value))
            {
                return this.GetNodeCount(current);
            }

            return this.GetNodeCount(current.LeftChild) + 1 + this.GetRankDfs(current.RightChild, element);
        }

        private int GetNodeCount(Node<T> current)
        {
            return current == null ? 0 : current.Count;
        }

        private bool IsGreater(T first, T second)
        {
            return first.CompareTo(second) > 0;
        }
        private bool IsLess(T first, T second)
        {
            return first.CompareTo(second) < 0;
        }
        private bool AreEqual(T first, T second)
        {
            return first.CompareTo(second) == 0;
        }

        private void InserDfs(Node<T> current, Node<T> previous, Node<T> toInsert)
        {
            if (current == null && this.IsLess(previous.Value, toInsert.Value))
            {
                previous.RightChild = toInsert;

                return;
            }
            if (current == null && this.IsGreater(previous.Value, toInsert.Value))
            {
                previous.LeftChild = toInsert;

                return;
            }

            if (this.IsLess(current.Value, toInsert.Value))
            {
                if (this.RightChild == null)
                {
                    this.RightChild = toInsert;
                }

                this.InserDfs(current.RightChild, current, toInsert);
                current.Count++;
            }
            else if (this.IsGreater(current.Value, toInsert.Value))
            {
                if (this.LeftChild == null)
                {
                    this.LeftChild = toInsert;
                }

                this.InserDfs(current.LeftChild, current, toInsert);
                current.Count++;
            }
        }

        private void EachInOrderDfs(Node<T> current, Action<T> action)
        {
            if (current != null)
            {
                this.EachInOrderDfs(current.LeftChild, action);
                action.Invoke(current.Value);
                this.EachInOrderDfs(current.RightChild, action);
            }
        }

        private void Copy(Node<T> current)
        {
            if (current != null)
            {
                this.Insert(current.Value);
                this.Copy(current.LeftChild);
                this.Copy(current.RightChild);
            }
        }

        private void EnsureNotEmpty()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("Tree is empty!");
            }
        }
    }
}
