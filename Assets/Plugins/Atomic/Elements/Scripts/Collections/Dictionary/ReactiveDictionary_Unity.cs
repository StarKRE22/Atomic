#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive dictionary that supports Unity serialization.
    /// Implements <see cref="ISerializationCallbackReceiver"/> to handle conversion
    /// between the internal dictionary and a serializable array of key-value pairs.
    /// </summary>
    [Serializable]
    public partial class ReactiveDictionary<K, V> : ISerializationCallbackReceiver
    {
        /// <summary>
        /// A serializable representation of a key-value pair.
        /// Used internally to serialize and deserialize the dictionary in Unity.
        /// </summary>
        [Serializable]
        internal struct Pair
        {
            /// <summary>
            /// The key of the pair.
            /// </summary>
            public K key;

            /// <summary>
            /// The value of the pair.
            /// </summary>
            public V value;
        }

        /// <summary>
        /// The array of serialized key-value pairs used by Unity's serialization system.
        /// This field is populated during serialization and deserialization.
        /// </summary>
        [SerializeField]
        internal Pair[] serializedItems;

        /// <summary>
        /// Unity callback invoked after the object has been deserialized.
        /// Reconstructs the internal dictionary from the serialized pair array.
        /// </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (this.serializedItems == null)
                return;

            this.Clear();
            for (int i = 0, count = this.serializedItems.Length; i < count; i++)
            {
                Pair pair = this.serializedItems[i];
                this.Add(pair.key, pair.value);
            }
        }

        /// <summary>
        /// Unity callback invoked before the object is serialized.
        /// Flattens the internal dictionary to a serializable array of key-value pairs.
        /// </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.serializedItems = new Pair[_count];

            int i = 0;
            foreach ((K key, V value) in this)
            {
                this.serializedItems[i++] = new Pair
                {
                    key = key,
                    value = value
                };
            }
        }
    }
}
#endif
