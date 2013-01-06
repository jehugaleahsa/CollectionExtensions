using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectionExtensions.Extensions;

namespace CollectionExtensions.Test
{
    /// <summary>
    /// Tests the PriorityQueue class.
    /// </summary>
    [TestClass]
    public class PriorityQueueTester
    {
        #region Real World Examples

        /// <summary>
        /// PriorityQueue allows us to order items based on their priority.
        /// To demonstrate, we'll order some students based on their GPAs.
        /// </summary>
        [TestMethod]
        public void TestPriorityQueue_PriorityStudentsBasedOnGPA()
        {
            PriorityQueue<decimal> queue = new PriorityQueue<decimal>();
            queue.Enqueue(1.23m);
            queue.Enqueue(3.98m);
            queue.Enqueue(2.55m);
            queue.Enqueue(4.00m);
            queue.Enqueue(2.29m);
            queue.Enqueue(3.01m);
            queue.Enqueue(3.11m);
            queue.Enqueue(2.95m);
            queue.Enqueue(3.99m);

            // The top-scoring student should be at the top
            Assert.AreEqual(4.00m, queue.Peek(), "The top-level student was not at the top.");
            
            // The students should be returned in order of highest GPA
            List<decimal> ordered = new List<decimal>(queue);
            ordered.Reverse();
            bool isSorted = Sublist.IsSorted(ordered.ToSublist());
            Assert.IsTrue(isSorted, "The students were not returned according to their GPA.");
        }

        #endregion

        #region Ctor

        /// <summary>
        /// The default constructor will create an empty queue.
        /// </summary>
        [TestMethod]
        public void TestCtor_EmptyQueue()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            Assert.AreEqual(0, queue.Count, "The queue was not empty.");
            ICollection collection = queue;
            Assert.IsFalse(collection.IsSynchronized, "The queue was not synchronized.");
            Assert.IsNotNull(collection.SyncRoot, "There was no sync root for the queue.");
        }

        /// <summary>
        /// If we try to create a priority queue with a negative initial capacity,
        /// an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_NegativeCapacity_Throws()
        {
            int capacity = -1;
            new PriorityQueue<int>(capacity);
        }

        /// <summary>
        /// The initial capacity will be used in the underlying heap. The
        /// queue provides no way to determining whether the parameter was
        /// used.
        /// </summary>
        [TestMethod]
        public void TestCtor_WithCapacity_EmptyQueue()
        {
            int capacity = 0;
            PriorityQueue<int> queue = new PriorityQueue<int>(capacity);
            Assert.AreEqual(0, queue.Count, "The queue was not empty.");
        }

        /// <summary>
        /// An exception should be thrown if we try to create a
        /// priorty queue with a null comparison function.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_ComparisonNull_Throws()
        {
            Func<int, int, int> comparison = null;
            new PriorityQueue<int>(comparison);
        }

        /// <summary>
        /// The comparison function should be used to prioritize the items.
        /// </summary>
        [TestMethod]
        public void TestCtor_Comparison_SetsComparison()
        {
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            PriorityQueue<int> queue = new PriorityQueue<int>(comparison);
            Assert.AreEqual(0, queue.Count, "The queue was not empty.");
        }

        /// <summary>
        /// An exception should be thrown if we try to create a
        /// priorty queue with a null comparer.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_ComparerNull_Throws()
        {
            IComparer<int> comparer = null;
            new PriorityQueue<int>(comparer);
        }

        /// <summary>
        /// The comparison function should be used to prioritize the items.
        /// </summary>
        [TestMethod]
        public void TestCtor_Comparer_SetsComparer()
        {
            IComparer<int> comparer = Comparer<int>.Default;
            PriorityQueue<int> queue = new PriorityQueue<int>(comparer);
            Assert.AreEqual(0, queue.Count, "The queue was not empty.");
        }

        /// <summary>
        /// If we try to create a priority queue with a negative initial capacity,
        /// an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_Comparison_NegativeCapacity_Throws()
        {
            int capacity = -1;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            new PriorityQueue<int>(capacity, comparison);
        }

        /// <summary>
        /// An exception should be thrown if we try to create a
        /// priorty queue with a null comparison function.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_Capacity_ComparisonNull_Throws()
        {
            int capacity = 0;
            Func<int, int, int> comparison = null;
            new PriorityQueue<int>(capacity, comparison);
        }

        /// <summary>
        /// The comparison function should be used to prioritize the items.
        /// </summary>
        [TestMethod]
        public void TestCtor_Capacity_Comparison_SetsComparison()
        {
            int capacity = 0;
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            PriorityQueue<int> queue = new PriorityQueue<int>(capacity, comparison);
            Assert.AreEqual(0, queue.Count, "The queue was not empty.");
        }

        /// <summary>
        /// If we try to create a priority queue with a negative initial capacity,
        /// an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCtor_Comparer_NegativeCapacity_Throws()
        {
            int capacity = -1;
            IComparer<int> comparer = Comparer<int>.Default;
            new PriorityQueue<int>(capacity, comparer);
        }

        /// <summary>
        /// An exception should be thrown if we try to create a
        /// priorty queue with a null comparer.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_Capacity_ComparerNull_Throws()
        {
            int capacity = 0;
            IComparer<int> comparer = null;
            new PriorityQueue<int>(capacity, comparer);
        }

        /// <summary>
        /// The comparer should be used to prioritize the items.
        /// </summary>
        [TestMethod]
        public void TestCtor_Capacity_Comparer_SetsComparer()
        {
            int capacity = 0;
            IComparer<int> comparer = Comparer<int>.Default;
            PriorityQueue<int> queue = new PriorityQueue<int>(capacity, comparer);
            Assert.AreEqual(0, queue.Count, "The queue was not empty.");
        }

        /// <summary>
        /// An exception should be thrown if we try to initialze a
        /// priority queue with a null collection.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_CollectionNull_Throws()
        {
            IEnumerable<int> collection = null;
            new PriorityQueue<int>(collection);
        }

        /// <summary>
        /// The default comparer should be used to prioritize the heap.
        /// </summary>
        [TestMethod]
        public void TestCtor_Collection_PrioritizesItems()
        {
            IEnumerable<int> collection = new int[] { 1, 2, 3, 4, 5 };
            PriorityQueue<int> queue = new PriorityQueue<int>(collection);
            Assert.AreEqual(5, queue.Count, "Not all of the items were added to the queue.");
            List<int> values = new List<int>(queue);
            int[] expected = new int[] { 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), values.ToSublist()), "The values were not prioritized.");
        }

        /// <summary>
        /// An exception should be thrown if the passed comparison function is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_Collection_ComparisonNull_Throws()
        {
            Func<int, int, int> comparison = null;
            IEnumerable<int> collection = new int[] { 5, 4, 3, 2, 1 };
            new PriorityQueue<int>(comparison, collection);
        }

        /// <summary>
        /// An exception should be thrown if the passed collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_Comparison_CollectionNull_Throws()
        {
            Func<int, int, int> comparison = Comparer<int>.Default.Compare;
            IEnumerable<int> collection = null;
            new PriorityQueue<int>(comparison, collection);
        }

        /// <summary>
        /// The passed comparison function should be used to prioritize the heap.
        /// </summary>
        [TestMethod]
        public void TestCtor_Comparison_Collection_PrioritizesItemsUsingComparison()
        {
            Func<int, int, int> comparison = (x, y) => Comparer<int>.Default.Compare(y, x);
            IEnumerable<int> collection = new int[] { 5, 4, 3, 2, 1 };
            PriorityQueue<int> queue = new PriorityQueue<int>(comparison, collection);
            Assert.AreEqual(5, queue.Count, "Not all of the items were added to the queue.");
            List<int> values = new List<int>(queue);
            int[] expected = new int[] { 1, 2, 3, 4, 5 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), values.ToSublist()), "The values were not prioritized.");
        }

        /// <summary>
        /// An exception should be thrown if the passed comparer is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_Collection_ComparerNull_Throws()
        {
            IComparer<int> comparer = null;
            IEnumerable<int> collection = new int[] { 5, 4, 3, 2, 1 };
            new PriorityQueue<int>(comparer, collection);
        }

        /// <summary>
        /// An exception should be thrown if the passed collection is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_Comparer_CollectionNull_Throws()
        {
            IComparer<int> comparer = Comparer<int>.Default;
            IEnumerable<int> collection = null;
            new PriorityQueue<int>(comparer, collection);
        }

        /// <summary>
        /// The passed comparer should be used to prioritize the heap.
        /// </summary>
        [TestMethod]
        public void TestCtor_Comparer_Collection_PrioritizesItemsUsingComparer()
        {
            IComparer<int> comparer = Comparer<int>.Default;
            IEnumerable<int> collection = new int[] { 1, 2, 3, 4, 5 };
            PriorityQueue<int> queue = new PriorityQueue<int>(comparer, collection);
            Assert.AreEqual(5, queue.Count, "Not all of the items were added to the queue.");
            List<int> values = new List<int>(queue);
            int[] expected = new int[] { 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), values.ToSublist()), "The values were not prioritized.");
        }

        #endregion

        #region Add

        /// <summary>
        /// If we add an item, the count should increase.
        /// </summary>
        [TestMethod]
        public void TestAdd_IncreasesCount()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            queue.Enqueue(0);
            Assert.AreEqual(1, queue.Count, "The count did not increase.");
        }

        /// <summary>
        /// If we add a smaller value, the larger value should stay on top.
        /// </summary>
        [TestMethod]
        public void TestAdd_AddSmaller_TopStaysTop()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 1, 2, 3 });
            queue.Enqueue(0);
            Assert.AreEqual(3, queue.Peek(), "The top is the wrong value.");
            Assert.AreEqual(4, queue.Count, "The count was wrong.");
        }

        /// <summary>
        /// If we add a larger value, the larger value should be moved to the top.
        /// </summary>
        [TestMethod]
        public void TestAdd_AddLarger_TopStaysTop()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 0, 1, 2 });
            queue.Enqueue(3);
            Assert.AreEqual(3, queue.Peek(), "The top is the wrong value.");
            Assert.AreEqual(4, queue.Count, "The count was wrong.");
        }

        #endregion

        #region Top

        /// <summary>
        /// An exception should be thrown if the top is requested and the queue is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestTop_EmptyQueue_Throws()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            queue.Peek();
        }

        #endregion

        #region Remove

        /// <summary>
        /// An exception should be thrown if an attempt is made to remove the top and the queue is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRemove_EmptyQueue_Throws()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            queue.Dequeue();
        }

        /// <summary>
        /// Removing from the queue should return the highest priority item
        /// and decrease the count.
        /// </summary>
        [TestMethod]
        public void TestRemove_ShouldDecreaseCount_ReturnTop()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 1, 2, 3, 4, 5 });
            int top = queue.Dequeue();
            Assert.AreEqual(4, queue.Count, "The count did not decrease.");
            Assert.AreEqual(5, top, "The highest priority item was not removed.");
        }

        #endregion

        #region Clear

        /// <summary>
        /// Clearing should remove all the items in the queue.
        /// </summary>
        [TestMethod]
        public void TestClear_ResetsCount()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 1, 2, 3, 4, 5 });
            queue.Clear();
            Assert.AreEqual(0, queue.Count, "The count was not reset.");
        }

        #endregion

        #region Contains

        /// <summary>
        /// If the item does not exist in the queue, false should be returned.
        /// </summary>
        [TestMethod]
        public void TestContains_Missing_ReturnsFalse()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 1, 2, 3 });
            bool result = queue.Contains(4);
            Assert.IsFalse(result, "The item should not have been found.");
        }

        /// <summary>
        /// If the item exists in the queue, true should be returned.
        /// </summary>
        [TestMethod]
        public void TestContains_Found_ReturnsTrue()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 1, 2, 3 });
            bool result = queue.Contains(2);
            Assert.IsTrue(result, "The item should have been found.");
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// An exception should be thrown if the array is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyTo_ArrayNull_Throws()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            int[] array = null;
            int index = 0;
            queue.CopyTo(array, index);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCopyTo_IndexNegative_Throws()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            int[] array = new int[0];
            int index = -1;
            queue.CopyTo(array, index);
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyTo_IndexTooBig_Throws()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            int[] array = new int[0];
            int index = 1;
            queue.CopyTo(array, index);
        }

        /// <summary>
        /// The items should be moved into an array in order of their priority.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_CopiesItemsInOrderOfPriority()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 1, 2, 3, 4, 5 });
            int[] array = new int[5];
            queue.CopyTo(array, 0);
            int[] expected = new int[] { 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), array.ToSublist()), "The items were not copied in the right order.");
            Assert.AreEqual(5, queue.Count, "The queue should be empty.");
        }

        /// <summary>
        /// An exception should be thrown if the array is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCopyTo_ICollection_ArrayNull_Throws()
        {
            ICollection collection = new PriorityQueue<int>();
            Array array = null;
            int index = 0;
            collection.CopyTo(array, index);
        }

        /// <summary>
        /// An exception should be thrown if the index is negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCopyTo_ICollection_IndexNegative_Throws()
        {
            ICollection collection = new PriorityQueue<int>();
            Array array = Array.CreateInstance(typeof(int), 0);
            int index = -1;
            collection.CopyTo(array, index);
        }

        /// <summary>
        /// An exception should be thrown if the index is too big.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyTo_ICollection_IndexTooBig_Throws()
        {
            ICollection collection = new PriorityQueue<int>();
            Array array = Array.CreateInstance(typeof(int), 0);
            int index = 1;
            collection.CopyTo(array, index);
        }

        /// <summary>
        /// An exception should be thrown if the array type is wrong.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCopyTo_ArrayWrongType_Throws()
        {
            ICollection collection = new PriorityQueue<int>(new int[] { 0 });
            Array array = Array.CreateInstance(typeof(string), 1);
            int index = 0;
            collection.CopyTo(array, index);
        }

        /// <summary>
        /// Copies the items to the array in order of priority.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_CopiesItemsByPriority()
        {
            ICollection collection = new PriorityQueue<int>(new int[] { 1, 2, 3, 4, 5 });
            Array array = Array.CreateInstance(typeof(int), 5);
            collection.CopyTo(array, 0);
            int[] values = array.Cast<int>().ToArray();
            int[] expected = new int[] { 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), values.ToSublist()), "The items were not copied in the right order.");
            Assert.AreEqual(5, collection.Count, "The queue should be empty.");
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// If the queue is changed after the enumerator is created, an exception should be thrown
        /// before the next item is iterated.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetEnumerator_Changed_Throws()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            IEnumerator<int> enumerator = queue.GetEnumerator();
            queue.Enqueue(123);
            enumerator.MoveNext();
        }

        /// <summary>
        /// If the queue is changed after the enumerator is created, an exception should be thrown
        /// before the next item is iterated.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetEnumerator_ChangedAfterFirst_Throws()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>();
            queue.Enqueue(123);
            IEnumerator<int> enumerator = queue.GetEnumerator();
            enumerator.MoveNext();
            queue.Enqueue(123);
            enumerator.MoveNext();
        }

        /// <summary>
        /// If two enumerators are created at the same time, they can interact with each other.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_MultipleEnumerators()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 1, 2 });
            IEnumerator<int> enumerator1 = queue.GetEnumerator();
            IEnumerator<int> enumerator2 = queue.GetEnumerator();
            Assert.IsTrue(enumerator1.MoveNext(), "The first item could not be iterated.");
            Assert.IsTrue(enumerator1.MoveNext(), "The second item could not be iterated.");
            Assert.IsFalse(enumerator1.MoveNext(), "There should not have been more items in the first enumerator.");
            Assert.IsTrue(enumerator2.MoveNext(), "The first item could not be iterated.");
            Assert.IsTrue(enumerator2.MoveNext(), "The second item could not be iterated.");
            Assert.IsFalse(enumerator2.MoveNext(), "There should not have been more items in the second enumerator.");
        }

        /// <summary>
        /// If two enumerators are created at the same time, they can interact with each other.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_IEnumerable()
        {
            IEnumerable collection = new PriorityQueue<int>(new int[] { 1, 2 });
            IEnumerator enumerator = collection.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext(), "The first item was not enumerated.");
            Assert.AreEqual(2, enumerator.Current, "The wrong first item was returned.");
            Assert.IsTrue(enumerator.MoveNext(), "The second item was not enumerated.");
            Assert.AreEqual(1, enumerator.Current, "The wrong second item was returned.");
            Assert.IsFalse(enumerator.MoveNext(), "The wrong number of items were enumerated.");
        }

        #endregion

        #region ToArray

        /// <summary>
        /// The items should be moved into an array in order of their priority.
        /// </summary>
        [TestMethod]
        public void TestToArray_CopiesItemsInOrderOfPriority()
        {
            PriorityQueue<int> queue = new PriorityQueue<int>(new int[] { 1, 2, 3, 4, 5 });
            int[] array = queue.ToArray();
            int[] expected = new int[] { 5, 4, 3, 2, 1 };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), array.ToSublist()), "The items were not copied in the right order.");
            Assert.AreEqual(5, queue.Count, "The queue should be empty.");
        }

        #endregion
    }
}
