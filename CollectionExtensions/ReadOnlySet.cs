using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using CollectionExtensions.Properties;

namespace CollectionExtensions
{
    /// <summary>
    /// Provides methods for creating read-only sets.
    /// </summary>
    public static class ReadOnlySet
    {
        /// <summary>
        /// Wraps the given set making it read-only.
        /// </summary>
        /// <typeparam name="T">The type of the items in the set.</typeparam>
        /// <param name="set">The set to make read-only.</param>
        /// <returns>A new read-only set wrapping the given set.</returns>
        /// <exception cref="System.ArgumentNullException">The set is null.</exception>
        public static ReadOnlySet<T> ReadOnly<T>(this ISet<T> set)
        {
            ReadOnlySet<T> readOnly = set as ReadOnlySet<T>;
            if (readOnly == null)
            {
                return new ReadOnlySet<T>(set);
            }
            return readOnly;
        }
    }

    /// <summary>
    /// Provides a view into a set such that it can't be modified.
    /// </summary>
    /// <typeparam name="T">The type of items in the set.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(ReadOnlySetDebugView<>))]
    public sealed class ReadOnlySet<T> : ISet<T>
    {
        private readonly ISet<T> _set;

        /// <summary>
        /// Initializes a new instance of a ReadOnlySet that wraps the given set.
        /// </summary>
        /// <param name="set">The set to make read-only.</param>
        /// <exception cref="System.ArgumentNullException">The set is null.</exception>
        public ReadOnlySet(ISet<T> set)
        {
            if (set == null)
            {
                throw new ArgumentNullException("set");
            }
            _set = set;
        }

        /// <summary>
        /// Gets the underlying set.
        /// </summary>
        public ISet<T> Set
        {
            get { return _set; }
        }
        
        /// <summary>
        /// Adds the item to the set, if it doesn't already exist.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was added; otherwise, false, if the item already existed.</returns>
        /// <exception cref="System.NotSupportedException">Cannot add to a read-only set.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Add(T item)
        {
            throw new NotSupportedException(Resources.EditReadonlySet);
        }

        /// <summary>
        /// Removes the items from the set that exist in the given collection.
        /// </summary>
        /// <param name="other">The items to remove from the set.</param>
        /// <exception cref="System.NotSupportedException">Cannot remove items from a read-only set.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException(Resources.EditReadonlySet);
        }

        /// <summary>
        /// Removes the items that do not appear in both the set and the given collection.
        /// </summary>
        /// <param name="other">The items to intersect with the set.</param>
        /// <exception cref="System.NotSupportedException">Cannot remove items from a read-only set.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void IntersectWith(IEnumerable<T> other)
        {
            throw new NotSupportedException(Resources.EditReadonlySet);
        }

        /// <summary>
        /// Determines whether the set has all of the items in the given collection
        /// and that the set is not equal to the other collection.
        /// </summary>
        /// <param name="other">The collection to compare against.</param>
        /// <returns>True if the set if a proper subset of the given collection; otherwise, false.</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return _set.IsProperSubsetOf(other);
        }

        /// <summary>
        /// Determines whether the given collection has all of the items in the set
        /// and that the given collection is not equal to the set.
        /// </summary>
        /// <param name="other">The collection to compare against.</param>
        /// <returns>True if the set is a proper superset of the given collection; otherwise, false.</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return _set.IsProperSupersetOf(other);
        }

        /// <summary>
        /// Determines whether the set has all of the items in the give collection.
        /// </summary>
        /// <param name="other">The collection to compare against.</param>
        /// <returns>True if the set is a subset of the given collection; otherwise, false.</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return _set.IsSubsetOf(other);
        }

        /// <summary>
        /// Determines whether the given collection has all of the items in the set.
        /// </summary>
        /// <param name="other">The collection to compare against.</param>
        /// <returns>True if the set is a superset of the given collection; otherwise, false.</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return _set.IsSupersetOf(other);
        }

        /// <summary>
        /// Determines whether the given collection shares any items with the set.
        /// </summary>
        /// <param name="other">The collection to compare against.</param>
        /// <returns>True if any items in the given collection are in the set; otherwise, false.</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            return _set.Overlaps(other);
        }

        /// <summary>
        /// Determines whether the given collection has the same items as the set.
        /// </summary>
        /// <param name="other">The collection to compare against.</param>
        /// <returns>True if the given collection has the same items as the set; otherwise, false.</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            return _set.SetEquals(other);
        }

        /// <summary>
        /// Removes any items that appear in both the set and the given collection.
        /// </summary>
        /// <param name="other">The items to perform the set symmetic difference with.</param>
        /// <exception cref="System.NotSupportedException">Cannot remove items from a read-only set.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotSupportedException(Resources.EditReadonlySet);
        }

        /// <summary>
        /// Adds any items in the given collection to the set that are not already present.
        /// </summary>
        /// <param name="other">The items to union the set with.</param>
        /// <exception cref="System.NotSupportedException">Cannot add items to a read-only set.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void UnionWith(IEnumerable<T> other)
        {
            throw new NotSupportedException(Resources.EditReadonlySet);
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException(Resources.EditReadonlySet);
        }

        /// <summary>
        /// Removes all the items from the set.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Cannot remove items from a read-only set.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Clear()
        {
            throw new NotSupportedException(Resources.EditReadonlySet);
        }

        /// <summary>
        /// Determines whether the given item exists in the set.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>True if the item is in the set; otherwise, false.</returns>
        public bool Contains(T item)
        {
            return _set.Contains(item);
        }

        /// <summary>
        /// Copies the items in the set to the given array, starting at the given index.
        /// </summary>
        /// <param name="array">The array to copy the items to.</param>
        /// <param name="arrayIndex">The index into the array to begin copying.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of items in the set.
        /// </summary>
        public int Count
        {
            get { return _set.Count; }
        }

        /// <summary>
        /// Determines whether the set is read-only.
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Removes the item from the set.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        /// <exception cref="System.NotSupportedException">Cannot remove items from a read-only set.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Remove(T item)
        {
            throw new NotSupportedException(Resources.EditReadonlySet);
        }

        /// <summary>
        /// Gets an enumerator over the items in the set.
        /// </summary>
        /// <returns>An enumerator over the items in the set.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
