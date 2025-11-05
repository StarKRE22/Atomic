using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    ///Represents entity state as key-value storage for data
    public partial interface IEntity
    {
        /// <summary>
        /// Event triggered when a value is added.
        /// </summary>
        event Action<IEntity, int> OnValueAdded;

        /// <summary>
        /// Event triggered when a value is deleted.
        /// </summary>
        event Action<IEntity, int> OnValueDeleted;

        /// <summary>
        /// Event triggered when a value is changed.
        /// </summary>
        event Action<IEntity, int> OnValueChanged;

        /// <summary>
        /// Number of values stored in this entity.
        /// </summary>
        int ValueCount { get; }

        /// <summary>
        /// Gets a value by key and casts it to the specified type.
        /// </summary>
        T GetValue<T>(int key);

        /// <summary>
        /// Gets a value by key with a reference (unsafe, no boxing).
        /// </summary>
        ref T GetValueUnsafe<T>(int key);

        /// <summary>
        /// Gets a value by key as an object.
        /// </summary>
        object GetValue(int key);

        /// <summary>
        /// Tries to get a value by key and cast it.
        /// </summary>
        bool TryGetValue<T>(int key, out T value);

        /// <summary>
        /// Tries to get a reference to a value (unsafe).
        /// </summary>
        bool TryGetValueUnsafe<T>(int key, out T value);

        /// <summary>
        /// Tries to get a value as an object.
        /// </summary>
        bool TryGetValue(int key, out object value);

        /// <summary>
        /// Sets or updates a value.
        /// </summary>
        void SetValue(int key, object value);

        /// <summary>
        /// Sets or updates a value of specified struct type.
        /// </summary>
        void SetValue<T>(int key, T value) where T : struct;

        /// <summary>
        /// Checks whether a value with the given key exists.
        /// </summary>
        bool HasValue(int key);

        /// <summary>
        /// Adds a value with the given key.
        /// </summary>
        void AddValue(int key, object value);

        /// <summary>
        /// Adds a struct value with the given key.
        /// </summary>
        void AddValue<T>(int key, T value) where T : struct;

        /// <summary>
        /// Deletes a value by key.
        /// </summary>
        bool DelValue(int key);

        /// <summary>
        /// Clears all values.
        /// </summary>
        void ClearValues();

        /// <summary>
        /// Gets all key-value pairs.
        /// </summary>
        KeyValuePair<int, object>[] GetValues();

        /// <summary>
        /// Copies all key-value pairs into the provided array.
        /// </summary>
        int CopyValues(KeyValuePair<int, object>[] results);

        /// <summary>
        /// Enumerates all key-value pairs.
        /// </summary>
        IEnumerator<KeyValuePair<int, object>> GetValueEnumerator();
    }
}