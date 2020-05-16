using Xunit;
using library.Structures;

namespace library.testing
{
    public class QueueTest
    {

        [Fact]
        public void TestThatEnQueuesCorrectly()
        {
            var stringQueue = new Queue<string>();
            stringQueue.EnQueue("Ali");
            stringQueue.EnQueue("Simbi");
            stringQueue.EnQueue("Bala");
            Assert.Equal(3, stringQueue.Size);
            Assert.False(stringQueue.isEmpty);
        }

        [Fact]
        public void TestThatDeQueuesCorectly()
        {
            var stringQueue = new Queue<string>();
            stringQueue.EnQueue("Ali");
            stringQueue.EnQueue("Simbi");
            stringQueue.EnQueue("Bala");
            string first = stringQueue.DeQueue();
            Assert.Equal("Ali", first);
            Assert.Equal(2, stringQueue.Size);
        }

        [Fact]
        public void TestPeek()
        {
            var stringQueue = new Queue<string>();
            stringQueue.EnQueue("Ali");
            stringQueue.EnQueue("Simbi");
            stringQueue.EnQueue("Bala");
            string first = stringQueue.Peek();
            Assert.Equal("Ali", first);
            Assert.Equal(3, stringQueue.Size);
        }

        [Fact]
        public void TestStringRepr()
        {
            var stringQueue = new Queue<string>();
            stringQueue.EnQueue("Ali");
            stringQueue.EnQueue("Simbi");
            stringQueue.EnQueue("Bala");
            Assert.Equal("Queue: 3 String Item(s)", stringQueue.ToString());
        }
    }
}
