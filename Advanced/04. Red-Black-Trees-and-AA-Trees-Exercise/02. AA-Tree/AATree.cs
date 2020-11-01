namespace _02._AA_Tree
{
    using System;
    using System.ComponentModel;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private Node<T> root;

        public int CountNodes()
        {
            return this.root == null ? 0 : this.root.Count;
        }

        public bool IsEmpty()
        {
            return this.root == null;
        }

        public void Clear()
        {
            this.root = null;
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        private Node<T> Insert(T element, Node<T> node)
        {
            if (node == null)
            {
                return new Node<T>(element);
            }

            int comp = element.CompareTo(node.Value);

            if (comp > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }
            if (comp < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }

            node = Skew(node);
            node = Split(node);

            node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);

            return node;
        }

        private int GetCount(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private Node<T> Split(Node<T> node)
        {
            if (node.Level == node.Right?.Right?.Level)
            {
                var temp = node.Right;
                node.Right = temp.Left;
                temp.Left = node;

                node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);
                temp.Level = this.UpdateLevel(temp.Right) + 1;

                return temp;
            }
            else
            {
                return node;
            }
        }

        private int UpdateLevel(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Level;
        }

        private Node<T> Skew(Node<T> node)
        {
            if (node.Level == node.Left?.Level)
            {
                var temp = node.Left;
                node.Left = temp.Right;
                temp.Right = node;
                node.Count = 1 + this.GetCount(node.Left) + this.GetCount(node.Right);

                return temp;
            }
            else
            {
                return node;
            }
        }

        public bool Search(T element)
        {
            Node<T> node = this.FindNode(element, this.root);

            if (node == null)
            {
                return false;
            }

            return true;
        }

        private Node<T> FindNode(T element, Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            int comp = element.CompareTo(node.Value);

            if (comp > 0)
            {
                node = this.FindNode(element, node.Right);
            }
            else if (comp < 0)
            {
                node = this.FindNode(element, node.Left);
            }
            else
            {
                return node;
            }

            return node;
        }

        public void InOrder(Action<T> action)
        {
            this.InOrder(action, this.root);
        }

        private void InOrder(Action<T> action, Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            this.InOrder(action, node.Left);
            action(node.Value);
            this.InOrder(action, node.Right);
        }

        public void PreOrder(Action<T> action)
        {
            this.PreOrder(action, this.root);
        }
        private void PreOrder(Action<T> action, Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            action(node.Value);
            this.PreOrder(action, node.Left);
            this.PreOrder(action, node.Right);
        }

        public void PostOrder(Action<T> action)
        {
            this.PostOrder(action, this.root);
        }
        private void PostOrder(Action<T> action, Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            this.PostOrder(action, node.Left);
            this.PostOrder(action, node.Right);
            action(node.Value);
        }
    }
}