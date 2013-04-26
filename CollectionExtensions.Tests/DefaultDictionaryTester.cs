using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectionExtensions.Test
{
    /// <summary>
    /// Tests the DefaultDictionary.
    /// </summary>
    [TestClass]
    public class DefaultDictionaryTester
    {
        #region Real World Example

        /// <summary>
        /// DefaultDictionary is useful when there is a logical default, or initial value. 
        /// A good example is counting occurrences, so we'll demonstrate counting the occurrence of numbers.
        /// </summary>
        [TestMethod]
        public void TestDefaultDictionary_CountOccurrences()
        {
            Random random = new Random();

            // build a list of random numbers
            var numbers = Enumerable.Range(0, 1000).Select(i => random.Next(100)).ToList(); // to avoid regenerating
            
            // now count the occurrences
            var dictionary = new DefaultDictionary<int, int>();
            foreach (int number in numbers)
            {
                ++dictionary[number];
            }

            // now make sure we found all of the occurrences
            var groups = numbers.GroupBy(number => number);
            foreach (var group in groups)
            {
                Assert.AreEqual(group.Count(), dictionary[group.Key], "Not all the numbers were accounted for.");
            }
        }

        /// <summary>
        /// DefaultDictionary works well when tracking the items with a given key; a.k.a., creating an ILookup.
        /// </summary>
        [TestMethod]
        public void TestDefaultDictionary_AddOccurrencesToCollection()
        {
            Random random = new Random();

            // build a list of random numbers
            var numbers = Enumerable.Range(0, 1000).Select(i => random.Next(100)).ToList(); // to avoid regenerating

            // now count the occurrences
            var dictionary = new Dictionary<int, List<int>>().Defaulted(i => new List<int>()); // we need a new list every time
            foreach (int number in numbers)
            {
                dictionary[number].Add(number); // gets and sets internally
            }

            // now make sure we found all of the occurrences
            var groups = numbers.GroupBy(number => number);
            foreach (var group in groups)
            {
                Assert.AreEqual(group.Count(), dictionary[group.Key].Count(), "Not all the numbers were accounted for.");
            }
        }

        #endregion

        #region Defaulted Extension Methods

        /// <summary>
        /// The dictionary we pass to the Defaulted method should appear as the backing field in the DefaultDictionary returned.
        /// </summary>
        [TestMethod]
        public void TestDefaulted_WithDictionary_CreatesDefaultDictionary()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            DefaultDictionary<int, int> defaulted = dictionary.Defaulted();
            Assert.AreSame(dictionary, defaulted.Dictionary, "The dictionary was not set in the backing field.");
        }

        /// <summary>
        /// The dictionary we pass to the Defaulted method should appear as the backing field in the DefaultDictionary returned.
        /// As should the generator delegate.
        /// </summary>
        [TestMethod]
        public void TestDefaulted_WithDefaultGenerator_CreatesDefaultDictionary()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            Func<int, int> defaultGenerator = i => 0;
            DefaultDictionary<int, int> defaulted = dictionary.Defaulted(defaultGenerator);
            Assert.AreSame(dictionary, defaulted.Dictionary, "The dictionary was not set in the backing field.");
            Assert.AreSame(defaultGenerator, defaulted.DefaultGenerator, "The default generator was not set in the backing field.");
        }

        #endregion

        #region Ctor and IsReadOnly

        /// <summary>
        /// If we use the default ctor, a Dictionary should be used internally, along with the
        /// default generator.
        /// </summary>
        [TestMethod]
        public void TestCtor_DefaultCtor_UsesDictionary()
        {
            DefaultDictionary<int, int> dictionary = new DefaultDictionary<int, int>();
            Assert.IsInstanceOfType(dictionary.Dictionary, typeof(Dictionary<int, int>), "The internal dictionary was the wrong type.");
            Assert.AreEqual(0, dictionary.DefaultGenerator(0), "The wrong generator was set.");
        }

        /// <summary>
        /// If we pass a null default generator to the ctor, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithDefaultGenerator_NullGenerator_Throws()
        {
            Func<int, int> generator = null;
            new DefaultDictionary<int, int>(generator);
        }

        /// <summary>
        /// If we pass a default generator to the ctor, it should be set as a backing field.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithDefaultGenerator_SetsGenerator()
        {
            Func<int, int> generator = i => i;
            DefaultDictionary<int, int> dictionary = new DefaultDictionary<int, int>(generator);
            Assert.IsInstanceOfType(dictionary.Dictionary, typeof(Dictionary<int, int>), "The internal dictionary was the wrong type.");
            Assert.AreSame(generator, dictionary.DefaultGenerator, "The wrong generator was set.");
        }

        /// <summary>
        /// If we pass a null equality comparer to the ctor, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithComparer_NullComparer_Throws()
        {
            IEqualityComparer<int> comparer = null;
            new DefaultDictionary<int, int>(comparer);
        }

        /// <summary>
        /// If we pass an equality comparer to the ctor, it should be used with the dictionary internally.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithComparer_SetsComparer()
        {
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            DefaultDictionary<int, int> dictionary = new DefaultDictionary<int, int>(comparer);
            Assert.IsInstanceOfType(dictionary.Dictionary, typeof(Dictionary<int, int>), "The internal dictionary was the wrong type.");
            Assert.AreEqual(0, dictionary.DefaultGenerator(0), "The wrong generator was set.");
            Assert.AreSame(comparer, ((Dictionary<int, int>)dictionary.Dictionary).Comparer, "The comparer was not set.");
        }

        /// <summary>
        /// If we pass a null equality comparer to the ctor, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithComparerAndGenerator_NullComparer_Throws()
        {
            IEqualityComparer<int> comparer = null;
            Func<int, int> generator = i => i;
            new DefaultDictionary<int, int>(comparer, generator);
        }

        /// <summary>
        /// If we pass a null default generator to the ctor, an exception will be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithComparerAndGenerator_NullGenerator_Throws()
        {
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Func<int, int> generator = null;
            new DefaultDictionary<int, int>(comparer, generator);
        }

        /// <summary>
        /// If we pass an equality comparer and default generator to the ctor, the comparer should
        /// be used internally and the default generator should be set.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithComparerAndGenerator_SetsComparerAndGenerator()
        {
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Func<int, int> defaultGenerator = i => i;
            DefaultDictionary<int, int> dictionary = new DefaultDictionary<int, int>(comparer, defaultGenerator);
            Assert.IsInstanceOfType(dictionary.Dictionary, typeof(Dictionary<int, int>), "The internal dictionary was the wrong type.");
            Assert.AreSame(defaultGenerator, dictionary.DefaultGenerator, "The wrong generator was set.");
            Assert.AreSame(comparer, ((Dictionary<int, int>)dictionary.Dictionary).Comparer, "The comparer was not set.");
        }

        /// <summary>
        /// If we try to pass a null dictionary, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullDictionary_Throws()
        {
            IDictionary<int, int> dictionary = null;
            new DefaultDictionary<int, int>(dictionary);
        }

        /// <summary>
        /// If we try to pass a null dictionary, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithGenerator_NullDictionary_Throws()
        {
            IDictionary<int, int> dictionary = null;
            Func<int, int> generator = i => 0;
            new DefaultDictionary<int, int>(dictionary, generator);
        }

        /// <summary>
        /// If we try to pass a null generator, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullGenerator_Throws()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            Func<int, int> generator = null;
            new DefaultDictionary<int, int>(dictionary, generator);
        }

        /// <summary>
        /// The dictionary we pass to the ctor should be accessible through the property.
        /// The default generator should return default(TValue).
        /// </summary>
        [TestMethod]
        public void TestCtor_SetsDictionary()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            DefaultDictionary<int, int> defaultDictionary = new DefaultDictionary<int, int>(dictionary);
            Assert.AreSame(dictionary, defaultDictionary.Dictionary, "The dictionary was not set.");
            Assert.AreEqual(default(int), defaultDictionary.DefaultGenerator(0), "The default generator generated the wrong value.");
            Assert.AreEqual(dictionary.IsReadOnly, ((IDictionary<int, int>)defaultDictionary).IsReadOnly, "The read-only property not the same.");
        }

        /// <summary>
        /// The dictionary and generator we pass to the ctor should be accessible through the property.
        /// </summary>
        [TestMethod]
        public void TestCtor_SetsDictionaryAndGenerator()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            Func<int, int> generator = i => 0;
            DefaultDictionary<int, int> defaultDictionary = new DefaultDictionary<int, int>(dictionary, generator);
            Assert.AreSame(dictionary, defaultDictionary.Dictionary, "The dictionary was not set.");
            Assert.AreSame(generator, defaultDictionary.DefaultGenerator, "The generator was not set.");
        }

        #endregion

        #region Aggregate



        #endregion

        #region Add, Count and ContainsKey

        /// <summary>
        /// Add just calls add on the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestAdd_AddsToDictionary()
        {
            var dictionary = new Dictionary<int, int>().Defaulted();
            dictionary.Add(0, 0);
            Assert.AreEqual(1, dictionary.Count, "Add did not increase the size of the dictionary.");
            Assert.IsTrue(dictionary.ContainsKey(0), "The wrong key was added.");
            Assert.AreEqual(0, dictionary[0], "The wrong value was added.");
        }

        /// <summary>
        /// Add just calls add on the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestAdd_Explicit_AddsToDictionary()
        {
            var dictionary = new Dictionary<int, int>().Defaulted();
            ICollection<KeyValuePair<int, int>> collection = dictionary;
            collection.Add(new KeyValuePair<int, int>(0, 0));
            Assert.AreEqual(1, dictionary.Count, "Add did not increase the size of the dictionary.");
            Assert.IsTrue(dictionary.ContainsKey(0), "The wrong key was added.");
            Assert.AreEqual(0, dictionary[0], "The wrong value was added.");
        }

        #endregion

        #region Keys & Values

        /// <summary>
        /// The keys collection is the same as the collection returned from the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestKeys_ReturnsSameKeys()
        {
            var dictionary = new Dictionary<int, int>() 
            {
                {0, 0},
                {1, 1},
                {2, 2},
            }.Defaulted();
            var actual = dictionary.Keys;
            var expected = dictionary.Dictionary.Keys;
            Assert.IsTrue(actual.SequenceEqual(expected), "The wrong keys were returned.");
        }

        /// <summary>
        /// The values collection is the same as the collection returned from the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestValues_ReturnsSameValues()
        {
            var dictionary = new Dictionary<int, int>() 
            {
                {0, 0},
                {1, 1},
                {2, 2},
            }.Defaulted();
            var actual = dictionary.Values;
            var expected = dictionary.Dictionary.Values;
            Assert.IsTrue(actual.SequenceEqual(expected), "The wrong values were returned.");
        }

        #endregion

        #region Remove

        /// <summary>
        /// We just call Remove on the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestRemove_KeyExists_RemovesPair()
        {
            var dictionary = new Dictionary<int, int>() { {0, 0}, {1, 1}, {2, 2} }.Defaulted();
            bool removed = dictionary.Remove(0);
            Assert.IsTrue(removed, "The key could not be found.");
            Assert.IsFalse(dictionary.ContainsKey(0), "The pair was not removed.");
        }

        /// <summary>
        /// We just call Remove on the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestRemove_KeyMissing_ReturnsFalse()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();
            bool removed = dictionary.Remove(3);
            Assert.IsFalse(removed, "The key was mistakenly found.");
        }

        #endregion

        #region TryGetValue

        /// <summary>
        /// The default value should be returned if the key is missing. The key should NOT be associated with the value.
        /// </summary>
        [TestMethod]
        public void TestTryGetValue_KeyMissing_ReturnsDefault()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted(i => -1);
            int value;
            bool found = dictionary.TryGetValue(3, out value);
            Assert.IsFalse(found, "The key was found.");
            Assert.AreEqual(-1, value, "The default value was not returned.");
            Assert.IsFalse(dictionary.ContainsKey(3), "The key was added by mistake.");
        }

        /// <summary>
        /// The actual value should be returned if the key is found.
        /// </summary>
        [TestMethod]
        public void TestTryGetValue_KeyFound_ReturnsValue()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted(i => -1);
            int value;
            bool found = dictionary.TryGetValue(1, out value);
            Assert.IsTrue(found, "The key was not found.");
            Assert.AreEqual(1, value, "The correct value was not returned.");
        }

        #endregion

        #region Indexer

        /// <summary>
        /// The default value should be returned if the key is missing and the key should be associated with the value.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_UnknownKey_AssociatesKeyWithDefault_ReturnsDefault()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted(i => -1);
            int value = dictionary[3];
            Assert.AreEqual(-1, value, "The default value was not returned.");
            Assert.IsTrue(dictionary.ContainsKey(3), "The key was not added to the dictionary.");
        }

        /// <summary>
        /// The default value should be returned if the key is missing. The key should not be associated with the value
        /// if the underlying dictionary is read-only.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_UnknownKey_IsReadOnly_ReturnsDefault()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.ReadOnly().Defaulted(i => -1);
            int value = dictionary[3];
            Assert.AreEqual(-1, value, "The default value was not returned.");
            Assert.IsFalse(dictionary.ContainsKey(3), "The key should not have been added to the underlying dictionary.");
        }

        /// <summary>
        /// The actual value should be returned if the key exists.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_KeyExists_ReturnsValue()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted(i => -1);
            int value = dictionary[1];
            Assert.AreEqual(1, value, "The correct value was not returned.");
        }

        /// <summary>
        /// The setter should associate the value with the key.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Setter_KeyExists_ChangesValue()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted(i => -1);
            dictionary[0] = 1;
            Assert.AreEqual(1, dictionary[0], "The value was not set.");
        }

        /// <summary>
        /// The setter should associate the value with the key.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Setter_UnknownKey_AssociatesKeyAndValue()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted(i => -1);
            dictionary[3] = 3;
            Assert.AreEqual(3, dictionary[3], "The key/value pair was not added.");
        }

        #endregion

        #region Clear

        /// <summary>
        /// All the items should be removed from the dictionary.
        /// </summary>
        [TestMethod]
        public void TestClear_RemovesAllItems()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();
            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count, "The items were not all removed.");
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// All the items should be enumerated in the dictionary.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_EnumeratesAllItems()
        {
            IEnumerable<KeyValuePair<int, int>> dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();

            Assert.IsTrue(enumerate(dictionary).Contains(new KeyValuePair<int, int>(0, 0)), "The first pair was not present.");
            Assert.IsTrue(enumerate(dictionary).Contains(new KeyValuePair<int, int>(1, 1)), "The second pair was not present.");
            Assert.IsTrue(enumerate(dictionary).Contains(new KeyValuePair<int, int>(2, 2)), "The third pair was not present.");
        }

        private static IEnumerable<T> enumerate<T>(IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
            {
                yield return item;
            }
        }

        /// <summary>
        /// All the items should be enumerated in the dictionary.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_Explicit_EnumeratesAllItems()
        {
            IEnumerable dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();

            Assert.IsTrue(enumerate(dictionary).Cast<KeyValuePair<int, int>>().Contains(new KeyValuePair<int, int>(0, 0)), "The first pair was not present.");
            Assert.IsTrue(enumerate(dictionary).Cast<KeyValuePair<int, int>>().Contains(new KeyValuePair<int, int>(1, 1)), "The second pair was not present.");
            Assert.IsTrue(enumerate(dictionary).Cast<KeyValuePair<int, int>>().Contains(new KeyValuePair<int, int>(2, 2)), "The third pair was not present.");
        }

        private static IEnumerable enumerate(IEnumerable enumerable)
        {
            foreach (object item in enumerable)
            {
                yield return item;
            }
        }

        #endregion

        #region Contains

        /// <summary>
        /// Contains simply calls to the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestContains_Explicit_PairExists_ReturnsTrue()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();
            ICollection<KeyValuePair<int, int>> collection = dictionary;
            bool contains = collection.Contains(new KeyValuePair<int, int>(0, 0));
            Assert.IsTrue(contains, "Did not find the pair.");
        }

        /// <summary>
        /// Contains simply calls to the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestContains_Explicit_OnlyKeyMatches_ReturnsFalse()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();
            ICollection<KeyValuePair<int, int>> collection = dictionary;
            bool contains = collection.Contains(new KeyValuePair<int, int>(0, 1));
            Assert.IsFalse(contains, "Was searching for value solely by key.");
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// CopyTo simply calls the method on the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_Explicit()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();
            ICollection<KeyValuePair<int, int>> collection = dictionary;

            KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[dictionary.Count];
            int arrayIndex = 0;
            collection.CopyTo(array, arrayIndex);

            KeyValuePair<int, int>[] expected = new KeyValuePair<int,int>[dictionary.Count];
            dictionary.Dictionary.CopyTo(expected, 0);
            Assert.IsTrue(array.SequenceEqual(expected), "The values were not copied as expected.");
        }

        #endregion

        #region Remove

        /// <summary>
        /// Remove simply calls to the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestRemove_Explicit_PairExists_ReturnsTrue()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();
            ICollection<KeyValuePair<int, int>> collection = dictionary;
            bool removed = collection.Remove(new KeyValuePair<int, int>(0, 0));
            Assert.IsTrue(removed, "Did not find the pair.");
            Assert.IsFalse(dictionary.ContainsKey(0), "The pair was not removed.");
        }

        /// <summary>
        /// Remove simply calls to the underlying dictionary.
        /// </summary>
        [TestMethod]
        public void TestRemove_Explicit_OnlyKeyMatches_ReturnsFalse()
        {
            var dictionary = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 2 } }.Defaulted();
            ICollection<KeyValuePair<int, int>> collection = dictionary;
            bool removed = collection.Remove(new KeyValuePair<int, int>(0, 1));
            Assert.IsFalse(removed, "Was removing solely by key.");
            Assert.IsTrue(dictionary.ContainsKey(0), "The pair was removed anyway.");
        }

        #endregion
    }
}
