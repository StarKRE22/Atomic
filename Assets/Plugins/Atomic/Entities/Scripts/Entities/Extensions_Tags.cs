using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Entities.EntityKeyStore;

namespace Atomic.Entities
{
    public partial class Extensions
    {
        #region AddTag

        /// <summary>
        /// Adds a tag to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag(this IEntity entity, string tag) => entity.AddTag(NameToId(tag));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag(this IEntity entity, TagKey tag) => entity.AddTag(tag.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag<E>(this E entity, TagKey<E> tag) where E : IEntity => entity.AddTag(tag.Id);

        /// <summary>
        /// Adds a tag to the entity and returns its numeric ID.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag(this IEntity entity, string tag, out int id)
        {
            id = NameToId(tag);
            return entity.AddTag(id);
        }

        #endregion

        #region AddTags

        /// <summary>
        /// Adds multiple tags to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags(this IEntity entity, IEnumerable<int> tags)
        {
            if (tags == null)
                return;

            foreach (int tag in tags)
                entity.AddTag(tag);
        }

        /// <summary>
        /// Adds multiple tags by string identifiers.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags(this IEntity entity, IEnumerable<string> tags)
        {
            if (tags == null)
                return;

            foreach (string tag in tags)
                entity.AddTag(tag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags(this IEntity entity, IEnumerable<TagKey> tags)
        {
            if (tags == null)
                return;

            foreach (TagKey tag in tags)
                entity.AddTag(tag.Id);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags<E>(this E entity, IEnumerable<TagKey<E>> tags) where E : IEntity
        {
            if (tags == null)
                return;

            foreach (TagKey<E> tag in tags)
                entity.AddTag(tag.Id);
        }

        #endregion

        #region DelTag

        /// <summary>
        /// Removes a tag from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTag(this IEntity entity, string tag) => entity.DelTag(NameToId(tag));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTag(this IEntity entity, TagKey tag) => entity.DelTag(tag.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTag<E>(this E entity, TagKey<E> tag) where E : IEntity => entity.DelTag(tag.Id);

        #endregion

        #region HasTag

        /// <summary>
        /// Checks if the entity has the specified tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTag(this IEntity entity, string tag) => entity.HasTag(NameToId(tag));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTag(this IEntity entity, TagKey tag) => entity.HasTag(tag.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTag<E>(this E entity, TagKey<E> tag) where E : IEntity => entity.HasTag(tag.Id);

        #endregion

        #region HasAllTags

        /// <summary>
        /// Checks if the entity contains all of the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags(this IEntity entity, params int[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (!entity.HasTag(tags[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Checks if the entity has all the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags(this IEntity entity, params string[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (!entity.HasTag(tags[i]))
                    return false;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags(this IEntity entity, params TagKey[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (!entity.HasTag(tags[i].Id))
                    return false;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags<E>(this E entity, params TagKey<E>[] tags) where E : IEntity
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (!entity.HasTag(tags[i].Id))
                    return false;

            return true;
        }

        #endregion

        #region HasAnyTag

        /// <summary>
        /// Checks if the entity has any of the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag(this IEntity entity, params string[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (entity.HasTag(tags[i]))
                    return true;

            return false;
        }

        /// <summary>
        /// Checks if the entity contains any of the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag(this IEntity entity, params int[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (entity.HasTag(tags[i]))
                    return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag(this IEntity entity, params TagKey[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (entity.HasTag(tags[i].Id))
                    return true;

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag<E>(this E entity, params TagKey<E>[] tags) where E : IEntity
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (entity.HasTag(tags[i].Id))
                    return true;

            return false;
        }

        #endregion
    }
}