using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Atomic.Entities
{
    /// <summary>
    /// Partial implementation of <see cref="IEntity"/> that manages attached behaviours.
    /// </summary>
    public partial class Entity<E>
    {
        /// <summary>
        /// Static comparer used to compare behaviours.
        /// </summary>
        private static readonly IEqualityComparer<IBehaviour<E>> s_behaviourComparer =
            EqualityComparer<IBehaviour<E>>.Default;

        /// <summary>
        /// Shared pool used to temporarily store behaviour arrays.
        /// </summary>
        private static readonly ArrayPool<IBehaviour<E>> s_behaviourPool =
            ArrayPool<IBehaviour<E>>.Shared;

        /// <summary>
        /// Invoked when a new behaviour is added.
        /// </summary>
        public event Action<Entity<E>, IBehaviour<E>> OnBehaviourAdded;

        /// <summary>
        /// Invoked when a behaviour is removed.
        /// </summary>
        public event Action<IEntity, IBehaviour<E>> OnBehaviourDeleted;

        /// <summary>
        /// Total number of behaviours attached to this entity.
        /// </summary>
        public int BehaviourCount => _behaviourCount;

        internal IBehaviour<E>[] _behaviours;
        internal int _behaviourCount;


        /// <summary>
        /// Checks whether a specific behaviour instance is attached to this entity.
        /// </summary>
        public bool HasBehaviour(IBehaviour<E> behaviour)
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
        public bool HasBehaviour<T>() where T : IBehaviour<E>
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T)
                    return true;

            return false;
        }

        /// <summary>
        /// Adds a new behaviour to the entity.
        /// </summary>
        public void AddBehaviour(IBehaviour<E> behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException(nameof(behaviour));

            InternalUtils.Add(ref _behaviours, ref _behaviourCount, in behaviour);

            if (this.initialized && behaviour is IInit initBehaviour)
                initBehaviour.Init(this.owner);

            if (this.enabled)
                this.EnableBehaviour(in behaviour);

            this.OnBehaviourAdded?.Invoke(this, behaviour);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Removes the first behaviour of type <typeparamref name="T"/>.
        /// </summary>
        public bool DelBehaviour<T>() where T : IBehaviour<E>
        {
            for (int i = 0; i < _behaviourCount; i++)
            {
                IBehaviour<E> behaviour = _behaviours[i];
                if (behaviour is T)
                    return this.DelBehaviour(behaviour);
            }

            return false;
        }

        /// <summary>
        /// Removes a specific behaviour instance.
        /// </summary>
        public bool DelBehaviour(IBehaviour<E> behaviour)
        {
            if (behaviour == null)
                return false;

            if (!InternalUtils.Remove(ref _behaviours, ref _behaviourCount, in behaviour, in s_behaviourComparer))
                return false;

            if (this.enabled)
                this.DisableBehaviour(in behaviour);

            if (this.initialized && behaviour is IDispose dispose)
                dispose.Dispose(this.owner);

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
            IBehaviour[] removedBehaviours = s_behaviourPool.Rent(removedCount);
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
        public T GetBehaviour<T>() where T : IBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T result)
                    return result;

            throw new Exception($"Entity Behaviour of type {typeof(T).Name} is not found!");
        }

        /// <summary>
        /// Attempts to get the first behaviour of type <typeparamref name="T"/>.
        /// </summary>
        public bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour
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
        public IBehaviour GetBehaviourAt(in int index)
        {
            if (index < 0 || index >= _behaviourCount)
                throw new IndexOutOfRangeException($"Index {index} is out of bounds.");

            return _behaviours[index];
        }

        /// <summary>
        /// Returns a new array of all behaviours attached to this entity.
        /// </summary>
        public IBehaviour[] GetBehaviours()
        {
            IBehaviour[] result = new IBehaviour[_behaviourCount];
            this.GetBehaviours(result);
            return result;
        }

        /// <summary>
        /// Copies all behaviours into the provided array.
        /// </summary>
        public int GetBehaviours(IBehaviour[] results)
        {
            Array.Copy(_behaviours, results, _behaviourCount);
            return _behaviourCount;
        }

        /// <summary>
        /// Returns an enumerator for iterating through behaviours.
        /// </summary>
        IEnumerator<IBehaviour> IEntity.GetBehaviourEnumerator() => new BehaviourEnumerator(this);
        
        public BehaviourEnumerator GetBehaviourEnumerator() => new(this);

        public struct BehaviourEnumerator : IEnumerator<IBehaviour>
        {
            public IBehaviour Current => _current;

            object IEnumerator.Current => _current;

            private readonly Entity _entity;
            private int _index;
            private IBehaviour _current;

            public BehaviourEnumerator(in Entity entity)
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
        /// Initializes the behaviour array with an optional capacity.
        /// </summary>
        private void InitializeBehaviours(in IEnumerable<IBehaviour> behaviours)
        {
            if (behaviours == null)
                this.InitializeBehaviours();
            else
            {
                this.InitializeBehaviours(behaviours.Count());

                foreach (IBehaviour behaviour in behaviours)
                    if (behaviour != null)
                        _behaviours[_behaviourCount++] = behaviour;
            }
        }

        /// <summary>
        /// Initializes the behaviour array from a collection.
        /// </summary>
        private void InitializeBehaviours(in int capacity = 0) =>
            _behaviours = new IBehaviour[capacity];
    }
}