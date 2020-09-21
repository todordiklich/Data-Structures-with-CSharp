namespace _01.BinaryTree
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            StringBuilder result = new StringBuilder();

            this.PreOrderIndentDfs(this, result, indent);

            return result.ToString();
        }


        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();

            this.InOrderSearch(list, this);

            return list;
        }


        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();

            this.PostOrderSearch(list, this);

            return list;
        }


        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();

            this.PreOrderSearch(list, this);

            return list;
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.ForEachInOrder(action);
            }

            action.Invoke(this.Value);

            if (this.RightChild != null)
            {
                this.RightChild.ForEachInOrder(action);
            }
        }

        // private Methods
        private void PreOrderIndentDfs(IAbstractBinaryTree<T> tree, StringBuilder result, int indent)
        {
            result.AppendLine(new string(' ', indent) + tree.Value);

            if (tree.LeftChild != null)
            {
                this.PreOrderIndentDfs(tree.LeftChild, result, indent + 2);
            }
            if (tree.RightChild != null)
            {
                this.PreOrderIndentDfs(tree.RightChild, result, indent + 2);
            }
        }

        private void InOrderSearch(List<IAbstractBinaryTree<T>> list, IAbstractBinaryTree<T> tree)
        {
            if (tree != null)
            {
                this.InOrderSearch(list, tree.LeftChild);
                list.Add(tree);
                this.InOrderSearch(list, tree.RightChild);
            }
        }

        private void PostOrderSearch(List<IAbstractBinaryTree<T>> list, IAbstractBinaryTree<T> tree)
        {
            if (tree != null)
            {
                this.PostOrderSearch(list, tree.LeftChild);
                this.PostOrderSearch(list, tree.RightChild);

                list.Add(tree);
            }
        }

        private void PreOrderSearch(List<IAbstractBinaryTree<T>> list, IAbstractBinaryTree<T> tree)
        {

            if (tree != null)
            {
                list.Add(tree);
                this.PreOrderSearch(list, tree.LeftChild);
                this.PreOrderSearch(list, tree.RightChild);
            }
        }
    }
}
