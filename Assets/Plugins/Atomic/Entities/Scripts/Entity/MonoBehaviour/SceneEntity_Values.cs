using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides value management functionality for the <see cref="SceneEntity"/>, allowing to set, get, check,
    /// and remove values associated with an entity. 
    /// </summary>
    public partial class SceneEntity<E>
    {
        /// <summary>
        /// Invoked when a value is added to the entity.
        /// </summary>
        public event Action<IEntity<E>, int> OnValueAdded
        {
            add => this.Entity.OnValueAdded += value;
            remove => this.Entity.OnValueAdded -= value;
        }
        
        /// <summary>
        /// Invoked when a value is deleted from the entity.
        /// </summary>
        public event Action<IEntity<E>, int> OnValueDeleted
        {
            add => this.Entity.OnValueDeleted += value;
            remove => this.Entity.OnValueDeleted -= value;
        }
        
        /// <summary>
        /// Invoked when a value is changed in the entity.
        /// </summary>
        public event Action<IEntity<E>, int> OnValueChanged
        {
            add => this.Entity.OnValueChanged += value;
            remove => this.Entity.OnValueChanged -= value;
        }
        
        /// <summary>
        /// Gets the total number of values stored in the entity.
        /// </summary>
        public int ValueCount => this.Entity.ValueCount;

        /// <summary>
        /// Retrieves a value of type <typeparamref name="T"/> by key.
        /// </summary>
        public T GetValue<T>(int key) => this.Entity.GetValue<T>(key);
        
        /// <summary>
        /// Retrieves a value as an object by key.
        /// </summary>
        public object GetValue(int key) => _entity.GetValue(key);

        /// <summary>
        /// Returns a reference to a value of type <typeparamref name="T"/> stored at the given key.
        /// </summary>
        public ref T GetValueUnsafe<T>(int key) => ref this.Entity.GetValueUnsafe<T>(key);

        /// <summary>
        /// Tries to retrieve a value of type <typeparamref name="T"/> by key.
        /// </summary>
        public bool TryGetValue<T>(int key, out T value) => this.Entity.TryGetValue(key, out value);
        
        /// <summary>
        /// Tries to retrieve a value of type <typeparamref name="T"/> by key without boxing.
        /// </summary>
        public bool TryGetValueUnsafe<T>(int key, out T value) => this.Entity.TryGetValueUnsafe(key, out value);

        /// <summary>
        /// Tries to retrieve a value as an object by key.
        /// </summary>
        public bool TryGetValue(int key, out object value) => this.Entity.TryGetValue(key, out value);

        /// <summary>
        /// Adds a new value by key.
        /// </summary>
        public void AddValue(int key, object value) => this.Entity.AddValue(key, value);
        
        /// <summary>
        /// Adds a strongly-typed value by key.
        /// </summary>
        public void AddValue<T>(int key, T value) where T : struct => this.Entity.AddValue(key, value);

        /// <summary>
        /// Sets a value by key.
        /// </summary>
        public void SetValue(int key, object value) => this.Entity.SetValue(key, value);

        /// <summary>
        /// Sets a strongly-typed value by key.
        /// </summary>
        public void SetValue<T>(int key, T value) where T : struct => this.Entity.SetValue(key, value);

        /// <summary>
        /// Deletes a value by key.
        /// </summary>
        public bool DelValue(int key) => this.Entity.DelValue(key);
        
        /// <summary>
        /// Checks whether a value with the given key exists.
        /// </summary>
        public bool HasValue(int key) => this.Entity.HasValue(key);
        
        /// <summary>
        /// Clears all values associated with the entity.
        /// </summary>
        public void ClearValues() => this.Entity.ClearValues();

        /// <summary>
        /// Returns all key-value pairs currently stored in the entity.
        /// </summary>
        public KeyValuePair<int, object>[] GetValues() => this.Entity.GetValues();
        
        /// <summary>
        /// Fills the provided array with the entity's key-value pairs.
        /// </summary>
        public int GetValues(KeyValuePair<int, object>[] results) => this.Entity.GetValues(results);
        
        /// <summary>
        /// Returns an enumerator over the entity's key-value pairs.
        /// </summary>
        IEnumerator<KeyValuePair<int, object>> IEntity<E>.GetValueEnumerator() => this.Entity.GetValueEnumerator();
        
        public Entity<E>.ValueEnumerator GetValueEnumerator() => this.Entity.GetValueEnumerator();
    }
}