using System;
using Xunit;
using library.Structures;

namespace library.testing
{
    public class StackTest
    {

        static string[] titles = {
            "Harry Potter and the Philosopher's Stone.",
            "Harry Potter and the Chamber of Secrets.",
            "Harry Potter and the Prisoner of Azkaban.",
            "Harry Potter and the Goblet of Fire.",
            "Harry Potter and the Order of the Phoenix.",
            "Harry Potter and the Half-Blood Prince.",
            "Harry Potter and the Deathly Hallows."
        };

        static Stack<string> populatedStack;

        [Fact]
        public void TestingToString()
        {
            Stack<int> myStack = new Stack<int>();
            Assert.Equal(@"Stack: Size: 0
Items: System.Collections.Generic.List`1[System.Int32]", myStack.ToString(), false, true);
        }

        [Fact]
        public void TestingThatStackStartsEmpty() {
            Stack<char> myCharStack = new Stack<char>();
            Assert.Equal(0, myCharStack.Size);
        }

        [Fact]
        public void TestingPopOnEmpty() {
            try
            {
                new Stack<string>().Pop();
            }
            catch (System.Exception exception)
            {
                Assert.Equal("System.ArgumentOutOfRangeException", exception.GetType().FullName);
            }
        }

        [Fact]
        public void TestingPeekOnEmpty() {
            try
            {
                new Stack<string>().Peek();
            }
            catch (System.Exception exception)
            {
                Assert.Equal("System.ArgumentOutOfRangeException", exception.GetType().FullName);
            }
        }

        [Fact]
        public void TestingPopulatingStack() {
            StackTest.populatedStack = new Stack<string>();
            foreach (string title in StackTest.titles)
            {
                populatedStack.Push(title);
            }

            Assert.Equal(StackTest.titles.Length, populatedStack.Size);
        }

        [Fact]
        public void TestingAccess() {
            Assert.Equal(StackTest.titles[StackTest.titles.Length - 1], populatedStack.Pop());
            Assert.Equal(StackTest.titles.Length - 1, populatedStack.Size);

            Assert.Equal(populatedStack.Peek(), StackTest.titles[StackTest.titles.Length - 2]);
            Assert.Equal(StackTest.titles.Length - 1, populatedStack.Size);
        }
    }
}
