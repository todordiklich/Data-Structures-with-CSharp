namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (Tree<T> child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();
            if (IsRootDeleted)
            {
                return result;
            }

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> subtree = queue.Dequeue();

                result.Add(subtree.Value);

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();

            if (IsRootDeleted)
            {
                return result;
            }

            this.Dfs(this, result);

            return result;
        }
       

        public void AddChild(T parentKey, Tree<T> child)
        {
            Tree<T> searchedNode = FindBfs(parentKey);

            CheckEmptyNode(searchedNode);

            searchedNode._children.Add(child);
        }

        

        public void RemoveNode(T nodeKey)
        {
            var currentNode = this.FindBfs(nodeKey);
            this.CheckEmptyNode(currentNode);

            foreach (var child in currentNode.Children)
            {
                child.Parent = null;
            }

            currentNode._children.Clear();

            var parentNode = currentNode.Parent;

            if (parentNode == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                parentNode._children.Remove(currentNode);
                currentNode.Parent = null;
            }
            
            currentNode.Value = default(T);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);

            this.CheckEmptyNode(firstNode);
            this.CheckEmptyNode(secondNode);

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent == null)
            {
                this.SwapRoot(secondNode);
                return;
            }
            if (secondParent == null)
            {
                this.SwapRoot(firstNode);
                return;
            }

            firstNode.Parent = secondParent;
            secondNode.Parent = firstParent;

            int indexOfFirst = firstParent._children.IndexOf(firstNode);
            int indexOfSecond = secondParent._children.IndexOf(secondNode);

            firstParent._children[indexOfFirst] = secondNode;
            secondParent._children[indexOfSecond] = firstNode;
        }
        

        private void Dfs(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree.Children)
            {
                this.Dfs(child, result);
            }

            result.Add(tree.Value);
        }

        private void CheckEmptyNode(Tree<T> searchedNode)
        {
            if (searchedNode == null)
            {
                throw new ArgumentNullException("Tree not found!");
            }
        }

        private Tree<T> FindBfs(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> treeToCheck = queue.Dequeue();

                if (treeToCheck.Value.Equals(parentKey))
                {
                    return treeToCheck;
                }

                foreach (var child in treeToCheck.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void SwapRoot(Tree<T> currentNode)
        {
            this.Value = currentNode.Value;
            this._children.Clear();

            foreach (var child in currentNode.Children)
            {
                this._children.Add(child);
            }
        }
    }
}
