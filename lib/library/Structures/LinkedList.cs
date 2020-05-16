using System;

public class Node<T> where T : class
{
    public T Value { get; }
    public Node<T> Next { get; set; }

    public Node(T value)
    {
        Value = value;
        Next = null;
    }
}


namespace library.Structures
{
    public class LinkedList<T> where T: class
    {
        private Node<T> _head { get; set; } = null;

        public Node<T> head => _head;

        public int Size { get; private set; } = 0;

        public bool IsEmpty => Size == 0;

        public LinkedList()
        {
        }

        public void Add(T element)
        {
            Node<T> node = new Node<T>(element);

            var currentNode = head;

            if (currentNode == null)
            {
                _head = node;
            } else
            {
                while (currentNode.Next != null)
                {
                    var previousNode = currentNode;
                    currentNode = currentNode.Next;
                }

                currentNode.Next = node;
            }

            Size++;
        }

        public void Remove(T element)
        {
            if (IsEmpty) return;

            var currentNode = _head;

            if (currentNode.Value == element)
            {
                currentNode = currentNode.Next;
                _head = currentNode;
                Size--;
            } else
            {
                while(currentNode.Next != null)
                {
                    var previousNode = currentNode;
                    currentNode = currentNode.Next;
                    if (currentNode.Value == element)
                    {
                        previousNode.Next = currentNode.Next;
                        Size--;
                        return;
                    }

                }
            }
        }

        public int Index(T element)
        {
            var currentNode = _head;
            int index = -1;

            while (currentNode != null)
            {
                index++;
                if (currentNode.Value == element) return index;
                currentNode = currentNode.Next;
            }

            return index;
        }

        public T GetAtIndex(int index)
        {
            var currentNode = _head;
            int links = 0;

            if (index < 0 || index > Size) return null;

            while (links < index)
            {
                links++;
                currentNode = currentNode.Next;
            }

            return currentNode.Value;
        }

        public void AddAtIndex(T element, int index)
        {

            if (index < 0 || index > Size) return;

            Node<T> node = new Node<T>(element);

            var currentNode = _head;
            int linkLength = 0;

            if (index == 0)
            {
                _head = node;
                _head.Next = currentNode;
            } else
            {
                Node<T> previousNode = new Node<T>(null);
                while (linkLength < index)
                {
                    linkLength++;
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }

                previousNode.Next = node;
                node.Next = currentNode;
            }

            Size++;

        }

        public void RemoveAtIndex(int index)
        {
            if (index < 0 || index > Size) return;

            var currentNode = _head;
            int links = 0;

            if (index == 0)
            {
                currentNode = currentNode.Next;
                _head = currentNode;
            } else
            {
                var previousNode = new Node<T>(null);
                while(links < index)
                {
                    links++;
                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }

                previousNode.Next = currentNode.Next;
            }

            Size--;
        }
    }
}
