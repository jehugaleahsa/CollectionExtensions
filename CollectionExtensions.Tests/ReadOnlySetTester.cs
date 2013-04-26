using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CollectionExtensions.Test
{
    /// <summary>
    /// Tests the ReadOnlySet class.
    /// </summary>
    [TestClass]
    public class ReadOnlySetTester
    {
        #region Extension Methods

        /// <summary>
        /// If the set is not read-only, ReadOnly will wrap it with a ReadOnlySet.
        /// </summary>
        [TestMethod]
        public void TestReadOnly_NotReadOnly_Wraps()
        {
            HashSet<int> set = new HashSet<int>();
            ReadOnlySet<int> readOnly = set.ReadOnly();
            Assert.AreSame(set, readOnly.Set, "The set was not correctly wrapped.");
        }

        /// <summary>
        /// If the set is already read-only, ReadOnly should return the given set.
        /// </summary>
        [TestMethod]
        public void TestReadOnly_ReadOnly_Wraps()
        {
            ISet<int> set = new ReadOnlySet<int>(new HashSet<int>());
            ReadOnlySet<int> readOnly = set.ReadOnly();
            Assert.AreSame(set, readOnly, "The set was wrapped again.");
        }

        #endregion

        #region Ctor, Set, Count and Read-Only

        /// <summary>
        /// If we try to pass a null set to the ctor, an exception should be thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestCtor_NullSet_Throws()
        {
            ISet<int> set = null;
            new ReadOnlySet<int>(set);
        }

        /// <summary>
        /// The ctor should store the given set as a backing field.
        /// </summary>
        [TestMethod]
        public void TestCtor_WrapsSet()
        {
            HashSet<int> set = new HashSet<int>() { 1, 2, 3, 4, 5 };
            ReadOnlySet<int> readOnly = new ReadOnlySet<int>(set);
            Assert.AreSame(set, readOnly.Set, "The set was set as a backing field.");
            Assert.AreEqual(set.Count, readOnly.Count, "The count was wrong.");
            Assert.IsTrue(((ICollection<int>)readOnly).IsReadOnly, "The set was not read-only.");
        }

        #endregion

        #region Add

        /// <summary>
        /// We cannot add to a read-only set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestAdd_Throws()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>());
            set.Add(0);
        }

        /// <summary>
        /// We cannot add to a read-only set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestAdd_Explicit_Throws()
        {
            ICollection<int> collection = new ReadOnlySet<int>(new HashSet<int>());
            collection.Add(0);
        }

        #endregion

        #region ExceptWith

        /// <summary>
        /// We cannot perform a set difference.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestExceptWith_Throws()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>());
            set.ExceptWith(Enumerable.Empty<int>());
        }

        #endregion

        #region IntersectWith

        /// <summary>
        /// We cannot intersect with a read-only set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestIntersectWith_Throws()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>());
            set.IntersectWith(Enumerable.Empty<int>());
        }

        #endregion

        #region IsProperSubsetOf

        /// <summary>
        /// IsProperSubsetOf should call IsProperSubsetOf on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestIsProperSubsetOf_IsProperSubset_ReturnsTrue()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4 });
            bool result = set.IsProperSubsetOf(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsTrue(result, "The set should have been a proper subset.");
        }

        /// <summary>
        /// IsProperSubsetOf should call IsProperSubsetOf on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestIsProperSubsetOf_IsNotProperSubset_ReturnsFalse()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4 });
            bool result = set.IsProperSubsetOf(new int[] { 1, 2, 3, 4 });
            Assert.IsFalse(result, "The set should not have been a proper subset.");
        }

        #endregion

        #region IsProperSupersetOf

        /// <summary>
        /// IsProperSupersetOf should call IsProperSupersetOf on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestIsProperSupersetOf_IsProperSupersetOf_ReturnsTrue()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4, 5 });
            bool result = set.IsProperSupersetOf(new int[] { 1, 2, 3, 4 });
            Assert.IsTrue(result, "The set should have been a proper superset.");
        }

        /// <summary>
        /// IsPropertySupersetOf should call IsPropertySupersetOf on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestIsProperSupersetOf_IsNotProperSupersetOf_ReturnsFalse()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4 });
            bool result = set.IsProperSupersetOf(new int[] { 1, 2, 3, 4 });
            Assert.IsFalse(result, "The set should not have been a proper superset.");
        }

        #endregion

        #region IsSubsetOf

        /// <summary>
        /// IsSubsetOf should call IsSubsetOf on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestIsSubsetOf_IsSubset_ReturnsTrue()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4 });
            bool result = set.IsSubsetOf(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsTrue(result, "The set should have been a subset.");
        }

        /// <summary>
        /// IsSubsetOf should call IsSubsetOf on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestIsSubsetOf_IsNotSubset_ReturnsFalse()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4 });
            bool result = set.IsSubsetOf(new int[] { 1, 2, 3 });
            Assert.IsFalse(result, "The set should not have been a subset.");
        }

        #endregion

        #region IsSupersetOf

        /// <summary>
        /// IsSupersetOf should call IsSupersetOf on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestIsSupersetOf_IsSupersetOf_ReturnsTrue()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4, 5 });
            bool result = set.IsSupersetOf(new int[] { 1, 2, 3, 4 });
            Assert.IsTrue(result, "The set should have been a superset.");
        }

        /// <summary>
        /// IsSupersetOf should call IsSupersetOf on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestIsSupersetOf_IsNotSupersetOf_ReturnsFalse()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3 });
            bool result = set.IsSupersetOf(new int[] { 1, 2, 3, 4 });
            Assert.IsFalse(result, "The set should not have been a superset.");
        }

        #endregion

        #region Overlaps

        /// <summary>
        /// Overlaps should call Overlaps on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestOverlaps_SharesItems_ReturnsTrue()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4, 5 });
            bool result = set.Overlaps(new int[] { 3, 8 });
            Assert.IsTrue(result, "The set should have overlapped with the collection.");
        }

        /// <summary>
        /// Overlaps should call Overlaps on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestOverlaps_NoSharedItems_ReturnsFalse()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4, 5 });
            bool result = set.Overlaps(new int[] { 7, 8 });
            Assert.IsFalse(result, "The set should not have overlapped with the collection.");
        }

        #endregion

        #region SetEquals

        /// <summary>
        /// SetEquals should call SetEquals on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestSetEquals_SameItems_ReturnsTrue()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4, 5 });
            bool result = set.SetEquals(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsTrue(result, "The sets should have been equal.");
        }

        /// <summary>
        /// SetEquals should call SetEquals on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestSetEquals_DifferentItems_ReturnsFalse()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4, 5 });
            bool result = set.SetEquals(new int[] { 1, 2, 3, 4 });
            Assert.IsFalse(result, "The sets should not have been equal.");
        }

        #endregion

        #region SymmetricExceptWith

        /// <summary>
        /// We cannot perform a symmetric difference on a read-only set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestSymmetricExceptWith_Throws()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>());
            set.SymmetricExceptWith(Enumerable.Empty<int>());
        }

        #endregion

        #region UnionWith

        /// <summary>
        /// We cannot union a read-only set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestUnionWith_Throws()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>());
            set.UnionWith(Enumerable.Empty<int>());
        }

        #endregion

        #region Clear

        /// <summary>
        /// We cannot clear a read-only set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestClear_Throws()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>());
            set.Clear();
        }

        #endregion

        #region Contains

        /// <summary>
        /// Contains calls Contains on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestContains_ItemExists_ReturnsTrue()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3 });
            bool result = set.Contains(2);
            Assert.IsTrue(result, "The item should have been found.");
        }

        /// <summary>
        /// Contains calls Contains on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestContains_ItemMissing_ReturnsFalse()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3 });
            bool result = set.Contains(0);
            Assert.IsFalse(result, "The item should not have been found.");
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// CopyTo should call CopyTo on the underlying set.
        /// </summary>
        [TestMethod]
        public void TestCopyTo_CopiesItems()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2, 3, 4, 5 });
            int[] array = new int[6];
            int arrayIndex = 1;
            set.CopyTo(array, arrayIndex);
            Assert.IsTrue(array.SequenceEqual(new int[] { 0, 1, 2, 3, 4, 5 }), "The items were not copied correctly.");
        }

        #endregion

        #region Remove

        /// <summary>
        /// We cannot remove items from a read-only set.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestRemove_Throws()
        {
            ReadOnlySet<int> set = new ReadOnlySet<int>(new HashSet<int>());
            set.Remove(0);
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// All the items should be enumerated in the set.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_EnumeratesAllItems()
        {
            IEnumerable<int> set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2 });

            Assert.IsTrue(enumerate(set).Contains(1), "The first item was not present.");
            Assert.IsTrue(enumerate(set).Contains(2), "The second item was not present.");
        }

        private static IEnumerable<T> enumerate<T>(IEnumerable<T> enumerable)
        {
            foreach (T item in enumerable)
            {
                yield return item;
            }
        }

        /// <summary>
        /// All the items should be enumerated in the set.
        /// </summary>
        [TestMethod]
        public void TestGetEnumerator_Explicit_EnumeratesAllItems()
        {
            IEnumerable set = new ReadOnlySet<int>(new HashSet<int>() { 1, 2 });

            Assert.IsTrue(enumerate(set).Cast<int>().Contains(1), "The first item was not present.");
            Assert.IsTrue(enumerate(set).Cast<int>().Contains(2), "The second item was not present.");
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
