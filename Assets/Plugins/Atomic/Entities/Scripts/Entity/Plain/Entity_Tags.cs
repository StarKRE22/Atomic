using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Atomic.Entities.AtomicHelper;

namespace Atomic.Entities
{
    public partial class Entity
    {
        public event Action<IEntity, int> OnTagAdded;
        public event Action<IEntity, int> OnTagDeleted;

        public int TagCount => _tagCount;

        private TagSlot[] _tagSlots;
        private int _tagCount;

        private int[] _tagBuckets;
        private int _tagFreeList;
        private int _tagLastIndex;

        public bool HasTag(in int key)
        {
            return this.FindTagIndex(key, out _);
        }

        public bool AddTag(in int key)
        {
            if (!this.AddTagInternal(in key))
                return false;

            this.OnTagAdded?.Invoke(this, key);
            this.OnStateChanged?.Invoke();
            return true;
        }

        public bool DelTag(in int key)
        {
            if (!this.DelTagInternal(in key))
                return false;

            this.OnTagDeleted?.Invoke(this, key);
            this.OnStateChanged?.Invoke();
            return true;
        }

        public int[] GetTags()
        {
            var results = new int[_tagCount];
            this.GetTags(results);
            return results;
        }

        public int GetTags(int[] results)
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

        public unsafe void ClearTags()
        {
            if (_tagCount == 0)
                return;

            int removeCount = 0;
            int* removedTags = stackalloc int[_tagCount];

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

        public IEnumerator<int> TagEnumerator()
        {
            return new _TagEnumerator(this);
        }
        
        private struct TagSlot
        {
            public int key;
            public int next;
            public bool exists;
        }

        private void IncreaseTagCapacity()
        {
            int size = GetPrime(_tagSlots.Length + 1);
            Array.Resize(ref _tagSlots, size);
            Array.Resize(ref _tagBuckets, size);

            for (int i = 0; i < size; i++)
                _tagBuckets[i] = UNDEFINED_INDEX;

            for (int i = 0; i < _tagLastIndex; i++)
            {
                ref TagSlot slot = ref _tagSlots[i];
                if (!slot.exists)
                    continue;

                ref int bucket = ref this.TagBucket(slot.key);
                slot.next = bucket;
                bucket = i;
            }
        }

        private bool DelTagInternal(in int key)
        {
            if (_tagCount == 0)
                return false;

            ref int bucket = ref this.TagBucket(in key);

            int index = bucket;
            int last = UNDEFINED_INDEX;

            while (index >= 0)
            {
                ref TagSlot slot = ref _tagSlots[index];
                if (slot.key == key)
                {
                    if (last == UNDEFINED_INDEX)
                        bucket = slot.next;
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
            if (this.FindTagIndex(in key, out int index))
                return false;

            if (_tagFreeList >= 0)
            {
                index = _tagFreeList;
                _tagFreeList = _tagSlots[index].next;
            }
            else
            {
                if (_tagLastIndex == _tagSlots.Length)
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

        private bool FindTagIndex(in int key, out int index)
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
            int index = (int) ((uint) key % _tagSlots.Length);
            return ref _tagBuckets[index];
        }

        private void InitializeTags(in IEnumerable<int> tags)
        {
            if (tags == null)
            {
                this.InitializeTags(0);
                return;
            }

            this.InitializeTags(tags.Count());

            foreach (int key in tags)
                this.AddTag(in key);
        }

        private void InitializeTags(in int capacity = 0)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            int size = GetPrime(capacity);

            _tagSlots = new TagSlot[size];
            _tagBuckets = new int[size];

            for (int i = 0; i < size; i++)
                _tagBuckets[i] = UNDEFINED_INDEX;

            _tagCount = 0;
            _tagLastIndex = 0;
            _tagFreeList = UNDEFINED_INDEX;
        }

        private struct _TagEnumerator : IEnumerator<int>
        {
            private readonly Entity _entity;
            private int _index;
            private int _current;

            public int Current => _current;
            object IEnumerator.Current => _current;

            public _TagEnumerator(Entity entity)
            {
                _entity = entity;
                _index = 0;
                _current = default;
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
    }
}