using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Events.InternalUtils;

#if UNITY_5_3_OR_NEWER
using Unsafe = Unity.Collections.LowLevel.Unsafe.UnsafeUtility;
#else
using Unsafe = System.Runtime.CompilerServices.Unsafe;
#endif

namespace Atomic.Events
{
    public class EventBus : IEventBus
    {
        private struct EventSlot
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

        public EventBus(int capacity = 0)
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

        #region Subscribe

        public Subscription Subscribe(int key, Action action)
        {
            if (TryGetEvent<Delegate>(key, out Delegate del))
                action = (Action) del + action;

            SetEvent(key, action);
            return new Subscription(this, key, action);
        }

        public Subscription<T> Subscribe<T>(int key, Action<T> action)
        {
            if (TryGetEvent<Delegate>(key, out Delegate del))
                action = (Action<T>) del + action;

            SetEvent(key, action);
            return new Subscription<T>(this, key, action);
        }

        public Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action)
        {
            if (TryGetEvent<Delegate>(key, out Delegate del))
                action = (Action<T1, T2>) del + action;

            SetEvent(key, action);
            return new Subscription<T1, T2>(this, key, action);
        }

        public Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            if (TryGetEvent<Delegate>(key, out Delegate del))
                action = (Action<T1, T2, T3>) del + action;

            SetEvent(key, action);
            return new Subscription<T1, T2, T3>(this, key, action);
        }

        #endregion

        #region SubscribeUnsafe

        public Subscription SubscribeUnsafe(int key, Action action)
        {
            AddEvent(key, action);
            return new Subscription(this, key, action);
        }

        public Subscription<T> SubscribeUnsafe<T>(int key, Action<T> action)
        {
            AddEvent(key, action);
            return new Subscription<T>(this, key, action);
        }

        public Subscription<T1, T2> SubscribeUnsafe<T1, T2>(int key, Action<T1, T2> action)
        {
            AddEvent(key, action);
            return new Subscription<T1, T2>(this, key, action);
        }

        public Subscription<T1, T2, T3> SubscribeUnsafe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            AddEvent(key, action);
            return new Subscription<T1, T2, T3>(this, key, action);
        }

        public bool IsSubscribed(int key)
        {
            return ContainsEvent(key);
        }

        #endregion

        #region Unsubscribe

        public void Unsubscribe(int key, Action action)
        {
            if (!TryGetEvent<Delegate>(key, out Delegate del))
                return;

            del = (Action) del - action;
            if (del == null)
                RemoveEvent(key);
            else
                SetEvent(key, del);
        }

        public void Unsubscribe<T>(int key, Action<T> action)
        {
            if (!TryGetEvent<Delegate>(key, out Delegate del))
                return;

            del = (Action<T>) del - action;
            if (del == null)
                RemoveEvent(key);
            else
                SetEvent(key, del);
        }

        public void Unsubscribe<T1, T2>(int key, Action<T1, T2> action)
        {
            if (!TryGetEvent<Delegate>(key, out Delegate del))
                return;

            del = (Action<T1, T2>) del - action;
            if (del == null)
                RemoveEvent(key);
            else
                SetEvent(key, del);
        }

        public void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            if (!TryGetEvent<Delegate>(key, out Delegate del))
                return;

            del = (Action<T1, T2, T3>) del - action;
            if (del == null)
                RemoveEvent(key);
            else
                SetEvent(key, del);
        }

        #endregion

        #region UnsubscribeUnsafe

        public void UnsubscribeUnsafe(int key, Action action)
        {
            Delegate del = GetEventUnsafe<Delegate>(key);
            del = (Action) del - action;
            if (del == null)
                RemoveEvent(key);
            else
                SetEvent(key, del);
        }

        public void UnsubscribeUnsafe<T>(int key, Action<T> action)
        {
            Delegate del = GetEventUnsafe<Delegate>(key);
            del = (Action<T>) del - action;
            if (del == null)
                RemoveEvent(key);
            else
                SetEvent(key, del);
        }

        public void UnsubscribeUnsafe<T1, T2>(int key, Action<T1, T2> action)
        {
            Delegate del = GetEventUnsafe<Delegate>(key);
            del = (Action<T1, T2>) del - action;
            if (del == null)
                RemoveEvent(key);
            else
                SetEvent(key, del);
        }

        public void UnsubscribeUnsafe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            Delegate del = GetEventUnsafe<Delegate>(key);
            del = (Action<T1, T2, T3>) del - action;
            if (del == null)
                RemoveEvent(key);
            else
                SetEvent(key, del);
        }

        #endregion

        #region Invoke

        public void Invoke(int key)
        {
            if (TryGetEvent<Action>(key, out Action del))
                del.Invoke();
        }

        public void Invoke<T>(int key, T arg)
        {
            if (TryGetEvent<Action<T>>(key, out Action<T> del))
                del.Invoke(arg);
        }

        public void Invoke<T1, T2>(int key, T1 arg1, T2 arg2)
        {
            if (TryGetEvent<Action<T1, T2>>(key, out Action<T1, T2> del))
                del.Invoke(arg1, arg2);
        }

        public void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3)
        {
            if (TryGetEvent<Action<T1, T2, T3>>(key, out Action<T1, T2, T3> del))
                del.Invoke(arg1, arg2, arg3);
        }

        #endregion

        #region InvokeUnsafe

        public void InvokeUnsafe(int key) =>
            GetEventUnsafe<Action>(key).Invoke();

        public void InvokeUnsafe<T>(int key, T arg) =>
            GetEventUnsafe<Action<T>>(key).Invoke(arg);

        public void InvokeUnsafe<T1, T2>(int key, T1 arg1, T2 arg2) =>
            GetEventUnsafe<Action<T1, T2>>(key).Invoke(arg1, arg2);

        public void InvokeUnsafe<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3) =>
            GetEventUnsafe<Action<T1, T2, T3>>(key).Invoke(arg1, arg2, arg3);

        #endregion

        public void Dispose() => ClearEvents();

        public bool Dispose(int key) => RemoveEvent(key);

        #region Storage

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddEvent(int key, Delegate value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (_count > 0 && FindIndex(key, out _))
                throw EventAlreadyAddedException(key);

            int index = AllocateSlot();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetEvent(int key, Delegate value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (FindIndex(key, out int index))
            {
                _slots[index].value = value;
                return;
            }

            index = AllocateSlot();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool RemoveEvent(int key)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool ContainsEvent(int key)
        {
            return FindIndex(key, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ClearEvents()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T GetEvent<T>(int key) where T : Delegate
        {
            if (FindIndex(key, out int index))
                return (T) _slots[index].value;

            throw EventNotFoundException(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryGetEvent<T>(int key, out T value) where T : Delegate
        {
            if (FindIndex(key, out int index))
            {
                value = (T) _slots[index].value;
                return true;
            }

            value = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T GetEventUnsafe<T>(int key) where T : Delegate
        {
            if (FindIndex(key, out int index))
                return Unsafe.As<Delegate, T>(ref _slots[index].value);

            throw EventNotFoundException(key);
        }

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
                    IncreaseCapacity();

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

        #endregion

        #region Debug

#if UNITY_EDITOR
        internal IEnumerable<KeyValuePair<int, Delegate>> GetEvents()
        {
            var results = new KeyValuePair<int, Delegate>[_count];
            int index = 0;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref readonly EventSlot slot = ref _slots[i];
                if (slot.exists)
                    results[index++] = new KeyValuePair<int, Delegate>(slot.key, slot.value);
            }

            return results;
        }
#endif

        #endregion
    }
}
