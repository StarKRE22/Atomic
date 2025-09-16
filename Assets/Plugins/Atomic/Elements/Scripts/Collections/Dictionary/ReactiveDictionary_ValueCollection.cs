using System;
using System.Collections;
using System.Collections.Generic;
// ReSharper disable MemberHidesStaticFromOuterClass

namespace Atomic.Elements
{
    public partial class ReactiveDictionary<K, V>
    {
        /// <summary>
        /// Represents a read-only collection of values in a <see cref="ReactiveDictionary{K,V}"/>.
        /// </summary>
        /// <remarks>
        /// This collection provides access to dictionary values without allowing modifications.
        /// Implements <see cref="ICollection{V}"/> for compatibility with standard collection APIs.
        /// </remarks>
        public readonly struct ReadOnlyValueCollection : ICollection<V>
        {
            private readonly ReactiveDictionary<K, V> _dictionary;

            /// <summary>
            /// Initializes a new instance of the <see cref="ReadOnlyValueCollection"/> struct for the specified dictionary.
            /// </summary>
            /// <param name="dictionary">The reactive dictionary whose values are exposed.</param>
            internal ReadOnlyValueCollection(ReactiveDictionary<K, V> dictionary)
            {
                _dictionary = dictionary;
            }

            /// <summary>
            /// Gets the number of values in the collection.
            /// </summary>
            public int Count => _dictionary._count;

            /// <summary>
            /// Gets a value indicating whether the collection is read-only. Always <c>true</c>.
            /// </summary>
            public bool IsReadOnly => true;

            /// <summary>
            /// Returns an enumerator that iterates through the values in the dictionary.
            /// </summary>
            public Enumerator GetEnumerator() => new(_dictionary);

            IEnumerator<V> IEnumerable<V>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <summary>
            /// Determines whether the collection contains a specific value.
            /// </summary>
            /// <param name="item">The value to locate.</param>
            /// <returns><c>true</c> if the value exists; otherwise, <c>false</c>.</returns>
            public bool Contains(V item)
            {
                var comparer = s_valueComparer;
                for (int i = 0; i < _dictionary._lastIndex; i++)
                {
                    ref readonly Slot slot = ref _dictionary._slots[i];
                    if (slot.exists && comparer.Equals(slot.value, item))
                        return true;
                }

                return false;
            }

            /// <summary>
            /// Copies the values to an array, starting at the specified index.
            /// </summary>
            /// <param name="array">The destination array.</param>
            /// <param name="arrayIndex">The zero-based index at which to begin copying.</param>
            /// <exception cref="ArgumentNullException">Thrown if <paramref name="array"/> is <c>null</c>.</exception>
            /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="arrayIndex"/> is negative.</exception>
            public void CopyTo(V[] array, int arrayIndex)
            {
                if (array == null)
                    throw new ArgumentNullException(nameof(array));

                if (arrayIndex < 0)
                    throw new ArgumentOutOfRangeException(nameof(arrayIndex));

                int i = arrayIndex;
                for (int s = 0; s < _dictionary._lastIndex; s++)
                {
                    ref readonly Slot slot = ref _dictionary._slots[s];
                    if (slot.exists)
                        array[i++] = slot.value;
                }
            }

            /// <inheritdoc/>
            void ICollection<V>.Add(V item) =>
                throw new NotSupportedException("ValueCollection is read-only.");

            /// <inheritdoc/>
            void ICollection<V>.Clear() =>
                throw new NotSupportedException("ValueCollection is read-only.");

            /// <inheritdoc/>
            bool ICollection<V>.Remove(V item) =>
                throw new NotSupportedException("ValueCollection is read-only.");

            /// <summary>
            /// Enumerates the values in a <see cref="ReadOnlyValueCollection"/>.
            /// </summary>
            public struct Enumerator : IEnumerator<V>
            {
                private readonly ReactiveDictionary<K, V> _dictionary;
                private int _index;
                private V _current;

                /// <summary>
                /// Initializes a new instance of the <see cref="Enumerator"/> struct for the specified dictionary.
                /// </summary>
                /// <param name="dictionary">The dictionary to enumerate values from.</param>
                internal Enumerator(ReactiveDictionary<K, V> dictionary)
                {
                    _dictionary = dictionary;
                    _index = 0;
                    _current = default;
                }

                /// <summary>
                /// Gets the value at the current position of the enumerator.
                /// </summary>
                public V Current => _current;

                /// <summary>
                /// Gets the current element in the collection.
                /// Explicit implementation for <see cref="IEnumerator"/>.
                /// </summary>
                object IEnumerator.Current => _current;

                /// <summary>
                /// Advances the enumerator to the next value in the collection.
                /// </summary>
                /// <returns>
                /// <c>true</c> if successfully advanced to the next value; <c>false</c> if the enumerator has passed the end.
                /// </returns>
                public bool MoveNext()
                {
                    while (_index < _dictionary._lastIndex)
                    {
                        ref readonly Slot slot = ref _dictionary._slots[_index++];
                        if (slot.exists)
                        {
                            _current = slot.value;
                            return true;
                        }
                    }

                    _current = default;
                    return false;
                }

                /// <summary>
                /// Resets the enumerator to its initial position, before the first value in the collection.
                /// </summary>
                public void Reset()
                {
                    _index = 0;
                    _current = default;
                }

                /// <summary>
                /// Performs application-defined tasks associated with freeing resources.
                /// </summary>
                /// <remarks>This enumerator does not hold unmanaged resources; Dispose does nothing.</remarks>
                public void Dispose()
                {
                    // Nothing to release
                }
            }
        }
    }
}