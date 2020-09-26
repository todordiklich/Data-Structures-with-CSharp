namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.RightChild = rightChild;
            this.LeftChild = leftChild;

            if (this.RightChild != null)
            {
                this.RightChild.Parent = this;
            }
            if (this.LeftChild != null)
            {
                this.LeftChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            BinaryTree<T> firstTree = null;
            BinaryTree<T> secondTree = null;

            this.FindTreeDfs(this, first, ref firstTree);
            this.FindTreeDfs(this, second, ref secondTree);

            List<T> firstTreeParents = this.FindParents(firstTree);
            List<T> secondTreeParents = this.FindParents(secondTree);

            for (int i = 0; i < firstTreeParents.Count; i++)
            {
                for (int j = 0; j < secondTreeParents.Count; j++)
                {
                    if (firstTreeParents[i].Equals(secondTreeParents[j]))
                    {
                        return firstTreeParents[i];
                    }
                }
            }

            return default;
        }

        private List<T> FindParents(BinaryTree<T> current)
        {
            var result = new List<T>();

            while (current.Parent != null)
            {
                current = current.Parent;
                result.Add(current.Value);
            }

            return result;
        }

        private void FindTreeDfs(BinaryTree<T> current, T searchedValue, ref BinaryTree<T> searchedTree)
        {
            if (current != null)
            {
                if (current.Value.Equals(searchedValue))
                {
                    searchedTree =  current;
                    return;
                }

                this.FindTreeDfs(current.LeftChild, searchedValue, ref searchedTree);
                this.FindTreeDfs(current.RightChild, searchedValue, ref searchedTree);
            }
        }
    }
}
