#if UNITY_5_3_OR_NEWER
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Atomic.Entities.InternalUtils;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        /// <summary>
        /// Static comparer used to compare behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IEntityBehaviour> s_behaviourComparer =
            EqualityComparer<IEntityBehaviour>.Default;

        /// <summary>
        /// Shared pool used to temporarily store behaviour arrays.
        /// </summary>
        private static readonly ArrayPool<IEntityBehaviour> s_behaviourPool =
            ArrayPool<IEntityBehaviour>.Shared;

        /// <summary>
        /// Invoked when a new behaviour is added.
        /// </summary>
        public event Action<IEntity, IEntityBehaviour> OnBehaviourAdded;

        /// <summary>
        /// Invoked when a behaviour is removed.
        /// </summary>
        public event Action<IEntity, IEntityBehaviour> OnBehaviourDeleted;

        /// <summary>
        /// Total number of behaviours attached to this entity.
        /// </summary>
        public int BehaviourCount => _behaviourCount;

        /// <summary>
        /// Initial behaviour capacity used to optimize behaviour allocation.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Optimization")]
        [ReadOnly]
#endif
        [Min(0)]
        [SerializeField]
        private int _initialBehaviourCapacity;
        
        private IEntityBehaviour[] _behaviours;
        private int _behaviourCount;

        /// <summary>
        /// Checks whether a specific behaviour instance is attached to this entity.
        /// </summary>
        public bool HasBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour == null)
                return false;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] == behaviour)
                    return true;

            return false;
        }

        /// <summary>
        /// Checks whether a behaviour of type <typeparamref name="T"/> is attached.
        /// </summary>
        public bool HasBehaviour<T>() where T : IEntityBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T)
                    return true;

            return false;
        }

        /// <summary>
        /// Adds a new behaviour to the entity.
        /// </summary>
        public void AddBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException(nameof(behaviour));

            if (!AddIfAbsent(ref _behaviours, ref _behaviourCount, behaviour, s_behaviourComparer))
                return;

            if (_spawned && behaviour is IEntitySpawn initBehaviour)
                initBehaviour.Spawn(this);

            if (_enabled)
                this.EnableBehaviour(behaviour);

            this.OnBehaviourAdded?.Invoke(this, behaviour);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Removes the first behaviour of type <typeparamref name="T"/>.
        /// </summary>
        public bool DelBehaviour<T>() where T : IEntityBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
            {
                IEntityBehaviour behaviour = _behaviours[i];
                if (behaviour is T)
                    return this.DelBehaviour(behaviour);
            }

            return false;
        }

        /// <summary>
        /// Removes a specific behaviour instance.
        /// </summary>
        public bool DelBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour == null)
                return false;

            if (!Remove(ref _behaviours, ref _behaviourCount, behaviour, s_behaviourComparer))
                return false;

            if (_enabled)
                this.DisableBehaviour(behaviour);

            if (_spawned && behaviour is IEntityDespawn dispose)
                dispose.Despawn(this);

            this.OnBehaviourDeleted?.Invoke(this, behaviour);
            this.OnStateChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// Removes all behaviours from this entity.
        /// </summary>
        public void ClearBehaviours()
        {
            if (_behaviourCount == 0)
                return;

            int removedCount = _behaviourCount;
            IEntityBehaviour[] removedBehaviours = s_behaviourPool.Rent(removedCount);
            Array.Copy(_behaviours, removedBehaviours, removedCount);

            _behaviourCount = 0;

            try
            {
                for (int i = 0; i < removedCount; i++)
                    this.OnBehaviourDeleted?.Invoke(this, removedBehaviours[i]);

                this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_behaviourPool.Return(removedBehaviours);
            }
        }

        /// <summary>
        /// Gets the first behaviour of type <typeparamref name="T"/>.
        /// </summary>
        public T GetBehaviour<T>() where T : IEntityBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T result)
                    return result;

            throw new Exception($"Entity Behaviour of type {typeof(T).Name} is not found!");
        }

        /// <summary>
        /// Attempts to get the first behaviour of type <typeparamref name="T"/>.
        /// </summary>
        public bool TryGetBehaviour<T>(out T behaviour) where T : IEntityBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
            {
                if (_behaviours[i] is T tBehaviour)
                {
                    behaviour = tBehaviour;
                    return true;
                }
            }

            behaviour = default;
            return false;
        }

        /// <summary>
        /// Returns the behaviour instance at the given index.
        /// </summary>
        public IEntityBehaviour GetBehaviourAt(int index)
        {
            if (index < 0 || index >= _behaviourCount)
                throw new IndexOutOfRangeException($"Index {index} is out of bounds.");

            return _behaviours[index];
        }

        /// <summary>
        /// Returns a new array of all behaviours attached to this entity.
        /// </summary>
        public IEntityBehaviour[] GetBehaviours()
        {
            IEntityBehaviour[] result = new IEntityBehaviour[_behaviourCount];
            this.CopyBehaviours(result);
            return result;
        }

        /// <summary>
        /// Copies all behaviours into the provided array.
        /// </summary>
        public int CopyBehaviours(IEntityBehaviour[] results)
        {
            Array.Copy(_behaviours, results, _behaviourCount);
            return _behaviourCount;
        }

        /// <summary>
        /// Returns an enumerator for iterating through behaviours.
        /// </summary>
        IEnumerator<IEntityBehaviour> IEntity.GetBehaviourEnumerator() => new BehaviourEnumerator(this);

        public BehaviourEnumerator GetBehaviourEnumerator() => new(this);

        public struct BehaviourEnumerator : IEnumerator<IEntityBehaviour>
        {
            public IEntityBehaviour Current => _current;

            object IEnumerator.Current => _current;

            private readonly SceneEntity _entity;
            private int _index;
            private IEntityBehaviour _current;

            public BehaviourEnumerator(SceneEntity entity)
            {
                _entity = entity;
                _index = -1;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_index + 1 == _entity._behaviourCount)
                    return false;

                _current = _entity._behaviours[++_index];
                return true;
            }

            public void Reset()
            {
                _index = -1;
                _current = default;
            }

            public void Dispose()
            {
                //Nothing...
            }
        }

        /// <summary>
        /// Initializes the behaviour array from a collection.
        /// </summary>
        private void ConstructBehaviours() =>
            _behaviours = new IEntityBehaviour[_initialBehaviourCapacity];
    }
}
#endif