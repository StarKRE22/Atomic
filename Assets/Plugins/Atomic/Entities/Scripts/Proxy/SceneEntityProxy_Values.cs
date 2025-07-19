using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class SceneEntityProxy<E>
    {
        public event Action<E, int> OnValueAdded
        {
            add => _source.OnValueAdded += value;
            remove => _source.OnValueAdded -= value;
        }

        public event Action<E, int> OnValueDeleted
        {
            add => _source.OnValueDeleted += value;
            remove => _source.OnValueDeleted -= value;
        }

        public event Action<E, int> OnValueChanged
        {
            add => _source.OnValueChanged += value;
            remove => _source.OnValueChanged -= value;
        }
        
        public int ValueCount => _source.ValueCount;

        public T GetValue<T>(int key) => _source.GetValue<T>(key);
        public ref T GetValueUnsafe<T>(int key) => ref _source.GetValueUnsafe<T>(key);
        public object GetValue(int key) => _source.GetValue(key);

        public bool TryGetValue<T>(int key, out T value) => _source.TryGetValue(key, out value);
        public bool TryGetValueUnsafe<T>(int key, out T value) => _source.TryGetValueUnsafe(key, out value);
        public bool TryGetValue(int key, out object value) => _source.TryGetValue(key, out value);

        public void AddValue(int key, object value) => _source.AddValue(key, value);
        public void AddValue<T>(int key, T value) where T : struct => _source.AddValue(key, value);

        public void SetValue(int key, object value) => _source.SetValue(key, value);
        public void SetValue<T>(int key, T value) where T : struct => _source.SetValue(key, value);

        public bool DelValue(int key) => _source.DelValue(key);
        public bool HasValue(int key) => _source.HasValue(key);
        public void ClearValues() => _source.ClearValues();
        
        public KeyValuePair<int, object>[] GetValues() => _source.GetValues();
        public int GetValues(KeyValuePair<int, object>[] results) => _source.GetValues(results);
        public IEnumerator<KeyValuePair<int, object>> GetValueEnumerator() => _source.GetValueEnumerator();
    }
}