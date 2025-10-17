using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Entities.InternalUtils;

#if UNITY_5_3_OR_NEWER
using Unsafe = Unity.Collections.LowLevel.Unsafe.UnsafeUtility;
#else
using Unsafe = System.Runtime.CompilerServices.Unsafe;
#endif

namespace Atomic.Entities
{
    public partial class Entity
    {
        /// <summary>
        /// Invoked when a new value is added to the entity.
        /// </summary>
        public event Action<IEntity, int> OnValueAdded;

        /// <summary>
        /// Invoked when a value is deleted from the entity.
        /// </summary>
        public event Action<IEntity, int> OnValueDeleted;

        /// <summary>
        /// Invoked when a value is changed in the entity.
        /// </summary>
        public event Action<IEntity, int> OnValueChanged;

        /// <summary>
        /// Gets the total number of values stored in the entity.
        /// </summary>
        public int ValueCount => _valueCount;

        internal struct ValueSlot
        {
            public int key;
            public object value;

            public bool primitive;
            public bool exists;
            public int next;
        }

        private interface IBoxing
        {
            object Value { get; }

            Type Type { get; }
        }

        private sealed class Boxing<T> : IBoxing
        {
            public Boxing()
            {
            }

            public Boxing(T value)
            {
                this.value = value;
            }

            object IBoxing.Value => value;

            Type IBoxing.Type => typeof(T);

            public T value;
        }

        private ValueSlot[] _valueSlots;
        private int _valueCapacity;
        private int _valueCount;
        private int _valuePrimeIndex;

        private int[] _valueBuckets;
        private int _valueFreeList;
        private int _valueLastIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConstructValues(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _valueCapacity = CeilToPrime(capacity, out _valuePrimeIndex);
            _valueSlots = new ValueSlot[_valueCapacity];
            _valueBuckets = new int[_valueCapacity];
            Array.Fill(_valueBuckets, UNDEFINED_INDEX);

            _valueCount = 0;
            _valueLastIndex = 0;
            _valueFreeList = UNDEFINED_INDEX;
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
                    return slot.primitive ? ((IBoxing) slot.value).Value : slot.value;

                index = slot.next;
            }

            throw this.ValueNotFoundException(key);
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
            if (_valueCount > 0)
            {
                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _valueCapacity;
                int index = _valueBuckets[bucket];

                while (index >= 0)
                {
                    ref readonly ValueSlot slot = ref _valueSlots[index];
                    if (slot.exists && slot.key == key)
                        return slot.primitive
                            ? ((Boxing<T>) slot.value).value
                            : (T) slot.value;

                    index = slot.next;
                }
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
            if (_valueCount > 0)
            {
                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _valueCapacity;
                int index = _valueBuckets[bucket];

                while (index >= 0)
                {
                    ref readonly ValueSlot slot = ref _valueSlots[index];
                    if (slot.exists && slot.key == key)
                    {
                        value = slot.primitive ? ((Boxing<T>) slot.value).value : (T) slot.value;
                        return true;
                    }

                    index = slot.next;
                }
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
            if (_valueCount > 0)
            {
                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _valueCapacity;
                int index = _valueBuckets[bucket];

                while (index >= 0)
                {
                    ref readonly ValueSlot slot = ref _valueSlots[index];
                    if (slot.exists && slot.key == key)
                    {
                        value = slot.primitive ? ((IBoxing) slot.value).Value : slot.value;
                        return true;
                    }

                    index = slot.next;
                }
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
        public ref T GetValueUnsafe<T>(int key)
        {
            if (_valueCount != 0)
            {
                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _valueCapacity;
                int index = _valueBuckets[bucket];

                while (index >= 0)
                {
                    ref ValueSlot slot = ref _valueSlots[index];
                    if (slot.exists && slot.key == key)
                        return ref slot.primitive
                            ? ref Unsafe.As<object, Boxing<T>>(ref slot.value).value
                            : ref Unsafe.As<object, T>(ref slot.value);

                    index = slot.next;
                }
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
        public bool TryGetValueUnsafe<T>(int key, out T value)
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
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                {
                    value = slot.primitive
                        ? Unsafe.As<object, Boxing<T>>(ref slot.value).value
                        : Unsafe.As<object, T>(ref slot.value);

                    return true;
                }

                index = slot.next;
            }

            value = default;
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
                primitive = false,
                next = next,
                exists = true
            };

            next = index;
            _valueCount++;

            this.OnValueAdded?.Invoke(this, key);
            this.OnStateChanged?.Invoke(this);
        }

        /// Adds a strongly-typed struct value to the entity.
        /// </summary>
        /// <typeparam name="T">The struct type of the value.</typeparam>
        /// <param name="key">The key for the value.</param>
        /// <param name="value">The value to add.</param>
        /// <exception cref="ArgumentException">Thrown if a value with the same key already exists.</exception>
        public void AddValue<T>(int key, T value) where T : struct
        {
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
                value = new Boxing<T>(value),
                primitive = true,
                next = next,
                exists = true
            };

            next = index;
            _valueCount++;

            this.OnValueAdded?.Invoke(this, key);
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

                        this.OnValueDeleted?.Invoke(this, key);
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
                if (!slot.primitive && slot.value.Equals(value))
                    return;

                slot.value = value;
                slot.primitive = false;

                this.NotifyAboutValueChanged(key);
            }
            else
            {
                this.AddValueInternal(key, value, boxing: false);
                this.NotifyAboutValueAdded(key);
            }
        }

        /// <summary>
        /// Sets or updates a value of struct type.
        /// </summary>
        /// <typeparam name="T">The struct type of the value.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="value">The new value.</param>
        public void SetValue<T>(int key, T value) where T : struct
        {
            if (this.FindValueIndex(key, out int index))
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (!slot.primitive)
                {
                    slot.value = new Boxing<T> {value = value};
                    slot.primitive = true;
                    this.NotifyAboutValueChanged(key);
                    return;
                }

                IBoxing iBoxing = (IBoxing) slot.value;
                if (iBoxing.Type != typeof(T))
                {
                    slot.value = new Boxing<T> {value = value};
                    this.NotifyAboutValueChanged(key);
                    return;
                }

                Boxing<T> tBoxing = (Boxing<T>) iBoxing;
                if (tBoxing.value.Equals(value))
                    return;

                tBoxing.value = value;
                this.NotifyAboutValueChanged(key);
            }
            else
            {
                this.AddValueInternal(key, new Boxing<T> {value = value}, boxing: true);
                this.NotifyAboutValueChanged(key);
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

            Span<int> removedItems = stackalloc int[_valueCount];
            int removedCount = 0;

            for (int i = 0; i < _valueLastIndex; i++)
            {
                _valueBuckets[i] = UNDEFINED_INDEX;

                ref ValueSlot slot = ref _valueSlots[i];
                if (!slot.exists)
                    continue;

                slot.exists = false;
                slot.next = UNDEFINED_INDEX;
                removedItems[removedCount++] = slot.key;
            }

            _valueCount = 0;
            _valueFreeList = UNDEFINED_INDEX;
            _valueLastIndex = 0;

            this.OnStateChanged?.Invoke(this);

            for (int i = 0; i < removedCount; i++)
                this.OnValueDeleted?.Invoke(this, removedItems[i]);
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

                object value = slot.primitive ? ((IBoxing) slot.value).Value : slot.value;
                KeyValuePair<int, object> pair = new KeyValuePair<int, object>(slot.key, value);
                results[count++] = pair;
            }

            return count;
        }

        /// <summary>
        /// Enumerates all key-value pairs stored in the entity.
        /// </summary>
        /// <returns>An enumerator over key-value pairs.</returns>
        IEnumerator<KeyValuePair<int, object>> IEntity.GetValueEnumerator()
        {
            return new ValueEnumerator(this);
        }

        public ValueEnumerator GetValueEnumerator()
        {
            return new ValueEnumerator(this);
        }

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
        private void AddValueInternal(int key, object value, bool boxing)
        {
            int index;
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
                primitive = boxing,
                next = next,
                exists = true
            };

            next = index;
            _valueCount++;
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
        private void NotifyAboutValueChanged(int key)
        {
            this.OnValueChanged?.Invoke(this, key);
            this.OnStateChanged?.Invoke(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void NotifyAboutValueAdded(int key)
        {
            this.OnValueAdded?.Invoke(this, key);
            this.OnStateChanged?.Invoke(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private KeyNotFoundException ValueNotFoundException(int key) =>
            new($"The given value {EntityNames.IdToName(key)} was not present in the entity: {_name}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Exception ValueAlreadyAddedException(int key) =>
            new ArgumentException(
                $"A value with the same key {EntityNames.IdToName(key)} already has been added in the entity: {_name}!");

        public struct ValueEnumerator : IEnumerator<KeyValuePair<int, object>>
        {
            private readonly Entity _entity;
            private int _index;
            private KeyValuePair<int, object> _current;

            public KeyValuePair<int, object> Current => _current;
            object IEnumerator.Current => _current;

            public ValueEnumerator(Entity entity)
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

                    _current = new KeyValuePair<int, object>(slot.key, slot.primitive
                        ? ((IBoxing) slot.value).Value
                        : slot.value);

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