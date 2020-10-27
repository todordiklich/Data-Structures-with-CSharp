namespace _01.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node<T> root;
        private Dictionary<T, Node<T>> elements = new Dictionary<T, Node<T>>();

        public Hierarchy(T root)
        {
            this.root = this.CreateNode(root);
        }

        public int Count => this.elements.Count;

        public void Add(T element, T child)
        {
            if (!this.Contains(element))
            {
                throw new ArgumentException();
            }
            if (this.Contains(child))
            {
                throw new ArgumentException();
            }

            Node<T> childNode = this.CreateNode(child);
            Node<T> parentNode = this.elements[element];
            childNode.Parent = parentNode;
            parentNode.Children.Add(childNode);
        }

        public void Remove(T element)
        {
            if (element.Equals(this.root.Value))
            {
                throw new InvalidOperationException();
            }
            if (!this.Contains(element))
            {
                throw new ArgumentException();
            }

            Node<T> node = this.elements[element];

            node.Parent?.Children.Remove(node);

            if (node.Parent != null && node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    child.Parent = node.Parent;
                    node.Parent.Children.Add(child);
                }
            }
            
            elements.Remove(element);
        }

        public IEnumerable<T> GetChildren(T element)
        {
            if (!this.Contains(element))
            {
                throw new ArgumentException();
            }

            Node<T> node = this.elements[element];

            return node.Children.Select(c => c.Value);
        }

        public T GetParent(T element)
        {
            if (!this.Contains(element))
            {
                throw new ArgumentException();
            }

            Node<T> node = this.elements[element];

            if (node.Parent == null)
            {
                return default;
            }

            return node.Parent.Value;
        }

        public bool Contains(T element)
        {
            if (elements.ContainsKey(element))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            foreach (var element in this.elements)
            {
                if (other.Contains(element.Value.Value))
                {
                    yield return element.Value.Value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            
            queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                Node<T> current = queue.Dequeue();
                yield return current.Value;

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node<T> CreateNode(T value)
        {
            Node<T> node = new Node<T>(value);
            this.elements[value] = node;

            return node;
        }
    }
}