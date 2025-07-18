using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

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

        private ValueSlot[] _valueSlots;
        private int _valueCapacity;
        private int _valueCount;

        private int[] _valueBuckets;
        private int _valueFreeList;
        private int _valueLastIndex;

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
                throw this.ValueNotFoundException(in key);

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                    if (slot.primitive) return ((Boxing<T>) slot.value).value;
                    else return (T) slot.value;

                index = slot.next;
            }

            throw this.ValueNotFoundException(in key);
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
            if (_valueCount == 0)
                throw this.ValueNotFoundException(in key);

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                    if (slot.primitive) return ref UnsafeUtility.As<object, Boxing<T>>(ref slot.value).value;
                    else return ref UnsafeUtility.As<object, T>(ref slot.value);

                index = slot.next;
            }

            throw this.ValueNotFoundException(in key);
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
                throw this.ValueNotFoundException(in key);

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                    return UnboxValue(in slot);

                index = slot.next;
            }

            throw this.ValueNotFoundException(in key);
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

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                {
                    value = slot.primitive ? ((Boxing<T>) slot.value).value : (T) slot.value;
                    return true;
                }

                index = slot.next;
            }

            value = default;
            return false;
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

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[bucket];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                {
                    value = slot.primitive
                        ? UnsafeUtility.As<object, Boxing<T>>(ref slot.value).value
                        : UnsafeUtility.As<object, T>(ref slot.value);

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
                value = default;
                return false;
            }

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[UnsafeUtility.As<uint, int>(ref bucket)];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                {
                    value = UnboxValue(in slot);
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
        public bool HasValue(int key) => this.FindValueIndex(key, out _);

        /// Adds a strongly-typed struct value to the entity.
        /// </summary>
        /// <typeparam name="T">The struct type of the value.</typeparam>
        /// <param name="key">The key for the value.</param>
        /// <param name="value">The value to add.</param>
        /// <exception cref="ArgumentException">Thrown if a value with the same key already exists.</exception>
        public void AddValue<T>(int key, T value) where T : struct
        {
            if (this.FindValueIndex(in key, out _))
                throw ValueAlreadyAddedException(key);

            this.AddValueInternal(in key, new Boxing<T> {value = value}, boxing: true);
            this.NotifyAboutValueAdded(in key);
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

            if (this.FindValueIndex(in key, out _))
                throw ValueAlreadyAddedException(key);

            this.AddValueInternal(in key, in value, boxing: false);
            this.NotifyAboutValueAdded(in key);
        }

        /// <summary>
        /// Deletes a value by key from the entity.
        /// </summary>
        /// <param name="key">The key associated with the value to delete.</param>
        /// <returns>True if the value was successfully deleted; otherwise, false.</returns>
        public bool DelValue(int key)
        {
            if (!this.DelValueInternal(key))
                return false;

            this.NotifyAboutValueDeleted(in key);
            return true;
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

            if (!this.FindValueIndex(in key, out int index))
            {
                this.AddValueInternal(in key, in value, boxing: false);
                this.NotifyAboutValueAdded(key);
                return;
            }

            ref ValueSlot slot = ref _valueSlots[index];
            if (!slot.primitive && slot.value.Equals(value))
                return;

            slot.value = value;
            slot.primitive = false;

            this.NotifyAboutValueChanged(in key);
        }

        /// <summary>
        /// Sets or updates a value of struct type.
        /// </summary>
        /// <typeparam name="T">The struct type of the value.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="value">The new value.</param>
        public void SetValue<T>(int key, T value) where T : struct
        {
            if (!this.FindValueIndex(in key, out int index))
            {
                this.AddValueInternal(in key, new Boxing<T> {value = value}, boxing: true);
                this.NotifyAboutValueChanged(in key);
                return;
            }

            ref ValueSlot slot = ref _valueSlots[index];
            if (!slot.primitive)
            {
                slot.value = new Boxing<T> {value = value};
                slot.primitive = true;
                this.NotifyAboutValueChanged(in key);
                return;
            }

            IBoxing boxing = (IBoxing) slot.value;
            if (boxing.Type != typeof(T))
            {
                slot.value = new Boxing<T> {value = value};
                this.NotifyAboutValueChanged(key);
                return;
            }

            Boxing<T> tBoxing = (Boxing<T>) boxing;
            tBoxing.value = value;
            this.NotifyAboutValueChanged(key);
        }
        
        /// <summary>
        /// Clears all values from the entity.
        /// </summary>
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

            for (int i = 0; i < removedCount; i++)
                this.OnValueDeleted?.Invoke(this, removedItems[i]);

            this.OnStateChanged?.Invoke();
        }
        
        /// <summary>
        /// Returns an array of all key-value pairs stored in the entity.
        /// </summary>
        /// <returns>An array of key-value pairs.</returns>
        public KeyValuePair<int, object>[] GetValues()
        {
            var results = new KeyValuePair<int, object>[_valueCount];
            this.GetValues(results);
            return results;
        }

        /// <summary>
        /// Copies all key-value pairs into the provided array.
        /// </summary>
        /// <param name="results">The array to copy values into.</param>
        /// <returns>The number of copied items.</returns>
        public int GetValues(KeyValuePair<int, object>[] results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            int count = 0;

            for (int i = 0; i < _valueLastIndex; i++)
            {
                ref ValueSlot slot = ref _valueSlots[i];
                if (!slot.exists)
                    continue;

                var pair = new KeyValuePair<int, object>(slot.key, UnboxValue(in slot));
                results[count++] = pair;
            }

            return count;
        }
        
        /// <summary>
        /// Enumerates all key-value pairs stored in the entity.
        /// </summary>
        /// <returns>An enumerator over key-value pairs.</returns>
        public IEnumerator<KeyValuePair<int, object>> ValueEnumerator() => new _ValueEnumerator(this);

        private bool FindValueIndex(in int key, out int index)
        {
            if (_valueCount == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            index = this.ValueBucket(in key);
            while (index >= 0)
            {
                ValueSlot slot = _valueSlots[index];
                if (slot.exists && slot.key == key)
                    return true;

                index = slot.next;
            }

            return false;
        }

        private void AddValueInternal(in int key, in object value, in bool boxing)
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

            ref int bucket = ref this.ValueBucket(in key);

            _valueSlots[index] = new ValueSlot
            {
                key = key,
                value = value,
                primitive = boxing,
                next = bucket,
                exists = true
            };

            bucket = index;
            _valueCount++;
        }

        private bool DelValueInternal(in int key)
        {
            if (_valueCount == 0)
                return false;

            ref int bucket = ref this.ValueBucket(in key);

            int index = bucket;
            int last = UNDEFINED_INDEX;

            while (index >= 0)
            {
                ref ValueSlot node = ref _valueSlots[index];
                if (node.key == key)
                {
                    if (last == UNDEFINED_INDEX)
                        bucket = node.next;
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

                    return true;
                }

                last = index;
                index = node.next;
            }

            return false;
        }

        private void IncreaseValueCapacity()
        {
            _valueCapacity = InternalUtils.GetPrime(_valueCapacity + 1);
            Array.Resize(ref _valueSlots, _valueCapacity);
            Array.Resize(ref _valueBuckets, _valueCapacity);

            for (int i = 0; i < _valueCapacity; i++)
                _valueBuckets[i] = UNDEFINED_INDEX;

            for (int i = 0; i < _valueLastIndex; i++)
            {
                ref ValueSlot slot = ref _valueSlots[i];
                if (!slot.exists)
                    continue;

                ref int bucket = ref this.ValueBucket(in slot.key);
                slot.next = bucket;
                bucket = i;
            }
        }

        private ref int ValueBucket(in int key)
        {
            int index = (int) ((uint) key % _valueCapacity);
            return ref _valueBuckets[index];
        }

        private void InitializeValues(in IEnumerable<KeyValuePair<int, object>> values)
        {
            if (values == null)
            {
                this.InitializeValues();
                return;
            }

            this.InitializeValues(values.Count());

            foreach ((int key, object value) in values)
                this.AddValue(key, value);
        }

        private void InitializeValues(in int capacity = 0)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _valueCapacity = InternalUtils.GetPrime(capacity);
            _valueSlots = new ValueSlot[_valueCapacity];
            _valueBuckets = new int[_valueCapacity];

            for (int i = 0; i < _valueCapacity; i++)
                _valueBuckets[i] = UNDEFINED_INDEX;

            _valueCount = 0;
            _valueLastIndex = 0;
            _valueFreeList = UNDEFINED_INDEX;
        }

        private void NotifyAboutValueChanged(in int key)
        {
            this.OnValueChanged?.Invoke(this, key);
            this.OnStateChanged?.Invoke();
        }

        private void NotifyAboutValueAdded(in int key)
        {
            this.OnValueAdded?.Invoke(this, key);
            this.OnStateChanged?.Invoke();
        }

        private void NotifyAboutValueDeleted(in int key)
        {
            this.OnValueDeleted?.Invoke(this, key);
            this.OnStateChanged?.Invoke();
        }

        private KeyNotFoundException ValueNotFoundException(in int key) =>
            new($"The given value {EntityUtils.IdToName(key)} was not present in the entity: {this.name}");

        private Exception ValueAlreadyAddedException(int key) =>
            new ArgumentException($"A value with the same key {EntityUtils.IdToName(key)} already has been added!");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object UnboxValue(in ValueSlot slot)
        {
            return slot.primitive
                ? ((IBoxing) slot.value).Value
                : slot.value;
        }

        private struct _ValueEnumerator : IEnumerator<KeyValuePair<int, object>>
        {
            private readonly Entity _entity;
            private int _index;
            private KeyValuePair<int, object> _current;

            public KeyValuePair<int, object> Current => _current;
            object IEnumerator.Current => _current;

            public _ValueEnumerator(Entity entity)
            {
                _entity = entity;
                _index = 0;
                _current = default;
            }

            public bool MoveNext()
            {
                while (_index < _entity._valueLastIndex)
                {
                    ref ValueSlot slot = ref _entity._valueSlots[_index++];
                    if (!slot.exists)
                        continue;

                    _current = new KeyValuePair<int, object>(slot.key, UnboxValue(in slot));
                    return true;
                }

                _current = default;
                return false;
            }

            void IEnumerator.Reset()
            {
                _index = 0;
                _current = default;
            }

            public void Dispose()
            {
                //Do nothing...
            }
        }

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
            object IBoxing.Value => value;
            
            Type IBoxing.Type => typeof(T);

            public T value;
        }
    }
}