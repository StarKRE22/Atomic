#if UNITY_5_3_OR_NEWER
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

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
        private struct TagSlot
        {
            public int key;
            public int next;
            public bool exists;
        }
        
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

        private TagSlot[] _tagSlots;
        private int[] _tagBuckets;
        private int _tagCapacity;
        private int _tagCount;
        private int _tagLastIndex;
        private int _tagFreeList = UNDEFINED_INDEX;

        /// <summary>
        /// Initial tag capacity used to optimize tag allocation.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [Min(1)]
        [SerializeField]
        private int _initialTagCapacity = 1;

        /// <summary>
        /// Checks if the entity has a tag with the specified key.
        /// </summary>
        public bool HasTag(int key) => this.FindTagIndex(key, out _);

        /// <summary>
        /// Adds a tag to the entity.
        /// </summary>
        public bool AddTag(int key)
        {
            if (!this.AddTagInternal(in key))
                return false;

            this.OnTagAdded?.Invoke(this, key);
            this.OnStateChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// Deletes a tag from the entity.
        /// </summary>
        public bool DelTag(int key)
        {
            if (!this.DelTagInternal(in key))
                return false;

            this.OnTagDeleted?.Invoke(this, key);
            this.OnStateChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// Returns an array containing all tag keys associated with the entity.
        /// </summary>
        public int[] GetTags()
        {
            var results = new int[_tagCount];
            this.CopyTags(results);
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
                TagSlot slot = _tagSlots[i];
                if (slot.exists)
                    results[count++] = slot.key;
            }

            return count;
        }

        /// <summary>
        /// Clears all tags from the entity.
        /// </summary>
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

            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Returns an enumerator over the tag keys of the entity.
        /// </summary>
        IEnumerator<int> IEntity.GetTagEnumerator() => new TagEnumerator(this);

        public TagEnumerator GetTagEnumerator() => new(this);

        private void IncreaseTagCapacity()
        {
            _tagCapacity = _tagSlots == null 
                ? GetPrime(_initialTagCapacity) 
                : GetPrime(_tagCapacity + 1);
            
            Array.Resize(ref _tagSlots, _tagCapacity);
            Array.Resize(ref _tagBuckets, _tagCapacity);

            for (int i = 0; i < _tagCapacity; i++)
                _tagBuckets[i] = UNDEFINED_INDEX;

            for (int i = 0; i < _tagLastIndex; i++)
            {
                ref TagSlot slot = ref _tagSlots[i];
                if (!slot.exists)
                    continue;

                ref int next = ref this.TagBucket(slot.key);
                slot.next = next;
                next = i;
            }
        }

        private bool DelTagInternal(in int key)
        {
            if (_tagCount == 0)
                return false;

            ref int next = ref this.TagBucket(in key);

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

        private bool AddTagInternal(in int key)
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

            ref int bucket = ref this.TagBucket(in key);

            _tagSlots[index] = new TagSlot
            {
                key = key,
                next = bucket,
                exists = true
            };

            bucket = index;

            _tagCount++;
            return true;
        }

        private bool FindTagIndex(int key, out int index)
        {
            if (_tagCount == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            index = this.TagBucket(in key);
            while (index >= 0)
            {
                TagSlot slot = _tagSlots[index];
                if (slot.exists && slot.key == key)
                    return true;

                index = slot.next;
            }

            return false;
        }

        private ref int TagBucket(in int key)
        {
            int index = (int) ((uint) key % _tagCapacity);
            return ref _tagBuckets[index];
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
                    ref TagSlot slot = ref _entity._tagSlots[_index++];
                    if (slot.exists)
                    {
                        _current = slot.key;
                        return true;
                    }
                }

                _current = 0;
                return false;
            }

            void IEnumerator.Reset()
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