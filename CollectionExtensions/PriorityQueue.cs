using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using CollectionExtensions.Properties;

namespace CollectionExtensions
{
    /// <summary>
    /// Stores and returns items in the order of their priority.
    /// </summary>
    /// <typeparam name="T">The type of the items in the queue.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(PriorityQueueDebugView<>))]
    public sealed class PriorityQueue<T> : IEnumerable<T>, ICollection
    {
        private readonly Heap _heap;
        private int _version;

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        public PriorityQueue()
        {
            _heap = new Heap(Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        /// <param name="capacity">The initial capacity of the queue.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The capacity is negative.</exception>
        public PriorityQueue(int capacity)
        {
            _heap = new Heap(capacity, Comparer<T>.Default.Compare);
        }

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        /// <param name="comparison">The comparison function to use to prioritize the items.</param>
        /// <exception cref="System.ArgumentNullException">The comparison function is null.</exception>
        public PriorityQueue(Func<T, T, int> comparison)
        {
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            _heap = new Heap(comparison);
        }

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        /// <param name="comparer">The comparer to use to prioritize the items.</param>
        /// <exception cref="System.ArgumentNullException">The comparer function is null.</exception>
        public PriorityQueue(IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparison");
            }
            _heap = new Heap(comparer.Compare);
        }

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        /// <param name="capacity">The initial capacity of the queue.</param>
        /// <param name="comparison">The comparison function to use to prioritize the items.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The capacity is negative.</exception>
        /// <exception cref="System.ArgumentNullException">The comparison function is null.</exception>
        public PriorityQueue(int capacity, Func<T, T, int> comparison)
        {
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            _heap = new Heap(capacity, comparison);
        }

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        /// <param name="capacity">The initial capacity of the queue.</param>
        /// <param name="comparer">The comparer to use to determine priority.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The capacity is negative.</exception>
        /// <exception cref="System.ArgumentNullException">The comparer function is null.</exception>
        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparison");
            }
            _heap = new Heap(capacity, comparer.Compare);
        }

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        /// <param name="collection">The items to initially populate the queue with.</param>
        /// <exception cref="System.ArgumentNullException">The collection is null.</exception>
        public PriorityQueue(IEnumerable<T> collection)
        {
            _heap = new Heap(Comparer<T>.Default.Compare, collection);
        }

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        /// <param name="comparison">The comparison function to use to determine priority.</param>
        /// <param name="collection">The items to initially populate the queue with.</param>
        /// <exception cref="System.ArgumentNullException">The comparison function is null.</exception>
        /// <exception cref="System.ArgumentNullException">The collection is null.</exception>
        public PriorityQueue(Func<T, T, int> comparison, IEnumerable<T> collection)
        {
            if (comparison == null)
            {
                throw new ArgumentNullException("comparison");
            }
            _heap = new Heap(comparison, collection);
        }

        /// <summary>
        /// Initializes a new instance of a PriorityQueue.
        /// </summary>
        /// <param name="comparer">The comparer to use to determine priority.</param>
        /// <param name="collection">The items to initially populate the queue with.</param>
        /// <exception cref="System.ArgumentNullException">The comparer is null.</exception>
        /// <exception cref="System.ArgumentNullException">The collection is null.</exception>
        public PriorityQueue(IComparer<T> comparer, IEnumerable<T> collection)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            _heap = new Heap(comparer.Compare, collection);
        }

        /// <summary>
        /// Adds the given item to the queue.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Enqueue(T item)
        {
            _heap.Enqueue(item);
            ++_version;
        }

        /// <summary>
        /// Gets the highest priority item from the queue.
        /// </summary>
        public T Peek()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException(Resources.TopEmptyPriorityQueue);
            }
            return _heap.Top;
        }

        /// <summary>
        /// Removes the highest priority item from the queue.
        /// </summary>
        /// <returns>The highest priority item in the queue.</returns>
        public T Dequeue()
        {
            if (_heap.Count == 0)
            {
                throw new InvalidOperationException(Resources.RemoveEmptyPriorityQueue);
            }
            T top = _heap.Dequeue();
            ++_version;
            return top;
        }

        /// <summary>
        /// Removes all of the items from the queue.
        /// </summary>
        public void Clear()
        {
            _heap.Clear();
            ++_version;
        }

        /// <summary>
        /// Gets the number of items in the queue.
        /// </summary>
        public int Count
        {
            get { return _heap.Count; }
        }

        /// <summary>
        /// Determines whether the given item appears in the queue.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>True if the item is in the queue.</returns>
        /// <remarks>This searches for the value by equality, not using the priority comparison.</remarks>
        public bool Contains(T item)
        {
            return _heap.Contains(item);
        }

        /// <summary>
        /// Copies the items in the queue into the array,
        /// removing the items from the queue.
        /// </summary>
        /// <param name="array">The array to copy the items to.</param>
        /// <param name="arrayIndex">The index into the array to start copying.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("index", arrayIndex, Resources.IndexOutOfRange);
            }
            if (_heap.Count > array.Length - arrayIndex)
            {
                throw new ArgumentException(Resources.IndexOutOfRange, "index");
            }
            copyTo(array, arrayIndex);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            if (_heap.Count > array.Length - index)
            {
                throw new ArgumentException(Resources.IndexOutOfRange, "index");
            }
            T[] values = array as T[];
            if (values == null)
            {
                throw new ArgumentException(Resources.ArrayWrongType);
            }
            copyTo(values, index);
        }

        private void copyTo(T[] array, int index)
        {
            Heap copy = _heap.Clone();
            while (copy.Count != 0)
            {
                array[index] = copy.Dequeue();
                ++index;
            }
        }

        /// <summary>
        /// Creates an array containing the items that were in the queue.
        /// </summary>
        /// <returns>The array of the items that were in the queue.</returns>
        public T[] ToArray()
        {
            T[] array = new T[_heap.Count];
            copyTo(array, 0);
            return array;
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// Gets an enumerator that returns the items from the queue
        /// in order of the highest priority.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int version = _version;
            Heap copy = _heap.Clone();
            return getEnumerator(copy, version).GetEnumerator();
        }

        private IEnumerable<T> getEnumerator(Heap heap, int version)
        {
            if (_version != version)
            {
                throw new InvalidOperationException(Resources.ListChanged);
            }
            while (heap.Count != 0)
            {
                yield return heap.Dequeue();
                if (_version != version)
                {
                    throw new InvalidOperationException(Resources.ListChanged);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private sealed class Heap
        {
            private readonly List<T> _list;
            private readonly Func<T, T, int> _comparison;

            public Heap(Func<T, T, int> comparison)
            {
                _list = new List<T>();
                _comparison = comparison;
            }

            public Heap(int capacity, Func<T, T, int> comparison)
            {
                _list = new List<T>(capacity);
                _comparison = comparison;
            }

            public Heap(Func<T, T, int> comparison, IEnumerable<T> collection)
            {
                _list = new List<T>(collection);
                _comparison = comparison;
                Sublist.makeHeap<List<T>, T>(_list, 0, _list.Count, _comparison);
            }

            private Heap(Heap other)
            {
                _list = new List<T>(other._list);
                _comparison = other._comparison;
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public void Enqueue(T item)
            {
                _list.Add(item);
                Sublist.heapAdd<List<T>, T>(_list, 0, _list.Count, _comparison);
            }

            public bool Contains(T item)
            {
                return _list.Contains(item);
            }

            public T Top
            {
                get { return _list[0]; }
            }

            public T Dequeue()
            {
                Sublist.heapRemove<List<T>, T>(_list, 0, _list.Count, _comparison);
                int lastIndex = _list.Count - 1;
                T top = _list[lastIndex];
                _list.RemoveAt(lastIndex);
                return top;
            }

            public void Clear()
            {
                _list.Clear();
            }

            public Heap Clone()
            {
                return new Heap(this);
            }
        }
    }
}
