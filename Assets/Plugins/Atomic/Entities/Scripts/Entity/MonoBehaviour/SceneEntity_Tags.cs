using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides tag management functionality for the <see cref="SceneEntity"/>, allowing to add, remove,
    /// check, and enumerate tags associated with the entity.
    /// </summary>
    public partial class SceneEntity<E> where E : class

    {
        /// <summary>
        /// Invoked when a tag is added to the entity.
        /// </summary>
        public event Action<IEntity, int> OnTagAdded
        {
            add => this.Entity.OnTagAdded += value;
            remove => this.Entity.OnTagAdded -= value;
        }

        /// <summary>
        /// Invoked when a tag is deleted from the entity.
        /// </summary>
        public event Action<IEntity, int> OnTagDeleted
        {
            add => this.Entity.OnTagDeleted += value;
            remove => this.Entity.OnTagDeleted -= value;
        }
        
        /// <summary>
        /// Gets the total number of tags currently associated with the entity.
        /// </summary>
        public int TagCount => this.Entity.TagCount;
        
        /// <summary>
        /// Adds a tag to the entity.
        /// </summary>
        public bool AddTag(int key) => this.Entity.AddTag(key);

        /// <summary>
        /// Removes a tag by its key.
        /// </summary>
        public bool DelTag(int key) => this.Entity.DelTag(key);
        
        /// <summary>
        /// Checks if the entity has the specified tag.
        /// </summary>
        public bool HasTag(int key) => this.Entity.HasTag(key);
        
        /// <summary>
        /// Clears all tags from the entity.
        /// </summary>
        public void ClearTags() => this.Entity.ClearTags();
        
        /// <summary>
        /// Returns an array of tag keys currently assigned to the entity.
        /// </summary>
        public int[] GetTags() => this.Entity.GetTags();
        
        /// <summary>
        /// Fills the provided array with tag keys assigned to the entity.
        /// </summary>
        public int GetTags(int[] results) => this.Entity.GetTags(results);
        
        /// <summary>
        /// Returns an enumerator over the entity's tag keys.
        /// </summary>
        IEnumerator<int> IEntity.GetTagEnumerator() => this.Entity.GetTagEnumerator();

        public Entity.TagEnumerator GetTagEnumerator() => this.Entity.GetTagEnumerator();
    }
}