using Xunit;

using library.Structures;

namespace library.testing
{
    public class LinkedListTest
    {
        [Fact]
        public void TestFunction()
        {
            var myList = new LinkedList<string>();

            myList.Add("A");
            myList.Add("B");
            myList.Add("F");

            Assert.Equal(2, myList.Index("F"));
            myList.Remove("F");
            Assert.Equal(2, myList.Size);
            Assert.Equal("B", myList.GetAtIndex(1));
        }
    }
}
