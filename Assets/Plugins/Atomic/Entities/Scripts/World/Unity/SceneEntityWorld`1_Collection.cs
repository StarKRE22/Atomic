#if UNITY_5_3_OR_NEWER
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
        /// <inheritdoc />
        public event Action<E> OnAdded
        {
            add => _world.OnAdded += value;
            remove => _world.OnAdded -= value;
        }

        /// <inheritdoc />
        public event Action<E> OnRemoved
        {
            add => _world.OnRemoved += value;
            remove => _world.OnRemoved -= value;
        }

        /// <inheritdoc />
        public bool IsReadOnly => _world.IsReadOnly;

        /// <inheritdoc />
        int IEntityCollection<E>.Count => _world.Count;

        /// <inheritdoc />
        int ICollection<E>.Count => _world.Count;

        /// <inheritdoc />
        int IReadOnlyCollection<E>.Count => _world.Count;

        /// <inheritdoc />
        void ICollection<E>.Add(E item) => _world.Add(item);

        /// <inheritdoc />
        void ICollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        void IEntityCollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        void IReadOnlyEntityCollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="IReadOnlyEntityCollection{E}.Contains" />
        public bool Contains(E entity) => _world.Contains(entity);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Add(E entity) => _world.Add(entity);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Remove(E entity) => _world.Remove(entity);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Clear() => _world.Clear();

        /// <inheritdoc />
        public void CopyTo(ICollection<E> results) => _world.CopyTo(results);

        /// <inheritdoc />
        public IEnumerator<E> GetEnumerator() => _world.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => _world.GetEnumerator();
    }
}
#endif