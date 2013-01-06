using System;
using System.Collections.Generic;

namespace CollectionExtensions.Extensions
{
    /// <summary>
    /// Provides extension methods for working with instances of <see cref="System.Collections.Generic.IDictionary&lt;TKey, TValue&gt;"/>.
    /// </summary>
    public static class Dictionary
    {
        #region DictionaryEquals

        /// <summary>
        /// Determines whether the two dictionarys have the same key/value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the two dictionaries.</typeparam>
        /// <typeparam name="TValue">The type of the values in two dictionaries.</typeparam>
        /// <param name="dictionary">The first dictionary.</param>
        /// <param name="other">The second dictionary.</param>
        /// <returns>True if the two dictionaries have the same key/value pairs; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first dictionary is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second dictionary is null.</exception>
        /// <remarks>If the key equality comparer is different for the two dictionarys, there could be unexpected behavior.</remarks>
        public static bool DictionaryEquals<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> other)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            return dictionaryEquals<TKey, TValue>(dictionary, other, EqualityComparer<TValue>.Default);
        }

        /// <summary>
        /// Determines whether the two dictionarys have the same key/value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the two dictionaries.</typeparam>
        /// <typeparam name="TValue">The type of the values in two dictionaries.</typeparam>
        /// <param name="dictionary">The first dictionary.</param>
        /// <param name="other">The second dictionary.</param>
        /// <param name="comparer">The comparer to use to compare the dictionary values -or- if null, the default equality comparison for the value type.</param>
        /// <returns>True if the two dictionaries have the same key/value pairs; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">The first dictionary is null.</exception>
        /// <exception cref="System.ArgumentNullException">The second dictionary is null.</exception>
        /// <remarks>If the key equality comparer is different for the two dictionarys, there could be unexpected behavior.</remarks>
        public static bool DictionaryEquals<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> other, IEqualityComparer<TValue> comparer)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException("dictionary");
            }
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (comparer == null)
            {
                comparer = EqualityComparer<TValue>.Default;
            }
            return dictionaryEquals<TKey, TValue>(dictionary, other, comparer);
        }

        private static bool dictionaryEquals<TKey, TValue>(IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> other, IEqualityComparer<TValue> comparer)
        {
            if (ReferenceEquals(dictionary, other))
            {
                return true;
            }
            if (dictionary.Count != other.Count)
            {
                return false;
            }
            foreach (KeyValuePair<TKey, TValue> pair in other)
            {
                TValue value;
                if (!dictionary.TryGetValue(pair.Key, out value))
                {
                    return false;
                }
                if (!comparer.Equals(pair.Value, value))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
