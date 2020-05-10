using System;
using System.Collections.Generic;
using System.Linq;

namespace library.Structures
{
    public class Set<T>
    {
        private List<T> items { get; set; }

        public Set()
        {
            items = new List<T>();
        }

        public int Size
        {
            get => items.Count;
        }

        public bool isEmpty
        {
            get => Size == 0;
        }

        public T[] Values
        {
            get => items.ToArray();
        }

        public void Add(T item)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }

        public void Remove(T item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }
        }


        /// <summary>
        /// S U T
        /// </summary>
        public Set<T> Union(Set<T> otherSet)
        {
            Set<T> unionSet = new Set<T>();

            List<T> allValues = otherSet.Values.ToList();
            allValues.AddRange(Values);

            foreach (var item in allValues)
            {
                unionSet.Add(item);
            }

            return unionSet;
        }

        /// <summary>
        /// S - T
        /// </summary>
        public Set<T> Difference(Set<T> otherSet)
        {
            Set<T> diffSet = new Set<T>();

            List<T> otherValues = otherSet.Values.ToList<T>();

            foreach (T item in items)
            {
                if (!otherValues.Contains(item))
                {
                    diffSet.Add(item);
                }
            }

            return diffSet;
        }


        /// <summary>
        /// S N T
        /// </summary>
        public Set<T> Intersection(Set<T> otherSet)
        {
            Set<T> interSet = new Set<T>();

            List<T> otherValues = otherSet.Values.ToList<T>();

            foreach (T item in otherValues)
            {
                if (items.Contains(item))
                {
                    interSet.Add(item);
                }
            }

            return interSet;
        }


        public bool Subset(Set<T> testSet)
        {
            List<T> otherValues = testSet.Values.ToList<T>();

            bool result = true;

            foreach (T item in otherValues)
            {
                result &= items.Contains(item);
            }

            return result;
        }

        public override string ToString() => $"Set({Size}) {{}}";

    }
}