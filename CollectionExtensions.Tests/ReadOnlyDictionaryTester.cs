using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectionExtensions.Test
{
    /// <summary>
    /// Tests the ReadOnlyDictionary class.
    /// </summary>
    [TestClass]
    public class ReadOnlyDictionaryTester
    {
        #region Extension Methods

        /// <summary>
        /// If was call ReadOnly on a dictionary, it should be wrapped with a read-only dictionary.
        /// </summary>
        [TestMethod]
        public void TestReadOnly_WrapsDictionary()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            ReadOnlyDictionary<int, int> wrapper = dictionary.ReadOnly();
            Assert.AreSame(dictionary, wrapper.Dictionary, "The dictionary was not wrapped.");
        }

        /// <summary>
        /// If was call ReadOnly on a read-only dictionary, it should simply return the given dictionary.
        /// </summary>
        [TestMethod]
        public void TestReadOnly_AlreadyReadOnly_ReturnsGiven()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            ReadOnlyDictionary<int, int> wrapper = new ReadOnlyDictionary<int, int>(dictionary);
            ReadOnlyDictionary<int, int> again = wrapper.ReadOnly();
            Assert.AreSame(wrapper, again, "The wrapper was not returned.");
        }

        #endregion

        #region Ctor, Dictionary, Keys, Values, Count & IsReadOnly

        /// <summary>
        /// If we try to pass null to the ctor, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullDictionary_Throws()
        {
            IDictionary<int, int> dictionary = null;
            new ReadOnlyDictionary<int, int>(dictionary);
        }

        /// <summary>
        /// The dictionary that is passed to the ctor should be set,
        /// be available via the Dictionary property, have its count available
        /// and be marked as read-only.
        /// </summary>
        [TestMethod]
        public void TestCtor_SetsDictionaryCountAndIsReadOnly()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>()
            {
                { 0, 2 },
                { 1, 3 },
            };
            ReadOnlyDictionary<int, int> readOnlyDictionary = new ReadOnlyDictionary<int, int>(dictionary);
            Assert.AreSame(dictionary, readOnlyDictionary.Dictionary, "The dictionary was not set.");
            Assert.AreEqual(dictionary.Count, readOnlyDictionary.Count, "The counts did not match.");
            Assert.IsTrue(((ICollection<KeyValuePair<int, int>>)readOnlyDictionary).IsReadOnly, "The dictionary was not read-only.");

            ICollection<int> keys = readOnlyDictionary.Keys;
            Assert.IsTrue(keys.Contains(0), "Zero was not found in the keys list.");
            Assert.IsTrue(keys.Contains(1), "One was not found in the keys list.");

            ICollection<int> values = readOnlyDictionary.Values;
            Assert.IsTrue(values.Contains(2), "Two was not found in the values list.");
            Assert.IsTrue(values.Contains(3), "Three was not found in the values list.");
        }

        #endregion

        #region Add

        /// <summary>
        /// If we try to add to a read-only dictionary, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestAdd_Throws()
        {
            ReadOnlyDictionary<int, int> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>());
            dictionary.Add(0, 0);
        }

        /// <summary>
        /// If we try to add to a read-only dictionary, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestAdd_Explicit_Throws()
        {
            ICollection<KeyValuePair<int, int>> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>());
            dictionary.Add(new KeyValuePair<int, int>());
        }

        #endregion

        #region ContainsKey

        /// <summary>
        /// If we check to see whether a key that exists exists, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestContainsKey_HasKey_ReturnsTrue()
        {
            var dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 1 } });
            bool exists = dictionary.ContainsKey(1);
            Assert.IsTrue(exists, "The key should have been found.");
        }

        /// <summary>
        /// If we check to see whether a key that does not exist exists, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestContainsKey_KeyMissing_ReturnsFalse()
        {
            var dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 1 } });
            bool exists = dictionary.ContainsKey(2);
            Assert.IsFalse(exists, "The key should not have been found.");
        }

        #endregion

        #region Remove

        /// <summary>
        /// If an attempt is made to remove from a read-only dictionary, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestRemove_Throws()
        {
            ReadOnlyDictionary<int, int> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>());
            dictionary.Remove(0);
        }

        /// <summary>
        /// If an attempt is made to remove from a read-only dictionary, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestRemove_Explicit_Throws()
        {
            ICollection<KeyValuePair<int, int>> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>());
            dictionary.Remove(new KeyValuePair<int, int>());
        }

        #endregion

        #region TryGetValue

        /// <summary>
        /// If the key/value pair exists, the value should be set and true returned.
        /// </summary>
        [TestMethod]
        public void TestTryGetValue_KeyExists_SetsValueReturnsTrue()
        {
            var dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 2 } });
            int value;
            bool exists = dictionary.TryGetValue(1, out value);
            Assert.IsTrue(exists, "The key/value pair should have been found.");
            Assert.AreEqual(2, value, "The value was not set.");
        }

        /// <summary>
        /// If the key/value pair does not exist, the value should be set to the default and false returned.
        /// </summary>
        [TestMethod]
        public void TestTryGetValue_KeyMissing_SetsValueReturnsFalse()
        {
            var dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 2 } });
            int value;
            bool exists = dictionary.TryGetValue(0, out value);
            Assert.IsFalse(exists, "The key/value pair should not have been found.");
        }

        #endregion

        #region Indexer

        /// <summary>
        /// The getter should return the value associated with the key.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_ReturnsValue()
        {
            ReadOnlyDictionary<int, int> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 2 } });
            int value = dictionary[1];
            Assert.AreEqual(2, value, "The wrong value was returned.");
        }

        /// <summary>
        /// The setter should always throw an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestIndexer_Setter_Throws()
        {
            ReadOnlyDictionary<int, int> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>());
            dictionary[1] = 2;
        }

        #endregion

        #region Clear

        /// <summary>
        /// We cannot clear a read-only dictionary.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestClear_Throws()
        {
            ReadOnlyDictionary<int, int> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>());
            dictionary.Clear();
        }

        #endregion

        #region Contains

        /// <summary>
        /// If a key/value pair exists in the dictionary, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestContains_PairExists_ReturnsTrue()
        {
            ReadOnlyDictionary<int, int> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 2 } });
            bool exists = dictionary.Contains(new KeyValuePair<int, int>(1, 2));
            Assert.IsTrue(exists, "The key/value pair should have been found.");
        }

        /// <summary>
        /// If a key/value pair does not exist in the dictionary, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestContains_PairMssing_ReturnsFalse()
        {
            ReadOnlyDictionary<int, int> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 2 } });
            bool exists = dictionary.Contains(new KeyValuePair<int, int>(1, 3));
            Assert.IsFalse(exists, "The key/value pair should not have been found.");
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Calling CopyTo should copy the key/value pairs to the given array.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_CopiesPairs()
        {
            ICollection<KeyValuePair<int, int>> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 2 }, { 2, 3 } });
            KeyValuePair<int, int>[] array = new KeyValuePair<int,int>[2];
            dictionary.CopyTo(array, 0);
            Assert.IsTrue(array.Contains(new KeyValuePair<int, int>(1, 2)), "The pair (1, 2) was not copied.");
            Assert.IsTrue(array.Contains(new KeyValuePair<int, int>(2, 3)), "The pair (2, 3) was not copied.");
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// All the items should be enumerated in the dictionary.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_EnumeratesAllItems()
        {
            IEnumerable<KeyValuePair<int, int>> dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 2 }, { 2, 3 } });

            Assert.IsTrue(enumerate(dictionary).Contains(new KeyValuePair<int, int>(1, 2)), "The first pair was not present.");
            Assert.IsTrue(enumerate(dictionary).Contains(new KeyValuePair<int, int>(2, 3)), "The second pair was not present.");
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
            IEnumerable dictionary = new ReadOnlyDictionary<int, int>(new Dictionary<int, int>() { { 1, 2 }, { 2, 3 } });

            Assert.IsTrue(enumerate(dictionary).Cast<KeyValuePair<int, int>>().Contains(new KeyValuePair<int, int>(1, 2)), "The first pair was not present.");
            Assert.IsTrue(enumerate(dictionary).Cast<KeyValuePair<int, int>>().Contains(new KeyValuePair<int, int>(2, 3)), "The second pair was not present.");
        }

        private static IEnumerable enumerate(IEnumerable enumerable)
        {
            foreach (object item in enumerable)
            {
                yield return item;
            }
        }

        #endregion
    }
}
