using Xunit;
using System;

using library.Structures;

namespace library.testing
{
    public class SetTest
    {
        [Fact]
        public void TestToString()
        {

        }


        [Fact]
        public void TestThatSetStartsEmpty()
        {
            Set<string> mySet = new Set<string>();
            Assert.Equal(0, mySet.Size);
        }

        [Fact]
        public void TestAdding()
        {
            Set<string> balonDorWinners = new Set<string>();

            balonDorWinners.Add("Kaka");
            balonDorWinners.Add("Cristiano Ronaldo");
            balonDorWinners.Add("Lionel Messi");
            balonDorWinners.Add("Luca Modric");
            balonDorWinners.Add("Cristiano Ronaldo");
            balonDorWinners.Add("Lionel Messi");

            Assert.Equal(4, balonDorWinners.Size);
        }

        [Fact]
        public void TestRemove()
        {
            Set<string> balonDorWinners = new Set<string>();

            balonDorWinners.Add("Kaka");
            balonDorWinners.Add("Wesley Sneijder");
            balonDorWinners.Add("Cristiano Ronaldo");

            balonDorWinners.Remove("Wesley Sneijder");

            Assert.Equal(2, balonDorWinners.Size);
        }


        [Fact]
        public void TestUnion()
        {
            Set<string> mySet = new Set<string>();

            mySet.Add("One");
            mySet.Add("Two");
            mySet.Add("One");


            Set<string> anotherSet = new Set<string>();

            anotherSet.Add("One");
            anotherSet.Add("Deux");
            anotherSet.Add("Trois");


            Assert.Equal(4, mySet.Union(anotherSet).Size);
        }

        [Fact]
        public void TestDifference()
        {
            Set<string> mySet = new Set<string>();

            mySet.Add("One");
            mySet.Add("Two");
            mySet.Add("One");


            Set<string> anotherSet = new Set<string>();

            anotherSet.Add("One");
            anotherSet.Add("Deux");
            anotherSet.Add("Trois");

            // S - T

            Assert.Equal(1, mySet.Difference(anotherSet).Size);
        }


        [Fact]
        public void TestIntersection()
        {
            Set<string> mySet = new Set<string>();

            mySet.Add("One");
            mySet.Add("Two");
            mySet.Add("One");


            Set<string> anotherSet = new Set<string>();

            anotherSet.Add("One");
            anotherSet.Add("Deux");
            anotherSet.Add("Trois");


            Assert.Equal(1, mySet.Intersection(anotherSet).Size);
        }

        [Fact]
        public void TestSubset()
        {
            Set<string> balonDorWinners = new Set<string>();

            balonDorWinners.Add("Kaka");
            balonDorWinners.Add("Cristiano Ronaldo");
            balonDorWinners.Add("Lionel Messi");
            balonDorWinners.Add("Luca Modric");
            balonDorWinners.Add("Cristiano Ronaldo");
            balonDorWinners.Add("Lionel Messi");

            Assert.Equal(4, balonDorWinners.Size);

            Set<string> somePlayers = new Set<string>();

            //somePlaye


            Assert.True(balonDorWinners.Subset(somePlayers));
        }
    }
}