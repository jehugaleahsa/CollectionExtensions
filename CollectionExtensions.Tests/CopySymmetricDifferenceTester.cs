﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CollectionExtensions;

namespace CollectionExtensions.Test
{
    /// <summary>
    /// Tests the CopySymmetricDifference methods.
    /// </summary>
    [TestClass]
    public class CopySymmetricDifferenceTester
    {
        #region Real World Examples

        /// <summary>
        /// we can find the items that are unique across both lists.
        /// </summary>
        [TestMethod]
        public void TestCopySymmetricDifference_FindUniqueAcrossLists()
        {
            Random random = new Random();

            // build two lists
            var list1 = new List<int>(100);
            Sublist.Grow(list1, 100, () => random.Next(100));
            var list2 = new List<int>(100);
            Sublist.Grow(list2, 100, () => random.Next(100));

            // make the lists sets
            Sublist.RemoveRange(list1.ToSublist(Sublist.MakeSet(list1.ToSublist())));
            Sublist.RemoveRange(list2.ToSublist(Sublist.MakeSet(list2.ToSublist())));

            // find the unique values
            var difference = new List<int>(100);
            Sublist.Grow(difference, 100, 0); // can't be bigger
            int index = Sublist.CopySymmetricDifference(list1.ToSublist(), list2.ToSublist(), difference.ToSublist());
            Sublist.RemoveRange(difference.ToSublist(index));

            // this is the opposite of the intersection, so they should share no items
            var intersection = new List<int>();
            Sublist.AddIntersection(list1.ToSublist(), list2.ToSublist(), intersection.ToSublist());

            bool result = Sublist.ContainsAny(intersection.ToSublist(), difference.ToSublist());
            Assert.IsFalse(result, "Found items in common in the intersection and symmetric difference.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopySymmetricDifference(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_WithComparer_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopySymmetricDifference(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the first list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_WithComparison_NullList1_Throws()
        {
            Sublist<List<int>, int> list1 = null;
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopySymmetricDifference(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.CopySymmetricDifference(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_WithComparer_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopySymmetricDifference(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the second list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_WithComparison_NullList2_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopySymmetricDifference(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.CopySymmetricDifference(list1, list2, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_WithComparer_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            IComparer<int> comparer = Comparer<int>.Default;
            Sublist.CopySymmetricDifference(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the destination is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_WithComparison_NullDestination_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            Sublist.CopySymmetricDifference(list1, list2, destination, comparison);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_NullComparer_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            IComparer<int> comparer = null;
            Sublist.CopySymmetricDifference(list1, list2, destination, comparer);
        }

        /// <summary>
        /// An exception should be thrown if the comparison is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopySymmetricDifference_NullComparison_Throws()
        {
            Sublist<List<int>, int> list1 = new List<int>();
            Sublist<List<int>, int> list2 = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int, int> comparison = null;
            Sublist.CopySymmetricDifference(list1, list2, destination, comparison);
        }

        #endregion

        /// <summary>
        /// The symmetric difference of equal sets is nothing.
        /// </summary>
        [TestMethod]
        public void TestCopySymmetricDifference_EqualLists_CopiesNothing()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3, 4, 5 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, });
            IComparer<int> comparer = Comparer<int>.Default;

            CopyTwoSourcesResult result = Sublist.CopySymmetricDifference(list, list, destination, comparer);
            Assert.AreEqual(list.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(0, result.DestinationOffset, "The destination offset was wrong.");
            int[] expected = { 0, 0, 0, 0, 0, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// The symmetric difference of disjoint sets is every item.
        /// </summary>
        [TestMethod]
        public void TestCopySymmetricDifference_Disjoint_AddsAll()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, 0 });

            CopyTwoSourcesResult result = Sublist.CopySymmetricDifference(list1, list2, destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong index was returned.");
            int[] expected = { 1, 2, 3, 4, 5, 6 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the first list is shorter, the remaining items in the second list are copied.
        /// </summary>
        [TestMethod]
        public void TestCopySymmetricDifference_List1Shorter_RemainingCopied()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0, });

            CopyTwoSourcesResult result = Sublist.CopySymmetricDifference(list1, list2, destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset, "The wrong index was returned.");
            int[] expected = { 3, 0, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If the second list is shorter, the remaining items in the first list are copied.
        /// </summary>
        [TestMethod]
        public void TestCopySymmetricDifference_List2Shorter_RemainingCopied()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var list2 = TestHelper.Wrap(new List<int>() { 1, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });

            CopyTwoSourcesResult result = Sublist.CopySymmetricDifference(list1, list2, destination);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset, "The wrong index was returned.");
            int[] expected = { 3, 0, 0, 0, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If we reverse the comparison, the items must match the sort criteria.
        /// </summary>
        [TestMethod]
        public void TestCopySymmetricDifference_ReversedOrder()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 3, 2, 1 });
            var list2 = TestHelper.Wrap(new List<int>() { 3, 2 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0, 0, 0 });
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);

            CopyTwoSourcesResult result = Sublist.CopySymmetricDifference(list1, list2, destination, comparison);
            Assert.AreEqual(list1.Count, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(list2.Count, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(1, result.DestinationOffset, "The wrong index was returned.");
            int[] expected = { 1, 0, 0, 0, 0, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// If there is not enough space in the destination, the algorithm is stopped prematurely.
        /// </summary>
        [TestMethod]
        public void TestCopySymmetricDifference_DestinationTooSmall_StopsPrematurely()
        {
            var list1 = TestHelper.Wrap(new List<int>() { 1, 3, 5 });
            var list2 = TestHelper.Wrap(new List<int>() { 2, 4, 6 });
            var destination = TestHelper.Wrap(new List<int>() { 0, 0, 0 });

            CopyTwoSourcesResult result = Sublist.CopySymmetricDifference(list1, list2, destination);
            Assert.AreEqual(2, result.SourceOffset1, "The first source offset was wrong.");
            Assert.AreEqual(1, result.SourceOffset2, "The second source offset was wrong.");
            Assert.AreEqual(destination.Count, result.DestinationOffset, "The wrong index was returned.");
            int[] expected = { 1, 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not copied as expected.");

            TestHelper.CheckHeaderAndFooter(list1);
            TestHelper.CheckHeaderAndFooter(list2);
            TestHelper.CheckHeaderAndFooter(destination);
        }
    }
}