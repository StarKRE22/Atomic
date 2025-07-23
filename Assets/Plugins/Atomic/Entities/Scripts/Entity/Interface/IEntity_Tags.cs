using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    ///Represents tag identifiers for categorization
    public partial interface IEntity
    {
        /// <summary>
        /// Event triggered when a tag is added.
        /// </summary>
        event Action<IEntity, int> OnTagAdded;

        /// <summary>
        /// Event triggered when a tag is deleted.
        /// </summary>
        event Action<IEntity, int> OnTagDeleted;

        /// <summary>
        /// Number of tags associated with this entity.
        /// </summary>
        int TagCount { get; }

        /// <summary>
        /// Checks whether the entity has the given tag.
        /// </summary>
        bool HasTag(int key);

        /// <summary>
        /// Adds a tag to the entity.
        /// </summary>
        bool AddTag(int key);

        /// <summary>
        /// Removes a tag from the entity.
        /// </summary>
        bool DelTag(int key);

        /// <summary>
        /// Removes all tags.
        /// </summary>
        void ClearTags();

        /// <summary>
        /// Returns all tag keys.
        /// </summary>
        int[] GetTags();

        /// <summary>
        /// Copies tag keys into the provided array.
        /// </summary>
        int CopyTags(int[] results);

        /// <summary>
        /// Enumerates all tag keys.
        /// </summary>
        IEnumerator<int> GetTagEnumerator();
    }
}