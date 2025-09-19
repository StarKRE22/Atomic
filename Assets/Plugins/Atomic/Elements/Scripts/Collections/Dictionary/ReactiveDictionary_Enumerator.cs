using System.Collections;
using System.Collections.Generic;

namespace Atomic.Elements
{
    public partial class ReactiveDictionary<K, V>
    {
        /// <summary>
        /// Enumerates the key-value pairs in a <see cref="ReactiveDictionary{K,V}"/>.
        /// </summary>
        /// <remarks>
        /// This struct allows iteration over the dictionary without modifying it. 
        /// It implements <see cref="IEnumerator{KeyValuePair{K,V}}"/> and is used by
        /// <see cref="ReactiveDictionary{K,V}"/> for <c>foreach</c> iteration.
        /// </remarks>
        public struct Enumerator : IEnumerator<KeyValuePair<K, V>>
        {
            private readonly ReactiveDictionary<K, V> _dictionary;
            private int _index;
            private KeyValuePair<K, V> _current;

            /// <summary>
            /// Gets the current <see cref="KeyValuePair{K,V}"/> in the enumeration.
            /// </summary>
            public KeyValuePair<K, V> Current => _current;

            /// <summary>
            /// Gets the current element in the collection.
            /// Explicit implementation for <see cref="IEnumerator"/>.
            /// </summary>
            object IEnumerator.Current => _current;

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> struct for the specified dictionary.
            /// </summary>
            /// <param name="dictionary">The dictionary to enumerate.</param>
            internal Enumerator(ReactiveDictionary<K, V> dictionary)
            {
                _dictionary = dictionary;
                _index = 0;
                _current = default;
            }

            /// <summary>
            /// Advances the enumerator to the next element of the dictionary.
            /// </summary>
            /// <returns>
            /// <c>true</c> if the enumerator was successfully advanced to the next element; 
            /// <c>false</c> if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                while (_index < _dictionary._lastIndex)
                {
                    ref readonly Slot slot = ref _dictionary._slots[_index++];
                    if (slot.exists)
                    {
                        _current = new KeyValuePair<K, V>(slot.key, slot.value);
                        return true;
                    }
                }

                _current = default;
                return false;
            }

            /// <summary>
            /// Resets the enumerator to its initial position, before the first element in the collection.
            /// </summary>
            void IEnumerator.Reset()
            {
                _index = 0;
                _current = default;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting resources.
            /// </summary>
            /// <remarks>This enumerator does not hold unmanaged resources, so <c>Dispose</c> does nothing.</remarks>
            public void Dispose()
            {
                // No resources to release
            }
        }
    }
}