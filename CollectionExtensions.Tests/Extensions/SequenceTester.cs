using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectionExtensions.Extensions;
using System.Collections.ObjectModel;

namespace CollectionExtensions.Tests.Extensions
{
    /// <summary>
    /// Tests the methods in the Sequence class.
    /// </summary>
    [TestClass]
    public class SequenceTester
    {
        #region CompareTo

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = new int[0];
            Sequence.CompareTo(source, other);
        }

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_Comparer_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = new int[0];
            IComparer<int> comparer = Comparer<int>.Default;
            Sequence.CompareTo(source, other, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the other collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_OtherNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = null;
            Sequence.CompareTo(source, other);
        }

        /// <summary>
        /// An exception should be thrown if the other collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCompareTo_Comparer_OtherNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sequence.CompareTo(source, other, comparer);
        }

        /// <summary>
        /// An exception should be thrown if there is no default comparer for the type.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCompareTo_NoDefaultComparer_Throws()
        {
            IEnumerable<StringBuilder> source = new StringBuilder[] { new StringBuilder() };
            IEnumerable<StringBuilder> other = new StringBuilder[] { new StringBuilder() };
            Sequence.CompareTo(source, other);
        }

        /// <summary>
        /// An exception should be thrown if there is no default comparer for the type.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCompareTo_Comparer_NoDefaultComparer_Throws()
        {
            IEnumerable<StringBuilder> source = new StringBuilder[] { new StringBuilder() };
            IEnumerable<StringBuilder> other = new StringBuilder[] { new StringBuilder() };
            IComparer<StringBuilder> comparer = null;
            Sequence.CompareTo(source, other, comparer);
        }

        /// <summary>
        /// If both collections are empty, the result should zero.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_BothEmpty_ReturnsZero()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = new int[0];
            int result = source.CompareTo(other);
            Assert.AreEqual(0, result, "The result was not zero.");
        }

        /// <summary>
        /// If the items are all equal in both collection, but the first collection is shorter, a negative should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_SourceShorter_ReturnsNegative()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3 };
            IEnumerable<int> other = new List<int>() { 1, 2, 3, 4, 5 };
            int result = source.CompareTo(other);
            Assert.IsTrue(result < 0, "The result was not negative.");
        }

        /// <summary>
        /// If the items are all equal in both collection, but the first collection is longer, a positive should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_SourceLonger_ReturnsPositive()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3, 4, 5 };
            IEnumerable<int> other = new List<int>() { 1, 2, 3 };
            int result = source.CompareTo(other);
            Assert.IsTrue(result > 0, "The result was not positive.");
        }

        /// <summary>
        /// If the items are all equal in both collection and both are the same size, a zero should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_SizesEqual_ReturnsZero()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3 };
            IEnumerable<int> other = new List<int>() { 1, 2, 3 };
            int result = source.CompareTo(other);
            Assert.AreEqual(0, result, "The result was not zero.");
        }

        /// <summary>
        /// If an item in the first collecton is smaller than an item in the second, a negative should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_ItemSmaller_ReturnsNegative()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 2 };
            IEnumerable<int> other = new List<int>() { 1, 2, 3 };
            int result = source.CompareTo(other);
            Assert.IsTrue(result < 0, "The result was not negative.");
        }

        /// <summary>
        /// If an item in the first collection is larger than an item in the second, a positive should be returned.
        /// </summary>
        [TestMethod]
        public void TestCompareTo_ItemLarger_ReturnsPositive()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 4 };
            IEnumerable<int> other = new List<int>() { 1, 2, 3 };
            int result = source.CompareTo(other, Comparer<int>.Default);
            Assert.IsTrue(result > 0, "The result was not positive.");
        }

        #endregion

        #region Except

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExcept_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = new int[0];
            Func<int, int> keySelector = i => i;
            Sequence.Except(source, other, keySelector);
        }

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExcept_KeyComparer_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = new int[0];
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            Sequence.Except(source, other, keySelector, keyComparer);
        }

        /// <summary>
        /// An exception should be thrown if the other collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExcept_OtherNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = null;
            Func<int, int> keySelector = i => i;
            Sequence.Except(source, other, keySelector);
        }

        /// <summary>
        /// An exception should be thrown if the other collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExcept_KeyComparer_OtherNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = null;
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            Sequence.Except(source, other, keySelector, keyComparer);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExcept_KeySelectorNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = new int[0];
            Func<int, int> keySelector = null;
            Sequence.Except(source, other, keySelector);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExcept_KeyComparer_KeySelectorNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = new int[0];
            Func<int, int> keySelector = null;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            Sequence.Except(source, other, keySelector, keyComparer);
        }

        /// <summary>
        /// If all of the items in the source are also in the other collection,
        /// nothing should be returned.
        /// </summary>
        [TestMethod]
        public void TestExcept_MatchAll_ReturnsEmpty()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            IEnumerable<int> other = new int[] { 1, 2, 3, 4, 5, 6 };
            Func<int, int> keySelector = i => i;
            IEnumerable<int> actual = source.Except(other, keySelector);
            IEnumerable<int> expected = Enumerable.Empty<int>();
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        /// <summary>
        /// If all of the items in the source are also in the other collection,
        /// nothing should be returned.
        /// </summary>
        [TestMethod]
        public void TestExcept_KeyComparer_MatchAll_ReturnsEmpty()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            IEnumerable<int> other = new int[] { 1, 2, 3, 4, 5, 6 };
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            IEnumerable<int> actual = source.Except(other, keySelector, keyComparer);
            IEnumerable<int> expected = Enumerable.Empty<int>();
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        /// <summary>
        /// If some of the items in the source appear in the other collection,
        /// then those items should be removed from the result.
        /// </summary>
        [TestMethod]
        public void TestExcept_MatchSome_ReturnsRemaining()
        {
            IEnumerable<int> source = Enumerable.Range(0, 10);
            IEnumerable<int> other = Enumerable.Range(0, 5).Select(i => i * 2);
            Func<int, int> keySelector = i => i;
            IEnumerable<int> actual = source.Except(other, keySelector);
            IEnumerable<int> expected = Enumerable.Range(0, 5).Select(i => i * 2 + 1);
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        /// <summary>
        /// If some of the items in the source appear in the other collection,
        /// then those items should be removed from the result.
        /// </summary>
        [TestMethod]
        public void TestExcept_KeyComparer_MatchSome_ReturnsRemaining()
        {
            IEnumerable<int> source = Enumerable.Range(0, 10);
            IEnumerable<int> other = Enumerable.Range(0, 5).Select(i => i * 2);
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            IEnumerable<int> actual = source.Except(other, keySelector, keyComparer);
            IEnumerable<int> expected = Enumerable.Range(0, 5).Select(i => i * 2 + 1);
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        /// <summary>
        /// If none of the items in the source appear in the other collection,
        /// then all of the items should be returned.
        /// </summary>
        [TestMethod]
        public void TestExcept_NoneMatch_ReturnSourceItems()
        {
            IEnumerable<int> source = Enumerable.Range(0, 5);
            IEnumerable<int> other = Enumerable.Range(5, 5);
            Func<int, int> keySelector = i => i;
            IEnumerable<int> actual = source.Except(other, keySelector);
            Assert.IsTrue(actual.SequenceEqual(source));
        }

        /// <summary>
        /// If none of the items in the source appear in the other collection,
        /// then all of the items should be returned.
        /// </summary>
        [TestMethod]
        public void TestExcept_KeyComparer_NoneMatch_ReturnSourceItems()
        {
            IEnumerable<int> source = Enumerable.Range(0, 5);
            IEnumerable<int> other = Enumerable.Range(5, 5);
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = null;
            IEnumerable<int> actual = source.Except(other, keySelector, keyComparer);
            Assert.IsTrue(actual.SequenceEqual(source));
        }

        #endregion

        #region ForEach

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestForEach_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            Action<int> action = i => { };
            Sequence.ForEach(source, action);
        }

        /// <summary>
        /// An exception should be thrown if the action is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestForEach_ActionNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            Action<int> action = null;
            Sequence.ForEach(source, action);
        }

        /// <summary>
        /// The action should be performed for each item.
        /// </summary>
        [TestMethod]
        public void TestForEach_CallsActionForEachItem()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3 };
            List<int> visited = new List<int>();
            Action<int> action = i => visited.Add(i);
            source.ForEach(action);
            Assert.IsTrue(source.SequenceEqual(visited), "Not all the items were visited.");
        }

        #endregion

        #region Intersect

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersect_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = new int[0];
            Func<int, int> keySelector = i => i;
            Sequence.Intersect(source, other, keySelector);
        }

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersect_KeyComparer_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = new int[0];
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            Sequence.Intersect(source, other, keySelector, keyComparer);
        }

        /// <summary>
        /// An exception should be thrown if the other collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersect_OtherNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = null;
            Func<int, int> keySelector = i => i;
            Sequence.Intersect(source, other, keySelector);
        }

        /// <summary>
        /// An exception should be thrown if the other collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersect_KeyComparer_OtherNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = null;
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            Sequence.Intersect(source, other, keySelector, keyComparer);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersect_KeySelectorNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = new int[0];
            Func<int, int> keySelector = null;
            Sequence.Intersect(source, other, keySelector);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestIntersect_KeyComparer_KeySelectorNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            IEnumerable<int> other = new int[0];
            Func<int, int> keySelector = null;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            Sequence.Intersect(source, other, keySelector, keyComparer);
        }

        /// <summary>
        /// If all of the items in the source are also in the other collection,
        /// then all of the items should be returned.
        /// </summary>
        [TestMethod]
        public void TestIntersect_MatchAll_ReturnsSourceItems()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            IEnumerable<int> other = new int[] { 1, 2, 3, 4, 5, 6 };
            Func<int, int> keySelector = i => i;
            IEnumerable<int> actual = source.Intersect(other, keySelector);
            Assert.IsTrue(actual.SequenceEqual(source));
        }

        /// <summary>
        /// If all of the items in the source are also in the other collection,
        /// then all of the items should be returned.
        /// </summary>
        [TestMethod]
        public void TestIntersect_KeyComparer_MatchAll_ReturnsSourceItems()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            IEnumerable<int> other = new int[] { 1, 2, 3, 4, 5, 6 };
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            IEnumerable<int> actual = source.Intersect(other, keySelector, keyComparer);
            Assert.IsTrue(actual.SequenceEqual(source));
        }

        /// <summary>
        /// If some of the items in the source appear in the other collection,
        /// then those items should be returned.
        /// </summary>
        [TestMethod]
        public void TestIntersect_MatchSome_ReturnsCommon()
        {
            IEnumerable<int> source = Enumerable.Range(0, 10);
            IEnumerable<int> other = Enumerable.Range(0, 6).Select(i => i * 2);
            Func<int, int> keySelector = i => i;
            IEnumerable<int> actual = source.Intersect(other, keySelector);
            IEnumerable<int> expected = Enumerable.Range(0, 5).Select(i => i * 2);
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        /// <summary>
        /// If some of the items in the source appear in the other collection,
        /// then those items should be returned.
        /// </summary>
        [TestMethod]
        public void TestIntersect_KeyComparer_MatchSome_ReturnsCommon()
        {
            IEnumerable<int> source = Enumerable.Range(0, 10);
            IEnumerable<int> other = Enumerable.Range(0, 6).Select(i => i * 2);
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = EqualityComparer<int>.Default;
            IEnumerable<int> actual = source.Intersect(other, keySelector, keyComparer);
            IEnumerable<int> expected = Enumerable.Range(0, 5).Select(i => i * 2);
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        /// <summary>
        /// If none of the items in the source appear in the other collection,
        /// then nothing should be returned.
        /// </summary>
        [TestMethod]
        public void TestIntersect_NoneMatch_ReturnNothing()
        {
            IEnumerable<int> source = Enumerable.Range(0, 5);
            IEnumerable<int> other = Enumerable.Range(5, 5);
            Func<int, int> keySelector = i => i;
            IEnumerable<int> actual = source.Intersect(other, keySelector);
            IEnumerable<int> expected = Enumerable.Empty<int>();
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        /// <summary>
        /// If none of the items in the source appear in the other collection,
        /// then nothing should be returned.
        /// </summary>
        [TestMethod]
        public void TestIntersect_KeyComparer_NoneMatch_ReturnNothing()
        {
            IEnumerable<int> source = Enumerable.Range(0, 5);
            IEnumerable<int> other = Enumerable.Range(5, 5);
            Func<int, int> keySelector = i => i;
            IEqualityComparer<int> keyComparer = null;
            IEnumerable<int> actual = source.Intersect(other, keySelector, keyComparer);
            IEnumerable<int> expected = Enumerable.Empty<int>();
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        #endregion

        #region MaxByKey

        /// <summary>
        /// An exception should be thrown if the collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMaxByKey_NullCollection_Throws()
        {
            IEnumerable<int> collection = null;
            Func<int, int> selector = i => i;
            Sequence.MaxByKey(collection, selector);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMaxByKey_NullKeySelector_Throws()
        {
            IEnumerable<int> collection = new int[0];
            Func<int, int> selector = null;
            collection.MaxByKey(selector);
        }

        /// <summary>
        /// An exception should be thrown if the key returned by the selector
        /// could not be compared.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMaxByKey_KeyNotComparable_Throws()
        {
            IEnumerable<int> collection = new int[] { 1, 2, 3 };
            Func<int, StringBuilder> selector = i => new StringBuilder();
            collection.MaxByKey(selector);
        }

        /// <summary>
        /// An exception should be thrown if the collection is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMaxByKey_EmptyCollection_Throws()
        {
            IEnumerable<int> collection = new int[0];
            Func<int, int> selector = i => i;
            collection.MaxByKey(selector);
        }

        /// <summary>
        /// If the collection only has one item, it should be returned.
        /// </summary>
        [TestMethod]
        public void TestMaxByKey_OneItem_ReturnsItem()
        {
            IEnumerable<int> collection = new int[] { 1 };
            Func<int, int> selector = i => i;
            int max = collection.MaxByKey(selector);
            Assert.AreEqual(1, max);
        }

        /// <summary>
        /// If the first item is the max, it should be returned.
        /// </summary>
        [TestMethod]
        public void TestMaxByKey_FirstItemMax_ReturnsMax()
        {
            IEnumerable<int> collection = new int[] { 30, 10, 20 };
            Func<int, int> selector = i => i / 10;
            int max = collection.MaxByKey(selector);
            Assert.AreEqual(30, max);
        }

        /// <summary>
        /// If the middle item is the max, it should be returned.
        /// </summary>
        [TestMethod]
        public void TestMaxByKey_MiddleItemMax_ReturnsMax()
        {
            IEnumerable<int> collection = new int[] { 10, 30, 20 };
            Func<int, int> selector = i => i / 10;
            int max = collection.MaxByKey(selector);
            Assert.AreEqual(30, max);
        }

        /// <summary>
        /// If the last item is the max, it should be returned.
        /// </summary>
        [TestMethod]
        public void TestMaxByKey_LastItemMax_ReturnsMax()
        {
            IEnumerable<int> collection = new int[] { 20, 10, 30 };
            Func<int, int> selector = i => i / 10;
            int max = collection.MaxByKey(selector);
            Assert.AreEqual(30, max);
        }

        /// <summary>
        /// If the max appears multiple times, the first instance should be returned.
        /// </summary>
        [TestMethod]
        public void TestMaxByKey_MultipleMaxes_ReturnsFirstMax()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Bob", "Taco", "Salad" };
            Func<string, int> selector = s => s.Length;
            string max = collection.MaxByKey(selector);
            Assert.AreEqual("Hello", max);
        }

        #endregion

        #region MinByKey

        /// <summary>
        /// An exception should be thrown if the collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinByKey_NullCollection_Throws()
        {
            IEnumerable<int> collection = null;
            Func<int, int> selector = i => i;
            Sequence.MinByKey(collection, selector);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMinByKey_NullKeySelector_Throws()
        {
            IEnumerable<int> collection = new int[0];
            Func<int, int> selector = null;
            collection.MinByKey(selector);
        }

        /// <summary>
        /// An exception should be thrown if the key returned by the selector
        /// could not be compared.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMinByKey_KeyNotComparable_Throws()
        {
            IEnumerable<int> collection = new int[] { 1, 2, 3 };
            Func<int, StringBuilder> selector = i => new StringBuilder();
            collection.MinByKey(selector);
        }

        /// <summary>
        /// An exception should be thrown if the collection is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestMinByKey_EmptyCollection_Throws()
        {
            IEnumerable<int> collection = new int[0];
            Func<int, int> selector = i => i;
            collection.MinByKey(selector);
        }

        /// <summary>
        /// If the collection only has one item, it should be returned.
        /// </summary>
        [TestMethod]
        public void TestMinByKey_OneItem_ReturnsItem()
        {
            IEnumerable<int> collection = new int[] { 1 };
            Func<int, int> selector = i => i;
            int min = collection.MinByKey(selector);
            Assert.AreEqual(1, min);
        }

        /// <summary>
        /// If the first item is the min, it should be returned.
        /// </summary>
        [TestMethod]
        public void TestMinByKey_FirstItemMin_ReturnsMin()
        {
            IEnumerable<int> collection = new int[] { 10, 30, 20 };
            Func<int, int> selector = i => i / 10;
            int min = collection.MinByKey(selector);
            Assert.AreEqual(10, min);
        }

        /// <summary>
        /// If the middle item is the min, it should be returned.
        /// </summary>
        [TestMethod]
        public void TestMinByKey_MiddleItemMin_ReturnsMin()
        {
            IEnumerable<int> collection = new int[] { 30, 10, 20 };
            Func<int, int> selector = i => i / 10;
            int min = collection.MinByKey(selector);
            Assert.AreEqual(10, min);
        }

        /// <summary>
        /// If the last item is the min, it should be returned.
        /// </summary>
        [TestMethod]
        public void TestMinByKey_LastItemMin_ReturnsMin()
        {
            IEnumerable<int> collection = new int[] { 20, 30, 10 };
            Func<int, int> selector = i => i / 10;
            int min = collection.MinByKey(selector);
            Assert.AreEqual(10, min);
        }

        /// <summary>
        /// If the min appears multiple times, the first instance should be returned.
        /// </summary>
        [TestMethod]
        public void TestMinByKey_MultipleMines_ReturnsFirstMin()
        {
            IEnumerable<string> collection = new string[] { "Bob", "Bobby", "Jon", "Salad" };
            Func<string, int> selector = s => s.Length;
            string min = collection.MinByKey(selector);
            Assert.AreEqual("Bob", min);
        }

        #endregion

        #region RandomSamples

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamples_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            int numberOfSamples = 0;
            Sequence.RandomSamples(source, numberOfSamples);
        }

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamples_Random_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            int numberOfSamples = 0;
            Random random = new Random();
            Sequence.RandomSamples(source, numberOfSamples, random);
        }

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamples_Generator_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            int numberOfSamples = 0;
            Func<int> generator = () => 0;
            Sequence.RandomSamples(source, numberOfSamples, generator);
        }

        /// <summary>
        /// An exception should be thrown if the requested number of samples is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomSamples_IndexNegative_Throws()
        {
            IEnumerable<int> source = new int[0];
            int numberOfSamples = -1;
            Sequence.RandomSamples(source, numberOfSamples);
        }

        /// <summary>
        /// An exception should be thrown if the requested number of samples is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomSamples_Random_IndexNegative_Throws()
        {
            IEnumerable<int> source = new int[0];
            int numberOfSamples = -1;
            Random random = new Random();
            Sequence.RandomSamples(source, numberOfSamples, random);
        }

        /// <summary>
        /// An exception should be thrown if the requested number of samples is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRandomSamples_Generator_IndexNegative_Throws()
        {
            IEnumerable<int> source = new int[0];
            int numberOfSamples = -1;
            Func<int> generator = () => 0;
            Sequence.RandomSamples(source, numberOfSamples, generator);
        }

        /// <summary>
        /// An exception should be thrown if the random number generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamples_RandomNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            int numberOfSamples = 0;
            Random random = null;
            Sequence.RandomSamples(source, numberOfSamples, random);
        }

        /// <summary>
        /// An exception should be thrown if the number generator is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRandomSamples_GeneratorNull_Throws()
        {
            IEnumerable<int> source = new int[0];
            int numberOfSamples = 0;
            Func<int> generator = null;
            Sequence.RandomSamples(source, numberOfSamples, generator);
        }

        /// <summary>
        /// If the source is smaller than the requested number of items, a copy
        /// of the source should be returned.
        /// </summary>
        [TestMethod]
        public void TestRandomSamples_SampleSizeBiggerThanSource_CopiesAllItems()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3, 4, 5 };
            int numberOfSamples = 6;
            IEnumerable<int> actual = source.RandomSamples(numberOfSamples);
            Assert.AreEqual(5, actual.Count(), "The wrong number of items were returned.");
            Assert.IsTrue(source.OrderBy(i => i).SequenceEqual(actual.OrderBy(i => i)), "The wrong items were returned.");
        }

        /// <summary>
        /// We should get the number of items we request and get no duplicates.
        /// </summary>
        [TestMethod]
        public void TestRandomSamples_Random_GrabsItemsAtRandom()
        {
            IEnumerable<int> source = Enumerable.Range(0, 100);
            int numberOfSamples = 5;
            Random random = new Random();
            IEnumerable<int> actual = source.RandomSamples(numberOfSamples, random);
            Assert.AreEqual(numberOfSamples, actual.Count(), "The wrong number of items were returned.");
            Assert.IsTrue(actual.Distinct().SequenceEqual(actual), "Some items appeared multiple times.");
            Assert.IsTrue(actual.All(item => source.Contains(item)), "An unknown value appeared in the results.");
        }

        /// <summary>
        /// We should be able to use a generator that returns negatives.
        /// </summary>
        [TestMethod]
        public void TestRandomSamples_GenerateNegative()
        {
            IEnumerable<int> source = Enumerable.Range(0, 100);
            int numberOfSamples = 5;
            Random random = new Random();
            Func<int> generator = () => -random.Next();
            IEnumerable<int> actual = source.RandomSamples(numberOfSamples, generator);
            Assert.AreEqual(numberOfSamples, actual.Count(), "The wrong number of items were returned.");
            Assert.IsTrue(actual.Distinct().SequenceEqual(actual), "Some items appeared multiple times.");
            Assert.IsTrue(actual.All(item => source.Contains(item)), "An unknown value appeared in the results.");
        }

        #endregion

        #region RotateLeft

        /// <summary>
        /// An exception should be thrown if the source collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRotateLeft_SourceNull_Throws()
        {
            IEnumerable<int> source = null;
            int shift = 0;
            Sequence.RotateLeft(source, shift);
        }

        /// <summary>
        /// If the shift is zero, the original collection should be returned.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_ShiftZero_ReturnsInOriginalOrder()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3 };
            int shift = 0;
            IEnumerable<int> actual = Sequence.RotateLeft(source, shift);
            Assert.IsTrue(actual.SequenceEqual(source), "The items should have been returned in the original order.");
        }

        /// <summary>
        /// If the source is a List, it should be cast and then rotated.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_SourceIsList_Rotates()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3 };
            int shift = 1;
            IEnumerable<int> actual = Sequence.RotateLeft(source, shift);
            IEnumerable<int> expected = new int[] { 2, 3, 1 };
            Assert.IsTrue(actual.SequenceEqual(expected), "The items were not in the expected order.");
        }

        /// <summary>
        /// If the source is an array, it should be cast and then rotated.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_SourceIsArray_Rotates()
        {
            IEnumerable<int> source = new int[] { 1, 2, 3 };
            int shift = 1;
            IEnumerable<int> actual = Sequence.RotateLeft(source, shift);
            IEnumerable<int> expected = new int[] { 2, 3, 1 };
            Assert.IsTrue(actual.SequenceEqual(expected), "The items were not in the expected order.");
        }

        /// <summary>
        /// If the source is a Collection, it should be cast and then rotated.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_SourceIsCollection_Rotates()
        {
            IEnumerable<int> source = new Collection<int>() { 1, 2, 3 };
            int shift = 1;
            IEnumerable<int> actual = Sequence.RotateLeft(source, shift);
            IEnumerable<int> expected = new int[] { 2, 3, 1 };
            Assert.IsTrue(actual.SequenceEqual(expected), "The items were not in the expected order.");
        }

        /// <summary>
        /// If the source is an IList, it should be cast and then rotated.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_SourceIsIList_Rotates()
        {
            IEnumerable<int> source = new List<int>() { 1, 2, 3 }.ToSublist();
            int shift = 1;
            IEnumerable<int> actual = Sequence.RotateLeft(source, shift);
            IEnumerable<int> expected = new int[] { 2, 3, 1 };
            Assert.IsTrue(actual.SequenceEqual(expected), "The items were not in the expected order.");
        }

        /// <summary>
        /// If the source is not indexable, it should be cast and then rotated.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_SourceIsEnumerable_Rotates()
        {
            IEnumerable<int> source = Enumerable.Range(1, 3);
            int shift = 1;
            IEnumerable<int> actual = Sequence.RotateLeft(source, shift);
            IEnumerable<int> expected = new int[] { 2, 3, 1 };
            Assert.IsTrue(actual.SequenceEqual(expected), "The items were not in the expected order.");
        }

        /// <summary>
        /// If the shift is larger than the collection, then the
        /// algorithm acts as if the collection is rotated a complete
        /// rotation, as many times as necessary.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_ShiftLargerThanCount_FullRotation()
        {
            IEnumerable<int> source = Enumerable.Range(1, 3);
            int shift = 4;
            IEnumerable<int> actual = Sequence.RotateLeft(source, shift);
            IEnumerable<int> expected = new int[] { 2, 3, 1 };
            Assert.IsTrue(actual.SequenceEqual(expected), "The items were not in the expected order.");
        }

        /// <summary>
        /// If the shift is negative, the collection will be rotated
        /// to the right.
        /// </summary>
        [TestMethod]
        public void TestRotateLeft_ShiftNegative_RotatedRight()
        {
            IEnumerable<int> source = Enumerable.Range(1, 3);
            int shift = -2;
            IEnumerable<int> actual = Sequence.RotateLeft(source, shift);
            IEnumerable<int> expected = new int[] { 2, 3, 1 };
            Assert.IsTrue(actual.SequenceEqual(expected), "The items were not in the expected order.");
        }

        #endregion

        #region ToOrderedDictionary

        /// <summary>
        /// An exception should be thrown if the collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToOrderedDictionary_NullCollection_ThrowsException()
        {
            IEnumerable<string> collection = null;
            Func<string, int> keySelector = s => s.Length;
            Sequence.ToOrderedDictionary(collection, keySelector);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToOrderedDictionary_NullKeySelector_ThrowsException()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Goodbye", "Farewell", "Greetings" };
            Func<string, int> keySelector = null;
            Sequence.ToOrderedDictionary(collection, keySelector);
        }

        /// <summary>
        /// We should be able to build an OrderedDictionary from a collection by extracting keys
        /// from the items.
        /// </summary>
        [TestMethod]
        public void TestToOrderedDictionary_BuildsOrderedDictionary()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Goodbye", "Farewell", "Greetings" };
            Func<string, int> keySelector = s => s.Length;
            var dictionary = collection.ToOrderedDictionary(keySelector);
            Assert.AreEqual(4, dictionary.Count);
        }

        /// <summary>
        /// An exception should be thrown if the collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToOrderedDictionary_WithKeyComparer_NullCollection_ThrowsException()
        {
            IEnumerable<string> collection = null;
            Func<string, int> keySelector = s => s.Length;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sequence.ToOrderedDictionary(collection, keySelector, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the key selector is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestToOrderedDictionary_WithComparer_NullKeySelector_ThrowsException()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Goodbye", "Farewell", "Greetings" };
            Func<string, int> keySelector = null;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            Sequence.ToOrderedDictionary(collection, keySelector, comparer);
        }

        /// <summary>
        /// We should be able to build an OrderedDictionary from a collection by extracting keys
        /// from the items.
        /// </summary>
        [TestMethod]
        public void TestToOrderedDictionary_WithComparer_BuildsOrderedDictionary()
        {
            IEnumerable<string> collection = new string[] { "Hello", "Goodbye", "Farewell", "Greetings" };
            Func<string, int> keySelector = s => s.Length;
            IEqualityComparer<int> comparer = EqualityComparer<int>.Default;
            var dictionary = collection.ToOrderedDictionary(keySelector, comparer);
            Assert.AreSame(comparer, dictionary.Comparer, "The comparer was not set.");
            var expected = new Dictionary<int, string>()
            {
                { 5, "Hello"},
                { 7, "Goodbye" },
                { 8, "Farewell" },
                { 9, "Greetings" },
            };
            Assert.IsTrue(dictionary.DictionaryEquals(expected), "The dictionary was generated as expected.");
        }

        #endregion
    }
}
