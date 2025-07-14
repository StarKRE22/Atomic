using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        public event Action<IEntity, int> OnValueAdded
        {
            add => this.Entity.OnValueAdded += value;
            remove => this.Entity.OnValueAdded -= value;
        }

        public event Action<IEntity, int> OnValueDeleted
        {
            add => this.Entity.OnValueDeleted += value;
            remove => this.Entity.OnValueDeleted -= value;
        }

        public event Action<IEntity, int> OnValueChanged
        {
            add => this.Entity.OnValueChanged += value;
            remove => this.Entity.OnValueChanged -= value;
        }

        public int ValueCount => this.Entity.ValueCount;

        public T GetValue<T>(int key) => this.Entity.GetValue<T>(key);
        public object GetValue(int key) => _entity.GetValue(key);
        public ref T GetValueUnsafe<T>(int key) => ref this.Entity.GetValueUnsafe<T>(key);

        public bool TryGetValue<T>(int key, out T value) => this.Entity.TryGetValue(key, out value);
        public bool TryGetValueUnsafe<T>(int key, out T value) => this.Entity.TryGetValueUnsafe(key, out value);
        public bool TryGetValue(int key, out object value) => this.Entity.TryGetValue(key, out value);

        public void AddValue(int key, in object value) => this.Entity.AddValue(key, in value);
        public void AddValue<T>(int key, in T value) where T : struct => this.Entity.AddValue(key, in value);

        public void SetValue(int key, in object value) => this.Entity.SetValue(key, in value);
        public void SetValue<T>(int key, in T value) where T : struct => this.Entity.SetValue(key, in value);

        public bool DelValue(int key) => this.Entity.DelValue(key);
        public bool HasValue(int key) => this.Entity.HasValue(key);
        public void ClearValues() => this.Entity.ClearValues();

        public KeyValuePair<int, object>[] GetValues() => this.Entity.GetValues();
        public int GetValues(in KeyValuePair<int, object>[] results) => this.Entity.GetValues(results);
        public IEnumerator<KeyValuePair<int, object>> ValueEnumerator() => this.Entity.ValueEnumerator();
    }
}