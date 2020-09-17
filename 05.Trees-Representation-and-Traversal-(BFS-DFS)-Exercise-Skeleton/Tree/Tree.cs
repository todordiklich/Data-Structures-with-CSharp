namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;
        private int deepestLevel = 0;

        public Tree(T key, params Tree<T>[] children)
        {
            this._children = new List<Tree<T>>();
            this.Key = key;

            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            StringBuilder result = new StringBuilder();

            this.OrderDfsForString(this, result, 0);

            return result.ToString().TrimEnd();
        }


        public Tree<T> GetDeepestLeftomostNode()
        {
            var list = new List<Tree<T>>() { this };
            this.SearchDfsDeepestLeftomstNode(this, 0, list);

            return list[0];
        }


        public List<T> GetLeafKeys()
        {
            var leafNodes = new List<T>();

            this.SeatchBfsLeafs(leafNodes);

            return leafNodes;
        }


        public List<T> GetMiddleKeys()
        {
            var middleNodes = new List<T>();

            this.SeatchBfsMiddleNodes(middleNodes);

            return middleNodes;
        }


        public List<T> GetLongestPath()
        {
            var current = this.GetDeepestLeftomostNode();
            var result = new Stack<T>();

            while (current != null)
            {
                result.Push(current.Key);
                current = current.Parent;
            }

            return result.ToList();
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();
            var path = new List<T>();

            this.SearchAllPathsWithGivenSum(this, result, path, sum);

            return result;
        }
        

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var result = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();
            int sumOfallNodes = 0;

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                int currentSum = Convert.ToInt32(currentNode.Key);
                sumOfallNodes += Convert.ToInt32(currentNode.Key);

                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                    currentSum += Convert.ToInt32(child.Key);
                }

                if (currentSum == sum)
                {
                    result.Add(currentNode);
                }
            }

            if (sumOfallNodes == sum)
            {
                result.Add(this);
            }

            return result;
        }

        private void OrderDfsForString(Tree<T> node, StringBuilder result, int depth)
        {
            result.AppendLine(new string(' ', depth) + node.Key);

            foreach (var child in node.Children)
            {
                this.OrderDfsForString(child, result, depth + 2);
            }
        }

        private void SeatchBfsLeafs(List<T> leafNodess)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);

                    if (child.Children.Count == 0)
                    {
                        leafNodess.Add(child.Key);
                    }
                }
            }
        }

        private void SeatchBfsMiddleNodes(List<T> middleNodes)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);

                    if (child.Children.Count > 0 && child.Parent != null)
                    {
                        middleNodes.Add(child.Key);
                    }
                }
            }
        }

        private void SearchDfsDeepestLeftomstNode(Tree<T> node, int level, List<Tree<T>> list)
        {
            foreach (var child in node.Children)
            {
                int currentLevel = level + 1;
                this.SearchDfsDeepestLeftomstNode(child, currentLevel, list);
            }

            if (level > this.deepestLevel)
            {
                this.deepestLevel = level;
                list[0] = node;
            }
        }

        private void SearchAllPathsWithGivenSum(Tree<T> current, List<List<T>> result, List<T> path, int sum)
        {
            path.Add(current.Key);

            foreach (var child in current.Children)
            {
                this.SearchAllPathsWithGivenSum(child, result, path, sum);
            }

            if (current.Children.Count == 0)
            {
                if (path.Sum(e => Convert.ToInt32(e)) == sum)
                {
                    var coppyPath = new List<T>(path);
                    result.Add(coppyPath);
                }
            }

            path.Remove(current.Key);
        }
    }
}
