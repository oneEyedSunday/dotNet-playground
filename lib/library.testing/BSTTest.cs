using Xunit;
using library.Structures;

namespace library.testing
{
    public class BSTTest
    {
        [Fact]
        public void TestBST()
        {
            var bst = new BST<int>();
            bst.Add(10);
            bst.Add(11);
            bst.Add(9);


            Assert.Equal(new int[] { 10, 9, 11 }, bst.LevelOrder());

            var tree = new TreeNode<int>(3);
            tree.Right = new TreeNode<int>(4);
            tree.Left = new TreeNode<int>(1);

            tree.Left.Left = new TreeNode<int>(2);
            tree.Left.Right = new TreeNode<int>(5);

            Assert.False(BST<int>.Test(tree));

            Assert.True(BST<int>.Test(bst.GetRoot));
        }

        [Fact]
        public void TestOrder()
        {
            var tree = new TreeNode<char>('5');
            tree.Left = new TreeNode<char>('2');
            tree.Left.Left = new TreeNode<char>('1');
            tree.Left.Right = new TreeNode<char>('3');

            tree.Right = new TreeNode<char>('g');
            tree.Right.Left = new TreeNode<char>('f');
            tree.Right.Right = new TreeNode<char>('j');
            Assert.True(BST<char>.Test(tree));
        }
    }
}
