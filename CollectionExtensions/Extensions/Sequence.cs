using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using CollectionExtensions.Properties;

namespace CollectionExtensions.Extensions
{
    /// <summary>
    /// Provides extension methods for working with instances of <see cref="System.Collections.Generic.IEnumerable&lt;T&gt;"/>.
    /// </summary>
    public static class Sequence
    {
        #region CompareTo

        /// <summary>
        /// Compares the collection to the other collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the collections.</typeparam>
        /// <param name="first">The collection to compare to the other collection.</param>
        /// <param name="second">The collection to compare to this collection.</param>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The other collection is null.</exception>
        /// <exception cref="System.ArgumentException">There is not a default comparison defined for the item type.</exception>
        /// <returns>
        /// If two items compare to be different, the result of the comparison will be returned.
        /// If all the items match but the first collection has fewer items, -1 will be returned.
        /// If all the items match but the second collection has fewer items, 1 will be returned.
        /// Otherwise, if all the items match and the collections are the same size, 0 will be returned.
        /// </returns>
        public static int CompareTo<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException("source");
            }
            if (second == null)
            {
                throw new ArgumentNullException("other");
            }
            return compareTo(first, second, Comparer<TSource>.Default.Compare);
        }

        /// <summary>
        /// Compares the collection to the other collection.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collections.</typeparam>
        /// <param name="first">The collection to compare to the other collection.</param>
        /// <param name="second">The collection to compare to this collection.</param>
        /// <param name="comparer">The comparer to use to compare items in the collections.</param>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The other collection is null.</exception>
        /// <exception cref="System.ArgumentException">The comparer is null and there is not a default comparison defined for the item type.</exception>
        /// <returns>
        /// If two items compare to be different, the result of the comparison will be returned.
        /// If all the items match but the first collection has fewer items, -1 will be returned.
        /// If all the items match but the second collection has fewer items, 1 will be returned.
        /// Otherwise, if all the items match and the collections are the same size, 0 will be returned.
        /// </returns>
        public static int CompareTo<T>(this IEnumerable<T> first, IEnumerable<T> second, IComparer<T> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException("source");
            }
            if (second == null)
            {
                throw new ArgumentNullException("other");
            }
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }
            return compareTo(first, second, comparer.Compare);
        }

        private static int compareTo<T>(IEnumerable<T> first, IEnumerable<T> second, Func<T, T, int> comparison)
        {
            using (IEnumerator<T> firstEnumerator = first.GetEnumerator())
            using (IEnumerator<T> secondEnumerator = second.GetEnumerator())
            {
                bool firstHasMore = firstEnumerator.MoveNext();
                bool secondHasMore = secondEnumerator.MoveNext();
                while (firstHasMore && secondHasMore)
                {
                    int result = comparison(firstEnumerator.Current, secondEnumerator.Current);
                    if (result < 0)
                    {
                        return -1;
                    }
                    if (result > 0)
                    {
                        return 1;
                    }
                    firstHasMore = firstEnumerator.MoveNext();
                    secondHasMore = secondEnumerator.MoveNext();
                }
                return (firstHasMore ? 1 : (secondHasMore ? -1 : 0));
            }
        }

        #endregion

        #region Except

        /// <summary>
        /// Creates a new collection containing the items in the source collection
        /// whose keys are not found in the other collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the source collection.</typeparam>
        /// <typeparam name="TKey">The type of the keys and the items in the other collection.</typeparam>
        /// <param name="first">The collection of items to filter.</param>
        /// <param name="second">The collection of values to remove from the source.</param>
        /// <param name="keySelector">A method that can extract a key from the source items.</param>
        /// <returns>
        /// A new collection containing the items in the source collection whose keys were not in the other collection.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The other collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The key selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">There is no default comparer for the key.</exception>
        public static IEnumerable<TSource> Except<TSource, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TKey> second,
            Func<TSource, TKey> keySelector)
        {
            if (first == null)
            {
                throw new ArgumentNullException("source");
            }
            if (second == null)
            {
                throw new ArgumentNullException("other");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            return except(first, second, keySelector, EqualityComparer<TKey>.Default);
        }

        /// <summary>
        /// Creates a new collection containing the items in the source collection
        /// whose keys are not found in the other collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the source collection.</typeparam>
        /// <typeparam name="TKey">The type of the keys and the items in the other collection.</typeparam>
        /// <param name="first">The collection of items to filter.</param>
        /// <param name="second">The collection of values to remove from the source.</param>
        /// <param name="keySelector">A method that can extract a key from the source items.</param>
        /// <param name="keyComparer">The comparer to use to compare key values.</param>
        /// <returns>
        /// A new collection containing the items in the source collection whose keys were not in the other collection.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The other collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The key selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">There is no default comparer for the key.</exception>
        /// <remarks>If the key comparer is null, the default comparison is used.</remarks>
        public static IEnumerable<TSource> Except<TSource, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TKey> second,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException("source");
            }
            if (second == null)
            {
                throw new ArgumentNullException("other");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            if (keyComparer == null)
            {
                keyComparer = EqualityComparer<TKey>.Default;
            }
            return except(first, second, keySelector, keyComparer);
        }

        private static IEnumerable<TSource> except<TSource, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TKey> second,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            var set = new HashSet<TKey>(keyComparer);
            foreach (TKey key in second)
            {
                set.Add(key);
            }
            foreach (TSource item in first)
            {
                TKey key = keySelector(item);
                if (set.Add(key))
                {
                    yield return item;
                }
            }
        }

        #endregion

        #region ForEach

        /// <summary>
        /// Performs an action for each item in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="source">The collection whose items the action should be performed on.</param>
        /// <param name="action">The action to perform.</param>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The action is null.</exception>
        /// <remarks>
        /// It is easy to abuse this function to create code that is hard to read. It is best practice
        /// to use a foreach loop or call a helper function.
        /// </remarks>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            forEach(source, action);
        }

        private static void forEach<T>(IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }

        #endregion

        #region Intersect

        /// <summary>
        /// Creates a new collection containing the items in the source collection
        /// whose keys are also found in the other collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the source collection.</typeparam>
        /// <typeparam name="TKey">The type of the keys and the items in the other collection.</typeparam>
        /// <param name="first">The collection of items to filter.</param>
        /// <param name="second">The collection of values to remove from the source.</param>
        /// <param name="keySelector">A method that can extract a key from the source items.</param>
        /// <returns>
        /// A new collection containing the items in the source collection whose keys were also in the other collection.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The other collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The key selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">There is no default comparer for the key.</exception>
        public static IEnumerable<TSource> Intersect<TSource, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TKey> second,
            Func<TSource, TKey> keySelector)
        {
            if (first == null)
            {
                throw new ArgumentNullException("source");
            }
            if (second == null)
            {
                throw new ArgumentNullException("other");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            return intersect(first, second, keySelector, EqualityComparer<TKey>.Default);
        }

        /// <summary>
        /// Creates a new collection containing the items in the source collection
        /// whose keys are also found in the other collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the source collection.</typeparam>
        /// <typeparam name="TKey">The type of the keys and the items in the other collection.</typeparam>
        /// <param name="first">The collection of items to filter.</param>
        /// <param name="second">The collection of values to remove from the source.</param>
        /// <param name="keySelector">A method that can extract a key from the source items.</param>
        /// <param name="keyComparer">The comparer to use to compare key values.</param>
        /// <returns>
        /// A new collection containing the items in the source collection whose keys were also in the other collection.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The other collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The key selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">There is no default comparer for the key.</exception>
        /// <remarks>If the key comparer is null, the default comparison is used.</remarks>
        public static IEnumerable<TSource> Intersect<TSource, TKey>(
            this IEnumerable<TSource> first,
            IEnumerable<TKey> second,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException("source");
            }
            if (second == null)
            {
                throw new ArgumentNullException("other");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            if (keyComparer == null)
            {
                keyComparer = EqualityComparer<TKey>.Default;
            }
            return intersect(first, second, keySelector, keyComparer);
        }

        private static IEnumerable<TSource> intersect<TSource, TKey>(
            IEnumerable<TSource> first,
            IEnumerable<TKey> second,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> keyComparer)
        {
            HashSet<TKey> set = new HashSet<TKey>(keyComparer);
            foreach (TKey key in second)
            {
                set.Add(key);
            }
            foreach (TSource item in first)
            {
                TKey key = keySelector(item);
                if (set.Remove(key))
                {
                    yield return item;
                }
            }
        }

        #endregion

        #region MaxByKey

        /// <summary>
        /// Finds the item in the source collection with the largest key, as returned by the given key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the collection.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the key selector.</typeparam>
        /// <param name="source">The collection to find the max item in.</param>
        /// <param name="keySelector">A method that extracts the key from an item.</param>
        /// <returns>The item in the source collection with the largest key.</returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The key selector is null.</exception>
        /// <exception cref="System.ArgumentException">The type of the key is not comparable.</exception>
        /// <exception cref="System.InvalidOperationException">The source collection is empty.</exception>
        public static TSource MaxByKey<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            return maxByKey(source, keySelector);
        }

        private static TSource maxByKey<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new InvalidOperationException(Resources.EmptySequence);
                }
                TSource max = enumerator.Current;
                IComparer<TKey> comparer = Comparer<TKey>.Default;
                while (enumerator.MoveNext())
                {
                    TKey maxKey = keySelector(max);
                    TKey currentKey = keySelector(enumerator.Current);
                    int result = comparer.Compare(maxKey, currentKey);
                    if (result < 0)
                    {
                        max = enumerator.Current;
                    }
                }
                return max;
            }
        }

        #endregion

        #region MinByKey

        /// <summary>
        /// Finds the item in the source collection with the smallest key, as returned by the given key selector.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the collection.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the key selector.</typeparam>
        /// <param name="source">The collection to find the min item in.</param>
        /// <param name="keySelector">A method that extracts the key from an item.</param>
        /// <returns>The item in the source collection with the smallest key.</returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentNullException">The key selector is null.</exception>
        /// <exception cref="System.ArgumentException">The type of the key is not comparable.</exception>
        /// <exception cref="System.InvalidOperationException">The source collection is empty.</exception>
        public static TSource MinByKey<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            return minByKey(source, keySelector);
        }

        private static TSource minByKey<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new InvalidOperationException(Resources.EmptySequence);
                }
                TSource min = enumerator.Current;
                IComparer<TKey> comparer = Comparer<TKey>.Default;
                while (enumerator.MoveNext())
                {
                    TKey minKey = keySelector(min);
                    TKey currentKey = keySelector(enumerator.Current);
                    int result = comparer.Compare(minKey, currentKey);
                    if (result > 0)
                    {
                        min = enumerator.Current;
                    }
                }
                return min;
            }
        }

        #endregion

        #region RandomSamples

        /// <summary>
        /// Gets the requested number of random items from the collection.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="source">The collection to get the random samples from.</param>
        /// <param name="numberOfSamples">The number of samples to retrieve.</param>
        /// <returns>The requested number of random items from the collection.</returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of samples is negative.</exception>
        /// <remarks>If the collection does not contain the needed number of items, all items are returned.</remarks>
        public static IEnumerable<T> RandomSamples<T>(this IEnumerable<T> source, int numberOfSamples)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0));
            }
            return randomSamples(source, numberOfSamples, new Random().Next);
        }

        /// <summary>
        /// Gets the requested number of random items from the collection.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="source">The collection to get the random samples from.</param>
        /// <param name="numberOfSamples">The number of samples to retrieve.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>The requested number of random items from the collection.</returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of samples is negative.</exception>
        /// <exception cref="System.ArgumentNullException">The random number generator is null.</exception>
        /// <remarks>If the collection does not contain the needed number of items, all items are returned.</remarks>
        public static IEnumerable<T> RandomSamples<T>(this IEnumerable<T> source, int numberOfSamples, Random random)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0));
            }
            if (random == null)
            {
                throw new ArgumentNullException("random");
            }
            return randomSamples(source, numberOfSamples, random.Next);
        }

        /// <summary>
        /// Gets the requested number of random items from the collection.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="source">The collection to get the random samples from.</param>
        /// <param name="numberOfSamples">The number of samples to retrieve.</param>
        /// <param name="generator">The random number generator to use.</param>
        /// <returns>The requested number of random items from the collection.</returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The number of samples is negative.</exception>
        /// <exception cref="System.ArgumentNullException">The generator is null.</exception>
        /// <remarks>If the collection does not contain the needed number of items, all items are returned.</remarks>
        public static IEnumerable<T> RandomSamples<T>(this IEnumerable<T> source, int numberOfSamples, Func<int> generator)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (numberOfSamples < 0)
            {
                throw new ArgumentOutOfRangeException("numberOfSamples", numberOfSamples, String.Format(CultureInfo.CurrentCulture, Resources.TooSmall, 0));
            }
            if (generator == null)
            {
                throw new ArgumentNullException("generator");
            }
            return randomSamples(source, numberOfSamples, generator);
        }

        private static IEnumerable<T> randomSamples<T>(IEnumerable<T> source, int numberOfSamples, Func<int> generator)
        {
            using (IEnumerator<T> enumerator = source.GetEnumerator())
            {
                List<T> samples = new List<T>(numberOfSamples);
                for (int sampleCount = 0; enumerator.MoveNext() && sampleCount != numberOfSamples; ++sampleCount)
                {
                    samples.Add(enumerator.Current);
                }
                int total = numberOfSamples;
                while (enumerator.MoveNext())
                {
                    ++total;
                    int likelihood = generator() % total;
                    if (likelihood < 0)
                    {
                        likelihood += total;
                    }
                    if (likelihood < numberOfSamples)
                    {
                        samples[likelihood] = enumerator.Current;
                    }
                }
                return samples;
            }
        }

        #endregion

        #region RotateLeft

        /// <summary>
        /// Creates a new collection with the items in the given collection rotated shift items.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <param name="source">The collection to rotate.</param>
        /// <param name="shift">The number of items to rotate the collection to the left.</param>
        /// <returns>A new collection with the items in the given collection rotated shift items.</returns>
        /// <exception cref="System.ArgumentNullException">The source collection is null.</exception>
        /// <remarks>
        /// If the shift is negative, the algoritm simulates rotating the items to the right. If the shift is larger than the number of items, 
        /// the algorithm will simulate a complete rotation as many times as necessary.
        /// </remarks>
        public static IEnumerable<T> RotateLeft<T>(this IEnumerable<T> source, int shift)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return rotateLeft_optimized(source, shift);
        }

        private static IEnumerable<T> rotateLeft_optimized<T>(IEnumerable<T> source, int shift)
        {
            if (shift == 0)
            {
                return source;
            }

            List<T> list = source as List<T>;
            if (list != null)
            {
                return rotateLeft<List<T>, T>(list, shift);
            }

            T[] array = source as T[];
            if (array != null)
            {
                return rotateLeft<T[], T>(array, shift);
            }

            Collection<T> collection = source as Collection<T>;
            if (collection != null)
            {
                return rotateLeft<Collection<T>, T>(collection, shift);
            }

            IList<T> iList = source as IList<T>;
            if (iList != null)
            {
                return rotateLeft<IList<T>, T>(iList, shift);
            }

            return rotateLeft<List<T>, T>(new List<T>(source), shift);
        }

        private static IEnumerable<T> rotateLeft<TList, T>(TList list, int shift)
            where TList : IList<T>
        {
            int count = list.Count;
            shift = getReducedOffset<TList, T>(list, 0, count, shift);
            for (int index = shift; index < count; ++index)
            {
                yield return list[index];
            }
            for (int index = 0; index < shift; ++index)
            {
                yield return list[index];
            }
        }

        private static int getReducedOffset<TList, T>(TList list, int first, int past, int shift)
        {
            int count = past - first;
            shift %= count;
            if (shift < 0)
            {
                shift += count;
            }
            return shift;
        }

        #endregion

        #region ToOrderedDictionary

        /// <summary>
        /// Creates a new OrderedDictionary from the given collection, using the key selector to extract the key.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the collection.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The items to created the OrderedDictionary from.</param>
        /// <param name="keySelector">A delegate that can extract a key from an item in the collection.</param>
        /// <returns>An OrderedDictionary mapping the extracted keys to their values.</returns>
        public static OrderedDictionary<TKey, TSource> ToOrderedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            return toOrderedDictionary(source, keySelector, null);
        }

        /// <summary>
        /// Creates a new OrderedDictionary from the given collection, using the key selector to extract the key.
        /// The key comparer is passed to the OrderedDictionary for comparing the extracted keys.
        /// </summary>
        /// <typeparam name="TSource">The type of the items in the collection.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The items to created the OrderedDictionary from.</param>
        /// <param name="keySelector">A delegate that can extract a key from an item in the collection.</param>
        /// <param name="comparer">The key equality comparer to use to compare keys in the dictionary.</param>
        /// <returns>An OrderedDictionary mapping the extracted keys to their values.</returns>
        public static OrderedDictionary<TKey, TSource> ToOrderedDictionary<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            return toOrderedDictionary(source, keySelector, comparer);
        }

        private static OrderedDictionary<TKey, TSource> toOrderedDictionary<TSource, TKey>(
            IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey> comparer)
        {
            OrderedDictionary<TKey, TSource> dictionary = new OrderedDictionary<TKey, TSource>(comparer);
            foreach (TSource item in source)
            {
                TKey key = keySelector(item);
                dictionary.Add(key, item);
            }
            return dictionary;
        }

        #endregion
    }
}
