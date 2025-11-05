#if UNITY_5_3_OR_NEWER
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides tag management functionality for the <see cref="SceneEntity"/>, allowing to add, remove,
    /// check, and enumerate tags associated with the entity.
    /// </summary>
    public partial class SceneEntity
    {
        /// <summary>
        /// Invoked when a new tag is added to the entity.
        /// </summary>
        public event Action<IEntity, int> OnTagAdded;

        /// <summary>
        /// Invoked when a tag is deleted from the entity.
        /// </summary>
        public event Action<IEntity, int> OnTagDeleted;

        /// <summary>
        /// Gets the total number of tags associated with the entity.
        /// </summary>
        public int TagCount => _tagCount;

        internal struct TagSlot
        {
            public int key;
            public int next;
            public bool exists;
        }
        
        private TagSlot[] _tagSlots;
        private int _tagCapacity;
        private int _tagCount;
        private int _tagPrimeIndex;

        private int[] _tagBuckets;
        private int _tagFreeList;
        private int _tagLastIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConstructTags()
        {
            _tagCapacity = CeilToPrime(initialTagCapacity, out _tagPrimeIndex);
            _tagSlots = new TagSlot[_tagCapacity];
            _tagBuckets = new int[_tagCapacity];
            Array.Fill(_tagBuckets, UNDEFINED_INDEX);

            _tagCount = 0;
            _tagLastIndex = 0;
            _tagFreeList = UNDEFINED_INDEX;
        }

       
         /// <summary>
        /// Checks if the entity has a tag with the specified key.
        /// </summary>
        public bool HasTag(int key)
        {
            if (_tagCount > 0)
            {
                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _tagCapacity;
                int index = _tagBuckets[bucket];

                while (index >= 0)
                {
                    ref readonly TagSlot slot = ref _tagSlots[index];
                    if (slot.exists && slot.key == key)
                        return true;

                    index = slot.next;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds a tag to the entity.
        /// </summary>
        public bool AddTag(int key)
        {
            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _tagCapacity;
            int index;

            if (_tagCount > 0)
            {
                bucket = hash % _tagCapacity;
                index = _tagBuckets[bucket];

                while (index >= 0)
                {
                    ref readonly TagSlot slot = ref _tagSlots[index];
                    if (slot.exists && slot.key == key)
                        return false;

                    index = slot.next;
                }
            }

            if (_tagFreeList >= 0)
            {
                index = _tagFreeList;
                _tagFreeList = _tagSlots[index].next;
            }
            else
            {
                if (_tagLastIndex == _tagCapacity)
                {
                    this.IncreaseTagCapacity();
                    bucket = hash % _tagCapacity;
                }

                index = _tagLastIndex;
                _tagLastIndex++;
            }

            ref int next = ref _tagBuckets[bucket];

            _tagSlots[index] = new TagSlot
            {
                key = key,
                next = next,
                exists = true
            };

            next = index;

            _tagCount++;

            this.OnTagAdded?.Invoke(this, key);
            this.OnStateChanged?.Invoke(this);
            return true;
        }

        /// <summary>
        /// Deletes a tag from the entity.
        /// </summary>
        public bool DelTag(int key)
        {
            if (_tagCount > 0)
            {
                int hash = key & 0x7FFFFFFF;
                int bucket = hash % _tagCapacity;
                ref int next = ref _tagBuckets[bucket];

                int index = next;
                int last = UNDEFINED_INDEX;

                while (index >= 0)
                {
                    ref TagSlot slot = ref _tagSlots[index];
                    if (slot.key == key)
                    {
                        if (last == UNDEFINED_INDEX)
                            next = slot.next;
                        else
                            _tagSlots[last].next = slot.next;

                        slot.next = _tagFreeList;
                        slot.exists = false;

                        _tagCount--;

                        if (_tagCount == 0)
                        {
                            _tagLastIndex = 0;
                            _tagFreeList = UNDEFINED_INDEX;
                        }
                        else
                        {
                            _tagFreeList = index;
                        }

                        this.OnTagDeleted?.Invoke(this, key);
                        this.OnStateChanged?.Invoke(this);
                        return true;
                    }

                    last = index;
                    index = slot.next;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns an array containing all tag keys associated with the entity.
        /// </summary>
        public int[] GetTags()
        {
            int[] results = new int[_tagCount];
            int index = 0;

            for (int i = 0; i < _tagLastIndex; i++)
            {
                ref readonly TagSlot slot = ref _tagSlots[i];
                if (slot.exists)
                    results[index++] = slot.key;
            }

            return results;
        }

        /// <summary>
        /// Fills the provided array with all tag keys.
        /// </summary>
        public int CopyTags(int[] results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            int count = 0;

            for (int i = 0; i < _tagLastIndex; i++)
            {
                ref readonly TagSlot slot = ref _tagSlots[i];
                if (slot.exists)
                    results[count++] = slot.key;
            }

            return count;
        }

        /// <summary>
        /// Clears all tags from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ClearTags()
        {
            if (_tagCount == 0)
                return;

            int removeCount = 0;
            Span<int> removedTags = stackalloc int[_tagCount];

            for (int i = 0; i < _tagLastIndex; i++)
            {
                _tagBuckets[i] = UNDEFINED_INDEX;

                ref TagSlot slot = ref _tagSlots[i];
                if (!slot.exists)
                    continue;

                slot.exists = false;
                slot.next = UNDEFINED_INDEX;
                removedTags[removeCount++] = slot.key;
            }

            _tagCount = 0;
            _tagFreeList = UNDEFINED_INDEX;
            _tagLastIndex = 0;

            for (int i = 0; i < removeCount; i++)
                this.OnTagDeleted?.Invoke(this, removedTags[i]);

            this.OnStateChanged?.Invoke(this);
        }

        /// <summary>
        /// Returns an enumerator over the tag keys of the entity.
        /// </summary>
        IEnumerator<int> IEntity.GetTagEnumerator()
        {
            return new TagEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator over the tag keys of the entity.
        /// </summary>
        public TagEnumerator GetTagEnumerator()
        {
            return new TagEnumerator(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseTagCapacity()
        {
            _tagCapacity = PrimeTable[++_tagPrimeIndex];
            Array.Resize(ref _tagSlots, _tagCapacity);
            Array.Resize(ref _tagBuckets, _tagCapacity);
            Array.Fill(_tagBuckets, UNDEFINED_INDEX);

            for (int i = 0; i < _tagLastIndex; i++)
            {
                ref TagSlot slot = ref _tagSlots[i];
                if (!slot.exists)
                    continue;

                int hash = slot.key & 0x7FFFFFFF;
                int bucket = hash % _tagCapacity;
                ref int next = ref _tagBuckets[bucket];
                slot.next = next;
                next = i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool DelTagInternal(int key)
        {
            if (_tagCount == 0)
                return false;

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _tagCapacity;
            ref int next = ref _tagBuckets[bucket];

            int index = next;
            int last = UNDEFINED_INDEX;

            while (index >= 0)
            {
                ref TagSlot slot = ref _tagSlots[index];
                if (slot.key == key)
                {
                    if (last == UNDEFINED_INDEX)
                        next = slot.next;
                    else
                        _tagSlots[last].next = slot.next;

                    slot.next = _tagFreeList;
                    slot.exists = false;

                    _tagCount--;

                    if (_tagCount == 0)
                    {
                        _tagLastIndex = 0;
                        _tagFreeList = UNDEFINED_INDEX;
                    }
                    else
                    {
                        _tagFreeList = index;
                    }

                    return true;
                }

                last = index;
                index = slot.next;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool AddTagInternal(int key)
        {
            if (this.FindTagIndex(key, out int index))
                return false;

            if (_tagFreeList >= 0)
            {
                index = _tagFreeList;
                _tagFreeList = _tagSlots[index].next;
            }
            else
            {
                if (_tagLastIndex == _tagCapacity)
                    this.IncreaseTagCapacity();

                index = _tagLastIndex;
                _tagLastIndex++;
            }

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _tagCapacity;
            ref int next = ref _tagBuckets[bucket];

            _tagSlots[index] = new TagSlot
            {
                key = key,
                next = next,
                exists = true
            };

            next = index;

            _tagCount++;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool FindTagIndex(int key, out int index)
        {
            if (_tagCount == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            int hash = key & 0x7FFFFFFF;
            int bucket = hash % _tagCapacity;
            index = _tagBuckets[bucket];

            while (index >= 0)
            {
                ref readonly TagSlot slot = ref _tagSlots[index];
                if (slot.exists && slot.key == key)
                    return true;

                index = slot.next;
            }

            return false;
        }

        public struct TagEnumerator : IEnumerator<int>
        {
            private readonly SceneEntity _entity;
            private int _index;
            private int _current;

            public int Current => _current;
            object IEnumerator.Current => _current;

            public TagEnumerator(SceneEntity entity)
            {
                _entity = entity;
                _index = 0;
                _current = 0;
            }

            public bool MoveNext()
            {
                while (_index < _entity._tagLastIndex)
                {
                    ref readonly TagSlot slot = ref _entity._tagSlots[_index++];
                    if (slot.exists)
                    {
                        _current = slot.key;
                        return true;
                    }
                }

                _current = 0;
                return false;
            }

            public void Reset()
            {
                _index = 0;
                _current = 0;
            }

            public void Dispose()
            {
                //Do nothing...
            }
        }
    }
}
#endif