﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectionExtensions.Test
{
    /// <summary>
    /// Tests the Sublist class.
    /// </summary>
    [TestClass]
    public class SublistTester
    {
        #region ToSublist

        /// <summary>
        /// If we just call ToSublist with no parameters, it wraps the entire list.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnList_WrapsEntireList()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            var sublist = list.ToSublist();
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(0, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(list.Count, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we just call ToSublist with no parameters, it wraps the entire array.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnArray_WrapsEntireList()
        {
            int[] array = new int[] { 1, 2, 3 };
            var sublist = array.ToSublist();
            Assert.AreSame(array, sublist.List, "The backing array was not set.");
            Assert.AreEqual(0, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(array.Length, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we just call ToSublist with no parameters, it wraps the entire collection.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnCollection_WrapsEntireCollection()
        {
            Collection<int> list = new Collection<int>() { 1, 2, 3 };
            var sublist = list.ToSublist();
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(0, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(list.Count, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset, the rest of the list is wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnList_WithOffset_WrapsRemaining()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            var sublist = list.ToSublist(1);
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(2, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset, the rest of the array is wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnArray_WithOffset_WrapsRemaining()
        {
            int[] array = new int[] { 1, 2, 3 };
            var sublist = array.ToSublist(1);
            Assert.AreSame(array, sublist.List, "The backing array was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(2, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset, the rest of the collection is wrapped.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnCollection_WithOffset_WrapsRemaining()
        {
            Collection<int> list = new Collection<int>() { 1, 2, 3 };
            var sublist = list.ToSublist(1);
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(2, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset and count, that portion of the list is returned.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnList_WithOffsetAndCount_WrapsRemaining()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            var sublist = list.ToSublist(1, 1);
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(1, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset and count, that portion of the list is returned.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnArray_WithOffsetAndCount_WrapsRemaining()
        {
            int[] array = new int[] { 1, 2, 3 };
            var sublist = array.ToSublist(1, 1);
            Assert.AreSame(array, sublist.List, "The backing array was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(1, sublist.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we call ToSublist with an offset and count, that portion of the collection is returned.
        /// </summary>
        [TestMethod]
        public void TestToSublist_OnCollection_WithOffsetAndCount_WrapsRemaining()
        {
            Collection<int> list = new Collection<int>() { 1, 2, 3 };
            var sublist = list.ToSublist(1, 1);
            Assert.AreSame(list, sublist.List, "The backing list was not set.");
            Assert.AreEqual(1, sublist.Offset, "The offset was not zero.");
            Assert.AreEqual(1, sublist.Count, "The count was wrong.");
        }

        #endregion

        #region Ctor

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullList_Throws()
        {
            List<int> list = null;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithOffset_NullList_Throws()
        {
            List<int> list = null;
            int offset = 0;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset);
        }

        /// <summary>
        /// An exception should be thrown if the list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_WithOffsetAndCount_NullList_Throws()
        {
            List<int> list = null;
            int offset = 0;
            int count = 0;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_NegativeOffset_Throws()
        {
            List<int> list = new List<int>();
            int offset = -1;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_WithCount_NegativeOffset_Throws()
        {
            List<int> list = new List<int>();
            int offset = -1;
            int count = 0;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_OffsetTooBig_Throws()
        {
            List<int> list = new List<int>();
            int offset = 1;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_WithCount_OffsetTooBig_Throws()
        {
            List<int> list = new List<int>();
            int offset = 1;
            int count = 0;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_NegativeCount_Throws()
        {
            List<int> list = new List<int>();
            int offset = 0;
            int count = -1;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_CountTooBig_Throws()
        {
            List<int> list = new List<int>();
            int offset = 0;
            int count = 1;
            Sublist<List<int>, int> sublist = new Sublist<List<int>, int>(list, offset, count);
        }

        /// <summary>
        /// The ctor just taking a list, should wrap the entire list.
        /// </summary>
        [TestMethod]
        public void TestCtor_WrapsEntireList()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4 };
            var sublist = new Sublist<List<int>, int>(list);
            Assert.AreSame(list, sublist.List, "The list was not set as a backing field.");
            Assert.AreEqual(list.Count, sublist.Count, "The sublist had the wrong count.");
            Assert.AreEqual(0, sublist.Offset, "The sublist had the wrong offset.");
            Assert.AreEqual(((IList<int>)list).IsReadOnly, ((IList<int>)sublist).IsReadOnly,
                "The read-only property doesn't reflect the underlying list.");
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), sublist), "The sublist did not contain the expected items.");
        }

        /// <summary>
        /// The ctor taking an offset defaults the count to the remaining number of items.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithOffset_WrapsRemainingList()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4 };
            var sublist = new Sublist<List<int>, int>(list, 1);
            Assert.AreSame(list, sublist.List, "The list was not set as a backing field.");
            Assert.AreEqual(list.Count - 1, sublist.Count, "The sublist had the wrong count.");
            Assert.AreEqual(1, sublist.Offset, "The sublist had the wrong offset.");
            Assert.AreEqual(((IList<int>)list).IsReadOnly, ((IList<int>)sublist).IsReadOnly,
                "The read-only property doesn't reflect the underlying list.");
            int[] expected = { 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), sublist), "The sublist did not contain the expected items.");
        }

        /// <summary>
        /// The ctor taking a count limits the number of items to a splice of the underlying list.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithOffsetAndCount_CreatesSplice()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4 };
            var sublist = new Sublist<List<int>, int>(list, 1, 2);
            Assert.AreSame(list, sublist.List, "The list was not set as a backing field.");
            Assert.AreEqual(2, sublist.Count, "The sublist had the wrong count.");
            Assert.AreEqual(1, sublist.Offset, "The sublist had the wrong offset.");
            Assert.AreEqual(((IList<int>)list).IsReadOnly, ((IList<int>)sublist).IsReadOnly,
                "The read-only property doesn't reflect the underlying list.");
            int[] expected = { 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), sublist), "The sublist did not contain the expected items.");
        }

        #endregion

        #region Nest

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_NegativeOffset_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = -1;
            list.Nest(offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_WithCount_NegativeOffset_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = -1;
            int count = 0;
            list.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the offset is past the end of the sublist.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_OffsetTooBig_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = 1;
            list.Nest(offset);
        }

        /// <summary>
        /// An exception should be thrown if the offset is past the end of the sublist.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_WithCount_OffsetTooBig_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = 1;
            int count = 0;
            list.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_NegativeCount_Throws()
        {
            var list = new List<int>().ToSublist();
            int offset = 0;
            int count = -1;
            list.Nest(offset, count);
        }

        /// <summary>
        /// An exception should be thrown if the count is greater than the remaining items.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestNest_CountTooBig_Throws()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5 }.ToSublist();
            int offset = 1;
            int count = 5; // one too big
            list.Nest(offset, count);
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_OffsetToPopFront()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1);
            int[] expected = { 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_ReadOnly_OffsetToPopFront()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1);
            int[] expected = { 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_Sublist_OffsetToPopFront()
        {
            IMutableSublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1);
            int[] expected = { 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the offset and there's no room to the right, we're essentially popping off the front.
        /// </summary>
        [TestMethod]
        public void TestNest_Expandable_OffsetToPopFront()
        {
            IExpandableSublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1);
            int[] expected = { 2, 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we nest a nested Sublist, the count should be adjusted as expected.
        /// </summary>
        [TestMethod]
        public void TestNest_DoublyNested_AdjustsCount()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1).Nest(1);
            int[] expected = { 3, 4, 5, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the first item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_CountToPopBack()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(0, list.Count - 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_ReadOnly_CountToPopBack()
        {
            IReadOnlySublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(0, list.Count - 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_Sublist_CountToPopBack()
        {
            IMutableSublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(0, list.Count - 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_Expandable_CountToPopBack()
        {
            IExpandableSublist<List<int>, int> list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(0, list.Count - 1);
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// If we just change the count, we're essentially popping off the back.
        /// </summary>
        [TestMethod]
        public void TestNest_ShiftAndShrink()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, }.ToSublist();
            var nested = list.Nest(1, list.Count - 2); // we want to remove the front and back, two items
            int[] expected = { 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), nested), "The offset did not pop the last item.");
        }

        /// <summary>
        /// We can use Sublist to represent partitions within a list.
        /// </summary>
        [TestMethod]
        public void TestNest_RepresentPartitions()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.ToSublist();
            int partition = Sublist.Partition(list, i => i % 2 == 0); // put evens in the front
            var evens = list.Nest(0, partition);
            var odds = list.Nest(partition);
            Assert.IsTrue(Sublist.TrueForAll(evens, i => i % 2 == 0), "Not all evens in the first nested list.");
            Assert.IsTrue(Sublist.TrueForAll(odds, i => i % 2 != 0), "Not all odds in the second nested list.");
        }

        #endregion

        #region Offset

        /// <summary>
        /// An exception should be thrown if the offset is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOffset_NegativeOffset_Throws()
        {
            var list = new List<int>().ToSublist();
            list.Offset = -1;
        }

        /// <summary>
        /// An exception should be thrown if the offset is greater than the size of the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOffset_OffsetTooBig_Throws()
        {
            var list = new List<int>().ToSublist();
            list.Offset = 1;
        }

        /// <summary>
        /// If we shift the offset to the right, the whole splice should shift.
        /// </summary>
        [TestMethod]
        public void TestOffset_ShiftOffsetRight_CountShrinks()
        {
            var list = new List<int>() { 1, 2, 3, }.ToSublist(0, 2);
            list.Offset = 1;
            Assert.AreEqual(2, list.Count, "The count did not shrink.");
            int[] expected = { 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The list was not shifted to the right.");
        }

        /// <summary>
        /// If we shift the offset to the right, the count should shrink automatically, if needed.
        /// </summary>
        [TestMethod]
        public void TestOffset_ShiftOffsetRight_ShiftsSplice()
        {
            var list = new List<int>() { 1, 2, 3, }.ToSublist();
            list.Offset = 1;
            Assert.AreEqual(2, list.Count, "The count did not shrink.");
            int[] expected = { 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The list was not shifted to the right.");
        }

        #endregion

        #region Count

        /// <summary>
        /// An exception should be thrown if the count is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCount_Setter_NegativeCount_Throws()
        {
            var list = new List<int>().ToSublist();
            list.Count = -1;
        }

        /// <summary>
        /// An exception should be thrown if the count is larger than the size of the underlying list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCount_Setter_CountTooBig_Throws()
        {
            var list = new List<int>().ToSublist();
            list.Count = 1;
        }

        /// <summary>
        /// The count can be modified to grow or shink the sublist.
        /// </summary>
        [TestMethod]
        public void TestCount_Setter_CountGrowing_GrowsSplice()
        {
            var list = new List<int>() { 1, 2, 3, }.ToSublist();
            list.Count = 1;
            int[] expected1 = { 1 };
            Assert.IsTrue(Sublist.AreEqual(expected1.ToSublist(), list), "A sublist of size 1 was wrong.");
            list.Count = 2;
            int[] expected2 = { 1, 2 };
            Assert.IsTrue(Sublist.AreEqual(expected2.ToSublist(), list), "A sublist of size 2 was wrong.");
            list.Count = 3;
            int[] expected3 = { 1, 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected3.ToSublist(), list), "A sublist of size 3 was wrong.");
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// IndexOf should return -1 when the item is not in the list.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_NoSuchItem_ReturnsNegativeOne()
        {
            var list = new int[] { 1, 2, 3, }.ToSublist();
            int result = list.IndexOf(4);
            Assert.AreEqual<int>(-1, result, "The index was not -1.");
        }

        /// <summary>
        /// IndexOf should return the index of the item, relative to the sublist.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_ItemExists_ReturnsIndex()
        {
            var list = new int[] { 1, 2, 3, }.ToSublist();
            int result = list.IndexOf(3);
            Assert.AreEqual<int>(2, result, "The index was not correct.");
        }

        /// <summary>
        /// IndexOf should return the index of the first occurrence of an item, relative to the sublist.
        /// </summary>
        [TestMethod]
        public void TestIndexOf_MultipleOccurrences_ReturnsFirstIndex()
        {
            var list = new int[] { 1, 2, 3, 2 }.ToSublist();
            int result = list.IndexOf(2);
            Assert.AreEqual<int>(1, result, "The index was not correct.");
        }

        #endregion

        #region Insert

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_NegativeIndex_Throws()
        {
            var list = new List<int>().ToSublist();
            list.Insert(-1, 1);
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInsert_IndexTooBig_Throws()
        {
            var list = new List<int>().ToSublist();
            list.Insert(1, 1);
        }

        /// <summary>
        /// Ensures that the Insert method properly inserts at the beginning of the sublist. The count should increase.
        /// </summary>
        [TestMethod]
        public void TestInsert_InFront_GrowsSublist()
        {
            var list = new List<int>() { 2, 3, }.ToSublist();
            list.Insert(0, 1);
            int[] expected = { 1, 2, 3, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not added as expected.");
        }

        /// <summary>
        /// Ensures that the Insert method properly inserts at the end of the sublist. The count should increase.
        /// </summary>
        [TestMethod]
        public void TestInsert_InBack_GrowsSublist()
        {
            var list = new List<int>() { 1, 2, }.ToSublist();
            list.Insert(list.Count, 3);
            int[] expected = { 1, 2, 3, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not added as expected.");
        }

        /// <summary>
        /// Ensures that the Insert method properly inserts in the middle of the sublist. The count should increase.
        /// </summary>
        [TestMethod]
        public void TestInsert_InMiddle_GrowsSublist()
        {
            var list = new List<int>() { 1, 3, }.ToSublist();
            list.Insert(1, 2);
            int[] expected = { 1, 2, 3, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not added as expected.");
        }

        #endregion

        #region RemoveAt

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemoveAt_NegativeIndex_Throws()
        {
            var list = new List<int>().ToSublist();
            list.RemoveAt(-1);
        }

        /// <summary>
        /// An exception should be thrown if the index is beyond the bounds of the list.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRemoveAt_IndexTooBig_Throws()
        {
            var list = new List<int>().ToSublist();
            list.RemoveAt(0);
        }

        /// <summary>
        /// Removing at the beginning should shrink the sublist.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_InFront_RemovesFirstItem()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist();
            list.RemoveAt(0);
            int[] expected = { 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not removed as expected.");
        }

        /// <summary>
        /// Removing at the end should shrink the sublist.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_InBack_RemovesLastItem()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist();
            list.RemoveAt(list.Count - 1);
            int[] expected = { 1, 2 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not removed as expected.");
        }

        /// <summary>
        /// Removing in the middle should shrink the sublist.
        /// </summary>
        [TestMethod]
        public void TestRemoveAt_InMiddle_RemovesMiddleItem()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist();
            list.RemoveAt(1);
            int[] expected = { 1, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not removed as expected.");
        }

        #endregion

        #region Indexer

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_NegativeIndex_Throws()
        {
            var list = new List<int>().ToSublist();
            int value = list[-1];
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Getter_IndexEqualsCount_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist(0, 2);
            int value = list[list.Count];
        }

        /// <summary>
        /// The indexer should add the given index to the sublist's offset to
        /// grab the value from the underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Getter_GetsValueAtIndexPlusOffset()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist(1);
            int value = list[1];
            Assert.AreEqual(3, value);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Setter_NegativeIndex_Throws()
        {
            var list = new List<int>().ToSublist();
            list[-1] = 0;
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestIndexer_Setter_IndexEqualsCount_Throws()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist(0, 2);
            list[list.Count] = 0;
        }

        /// <summary>
        /// The indexer should add the given index to the sublist's offset to
        /// grab the value from the underlying list.
        /// </summary>
        [TestMethod]
        public void TestIndexer_Setter_SetsValueAtIndexPlusOffset()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist(1);
            list[1] = 0;
            int[] expected = { 1, 2, 0 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.ToSublist()), "The item was not set as expected.");
        }

        #endregion

        #region Add

        /// <summary>
        /// Add simply places an item at the end of the sublist. If the sublist
        /// is in the middle of the underlying list, then Add will insert in
        /// its middle. Adding to a sublist changes the sublist's count.
        /// </summary>
        [TestMethod]
        public void TestAdd_IncrementsCount()
        {
            var list = new List<int>() { 1, 2, 4, }.ToSublist(0, 2);
            list.Add(3);
            Assert.AreEqual(3, list.Count, "The Sublist did not grow.");
            int[] expected = { 1, 2, 3, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.ToSublist()), "The item was not inserted as expected.");
        }

        #endregion

        #region Clear

        /// <summary>
        /// Clear removes all of the items of the sublist from the underlying list and
        /// sets the sublist's count to zero.
        /// </summary>
        [TestMethod]
        public void TestClear_CountGoesToZero()
        {
            var list = new List<int>() { 1, 2, 3, }.ToSublist(0, 2);
            list.Clear();
            Assert.AreEqual(0, list.Count);
            int[] expected = { 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.List.ToSublist()), "The items were not removed as expected.");
        }

        #endregion

        #region Contains

        /// <summary>
        /// False should be returned if the value does not exist in the list.
        /// </summary>
        [TestMethod]
        public void TestContains_ValueMissing_ReturnsFalse()
        {
            var list = new List<int>() { 1, 2, 4 }.ToSublist();
            bool result = list.Contains(3);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// True should be returned if the value is in the beginning of the list.
        /// </summary>
        [TestMethod]
        public void TestContains_FirstValue_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist();
            bool result = list.Contains(1);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// True should be returned if the value is in the middle of the list.
        /// </summary>
        [TestMethod]
        public void TestContains_MiddleValue_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist();
            bool result = list.Contains(2);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// True should be returned if the value is at the end of the list.
        /// </summary>
        [TestMethod]
        public void TestContains_LastValue_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3 }.ToSublist();
            bool result = list.Contains(3);
            Assert.IsTrue(result);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// An exception should be thrown if the array is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyTo_NullArray_Throws()
        {
            var list = new List<int>() { 1, 2, }.ToSublist();
            int[] array = null;
            int arrayIndex = 0;
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// An exception should be thrown if the array index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCopyTo_NegativeArrayIndex_Throws()
        {
            var list = new List<int>() { 1, 2 }.ToSublist();
            int[] array = new int[2];
            int arrayIndex = -1;
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// An exception should be thrown if the array index is larger than the array's count.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyTo_ArrayIndexTooBig_Throws()
        {
            var list = new List<int>() { 1, 2 }.ToSublist();
            int[] array = new int[2];
            int arrayIndex = 3;
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// An exception should be thrown if the array is too small to hold all of the values.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyTo_ArrayTooSmall_Throws()
        {
            var list = new List<int>() { 1, 2 }.ToSublist();
            int[] array = new int[1];
            int arrayIndex = 0;
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// CopyTo should only copy the items in the sublist's range.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_CopiesToArray()
        {
            var list = new List<int>() { 1, 2, 3, 4 }.ToSublist(1, 2);

            int[] array = new int[2];
            int arrayIndex = 0;
            list.CopyTo(array, arrayIndex);

            int[] expected = { 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), array.ToSublist()), "The items were not copied as expected.");
        }

        /// <summary>
        /// CopyTo should only copy the items in the sublist's range.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_CopiesToArray_WithArrayIndex()
        {
            var list = new List<int>() { 1, 2, 3, 4 }.ToSublist(1, 2);

            int[] array = new int[3];
            int arrayIndex = 1;
            list.CopyTo(array, arrayIndex);

            int[] expected = { 0, 2, 3 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), array.ToSublist()), "The items were not copied as expected.");
        }

        #endregion

        #region Remove

        /// <summary>
        /// If the given value exists is the first in the list, it should be removed.
        /// </summary>
        [TestMethod]
        public void TestRemove_FirstValue_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3, }.ToSublist();
            bool result = list.Remove(1);
            Assert.IsTrue(result, "Value not removed.");
            Assert.AreEqual(2, list.Count, "The Sublist did not shrink.");
            int[] expected = { 2, 3, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not removed as expected.");
        }

        /// <summary>
        /// If the given value exists is the last in the list, it should be removed.
        /// </summary>
        [TestMethod]
        public void TestRemove_LastValue_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3, }.ToSublist();
            bool result = list.Remove(3);
            Assert.IsTrue(result, "Value not removed.");
            Assert.AreEqual(2, list.Count, "The Sublist did not shrink.");
            int[] expected = { 1, 2, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not removed as expected.");
        }

        /// <summary>
        /// If the given value exists is in the middle of the list, it should be removed.
        /// </summary>
        [TestMethod]
        public void TestRemove_MiddleValue_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3, }.ToSublist();
            bool result = list.Remove(2);
            Assert.IsTrue(result, "Value not removed.");
            Assert.AreEqual(2, list.Count, "The Sublist did not shrink.");
            int[] expected = { 1, 3, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not removed as expected.");
        }

        /// <summary>
        /// If the given value exists twice in the list, the first occurrence should be removed.
        /// </summary>
        [TestMethod]
        public void TestRemove_DuplicateValue_ReturnsTrue()
        {
            var list = new List<int>() { 1, 2, 3, 2, }.ToSublist();
            bool result = list.Remove(2);
            Assert.IsTrue(result, "Value not removed.");
            Assert.AreEqual(3, list.Count, "The Sublist did not shrink.");
            int[] expected = { 1, 3, 2, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The item was not removed as expected.");
        }

        /// <summary>
        /// If the given value does not exist is the list, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestRemove_ValueMissing_ReturnsFalse()
        {
            var list = new List<int>() { 1, 2, 4, }.ToSublist();
            bool result = list.Remove(3);
            Assert.IsFalse(result, "Value was removed.");
            Assert.AreEqual(3, list.Count, "The Sublist was modified.");
            int[] expected = { 1, 2, 4, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list), "The list was modified unexpectedly.");
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// Enumeration should move through the items in the sublist.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_GetsItemsInReverse()
        {
            var view = new int[] { 0, 1, 2, 3 }.ToSublist(1, 2);
            var list = new List<int>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            int[] expected = { 1, 2 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.ToSublist()), "The correct values were not enumerated.");
        }

        /// <summary>
        /// Enumeration should move through the items in the sublist.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerable_Implicit_StillEnumerates()
        {
            IEnumerable view = new int[] { 0, 1, 2, 3 }.ToSublist(1, 2);
            var list = new List<object>();
            foreach (int item in view)
            {
                list.Add(item);
            }
            object[] expected = { 1, 2 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), list.ToSublist()), "The correct values were not enumerated.");
        }

        #endregion
    }
}