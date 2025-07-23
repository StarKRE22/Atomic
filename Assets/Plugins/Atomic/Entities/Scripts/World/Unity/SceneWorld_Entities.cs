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

        public bool FindWithTag(int tag, out E entity) => _world.FindWithTag(tag, out entity);
        public int CopyWithTag(int tag, E[] results) => _world.CopyWithTag(tag, results);
        public E[] FindAllWithTag(int tag) => _world.FindAllWithTag(tag);

        public bool FindWithValue(int valueId, out E result) => _world.FindWithValue(valueId, out result);
        public E[] FindAllWithValue(int valueKey) => _world.FindAllWithValue(valueKey);

        public int CopyWithValue(int valueKey, E[] results) =>
            _world.CopyWithValue(valueKey, results);

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

        public int CopyTo(E[] results) => _world.CopyTo(results);

        public void CopyTo(ICollection<E> results) => _world.CopyTo(results);

        public IEnumerator<E> GetEnumerator() => _world.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _world).GetEnumerator();
    }
}