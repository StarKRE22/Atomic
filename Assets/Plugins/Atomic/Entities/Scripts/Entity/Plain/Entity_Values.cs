using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Atomic.Entities
{
    public unsafe partial class Entity
    {
        public event Action<IEntity, int> OnValueAdded;
        public event Action<IEntity, int> OnValueDeleted;
        public event Action<IEntity, int> OnValueChanged;

        public int ValueCount => _valueCount;

        private ValueSlot[] _valueSlots;
        private int _valueCapacity;
        private int _valueCount;

        private int[] _valueBuckets;
        private int _valueFreeList;
        private int _valueLastIndex;

        public T GetValue<T>(int key)
        {
            if (_valueCount == 0)
                throw this.ValueNotFoundException(in key);

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[UnsafeUtility.As<uint, int>(ref bucket)];

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

        public ref T GetValueUnsafe<T>(int key)
        {
            if (_valueCount == 0)
                throw this.ValueNotFoundException(in key);

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[UnsafeUtility.As<uint, int>(ref bucket)];

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

        public object GetValue(int key)
        {
            if (_valueCount == 0)
                throw this.ValueNotFoundException(in key);

            uint bucket = UnsafeUtility.As<int, uint>(ref key) % UnsafeUtility.As<int, uint>(ref _valueCapacity);
            int index = _valueBuckets[UnsafeUtility.As<uint, int>(ref bucket)];

            while (index >= 0)
            {
                ref ValueSlot slot = ref _valueSlots[index];
                if (slot.exists && slot.key == key)
                    return UnboxValue(in slot);

                index = slot.next;
            }

            throw this.ValueNotFoundException(in key);
        }

        public bool TryGetValue<T>(int key, out T value)
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
                    value = slot.primitive ? ((Boxing<T>) slot.value).value : (T) slot.value;
                    return true;
                }

                index = slot.next;
            }

            value = default;
            return false;
        }

        public bool TryGetValueUnsafe<T>(int key, out T value)
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

        public bool HasValue(int key)
        {
            return this.FindValueIndex(key, out _);
        }

        public void AddValue<T>(int key, in T value) where T : struct
        {
            if (this.FindValueIndex(in key, out _))
                throw ValueAlreadyAddedException(key);

            this.AddValueInternal(in key, new Boxing<T> {value = value}, boxing: true);
            this.NotifyAboutValueAdded(in key);
        }

        public void AddValue(int key, in object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (this.FindValueIndex(in key, out _))
                throw ValueAlreadyAddedException(key);

            this.AddValueInternal(in key, in value, boxing: false);
            this.NotifyAboutValueAdded(in key);
        }

        public bool DelValue(int key)
        {
            if (!this.DelValueInternal(key))
                return false;

            this.NotifyAboutValueDeleted(in key);
            return true;
        }

        public void SetValue(int key, in object value)
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

        public void SetValue<T>(int key, in T value) where T : struct
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

        public void ClearValues()
        {
            if (_valueCount == 0)
                return;

            int* removedItems = stackalloc int[_valueCount];
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

        public KeyValuePair<int, object>[] GetValues()
        {
            var results = new KeyValuePair<int, object>[_valueCount];
            this.GetValues(in results);
            return results;
        }

        public int GetValues(in KeyValuePair<int, object>[] results)
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

        public IEnumerator<KeyValuePair<int, object>> ValueEnumerator()
        {
            return new _ValueEnumerator(this);
        }

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
            _valueCapacity = AtomicHelper.GetPrime(_valueCapacity + 1);
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
                this.AddValue(key, in value);
        }

        private void InitializeValues(in int capacity = 0)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _valueCapacity = AtomicHelper.GetPrime(capacity);
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