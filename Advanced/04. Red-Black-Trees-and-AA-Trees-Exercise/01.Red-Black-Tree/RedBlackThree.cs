namespace _01.Red_Black_Tree
{
    using System;
    using System.Collections.Generic;

    public class RedBlackTree<T>
        : IBinarySearchTree<T> where T : IComparable
    {
        private const bool Red = true;
        private const bool Black = false;

        private Node root;

        public RedBlackTree()
        {
        }

        public int Count => this.root == null ? 0 : this.root.Count;

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
            this.root.Color = Black;
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                return new Node(element);
            }

            int copm = element.CompareTo(node.Value);

            if (copm > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }
            else if (copm < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }

            if (IsRed(node.Right) && !IsRed(node.Left))
            {
                node = this.RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColors(node);
            }

            node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);

            return node;
        }

        public T Select(int rank)
        {
            var node = this.Select(rank, this.root);

            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }

        private Node Select(int rank, Node node)
        {
            if (node == null)
            {
                return null;
            }

            int leftCount = this.GetCount(node.Left);

            if (leftCount == rank)
            {
                return node;
            }
            if (leftCount > rank)
            {
                return this.Select(rank, node.Left);
            }

            return this.Select(rank - (leftCount + 1), node.Right);
        }

        public int Rank(T element)
        {
            return this.Rank(element, this.root);
        }

        public bool Contains(T element)
        {
            Node node = FindNode(element, this.root);

            if (node == null)
            {
                return false;
            }

            return true;
        }


        public IBinarySearchTree<T> Search(T element)
        {
            Node node = this.FindNode(element, this.root);
            RedBlackTree<T> tree = new RedBlackTree<T>();
            tree.root = node;

            return tree;
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMin(this.root);
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMax(this.root);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            return null;
        }

        public void Delete(T element)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.Delete(element, this.root);
        }

        public T Ceiling(T element)
        {
            return this.Select(this.Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return this.Select(this.Rank(element) - 1);
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(action, this.root);
        }

        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(action, node.Left);
            action(node.Value);
            this.EachInOrder(action, node.Right);
        }

        // Private Methods

        private void FlipColors(Node node)
        {
            node.Color = !node.Color;
            node.Left.Color = !node.Left.Color;
            node.Right.Color = !node.Right.Color;
        }

        private Node RotateLeft(Node node)
        {
            var temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;

            temp.Color = node.Color;
            node.Color = Red;
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return temp;
        }

        private Node RotateRight(Node node)
        {
            var temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            temp.Color = node.Color;
            node.Color = Red;
            node.Count = 1 + GetCount(node.Left) + GetCount(node.Right);

            return temp;
        }

        private int GetCount(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Color;
        }

        private Node FindNode(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }

            int comp = element.CompareTo(node.Value);

            if (comp > 0)
            {
                node = FindNode(element, node.Right);
            }
            else if (comp < 0)
            {
                node = FindNode(element, node.Left);
            }
            else
            {
                return node;
            }

            return node;
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = this.DeleteMin(node.Left);
            node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);

            return node;
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = this.DeleteMax(node.Right);
            node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);

            return node;
        }

        private Node Delete(T element, Node node)
        {
            int comp = element.CompareTo(node.Value);

            if (comp > 0)
            {
                node.Right = this.Delete(element, node.Right);
            }
            else if (comp < 0)
            {
                node.Left = this.Delete(element, node.Left);
            }
            else
            {
                var temp = node;
                node = this.FindMin(temp.Right);
                node.Right = this.DeleteMin(temp.Right);
                node.Left = temp.Left;
            }

            node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);

            return node;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            node = this.FindMin(node.Left);

            return node;
        }

        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            int comp = element.CompareTo(node.Value);

            if (comp > 0)
            {
                return this.GetCount(node.Left) + 1 + Rank(element, node.Right);
            }
            if (comp < 0)
            {
                return this.Rank(element, node.Left);
            }

            return this.GetCount(node.Left);
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Color = Red;
                this.Count = 1;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Count { get; set; }
            public bool Color { get; set; }
        }
    }
}