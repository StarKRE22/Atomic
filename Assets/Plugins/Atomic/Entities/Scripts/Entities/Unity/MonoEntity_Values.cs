#if UNITY_5_3_OR_NEWER
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Entities.InternalUtils;
using Unsafe = Unity.Collections.LowLevel.Unsafe.UnsafeUtility;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides value management functionality for the <see cref="MonoEntity"/>, allowing to set, get, check,
    /// and remove values associated with an entity. 
    /// </summary>
    public partial class MonoEntity
    {
        /// <summary>
        /// Invoked when a new value is added to the entity.
        /// </summary>
        public event Action<IEntity, int, object> OnValueAdded;

        /// <summary>
        /// Invoked when a value is deleted from the entity.
        /// </summary>
        public event Action<IEntity, int, object> OnValueDeleted;

        /// <summary>
        /// Invoked when a value is changed in the entity.
        /// </summary>
        public event Action<IEntity, int, object> OnValueChanged;

        /// <summary>
        /// Gets the total number of values stored in the entity.
        /// </summary>
        public int ValueCount => _valueCount;

        internal struct ValueSlot
        {
            public int key;
            public object value;
            public bool exists;
            public int next;
        }

        private ValueSlot[] _valueSlots;
        private int _valueCapacity;
        private int _valueCount;
        private int _valuePrimeIndex;

        private int[] _valueBuckets;
        private int _valueFreeList;
        private int _valueLastIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConstructValues()
        {
            _valueCapacity = CeilToPrime(initialValueCapacity, out _valuePrimeIndex);
            _valueSlots = new ValueSlot[_valueCapacity];
            _valueBuckets = new int[_valueCapacity];
            Array.Fill(_valueBuckets, UNDEFINED_INDEX);

            _valueCount = 0;
            _valueLastIndex = 0;
            _valueFreeList = UNDEFINED_INDEX;
        }

        /// <summary>
        /// Gets the value associated with the specified key and casts it to type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected type of the value.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <returns>The value cast to type <typeparamref name="T"/>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the key does not exist in the entity.</exception>
        public T GetValue<T>(int key)
        {
            if (_valueCount == 0)
                throw this.ValueNotFoundException(key);

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _valueCapacity;
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref readonly ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                    return (T) slot.value;

                index = slot.next;
            }

            throw this.ValueNotFoundException(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key as an object.
        /// </summary>
        /// <param name="key">The key associated with the value.</param>
        /// <returns>The boxed value.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the key does not exist in the entity.</exception>
        public object GetValue(int key)
        {
            if (_valueCount == 0)
                throw this.ValueNotFoundException(key);

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _valueCapacity;
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref readonly ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                    return slot.value;

                index = slot.next;
            }

            throw this.ValueNotFoundException(key);
        }

        /// <summary>
        /// Tries to get a value by key and cast it to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected type of the value.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="value">The output value if found.</param>
        /// <returns>True if the value is found; otherwise, false.</returns>
        public bool TryGetValue<T>(int key, out T value)
        {
            if (_valueCount == 0)
            {
                value = default;
                return false;
            }

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _valueCapacity;
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref readonly ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                {
                    value = (T) slot.value;
                    return true;
                }

                index = slot.next;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Tries to get a value as an object by key.
        /// </summary>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="value">The output value if found.</param>
        /// <returns>True if the value is found; otherwise, false.</returns>
        public bool TryGetValue(int key, out object value)
        {
            if (_valueCount == 0)
            {
                value = null;
                return false;
            }

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _valueCapacity;
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref readonly ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                {
                    value = slot.value;
                    return true;
                }

                index = slot.next;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key by reference (unsafe, no boxing).
        /// </summary>
        /// <typeparam name="T">The expected struct type of the value.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <returns>A reference to the stored value.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the key does not exist in the entity.</exception>
        public ref T GetValueUnsafe<T>(int key) where T : class
        {
            if (_valueCount == 0)
                throw this.ValueNotFoundException(key);

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _valueCapacity;
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                    return ref Unsafe.As<object, T>(ref slot.value);

                index = slot.next;
            }

            throw this.ValueNotFoundException(key);
        }

        /// <summary>
        /// Tries to get a reference to a struct value by key (unsafe).
        /// </summary>
        /// <typeparam name="T">The struct type of the value.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="value">The output value if found.</param>
        /// <returns>True if the value is found; otherwise, false.</returns>
        public bool TryGetValueUnsafe<T>(int key, out T value) where T : class
        {
            if (_valueCount == 0)
            {
                value = null;
                return false;
            }

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _valueCapacity;
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                {
                    value = Unsafe.As<object, T>(ref slot.value);
                    return true;
                }

                index = slot.next;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Checks whether the entity contains a value with the specified key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if a value exists for the key; otherwise, false.</returns>
        public bool HasValue(int key)
        {
            if (_valueCount > 0)
            {
                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _valueCapacity;
                int index = _valueBuckets[bucket];

                while (index >= 0)
                {
                    ref readonly ValueSlot slot = ref _valueSlots[index];
                    if (slot.exists && slot.key == key)
                        return true;

                    index = slot.next;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a reference type value to the entity.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <param name="value">The value to add.</param>
        /// <exception cref="ArgumentException">Thrown if a value with the same key already exists.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the value is null.</exception>
        /// <summary>
        public void AddValue(int key, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            int hash, bucket, index;
            if (_valueCount > 0)
            {
                hash = key & 0x7FFFFFFF;
                bucket = hash % _valueCapacity;
                index = _valueBuckets[bucket];

                while (index >= 0)
                {
                    ref readonly ValueSlot slot = ref _valueSlots[index];
                    if (slot.exists && slot.key == key)
                        throw ValueAlreadyAddedException(key);

                    index = slot.next;
                }
            }

            if (_valueFreeList >= 0)
            {
                index = _valueFreeList;
                _valueFreeList = _valueSlots[index].next;
            }
            else
            {
                if (_valueLastIndex == _valueCapacity)
                    this.IncreaseValueCapacity();

                index = _valueLastIndex;
                _valueLastIndex++;
            }

            hash = key & 0x7FFFFFFF;
            bucket = hash % _valueCapacity;
            ref int next = ref _valueBuckets[bucket];
            
            _valueSlots[index] = new ValueSlot
            {
                key = key,
                value = value,
                next = next,
                exists = true
            };

            next = index;
            _valueCount++;

            this.OnValueAdded?.Invoke(this, key, value);
            this.OnStateChanged?.Invoke(this);
        }

        /// <summary>
        /// Deletes a value by key from the entity.
        /// </summary>
        /// <param name="key">The key associated with the value to delete.</param>
        /// <returns>True if the value was successfully deleted; otherwise, false.</returns>
        public bool DelValue(int key)
        {
            if (_valueCount > 0)
            {
                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _valueCapacity;
                ref int next = ref _valueBuckets[bucket];

                int index = next;
                int last = UNDEFINED_INDEX;

                while (index >= 0)
                {
                    ref ValueSlot node = ref _valueSlots[index];
                    if (node.key == key)
                    {
                        if (last == UNDEFINED_INDEX)
                            next = node.next;
                        else
                            _valueSlots[last].next = node.next;

                        object removed = node.value;
                        node.next = _valueFreeList;
                        node.exists = false;

                        _valueCount--;

                        if (_valueCount == 0)
                        {
                            _valueLastIndex = 0;
                            _valueFreeList = UNDEFINED_INDEX;
                        }
                        else
                        {
                            _valueFreeList = index;
                        }

                        this.OnValueDeleted?.Invoke(this, key, removed);
                        this.OnStateChanged?.Invoke(this);
                        return true;
                    }

                    last = index;
                    index = node.next;
                }
            }

            return false;
        }

        /// <summary>
        /// Sets or updates a value of reference type.
        /// </summary>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="value">The new value.</param>
        /// <exception cref="ArgumentNullException">Thrown if the value is null.</exception>
        public void SetValue(int key, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (this.FindValueIndex(key, out int index))
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (!slot.value.Equals(value))
                {
                    slot.value = value;
                    this.OnValueChanged?.Invoke(this, key, value);
                    this.OnStateChanged?.Invoke(this);
                }
            }
            else
            {
                if (_valueFreeList >= 0)
                {
                    index = _valueFreeList;
                    _valueFreeList = _valueSlots[index].next;
                }
                else
                {
                    if (_valueLastIndex == _valueCapacity)
                        this.IncreaseValueCapacity();

                    index = _valueLastIndex;
                    _valueLastIndex++;
                }

                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _valueCapacity;
                ref int next = ref _valueBuckets[bucket];

                _valueSlots[index] = new ValueSlot
                {
                    key = key,
                    value = value,
                    next = next,
                    exists = true
                };

                next = index;
                _valueCount++;
                
                this.OnValueAdded?.Invoke(this, key, value);
                this.OnStateChanged?.Invoke(this);
            }
        }

        /// <summary>
        /// Clears all values from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearValues()
        {
            if (_valueCount == 0)
                return;

            var arrayPool = ArrayPool<KeyValuePair<int, object>>.Shared;
            KeyValuePair<int, object>[] removedItems = arrayPool.Rent(_valueCount);
            int removedCount = 0;

            for (int i = 0; i < _valueLastIndex; i++)
            {
                _valueBuckets[i] = UNDEFINED_INDEX;

                ref ValueSlot slot = ref _valueSlots[i];
                if (!slot.exists)
                    continue;

                slot.exists = false;
                slot.next = UNDEFINED_INDEX;
                removedItems[removedCount++] = new KeyValuePair<int, object>(slot.key, slot.value);
            }

            _valueCount = 0;
            _valueFreeList = UNDEFINED_INDEX;
            _valueLastIndex = 0;

            try
            {
                this.OnStateChanged?.Invoke(this);

                for (int i = 0; i < removedCount; i++)
                {
                    KeyValuePair<int, object> item = removedItems[i];
                    this.OnValueDeleted?.Invoke(this, item.Key, item.Value);
                }
            }
            finally
            {
                arrayPool.Return(removedItems);
            }
        }

        /// <summary>
        /// Returns an array of all key-value pairs stored in the entity.
        /// </summary>
        /// <returns>An array of key-value pairs.</returns>
        public KeyValuePair<int, object>[] GetValues()
        {
            var results = new KeyValuePair<int, object>[_valueCount];
            this.CopyValues(results);
            return results;
        }

        /// <summary>
        /// Copies all key-value pairs into the provided array.
        /// </summary>
        /// <param name="results">The array to copy values into.</param>
        /// <returns>The number of copied items.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CopyValues(KeyValuePair<int, object>[] results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            int count = 0;

            for (int i = 0; i < _valueLastIndex; i++)
            {
                ref readonly ValueSlot slot = ref _valueSlots[i];
                if (!slot.exists)
                    continue;

                KeyValuePair<int, object> pair = new KeyValuePair<int, object>(slot.key, slot.value);
                results[count++] = pair;
            }

            return count;
        }

        /// <summary>
        /// Enumerates all key-value pairs stored in the entity.
        /// </summary>
        /// <returns>An enumerator over key-value pairs.</returns>
        IEnumerator<KeyValuePair<int, object>> IEntity.GetValueEnumerator() => new ValueEnumerator(this);

        public ValueEnumerator GetValueEnumerator() => new(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool FindValueIndex(int key, out int index)
        {
            if (_valueCount == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _valueCapacity;
            index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref readonly ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                    return true;

                index = slot.next;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseValueCapacity()
        {
            _valueCapacity = PrimeTable[++_valuePrimeIndex];

            Array.Resize(ref _valueSlots, _valueCapacity);
            Array.Resize(ref _valueBuckets, _valueCapacity);
            Array.Fill(_valueBuckets, UNDEFINED_INDEX);

            for (int i = 0; i < _valueLastIndex; i++)
            {
                ref ValueSlot slot = ref _valueSlots[i];
                if (!slot.exists)
                    continue;

                int hash = slot.key & 0x7FFFFFFF;
                int bucket = hash % _valueCapacity;
                ref int next = ref _valueBuckets[bucket];

                slot.next = next;
                next = i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private KeyNotFoundException ValueNotFoundException(int key) =>
            new($"The given value {EntityKeyStore.IdToName(key)} was not present in the entity: {this.name}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Exception ValueAlreadyAddedException(int key) =>
            new ArgumentException($"A value with the same key {EntityKeyStore.IdToName(key)} already has been added!");

        public struct ValueEnumerator : IEnumerator<KeyValuePair<int, object>>
        {
            private readonly MonoEntity _entity;
            private int _index;
            private KeyValuePair<int, object> _current;

            public KeyValuePair<int, object> Current => _current;
            object IEnumerator.Current => _current;

            public ValueEnumerator(MonoEntity entity)
            {
                _entity = entity;
                _index = 0;
                _current = default;
            }

            public bool MoveNext()
            {
                while (_index < _entity._valueLastIndex)
                {
                    ref readonly ValueSlot slot = ref _entity._valueSlots[_index++];
                    if (!slot.exists)
                        continue;

                    _current = new KeyValuePair<int, object>(slot.key, slot.value);
                    return true;
                }

                _current = default;
                return false;
            }

            public void Reset()
            {
                _index = 0;
                _current = default;
            }

            public void Dispose()
            {
                //Do nothing...
            }
        }
    }
}
#endif