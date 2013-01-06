using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using CollectionExtensions.Properties;

namespace CollectionExtensions
{
    /// <summary>
    /// Provides methods for creating read-only dictionaries.
    /// </summary>
    public static class ReadOnlyDictionary
    {
        /// <summary>
        /// Wraps the given dictionary making it read-only.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys.</typeparam>
        /// <typeparam name="TValue">The type of the values.</typeparam>
        /// <param name="dictionary">The dictionary to make read-only.</param>
        /// <returns>A new read-only dictionary wrapping the given dictionary.</returns>
        /// <exception cref="System.ArgumentNullException">The dictionary is null.</exception>
        public static ReadOnlyDictionary<TKey, TValue> ReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            ReadOnlyDictionary<TKey, TValue> readOnly = dictionary as ReadOnlyDictionary<TKey, TValue>;
            if (readOnly == null)
            {
                return new ReadOnlyDictionary<TKey, TValue>(dictionary);
            }
            return readOnly;
        }
    }

    /// <summary>
    /// Provides a view into a dictionary such that it can't be modified.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(ReadOnlyDictionaryDebugView<,>))]
    public sealed class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        /// <summary>
        /// Initializes a new instance of a ReadOnlyDictionary that wraps the given dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to make read-only.</param>
        /// <exception cref="System.ArgumentNullException">The dictionary is null.</exception>
        public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            _dictionary = dictionary;
        }

        /// <summary>
        /// Gets the underlying dictionary.
        /// </summary>
        public IDictionary<TKey, TValue> Dictionary
        {
            get { return _dictionary; }
        }

        /// <summary>
        /// Adds the key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key to associated with the value.</param>
        /// <param name="value">The value to associate with the key.</param>
        /// <exception cref="System.NotSupportedException">Cannot add to a read-only dictionary.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Add(TKey key, TValue value)
        {
            throw new NotSupportedException(Resources.EditReadonlyDictionary);
        }

        /// <summary>
        /// Determines whether the given key exists in the dictionary.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns>True if the key exists; otherwise, false.</returns>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the keys in the dictionary.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return _dictionary.Keys; }
        }

        /// <summary>
        /// Removes the key/value pair with the given key, if it exists.
        /// </summary>
        /// <param name="key">The key of the pair to remove.</param>
        /// <returns>True if the pair was removed; otherwise, false.</returns>
        /// <exception cref="System.NotSupportedException">Cannot remove from a read-only dictionary.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Remove(TKey key)
        {
            throw new NotSupportedException(Resources.EditReadonlyDictionary);
        }

        /// <summary>
        /// Tries to get the value for the given key, storing it in the output variable if it exists.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="value">The variable to store the value in, or the default value if it is not found.</param>
        /// <returns>True if the key is found and the value is set; otherwise, false.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the values in the dictionary.
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _dictionary.Values; }
        }

        /// <summary>
        /// Gets the value associated with the given key.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <returns>The value associated with the key.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The key was not in the dictionary.</exception>
        public TValue this[TKey key]
        {
            get
            {
                return _dictionary[key];
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                throw new NotSupportedException(Resources.EditReadonlyDictionary);
            }
        }

        /// <summary>
        /// Removes all of the items from the dictionary.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Cannot clear a read-only dictionary.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Clear()
        {
            throw new NotSupportedException(Resources.EditReadonlyDictionary);
        }

        /// <summary>
        /// Gets the number of key/value pairs in the dictionary.
        /// </summary>
        public int Count
        {
            get { return _dictionary.Count; }
        }

        /// <summary>
        /// Gets whether the dictionary is read-only, which will always be true.
        /// </summary>
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return true; }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException(Resources.EditReadonlyDictionary);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException(Resources.EditReadonlyDictionary);
        }

        /// <summary>
        /// Gets an enumerator over the key/value pairs in the dictionary.
        /// </summary>
        /// <returns>An enumerator over the key/value pairs.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
