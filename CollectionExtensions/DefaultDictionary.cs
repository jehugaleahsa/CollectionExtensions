using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace CollectionExtensions
{
    /// <summary>
    /// Provides extension methods for creating instances of DefaultDictionary.
    /// </summary>
    public static class DefaultDictionary
    {
        /// <summary>
        /// Creates a new DefaultDictionary that wraps the given dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
        /// <param name="dictionary">The dictionary to wrap.</param>
        /// <returns>A new DefaultDictionary wrapping the given dictionary.</returns>
        /// <exception cref="System.ArgumentNullException">The dictionary is null.</exception>
        public static DefaultDictionary<TKey, TValue> Defaulted<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new DefaultDictionary<TKey, TValue>(dictionary);
        }

        /// <summary>
        /// Creates a new DefaultDictionary that wraps the given dictionary and uses the given generator to create default values.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
        /// <param name="dictionary">The dictionary to wrap.</param>
        /// <param name="defaultGenerator">The generator to use to create default values.</param>
        /// <returns>A new DefaultDictionary wrapping the given dictionary that uses the given generator to create default values.</returns>
        /// <exception cref="System.ArgumentNullException">The dictionary is null.</exception>
        /// <exception cref="System.ArgumentNullException">The default generator is null.</exception>
        public static DefaultDictionary<TKey, TValue> Defaulted<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, 
            Func<TKey, TValue> defaultGenerator)
        {
            return new DefaultDictionary<TKey, TValue>(dictionary, defaultGenerator);
        }
    }

    /// <summary>
    /// Wraps a dictionary such that accessing the dictionary with an unknown key results in a default value being returned.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(DefaultDictionaryDebugView<,>))]
    public sealed class DefaultDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;
        private readonly Func<TKey, TValue> _defaultGenerator;

        /// <summary>
        /// Initializes a new instance of a DefaultDictionary.
        /// </summary>
        public DefaultDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
            _defaultGenerator = (TKey key) => default(TValue);
        }

        /// <summary>
        /// Initializes a new instance of a DefaultDictionary using the given default generator.
        /// </summary>
        /// <param name="defaultGenerator">The default generator to use.</param>
        /// <exception cref="System.ArgumentNullException">The default generator is null.</exception>
        public DefaultDictionary(Func<TKey, TValue> defaultGenerator)
        {
            if (defaultGenerator == null)
            {
                throw new ArgumentNullException("defaultGenerator");
            }
            _dictionary = new Dictionary<TKey, TValue>();
            _defaultGenerator = defaultGenerator;
        }

        /// <summary>
        /// Initializes a new instance of a DefaultDictionary using the given equality comparer.
        /// </summary>
        /// <param name="comparer">The equality comparer to use to compare key values.</param>
        /// <exception cref="System.ArgumentNullException">The equality comparer is null.</exception>
        public DefaultDictionary(IEqualityComparer<TKey> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            _dictionary = new Dictionary<TKey, TValue>(comparer);
            _defaultGenerator = (TKey key) => default(TValue);
        }

        /// <summary>
        /// Initializes a new instance instance of a DefaultDictionary using the given equality comparer and default generator.
        /// </summary>
        /// <param name="comparer">The equality comparer to use to compare key values.</param>
        /// <param name="defaultGenerator">The generator to use to create default values.</param>
        /// <exception cref="System.ArgumentNullException">The equality comparer is null.</exception>
        /// <exception cref="System.ArgumentNullException">The default generator is null.</exception>
        public DefaultDictionary(IEqualityComparer<TKey> comparer, Func<TKey, TValue> defaultGenerator)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            if (defaultGenerator == null)
            {
                throw new ArgumentNullException("defaultGenerator");
            }
            _dictionary = new Dictionary<TKey, TValue>(comparer);
            _defaultGenerator = defaultGenerator;
        }

        /// <summary>
        /// Initializes a new instance of a DefaultDictionary using the given dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to wrap.</param>
        /// <exception cref="System.ArgumentNullException">The dictionary is null.</exception>
        public DefaultDictionary(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            _dictionary = dictionary;
            _defaultGenerator = k => default(TValue);
        }

        /// <summary>
        /// Initializes a new instance of a DefaultDictionary using the given dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to wrap.</param>
        /// <param name="defaultGenerator">The generator to use to generate default values.</param>
        /// <exception cref="System.ArgumentNullException">The dictionary is null.</exception>
        /// <exception cref="System.ArgumentNullException">The default generator is null.</exception>
        public DefaultDictionary(IDictionary<TKey, TValue> dictionary, Func<TKey, TValue> defaultGenerator)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (defaultGenerator == null)
            {
                throw new ArgumentNullException("defaultGenerator");
            }
            _dictionary = dictionary;
            _defaultGenerator = defaultGenerator;
        }

        /// <summary>
        /// Gets the underlying dictionary.
        /// </summary>
        public IDictionary<TKey, TValue> Dictionary
        {
            get
            {
                return _dictionary;
            }
        }

        /// <summary>
        /// Gets the generator used to generate default values.
        /// </summary>
        public Func<TKey, TValue> DefaultGenerator
        {
            get
            {
                return _defaultGenerator;
            }
        }

        /// <summary>
        /// Adds the given key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key to associate the value with.</param>
        /// <param name="value">The value to associate with the key.</param>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        /// <exception cref="System.ArgumentException">The given key already exists.</exception>
        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        /// <summary>
        /// Determines whether the given key exists in the dictionary.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>True if the key exists in the dictionary; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets the keys in the dictionary.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get 
            {
                return _dictionary.Keys;
            }
        }

        /// <summary>
        /// Removes the key/value pair with the given key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the pair to remove.</param>
        /// <returns>True if the key was found and the pair removed; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        /// <summary>
        /// Tries to get the value associated with the given key. If the key is not found,
        /// the default value is stored in the value.
        /// </summary>
        /// <param name="key">The key to get the value for.</param>
        /// <param name="value">The value used to hold the results.</param>
        /// <returns>True if the key was found; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_dictionary.TryGetValue(key, out value))
            {
                return true;
            }
            else
            {
                value = _defaultGenerator(key);
                return false;
            }
        }

        /// <summary>
        /// Gets the values in the dictionary.
        /// </summary>
        public ICollection<TValue> Values
        {
            get 
            {
                return _dictionary.Values;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the given key.
        /// </summary>
        /// <param name="key">The key to get associated value by or to associate with the value.</param>
        /// <returns>The value associated with the given key.</returns>
        /// <exception cref="System.ArgumentNullException">The key is null.</exception>
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (!_dictionary.TryGetValue(key, out value))
                {
                    // needs associated for reference types
                    // can lead to the setter being called twice in a row, but only the first time
                    // don't try to modify a read-only dictionary
                    value = _defaultGenerator(key);
                    if (!_dictionary.IsReadOnly)
                    {
                        _dictionary[key] = value;
                    }
                }
                return value;
            }
            set
            {
                _dictionary[key] = value;
            }
        }

        /// <summary>
        /// Removes all key/value pairs from the dictionary.
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        /// Gets the number of key/value pairs in the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                return _dictionary.Count;
            }
        }

        /// <summary>
        /// Gets an enumerator that enumerates through the key/value pairs.
        /// </summary>
        /// <returns>An enumerator that enumerates through the key/value pairs.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get 
            {
                return _dictionary.IsReadOnly;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Remove(item);
        }
    }
}
