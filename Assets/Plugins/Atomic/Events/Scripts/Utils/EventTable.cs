using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Events.EventBusInternalUtils;

#if UNITY_5_3_OR_NEWER
using Unsafe = Unity.Collections.LowLevel.Unsafe.UnsafeUtility;
#else
using Unsafe = System.Runtime.CompilerServices.Unsafe;
#endif

namespace Atomic.Events
{
    /// <summary>
    /// Custom hash-table storage that maps integer event keys to <see cref="Delegate"/> values.
    /// Mirrors the slot/bucket/free-list design used by <see cref="Atomic.Entities.Entity"/> values.
    /// </summary>
    internal sealed class EventTable
    {
        internal struct EventSlot
        {
            public int key;
            public Delegate value;
            public bool exists;
            public int next;
        }

        private const int UNDEFINED_INDEX = -1;

        private EventSlot[] _slots;
        private int[] _buckets;

        private int _capacity;
        private int _count;
        private int _primeIndex;
        private int _freeList;
        private int _lastIndex;

        /// <summary>
        /// Gets the number of events stored in the table.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Creates a new event table with the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity. Will be rounded up to the nearest prime.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="capacity"/> is negative.</exception>
        public EventTable(int capacity = 0)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _capacity = CeilToPrime(capacity, out _primeIndex);
            _slots = new EventSlot[_capacity];
            _buckets = new int[_capacity];
            Array.Fill(_buckets, UNDEFINED_INDEX);

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;
        }

        /// <summary>
        /// Adds a new event delegate for the specified key.
        /// </summary>
        /// <param name="key">The event key.</param>
        /// <param name="value">The delegate value.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if an event with the same key already exists.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEvent(int key, Delegate value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (_count > 0 && this.FindIndex(key, out _))
                throw EventAlreadyAddedException(key);

            int index = this.AllocateSlot();
            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            ref int next = ref _buckets[bucket];

            _slots[index] = new EventSlot
            {
                key = key,
                value = value,
                next = next,
                exists = true
            };

            next = index;
            _count++;
        }

        /// <summary>
        /// Sets the delegate for the specified key, replacing any existing value, or adds it if absent.
        /// </summary>
        /// <param name="key">The event key.</param>
        /// <param name="value">The delegate value.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEvent(int key, Delegate value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (this.FindIndex(key, out int index))
            {
                _slots[index].value = value;
                return;
            }

            index = this.AllocateSlot();
            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            ref int next = ref _buckets[bucket];

            _slots[index] = new EventSlot
            {
                key = key,
                value = value,
                next = next,
                exists = true
            };

            next = index;
            _count++;
        }

        /// <summary>
        /// Removes the event associated with the specified key.
        /// </summary>
        /// <param name="key">The event key.</param>
        /// <returns>True if the event was removed; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool RemoveEvent(int key)
        {
            if (_count == 0)
                return false;

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            ref int next = ref _buckets[bucket];

            int index = next;
            int last = UNDEFINED_INDEX;

            while (index >= 0)
            {
                ref EventSlot slot = ref _slots[index];
                if (slot.exists && slot.key == key)
                {
                    if (last == UNDEFINED_INDEX)
                        next = slot.next;
                    else
                        _slots[last].next = slot.next;

                    slot.value = null;
                    slot.exists = false;
                    slot.next = _freeList;
                    _freeList = index;

                    _count--;
                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                    }

                    return true;
                }

                last = index;
                index = slot.next;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the table contains an event with the specified key.
        /// </summary>
        /// <param name="key">The event key.</param>
        /// <returns>True if the event exists; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsEvent(int key)
        {
            return this.FindIndex(key, out _);
        }

        /// <summary>
        /// Clears all events from the table.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearEvents()
        {
            if (_count == 0)
                return;

            Array.Fill(_buckets, UNDEFINED_INDEX);

            for (int i = 0; i < _lastIndex; i++)
            {
                ref EventSlot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                slot.exists = false;
                slot.value = null;
                slot.next = UNDEFINED_INDEX;
            }

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;
        }

        /// <summary>
        /// Gets the event delegate for the specified key and casts it to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected delegate type.</typeparam>
        /// <param name="key">The event key.</param>
        /// <returns>The delegate cast to <typeparamref name="T"/>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the key does not exist.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetEvent<T>(int key) where T : Delegate
        {
            if (this.FindIndex(key, out int index))
                return (T) _slots[index].value;

            throw EventNotFoundException(key);
        }

        /// <summary>
        /// Tries to get the event delegate for the specified key and cast it to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected delegate type.</typeparam>
        /// <param name="key">The event key.</param>
        /// <param name="value">The delegate if found.</param>
        /// <returns>True if the event was found; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetEvent<T>(int key, out T value) where T : Delegate
        {
            if (this.FindIndex(key, out int index))
            {
                value = (T) _slots[index].value;
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Gets the event delegate for the specified key using an unsafe cast.
        /// </summary>
        /// <typeparam name="T">The expected delegate type.</typeparam>
        /// <param name="key">The event key.</param>
        /// <returns>The delegate cast to <typeparamref name="T"/>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the key does not exist.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetEventUnsafe<T>(int key) where T : Delegate
        {
            if (this.FindIndex(key, out int index))
                return Unsafe.As<Delegate, T>(ref _slots[index].value);

            throw EventNotFoundException(key);
        }

        /// <summary>
        /// Tries to get the event delegate for the specified key using an unsafe cast.
        /// </summary>
        /// <typeparam name="T">The expected delegate type.</typeparam>
        /// <param name="key">The event key.</param>
        /// <param name="value">The delegate if found.</param>
        /// <returns>True if the event was found; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetEventUnsafe<T>(int key, out T value) where T : Delegate
        {
            if (this.FindIndex(key, out int index))
            {
                value = Unsafe.As<Delegate, T>(ref _slots[index].value);
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// Returns an array containing all stored key-value pairs.
        /// </summary>
        /// <returns>An array of key-value pairs.</returns>
        public KeyValuePair<int, Delegate>[] GetEvents()
        {
            var results = new KeyValuePair<int, Delegate>[_count];
            this.CopyEvents(results);
            return results;
        }

        /// <summary>
        /// Copies all stored key-value pairs into the provided array.
        /// </summary>
        /// <param name="results">The array to copy into.</param>
        /// <returns>The number of copied items.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="results"/> is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CopyEvents(KeyValuePair<int, Delegate>[] results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            int count = 0;
            for (int i = 0; i < _lastIndex; i++)
            {
                ref readonly EventSlot slot = ref _slots[i];
                if (slot.exists)
                    results[count++] = new KeyValuePair<int, Delegate>(slot.key, slot.value);
            }

            return count;
        }

        /// <summary>
        /// Returns an enumerator over all stored key-value pairs.
        /// </summary>
        /// <returns>An enumerator over key-value pairs.</returns>
        public Enumerator GetEnumerator() => new Enumerator(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int AllocateSlot()
        {
            int index;
            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = _slots[index].next;
            }
            else
            {
                if (_lastIndex == _capacity)
                    this.IncreaseCapacity();

                index = _lastIndex;
                _lastIndex++;
            }

            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool FindIndex(int key, out int index)
        {
            if (_count == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly EventSlot slot = ref _slots[index];
                if (slot.exists && slot.key == key)
                    return true;

                index = slot.next;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseCapacity()
        {
            _capacity = PrimeTable[++_primeIndex];

            Array.Resize(ref _slots, _capacity);
            Array.Resize(ref _buckets, _capacity);
            Array.Fill(_buckets, UNDEFINED_INDEX);

            for (int i = 0; i < _lastIndex; i++)
            {
                ref EventSlot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                int hash = slot.key & 0x7FFFFFFF;
                int bucket = hash % _capacity;
                ref int next = ref _buckets[bucket];

                slot.next = next;
                next = i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static KeyNotFoundException EventNotFoundException(int key) =>
            new KeyNotFoundException($"The given event {EventKeyStore.IdToName(key)} was not present in the event bus.");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Exception EventAlreadyAddedException(int key) =>
            new ArgumentException(
                $"An event with the same key {EventKeyStore.IdToName(key)} already has been added in the event bus!");

        public struct Enumerator : IEnumerator<KeyValuePair<int, Delegate>>
        {
            private readonly EventTable _table;
            private int _index;
            private KeyValuePair<int, Delegate> _current;

            public KeyValuePair<int, Delegate> Current => _current;
            object IEnumerator.Current => _current;

            public Enumerator(EventTable table)
            {
                _table = table;
                _index = 0;
                _current = default;
            }

            public bool MoveNext()
            {
                while (_index < _table._lastIndex)
                {
                    ref readonly EventSlot slot = ref _table._slots[_index++];
                    if (!slot.exists)
                        continue;

                    _current = new KeyValuePair<int, Delegate>(slot.key, slot.value);
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
                // Do nothing...
            }
        }
    }
}
