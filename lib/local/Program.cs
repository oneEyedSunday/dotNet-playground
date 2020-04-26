using System;
using library.Structures;

namespace local
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> numbers = new Stack<int>();
            Console.WriteLine("Size of stack is {0}", numbers.Size);
            numbers.Push(42);
            numbers.Push(0);
            Console.WriteLine("Size of stack is {0}, Top is {1}", numbers.Size, numbers.Peek());
        }
    }
}
