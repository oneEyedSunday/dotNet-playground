using System;

namespace library.Structures
{
    public class Queue<T>
    {
        private T[] _items;

        public int Size => _items.Length;

        public bool isEmpty => Size == 0;

        public Queue()
        {
            _items = new T[] { };
        }

        public void EnQueue(T item)
        {
            T[] newArray = new T[Size + 1];

            _items.CopyTo(newArray, 0);

            new T[] { item }.CopyTo(newArray, Size);

            _items = newArray;

        }

        public T DeQueue()
        {
            T[] newArray = new T[Size - 1];

            T first = _items[0];

            _items.AsSpan(1).ToArray().CopyTo(newArray, 0);

            _items = newArray;

            return first;
        }

        public T Peek()
        {
            return _items[0];
        }

        public override string ToString() => String.Format("Queue: {0} {1} Item(s)", Size, typeof(T).Name);
    }
}
