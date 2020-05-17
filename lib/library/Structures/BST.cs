using System;
using System.Collections.Generic;

public class TreeNode<T> where T : IComparable<T>, IEquatable<T>
{
    public T Value { get; set; }
    public TreeNode<T> Left { get; set; }
    public TreeNode<T> Right { get; set; }

    public TreeNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

namespace library.Structures
{
    public class BST<T> where T: IComparable<T>, IEquatable<T>
    {
        private TreeNode<T> root { get; set; } = null;

        public TreeNode<T> GetRoot { get => root; }


        public void Add(T data)
        {
            if (root == null)
            {
                root = new TreeNode<T>(data);
                return;
            }

            void searchTree(TreeNode<T> node)
            {
                if (data.CompareTo(node.Value) < 0)
                {
                    if (node.Left == null)
                    {
                        node.Left = new TreeNode<T>(data);
                        return;
                    }

                    node = node.Left;
                    searchTree(node);

                }
                else if (data.CompareTo(node.Value) > 0)
                {
                    if (node.Right == null)
                    {
                        node.Right = new TreeNode<T>(data);
                        return;
                    }
                    node = node.Right;
                    searchTree(node);
                }
            }

            searchTree(root);

        }

        public TreeNode<T> Find(T data)
        {
            if (EqualityComparer<T>.Default.Equals(data, root.Value))
            {
                return new TreeNode<T>(data);
            } else
            {
                TreeNode<T> findNodeInTree(TreeNode<T> node)
                {
                    if (data.CompareTo(node.Value) < 0)
                    {
                        if (node.Left == null) return null;
                        if (EqualityComparer<T>.Default.Equals(node.Left.Value, data)) return node.Left;

                        node = node.Left;
                        return findNodeInTree(node);

                    } else if (data.CompareTo(node.Value) > 0)
                    {
                        if (node.Right == null) return null;
                        if (EqualityComparer<T>.Default.Equals(node.Right.Value, data)) return node.Right;
                        node = node.Right;
                        return findNodeInTree(node);
                    }
                    return findNodeInTree(root);
                }

                return findNodeInTree(root);
            }
        }

        public bool IsPresent(T data) => Find(data) != null;


        public void Remove(T element)
        {
            TreeNode<T> RemoveNode(TreeNode<T> node, T data)
            {
                if (node == null) return null;

                if (EqualityComparer<T>.Default.Equals(node.Value, data))
                {
                    if (node.Left == null && node.Right == null) return null;
                    if (node.Right == null) return node.Left;
                    if (node.Left == null) return node.Right;

                    var successor = node.Right;
                    while (successor.Left != null)
                    {
                        successor = successor.Left;
                    }

                    node.Value = successor.Value;

                    node.Right = RemoveNode(node, successor.Value);
                    return node.Right;
                } else if (data.CompareTo(node.Value) < 0)
                {
                    node.Left = RemoveNode(node.Left, data);
                    return node;
                } else
                {
                    node.Right = RemoveNode(node.Right, data);
                    return node;
                }
            }

            root = RemoveNode(root, element);
        }

        public int Height(TreeNode<T> node)
        {
            if (node == null) return 0;

            (int ldepth, int rdepth) = (Height(node.Left), Height(node.Right));

            return Math.Max(ldepth, rdepth) + 1;
        }

        public bool IsBalanced(TreeNode<T> node)
        {
            if (node == null) return true;

            (int lh, int rh) = (Height(node.Left), Height(node.Right));

            if (Math.Abs(lh - rh) <= 1 && IsBalanced(node.Left) && IsBalanced(node.Right)) return true;

            return false;
        }

        public T[] PreOrder()
        {
            if (root == null) return new T[] { };

            List<T> transversals = new List<T>();

            void TransversePreOrder(TreeNode<T> node)
            {
                transversals.Add(node.Value);
                if (node.Left != null) TransversePreOrder(node.Left);
                if (node.Right != null) TransversePreOrder(node.Right);

            }

            TransversePreOrder(root);
            return transversals.ToArray();
        }

        public T[] InOrder()
        {
            if (root == null) return new T[] { };

            List<T> transaversals = new List<T>();

            void TransverseInOrder(TreeNode<T> node)
            {
                if (node.Left != null) TransverseInOrder(node.Left);
                transaversals.Add(node.Value);
                if (node.Right != null) TransverseInOrder(node.Right);
            }

            TransverseInOrder(root);

            return transaversals.ToArray();
        }

        public T[] PostOrder()
        {
            if (root == null) return new T[] { };

            List<T> transaversals = new List<T>();

            void TransversePostOrder(TreeNode<T> node)
            {
                if (node.Left != null) TransversePostOrder(node.Left);
                if (node.Right != null) TransversePostOrder(node.Right);
                transaversals.Add(node.Value);
            }

            TransversePostOrder(root);

            return transaversals.ToArray();
        }

        public T[] LevelOrder()
        {
            var transversals = new List<T>();

            var queue = new Queue<TreeNode<T>>();

            if (root != null)
            {
                queue.EnQueue(root);
                while (!queue.isEmpty)
                {
                    var node = queue.DeQueue();
                    transversals.Add(node.Value);

                    if (node.Left != null) queue.EnQueue(node.Left);

                    if (node.Right != null) queue.EnQueue(node.Right);
                }
            }

            return transversals.ToArray();
        }

        public static bool Test(TreeNode<T> node)
        {
            bool TestNode(TreeNode<T> aNode)
            {
                if (aNode == null) return true;

                if (aNode.Left == null && aNode.Right == null) return true;

                bool test_l = false;
                bool test_r = false;

                if (aNode.Left == null) test_l = true;

                if (aNode.Right == null) test_r = true;

                if (aNode.Left != null && aNode.Left.Value.CompareTo(aNode.Value) < 0)
                {
                    test_l = TestNode(aNode.Left);
                }

                if (aNode.Right != null && aNode.Right.Value.CompareTo(aNode.Value) > 0)
                {
                    test_r = TestNode(aNode.Right);
                }

                return test_l && test_r;
            }

            return TestNode(node);
        }
    }
}
