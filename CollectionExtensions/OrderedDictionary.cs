﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using CollectionExtensions.Properties;

namespace CollectionExtensions
{
    /// <summary>
    /// Represents a dictionary that tracks the order that items were added.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
    /// <remarks>
    /// This dictionary makes it possible to get the index of a key and a key based on an index.
    /// It can be costly to find the index of a key because it must be searched for linearly.
    /// It can be costly to insert a key/value pair because other key's indexes must be adjusted.
    /// It can be costly to remove a key/value pair because other keys' indexes must be adjusted.
    /// </remarks>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(OrderedDictionaryDebugView<,>))]
    public sealed class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IList<KeyValuePair<TKey, TValue>>
    {
        private readonly Dictionary<TKey, TValue> dictionary;
        private readonly List<TKey> keys;

        /// <summary>
        /// Initializes a new instance of an OrderedDictionary.
        /// </summary>
        public OrderedDictionary()
        {
            dictionary = new Dictionary<TKey, TValue>();
            keys = new List<TKey>();
        }

        /// <summary>
        /// Initializes a new instance of an OrderedDictionary.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The capacity is less than zero.</exception>
        public OrderedDictionary(int capacity)
        {
            dictionary = new Dictionary<TKey, TValue>(capacity);
            keys = new List<TKey>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of an OrderedDictionary.
        /// </summary>
        /// <param name="comparer">The equality comparer to use to compare keys.</param>
        public OrderedDictionary(IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, TValue>(comparer);
            keys = new List<TKey>();
        }

        /// <summary>
        /// Initializes a new instance of an OrderedDictionary.
        /// </summary>
        /// <param name="capacity">The initial capacity of the dictionary.</param>
        /// <param name="comparer">The equality comparer to use to compare keys.</param>
        public OrderedDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
            keys = new List<TKey>(capacity);
        }

        /// <summary>
        /// Gets the equality comparer used to compare keys.
        /// </summary>
        public IEqualityComparer<TKey> Comparer
        {
            get { return dictionary.Comparer; }
        }

        /// <summary>
        /// Adds the given key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key to add to the dictionary.</param>
        /// <param name="value">The value to associated with the key.</param>
        /// <exception cref="System.ArgumentException">The given key already exists in the dictionary.</exception>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        public void Add(TKey key, TValue value)
        {
            dictionary.Add(key, value);
            keys.Add(key);
        }

        /// <summary>
        /// Inserts the given key/value pair at the specified index.
        /// </summary>
        /// <param name="index">The index to insert the key/value pair.</param>
        /// <param name="key">The key to insert.</param>
        /// <param name="value">The value to insert.</param>
        /// <exception cref="System.ArgumentException">The given key already exists in the dictionary.</exception>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- larger than the size of the dictionary.</exception>
        public void Insert(int index, TKey key, TValue value)
        {
            if (index < 0 || index > dictionary.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            dictionary.Add(key, value);
            keys.Insert(index, key);
        }

        /// <summary>
        /// Determines whether the given key exists in the dictionary.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>True if the key exists in the dictionary; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the key at the given index.
        /// </summary>
        /// <param name="index">The index of the key to get.</param>
        /// <returns>The key at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- larger than the number of keys.</exception>
        public TKey GetKey(int index)
        {
            return keys[index];
        }

        /// <summary>
        /// Gets the index of the given key.
        /// </summary>
        /// <param name="key">The key to get the index of.</param>
        /// <returns>The index of the key in the dictionary -or- -1 if the key is not found.</returns>
        /// <remarks>The operation runs in O(n).</remarks>
        public int IndexOf(TKey key)
        {
            if (!dictionary.ContainsKey(key))
            {
                return -1;
            }
            return keys.FindIndex(item => dictionary.Comparer.Equals(item, key));
        }

        /// <summary>
        /// Gets the keys in the dictionary in the order they were added.
        /// </summary>
        public KeyCollection Keys
        {
            get { return new KeyCollection(this); }
        }

        /// <summary>
        /// Removes the key/value pair with the given key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the pair to remove.</param>
        /// <returns>True if the key was found and the pair removed; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        /// <remarks>This operation runs in O(n).</remarks>
        public bool Remove(TKey key)
        {
            if (dictionary.Remove(key))
            {
                int index = keys.FindIndex(item => dictionary.Comparer.Equals(item, key));
                keys.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the key/value pair at the given index.
        /// </summary>
        /// <param name="index">The index of the key/value pair to remove.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- larger than the size of the dictionary.</exception>
        /// <remarks>This operation runs in O(n).</remarks>
        public void RemoveAt(int index)
        {
            TKey key = keys[index];
            dictionary.Remove(key);
            keys.RemoveAt(index);
        }

        /// <summary>
        /// Tries to get the value associated with the given key. If the key is not found,
        /// default(TValue) value is stored in the value.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="value">The value used to hold the results.</param>
        /// <returns>True if the key was found; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the values in the dictionary.
        /// </summary>
        public ValueCollection Values
        {
            get { return new ValueCollection(this); }
        }

        /// <summary>
        /// Gets or sets the value at the given index.
        /// </summary>
        /// <param name="index">The index of the value to get.</param>
        /// <returns>The value at the given index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The index is negative -or- beyond the length of the dictionary.</exception>
        public TValue this[int index]
        {
            get
            {
                return dictionary[keys[index]];
            }
            set
            {
                dictionary[keys[index]] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the given key.
        /// </summary>
        /// <param name="key">The key to get the associated value by or to associate with the value.</param>
        /// <returns>The value associated with the given key.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The key is not in the dictionary.</exception>
        public TValue this[TKey key]
        {
            get
            {
                return dictionary[key];
            }
            set
            {
                if (!dictionary.ContainsKey(key))
                {
                    keys.Add(key);
                }
                dictionary[key] = value;
            }
        }

        /// <summary>
        /// Removes all key/value pairs from the dictionary.
        /// </summary>
        public void Clear()
        {
            dictionary.Clear();
            keys.Clear();
        }

        /// <summary>
        /// Gets the number of key/value pairs in the dictionary.
        /// </summary>
        public int Count
        {
            get { return dictionary.Count; }
        }

        /// <summary>
        /// Gets the key/value pairs in the dictionary in the order they were added.
        /// </summary>
        /// <returns>An enumerator over the key/value pairs in the dictionary.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (TKey key in keys)
            {
                yield return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
            }
        }

        int IList<KeyValuePair<TKey, TValue>>.IndexOf(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            if (!dictionary.TryGetValue(item.Key, out value))
            {
                return -1;
            }
            if (!Equals(item.Value, value))
            {
                return -1;
            }
            int index = keys.FindIndex(key => dictionary.Comparer.Equals(item.Key, key));
            return index;

        }

        void IList<KeyValuePair<TKey, TValue>>.Insert(int index, KeyValuePair<TKey, TValue> item)
        {
            if (index < 0 || index > dictionary.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, Resources.IndexOutOfRange);
            }
            dictionary.Add(item.Key, item.Value);
            keys.Insert(index, item.Key);
        }

        KeyValuePair<TKey, TValue> IList<KeyValuePair<TKey, TValue>>.this[int index]
        {
            get
            {
                TKey key = keys[index];
                TValue value = dictionary[key];
                return new KeyValuePair<TKey, TValue>(key, value);
            }
            set
            {
                TKey key = keys[index];
                if (dictionary.Comparer.Equals(key, value.Key))
                {
                    dictionary[value.Key] = value.Value;
                }
                else
                {
                    dictionary.Add(value.Key, value.Value);
                    dictionary.Remove(key);
                    keys[index] = value.Key;
                }
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return Keys; }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return Values; }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            dictionary.Add(item.Key, item.Value);
            keys.Add(item.Key);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            if (!dictionary.TryGetValue(item.Key, out value))
            {
                return false;
            }
            return Equals(value, item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0));
            }
            if (dictionary.Count > array.Length - arrayIndex)
            {
                throw new ArgumentException(Resources.ArrayTooSmall, "array");
            }
            foreach (TKey key in keys)
            {
                TValue value = dictionary[key];
                array[arrayIndex] = new KeyValuePair<TKey, TValue>(key, value);
                ++arrayIndex;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            if (!dictionary.TryGetValue(item.Key, out value))
            {
                return false;
            }
            if (!Equals(item.Value, value))
            {
                return false;
            }
            // O(n)
            dictionary.Remove(item.Key);
            int index = keys.FindIndex(key => dictionary.Comparer.Equals(item.Key, key));
            keys.RemoveAt(index);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Wraps the keys in an OrderDictionary.
        /// </summary>
        public sealed class KeyCollection : ICollection<TKey>
        {
            private readonly OrderedDictionary<TKey, TValue> parent;

            /// <summary>
            /// Initializes a new instance of a KeyCollection.
            /// </summary>
            /// <param name="dictionary">The OrderedDictionary whose keys to wrap.</param>
            /// <exception cref="System.ArgumentNullException">The dictionary is null.</exception>
            public KeyCollection(OrderedDictionary<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                {
                    throw new ArgumentNullException("dictionary");
                }
                parent = dictionary;
            }

            /// <summary>
            /// Copies the keys from the OrderedDictionary to the given array, starting at the given index.
            /// </summary>
            /// <param name="array">The array to copy the keys to.</param>
            /// <param name="arrayIndex">The index into the array to start copying the keys.</param>
            /// <exception cref="System.ArgumentNullException">The array is null.</exception>
            /// <exception cref="System.ArgumentOutOfRangeException">The arrayIndex is negative.</exception>
            /// <exception cref="System.ArgumentException">The array, starting at the given index, is not large enough to contain all the keys.</exception>
            public void CopyTo(TKey[] array, int arrayIndex)
            {
                parent.keys.CopyTo(array, arrayIndex);
            }

            /// <summary>
            /// Gets the number of keys in the OrderedDictionary.
            /// </summary>
            public int Count
            {
                get { return parent.dictionary.Count; }
            }

            /// <summary>
            /// Gets an enumerator over the keys in the OrderedDictionary.
            /// </summary>
            /// <returns>The enumerator.</returns>
            public IEnumerator<TKey> GetEnumerator()
            {
                return parent.keys.GetEnumerator();
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            bool ICollection<TKey>.Contains(TKey item)
            {
                return parent.dictionary.ContainsKey(item);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            void ICollection<TKey>.Add(TKey item)
            {
                throw new NotSupportedException(Resources.EditReadonlyList);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            void ICollection<TKey>.Clear()
            {
                throw new NotSupportedException(Resources.EditReadonlyList);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            bool ICollection<TKey>.IsReadOnly
            {
                get { return true; }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            bool ICollection<TKey>.Remove(TKey item)
            {
                throw new NotSupportedException(Resources.EditReadonlyList);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        /// <summary>
        /// Wraps the keys in an OrderDictionary.
        /// </summary>
        public sealed class ValueCollection : ICollection<TValue>
        {
            private readonly OrderedDictionary<TKey, TValue> parent;

            /// <summary>
            /// Initializes a new instance of a ValueCollection.
            /// </summary>
            /// <param name="dictionary">The OrderedDictionary whose keys to wrap.</param>
            /// <exception cref="System.ArgumentNullException">The dictionary is null.</exception>
            public ValueCollection(OrderedDictionary<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                {
                    throw new ArgumentNullException("dictionary");
                }
                parent = dictionary;
            }

            /// <summary>
            /// Copies the values from the OrderedDictionary to the given array, starting at the given index.
            /// </summary>
            /// <param name="array">The array to copy the values to.</param>
            /// <param name="arrayIndex">The index into the array to start copying the values.</param>
            /// <exception cref="System.ArgumentNullException">The array is null.</exception>
            /// <exception cref="System.ArgumentOutOfRangeException">The arrayIndex is negative.</exception>
            /// <exception cref="System.ArgumentException">The array, starting at the given index, is not large enough to contain all the values.</exception>
            public void CopyTo(TValue[] array, int arrayIndex)
            {
                if (array == null)
                {
                    throw new ArgumentNullException("array");
                }
                if (arrayIndex < 0)
                {
                    throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, String.Format(Resources.TooSmall, 0));
                }
                if (parent.dictionary.Count > array.Length - arrayIndex)
                {
                    throw new ArgumentException(Resources.ArrayTooSmall, "array");
                }
                foreach (TKey key in parent.keys)
                {
                    TValue value = parent.dictionary[key];
                    array[arrayIndex] = value;
                    ++arrayIndex;
                }
            }

            /// <summary>
            /// Gets the number of values in the OrderedDictionary.
            /// </summary>
            public int Count
            {
                get { return parent.dictionary.Count; }
            }

            /// <summary>
            /// Gets an enumerator over the values in the OrderedDictionary.
            /// </summary>
            /// <returns>The enumerator.</returns>
            public IEnumerator<TValue> GetEnumerator()
            {
                foreach (TKey key in parent.keys)
                {
                    TValue value = parent.dictionary[key];
                    yield return value;
                }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            bool ICollection<TValue>.Contains(TValue item)
            {
                return parent.dictionary.ContainsValue(item);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            void ICollection<TValue>.Add(TValue item)
            {
                throw new NotSupportedException(Resources.EditReadonlyList);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            void ICollection<TValue>.Clear()
            {
                throw new NotSupportedException(Resources.EditReadonlyList);
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            bool ICollection<TValue>.IsReadOnly
            {
                get { return true; }
            }

            [EditorBrowsable(EditorBrowsableState.Never)]
            bool ICollection<TValue>.Remove(TValue item)
            {
                throw new NotSupportedException(Resources.EditReadonlyList);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
