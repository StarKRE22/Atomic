using System;
using System.Collections;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class SceneEntityWorld<E>
    {
        public event Action<E> OnAdded
        {
            add => _world.OnAdded += value;
            remove => _world.OnAdded -= value;
        }

        public event Action<E> OnRemoved
        {
            add => _world.OnRemoved += value;
            remove => _world.OnRemoved -= value;
        }

        public bool IsReadOnly => _world.IsReadOnly;

        int IEntityCollection<E>.Count => _world.Count;
        int ICollection<E>.Count => _world.Count;
        int IReadOnlyCollection<E>.Count => _world.Count;
        
        public E this[int index] => _world[index];

        void ICollection<E>.Add(E item) => _world.Add(item);

        void ICollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);
        void IEntityCollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);
        void IReadOnlyEntityCollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);

        public bool FindWithTag(int tag, out E entity) => _world.FindWithTag(tag, out entity);
        public int CopyWithTag(int tag, E[] results) => _world.CopyWithTag(tag, results);
        public E[] FindAllWithTag(int tag) => _world.FindAllWithTag(tag);

        public bool FindWithValue(int valueId, out E result) => _world.FindWithValue(valueId, out result);
        public E[] FindAllWithValue(int valueKey) => _world.FindAllWithValue(valueKey);

        public int CopyWithValue(int valueKey, E[] results) =>
            _world.CopyWithValue(valueKey, results);

        public bool Contains(E entity) => _world.Contains(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Add(E entity) => _world.Add(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Remove(E entity) => _world.Remove(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Clear() => _world.Clear();

        public void CopyTo(ICollection<E> results) => _world.CopyTo(results);

        public IEnumerator<E> GetEnumerator() => _world.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _world.GetEnumerator();
    }
}