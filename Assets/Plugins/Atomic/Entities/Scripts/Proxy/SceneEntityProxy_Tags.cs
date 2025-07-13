using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntityProxy<E>
    {
        public event Action<IEntity, int> OnTagAdded
        {
            add => _source.OnTagAdded += value;
            remove => _source.OnTagAdded -= value;
        }

        public event Action<IEntity, int> OnTagDeleted
        {
            add => _source.OnTagDeleted += value;
            remove => _source.OnTagDeleted -= value;
        }

        public int TagCount => _source.TagCount;

        public bool HasTag(in int key) => _source.HasTag(in key);
        public bool AddTag(in int key) => _source.AddTag(in key);
        public bool DelTag(in int key) => _source.DelTag(in key);
        public void ClearTags() => _source.ClearTags();
        
        public int[] GetTags() => _source.GetTags();
        public int GetTags(int[] results) => _source.GetTags(results);
        public IEnumerator<int> TagEnumerator() => _source.TagEnumerator();
    }
}