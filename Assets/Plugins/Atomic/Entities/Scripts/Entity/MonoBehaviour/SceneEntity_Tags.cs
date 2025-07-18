using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        public event Action<IEntity, int> OnTagAdded
        {
            add => this.Entity.OnTagAdded += value;
            remove => this.Entity.OnTagAdded -= value;
        }

        public event Action<IEntity, int> OnTagDeleted
        {
            add => this.Entity.OnTagDeleted += value;
            remove => this.Entity.OnTagDeleted -= value;
        }
        
        public bool DelTag(int key) => this.Entity.DelTag(key);
        public int TagCount => Entity.TagCount;

        public bool HasTag(int key) => this.Entity.HasTag(key);
        public bool AddTag(int key) => this.Entity.AddTag(key);
        public void ClearTags() => this.Entity.ClearTags();
        
        public int[] GetTags() => Entity.GetTags();
        public int GetTags(int[] results) => Entity.GetTags(results);
        public IEnumerator<int> TagEnumerator() => Entity.TagEnumerator();
    }
}