using System;
using System.Collections;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class SceneWorld<E>
    {
        public event Action<E> OnAdded
        {
            add => _world.OnAdded += value;
            remove => _world.OnAdded -= value;
        }

        public event Action<E> OnDeleted
        {
            add => _world.OnDeleted += value;
            remove => _world.OnDeleted -= value;
        }

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public int Count => _world.Count;

#if ODIN_INSPECTOR
        [Searchable]
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        public IReadOnlyCollection<E> All => _world.All;

        public bool GetWithTag(int tag, out E entity) => _world.GetWithTag(tag, out entity);
        public int GetAllWithTag(int tag, E[] results) => _world.GetAllWithTag(tag, results);
        public IReadOnlyList<E> GetAllWithTag(int tag) => _world.GetAllWithTag(tag);

        public bool GetWithValue(int valueId, out E result) => _world.GetWithValue(valueId, out result);
        public IReadOnlyList<E> GetAllWithValue(int valueKey) => _world.GetAllWithValue(valueKey);

        public int GetAllWithValue(int valueKey, E[] results) =>
            _world.GetAllWithValue(valueKey, results);

        public bool Has(E entity) => _world.Has(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Add(E entity) => _world.Add(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Del(E entity) => _world.Del(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Clear() => _world.Clear();

        public E[] GetAll() => _world.GetAll();

        public int GetAll(E[] results) => _world.GetAll(results);

        public void CopyTo(ICollection<E> results) => _world.CopyTo(results);

        public IEnumerator<E> GetEnumerator() => _world.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _world).GetEnumerator();
    }
}