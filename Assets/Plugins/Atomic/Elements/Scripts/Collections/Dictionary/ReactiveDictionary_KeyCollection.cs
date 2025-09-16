using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable MemberHidesStaticFromOuterClass

namespace Atomic.Elements
{
    public partial class ReactiveDictionary<K, V>
    {
        /// <summary>
        /// Represents a read-only collection of keys in a <see cref="ReactiveDictionary{K,V}"/>.
        /// </summary>
        /// <remarks>
        /// This collection provides access to dictionary keys without allowing modifications.
        /// Implements <see cref="ICollection{K}"/> for compatibility with standard collection APIs.
        /// </remarks>
        public readonly struct ReadOnlyKeyCollection : ICollection<K>
        {
            private readonly ReactiveDictionary<K, V> _dictionary;

            /// <summary>
            /// Initializes a new instance of the <see cref="ReadOnlyKeyCollection"/> struct for the specified dictionary.
            /// </summary>
            /// <param name="dictionary">The reactive dictionary whose keys are exposed.</param>
            internal ReadOnlyKeyCollection(ReactiveDictionary<K, V> dictionary)
            {
                _dictionary = dictionary;
            }

            /// <summary>
            /// Gets the number of keys in the collection.
            /// </summary>
            public int Count => _dictionary._count;

            /// <summary>
            /// Gets a value indicating whether the collection is read-only. Always <c>true</c>.
            /// </summary>
            public bool IsReadOnly => true;

            /// <summary>
            /// Returns an enumerator that iterates through the keys in the dictionary.
            /// </summary>
            public Enumerator GetEnumerator() => new(_dictionary);

            /// <inheritdoc/>
            IEnumerator<K> IEnumerable<K>.GetEnumerator() => new Enumerator(_dictionary);

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() => new Enumerator(_dictionary);

            /// <summary>
            /// Determines whether the collection contains a specific key.
            /// </summary>
            /// <param name="item">The key to locate.</param>
            /// <returns><c>true</c> if the key exists; otherwise, <c>false</c>.</returns>
            public bool Contains(K item) => item != null && _dictionary.ContainsKey(item);

            /// <summary>
            /// Copies the keys to an array, starting at the specified index.
            /// </summary>
            /// <param name="array">The destination array.</param>
            /// <param name="arrayIndex">The zero-based index at which to begin copying.</param>
            /// <exception cref="ArgumentNullException">Thrown if <paramref name="array"/> is <c>null</c>.</exception>
            /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="arrayIndex"/> is negative.</exception>
            public void CopyTo(K[] array, int arrayIndex)
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
                        array[i++] = slot.key;
                }
            }

            /// <inheritdoc/>
            void ICollection<K>.Add(K item) =>
                throw new NotSupportedException("KeyCollection is read-only.");

            /// <inheritdoc/>
            void ICollection<K>.Clear() =>
                throw new NotSupportedException("KeyCollection is read-only.");

            /// <inheritdoc/>
            bool ICollection<K>.Remove(K item) =>
                throw new NotSupportedException("KeyCollection is read-only.");

            /// <summary>
            /// Enumerates the keys in a <see cref="ReadOnlyKeyCollection"/>.
            /// </summary>
            public struct Enumerator : IEnumerator<K>
            {
                private readonly ReactiveDictionary<K, V> _dictionary;
                private int _index;
                private K _current;

                /// <summary>
                /// Initializes a new instance of the <see cref="Enumerator"/> struct for the specified dictionary.
                /// </summary>
                /// <param name="dictionary">The dictionary to enumerate keys from.</param>
                internal Enumerator(ReactiveDictionary<K, V> dictionary)
                {
                    _dictionary = dictionary;
                    _index = 0;
                    _current = default;
                }

                /// <summary>
                /// Gets the key at the current position of the enumerator.
                /// </summary>
                public K Current => _current;

                /// <summary>
                /// Gets the current element in the collection.
                /// Explicit implementation for <see cref="IEnumerator"/>.
                /// </summary>
                object IEnumerator.Current => _current;

                /// <summary>
                /// Advances the enumerator to the next key in the collection.
                /// </summary>
                /// <returns>
                /// <c>true</c> if successfully advanced to the next key; <c>false</c> if the enumerator has passed the end.
                /// </returns>
                public bool MoveNext()
                {
                    while (_index < _dictionary._lastIndex)
                    {
                        ref readonly Slot slot = ref _dictionary._slots[_index++];
                        if (slot.exists)
                        {
                            _current = slot.key;
                            return true;
                        }
                    }

                    _current = default;
                    return false;
                }

                /// <summary>
                /// Resets the enumerator to its initial position, before the first key in the collection.
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