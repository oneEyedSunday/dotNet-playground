using Xunit;
using System;

using library.Structures;

namespace library.testing
{
    public class MapTest
    {
        public MapTest()
        {
        }

        [Fact]
        public void TestStringRep()
        {
            var myMap = new Map<string, string>();
            Assert.True(myMap.IsEmpty);
            Assert.Equal("Map<String, String>: 0 Item Pair(s)", myMap.ToString());
        }

        [Fact]
        public void TestBasicFunc()
        {
            var myMap = new Map<string, short>();

            myMap.Set("Cristiano Ronaldo", 7);
            myMap.Set("Wayne Rooney", 10);
            myMap.Set("Arjen Robben", 10);
            myMap.Set("Claude Makelele", 4);

            Assert.Equal(4, myMap.Size);

            Assert.True(myMap.Has("Wayne Rooney"));

            Assert.Equal(4, myMap.Get("Claude Makelele"));

            myMap.Delete("Arjen Robben");
            Assert.Equal(3, myMap.Size);
            Assert.False(myMap.Has("Arjen Robben"));


            myMap.Set("Arjen Robben", 11);
            Assert.True(myMap.Has("Arjen Robben"));
        }

        [Fact]
        public void TestEnumerators()
        {
            var myMap = new Map<string, short>();

            myMap.Set("Cristiano Ronaldo", 7);
            myMap.Set("Wayne Rooney", 10);
            myMap.Set("Arjen Robben", 10);
            myMap.Set("Claude Makelele", 4);

            Assert.Equal(4, myMap.Keys().Count);
            foreach (string key in myMap.Keys())
            {
                Assert.IsType<String>(key);
            }

            Assert.Equal(4, myMap.Values().Count);
            foreach (short jerseyNumber in myMap.Values())
            {
                Assert.IsType<short>(jerseyNumber);
            } 
        }
    }
}
