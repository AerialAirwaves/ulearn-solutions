using System;

namespace BinaryTrees
{
    internal class TreeNode<T>
        where T : IComparable
    {
        public T Key;
        public TreeNode<T> Left;
        public TreeNode<T> Right;
        
        public TreeNode(T key) => Key = key;
    }

    public class BinaryTree<T>
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
                destination = key.CompareTo(destination.Key) < 0 ? destination.Left : destination.Right;
            }
            
            ConnectLeaf(parent, new TreeNode<T>(key));
        }
        
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