using System;
using System.Collections.Generic;
using CollectionExtensions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectionExtensions.Tests.Extensions
{
    /// <summary>
    /// Tests the Dictionary extension methods.
    /// </summary>
    [TestClass]
    public class DictionaryTester
    {
        #region DictionaryEquals

        /// <summary>
        /// If the first dictionary is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDictionaryEquals_FirstDictionaryNull_ThrowsException()
        {
            IDictionary<int, int> dictionary = null;
            IDictionary<int, int> other = new Dictionary<int, int>();
            Dictionary.DictionaryEquals(dictionary, other);
        }

        /// <summary>
        /// If the second dictionary is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDictionaryEquals_SecondDictionaryNull_ThrowsException()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            IDictionary<int, int> other = null;
            Dictionary.DictionaryEquals(dictionary, other);
        }

        /// <summary>
        /// If the first dictionary is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDictionaryEquals_WithComparer_FirstDictionaryNull_ThrowsException()
        {
            IDictionary<int, int> dictionary = null;
            IDictionary<int, int> other = new Dictionary<int, int>();
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Dictionary.DictionaryEquals(dictionary, other, comparer);
        }

        /// <summary>
        /// If the second dictionary is null, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestDictionaryEquals_WithComparer_SecondDictionaryNull_ThrowsException()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>();
            IDictionary<int, int> other = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Dictionary.DictionaryEquals(dictionary, other, comparer);
        }

        /// <summary>
        /// If the value comparer is null, the default comparer should be used.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEquals_NullComparer_UsesDefaultEqualityComparer()
        {
            IDictionary<int, string> dictionary = new Dictionary<int, string>() { { 0, "KEY" } };
            IDictionary<int, string> other = new Dictionary<int, string>() { { 0, "key" } };
            IEqualityComparer<string> comparer = null;
            bool result = dictionary.DictionaryEquals(other, comparer);
            Assert.IsFalse(result, "The default equality comparer was not used.");
        }

        /// <summary>
        /// If the value comparer is provided, it should be used to compare values.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEquals_CaseInsensitiveComparer_UsesComparer()
        {
            IDictionary<int, string> dictionary = new Dictionary<int, string>() { { 0, "KEY" } };
            IDictionary<int, string> other = new Dictionary<int, string>() { { 0, "key" } };
            IEqualityComparer<string> comparer = StringComparer.CurrentCultureIgnoreCase;
            bool result = dictionary.DictionaryEquals(other, comparer);
            Assert.IsTrue(result, "The given equality comparer was not used.");
        }

        /// <summary>
        /// If the two dictionaries are the same in-memory reference, then they are equal, even if
        /// passed a bad value comparer that would say otherwise.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEquals_IdenticalDictionaries_ReturnsTrue()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>() { { 0, 0 } };
            bool result = dictionary.DictionaryEquals(dictionary, new BadEqualityComparer<int>());
            Assert.IsTrue(result, "Identical dictionaries were not considered equal.");
        }

        private class BadEqualityComparer<T> : IEqualityComparer<T>
        {
            public bool Equals(T x, T y)
            {
                return false;
            }

            public int GetHashCode(T obj)
            {
                return 0;
            }
        }


        /// <summary>
        /// If two dictionaries do not have the same count, we know they are not equal.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEquals_DifferentCounts_ReturnsFalse()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>() { { 0, 0 } };
            IDictionary<int, int> other = new Dictionary<int, int>() { { 0, 0 }, { 1, 1 } };
            bool result = dictionary.DictionaryEquals(other);
            Assert.IsFalse(result, "Dictionaries with different counts were considered equal.");
        }

        /// <summary>
        /// If one dictionary does not have a key that is in another, it is not equal.
        /// </summary>
        [TestMethod]
        public void TestDictionaryEquals_DifferentKeys_ReturnsFalse()
        {
            IDictionary<int, int> dictionary = new Dictionary<int, int>() { { 0, 0 } };
            IDictionary<int, int> other = new Dictionary<int, int>() { { 1, 1 } };
            bool result = dictionary.DictionaryEquals(other);
            Assert.IsFalse(result, "Dictionaries with different keys were considered equal.");
        }

        #endregion
    }
}
