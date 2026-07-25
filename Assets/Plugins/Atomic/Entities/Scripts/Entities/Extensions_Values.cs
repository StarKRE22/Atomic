using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Entities.EntityKeyStore;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
        #region AddValue

        /// <summary>
        /// Adds a value to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue(this IEntity entity, string key, object value) =>
            entity.AddValue(NameToId(key), value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<T>(this IEntity entity, ValueKey<T> key, T value) =>
            entity.AddValue(key.Id, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<E, T>(this E entity, ValueKey<E, T> key, T value)
            where E : IEntity =>
            entity.AddValue(key.Id, value);


        /// <summary>
        /// Adds a value to the entity and returns the corresponding ID.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue(this IEntity entity, string key, object value, out int id)
        {
            id = NameToId(key);
            entity.AddValue(id, value);
        }

        #endregion

        #region AddValues

        /// <summary>
        /// Adds multiple values to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValues(this IEntity entity, IEnumerable<KeyValuePair<int, object>> values)
        {
            if (values != null)
                foreach ((int key, object value) in values)
                    entity.AddValue(key, value);
        }

        /// <summary>
        /// Adds multiple values by string keys.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValues(this IEntity entity, IEnumerable<KeyValuePair<string, object>> values)
        {
            if (values != null)
                foreach ((string key, object value) in values)
                    entity.AddValue(key, value);
        }

        #endregion

        #region DelValue

        /// <summary>
        /// Removes a value from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue(this IEntity entity, string key) =>
            entity.DelValue(NameToId(key));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue<T>(this IEntity entity, ValueKey<T> key) =>
            entity.DelValue(key.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue<E, T>(this E entity, ValueKey<E, T> key) where E : IEntity =>
            entity.DelValue(key.Id);

        #endregion

        #region GetValue

        /// <summary>
        /// Retrieves a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IEntity entity, string key) =>
            entity.GetValue<T>(NameToId(key));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetValue(this IEntity entity, string key) =>
            entity.GetValue(NameToId(key));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IEntity entity, ValueKey<T> key) =>
            entity.GetValue<T>(key.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<E, T>(this E entity, ValueKey<E, T> key) where E : IEntity =>
            entity.GetValue<T>(key.Id);

        #endregion

        #region TryGetValue

        /// <summary>
        /// Tries to retrieve a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IEntity entity, string key, out T value) =>
            entity.TryGetValue(NameToId(key), out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue(this IEntity entity, string key, out object value) =>
            entity.TryGetValue(NameToId(key), out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IEntity entity, ValueKey<T> key, out T value) =>
            entity.TryGetValue(key.Id, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<E, T>(this E entity, ValueKey<E, T> key, out T value) where E : IEntity =>
            entity.TryGetValue(key.Id, out value);

        #endregion

        #region GetValueUnsafe

        /// <summary>
        /// Retrieves a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetValueUnsafe<T>(this IEntity entity, string key) where T : class =>
            ref entity.GetValueUnsafe<T>(NameToId(key));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetValueUnsafe<T>(this IEntity entity, ValueKey<T> key) where T : class =>
            ref entity.GetValueUnsafe<T>(key.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValueUnsafe<E, T>(this E entity, ValueKey<E, T> key) 
            where E : IEntity 
            where T : class =>
            entity.GetValueUnsafe<T>(key.Id);

        #endregion


        #region SetValue

        /// <summary>
        /// Sets a value in the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue(this IEntity entity, string key, object value) =>
            entity.SetValue(NameToId(key), value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<T>(this IEntity entity, ValueKey<T> key, T value) =>
            entity.SetValue(key.Id, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<E, T>(this E entity, ValueKey<E, T> key, T value) where E : IEntity =>
            entity.SetValue(key.Id, value);

        #endregion

        #region HasValue

        /// <summary>
        /// Checks if the entity has a value with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue(this IEntity entity, string key) => entity.HasValue(NameToId(key));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue<T>(this IEntity entity, ValueKey<T> key) =>
            entity.HasValue(key.Id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue<E, T>(this E entity, ValueKey<E, T> key) where E : IEntity =>
            entity.HasValue(key.Id);

        #endregion

        /// <summary>
        /// Disposes all disposable values stored in the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeValues(this IEntity entity)
        {
            KeyValuePair<int, object>[] pairs = entity.GetValues();
            for (int i = 0, count = pairs.Length; i < count; i++)
                if (pairs[i].Value is IDisposable disposable)
                    disposable.Dispose();
        }
    }
}