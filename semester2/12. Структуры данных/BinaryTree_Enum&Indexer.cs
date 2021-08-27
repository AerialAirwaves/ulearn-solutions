using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTrees
{
    internal class TreeNode<T>
        where T : IComparable
    {
        public int SubtreeSize = 1;
        public T Key;
        public TreeNode<T> Left;
        public TreeNode<T> Right;
        
        public TreeNode(T key) => Key = key;
    }

    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable
    {
        private TreeNode<T> rootNode;

        public bool Contains(T key)
        {
            var node = rootNode;
            while (node != null)
            {
                if (key.Equals(node.Key))
                    return true;

                node = key.CompareTo(node.Key) < 0 ? node.Left : node.Right;
            }
            return false;
        }

        public void Add(T key)
        {
            var parent = default(TreeNode<T>);
            var destination = rootNode;
            
            while (destination != null)
            {
                parent = destination;
                destination.SubtreeSize++;
                destination = key.CompareTo(destination.Key) < 0 ? destination.Left : destination.Right;
            }
            
            ConnectLeaf(parent, new TreeNode<T>(key));
        }

        public T this[int index]
        {
            get
            {
                var node = rootNode;
                while (node != null)
                {
                    var leftSize = node.Left?.SubtreeSize ?? 0;
                    if (index == leftSize)
                        return node.Key;

                    if (index < leftSize)
                    {
                        node = node.Left;
                        continue;
                    }

                    node = node.Right;
                    index -= leftSize + 1;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerable<T> GetIEnumerable(TreeNode<T> node)
            {
                if (node != null)
                    foreach (var e in GetIEnumerable(node.Left)
                                        .Append(node.Key)
                                        .Concat(GetIEnumerable(node.Right)))
                        yield return e;
            }
            return GetIEnumerable(rootNode).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void ConnectLeaf(TreeNode<T> parent, TreeNode<T> leaf)
        {
            if (parent == null)
                rootNode = leaf;
            else
                if (leaf.Key.CompareTo(parent.Key) < 0)
                    parent.Left = leaf;
                else
                    parent.Right = leaf;
        }
    }
}