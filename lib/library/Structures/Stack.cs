using System;
using System.Collections.Generic;
namespace library.Structures
{
    public class Stack<T>
    {

        private List<T> items { get; set; }

        public int Size { 
            get => items.Count;
        }

        public bool isEmpty {
            get => Size == 0;
        }
        public Stack()
        {
            items = new List<T>();
        }

        public void Push(T _item) {
            items.Add(_item);
        }

        public T Pop() {
            T item = items[Size - 1];
            items.RemoveAt(Size - 1);
            return item;
        }

        public T Peek() => items[Size -1];

        public override string ToString() => $"Stack: Size: {Size}{Environment.NewLine}Items: {items}";
    }
}
