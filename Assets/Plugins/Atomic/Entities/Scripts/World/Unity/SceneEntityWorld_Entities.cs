using System;
using System.Collections;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class SceneEntityWorld
    {
        public event Action<IEntity> OnAdded
        {
            add => _world.OnAdded += value;
            remove => _world.OnAdded -= value;
        }

        public event Action<IEntity> OnDeleted
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
        public IReadOnlyCollection<IEntity> All => _world.All;

        public bool GetWithTag(in int tag, out IEntity entity) => _world.GetWithTag(in tag, out entity);
        public int GetAllWithTag(in int tag, in IEntity[] results) => _world.GetAllWithTag(in tag, results);
        public IReadOnlyList<IEntity> GetAllWithTag(in int tag) => _world.GetAllWithTag(in tag);

        public bool GetWithValue(in int valueId, out IEntity result) => _world.GetWithValue(in valueId, out result);
        public IReadOnlyList<IEntity> GetAllWithValue(in int valueKey) => _world.GetAllWithValue(in valueKey);

        public int GetAllWithValue(in int valueKey, in IEntity[] results) =>
            _world.GetAllWithValue(in valueKey, results);
        
        public bool Has(in IEntity entity) => _world.Has(in entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Add(in IEntity entity) => _world.Add(in entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Del(in IEntity entity) => _world.Del(in entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Clear() => _world.Clear();

        public IEntity[] GetAll() => _world.GetAll();

        public int GetAll(in IEntity[] results) => _world.GetAll(in results);

        public void CopyTo(ICollection<IEntity> results) => _world.CopyTo(results);

        public IEnumerator<IEntity> GetEnumerator() => _world.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _world).GetEnumerator();
    }
}