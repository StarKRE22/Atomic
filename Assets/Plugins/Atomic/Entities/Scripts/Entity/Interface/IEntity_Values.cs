using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IEntity
    {
        event Action<IEntity, int> OnValueAdded;
        event Action<IEntity, int> OnValueDeleted;
        event Action<IEntity, int> OnValueChanged;

        int ValueCount { get; }

        T GetValue<T>(int key);
        ref T GetValueUnsafe<T>(int key);
        object GetValue(int key);
        
        bool TryGetValue<T>(int key, out T value);
        bool TryGetValueUnsafe<T>(int key, out T value);
        bool TryGetValue(int key, out object value);

        void SetValue(int key, in object value);
        void SetValue<T>(int key, in T value) where T : struct;

        bool HasValue(int key);

        void AddValue(int key, in object value);
        void AddValue<T>(int key, in T value) where T : struct;

        bool DelValue(int key);
        void ClearValues();

        KeyValuePair<int, object>[] GetValues();
        int GetValues(in KeyValuePair<int, object>[] results);
        IEnumerator<KeyValuePair<int, object>> ValueEnumerator();
    }
}