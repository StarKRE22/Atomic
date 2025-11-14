using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// Partial implementation of <see cref="IEntity"/> that manages attached behaviours.
    /// </summary>
    public partial class Entity
    {
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

            //Check for capacity:
            int capacity = _behaviours.Length;
            if (_behaviourCount == capacity)
                Array.Resize(ref _behaviours, capacity == 0 ? 1 : capacity * 2);

            //Push as last
            _behaviours[_behaviourCount] = behaviour;
            _behaviourCount++;

            if (_initialized && behaviour is IEntityInit init)
                init.Init(this);

            if (_enabled)
                this.EnableBehaviour(behaviour);

            this.OnBehaviourAdded?.Invoke(this, behaviour);
            this.OnStateChanged?.Invoke(this);
        }

        /// <summary>
        /// Removes the first behaviour of type <typeparamref name="T"/>.
        /// </summary>
        public bool DelBehaviour<T>() where T : IEntityBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T)
                    return this.DelBehaviourAt(i);

            return false;
        }

        /// <summary>
        /// Removes the all behaviours of type <typeparamref name="T"/>.
        /// </summary>
        public void DelBehaviours<T>() where T : IEntityBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T)
                    this.DelBehaviourAt(i);
        }

        /// <summary>
        /// Removes the behaviour at the specified index.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool DelBehaviourAt(int index)
        {
            if (index < 0 || index >= _behaviourCount)
                return false;

            IEntityBehaviour behaviour = _behaviours[index];

            _behaviourCount--;
            if (index < _behaviourCount)
                Array.Copy(_behaviours, index + 1, _behaviours, index, _behaviourCount - index);
           
            _behaviours[_behaviourCount] = null;

            if (_enabled)
                this.DisableBehaviour(behaviour);

            if (_initialized && behaviour is IEntityDispose dispose)
                dispose.Dispose(this);

            this.OnBehaviourDeleted?.Invoke(this, behaviour);
            this.OnStateChanged?.Invoke(this);
            return true;
        }

        /// <summary>
        /// Removes a specific behaviour instance.
        /// </summary>
        public bool DelBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour == null)
                return false;

            for (int i = 0; i < _behaviourCount; i++)
            {
                if (_behaviours[i] != behaviour)
                    continue;

                _behaviourCount--;
                if (i < _behaviourCount)
                    Array.Copy(_behaviours, i + 1, _behaviours, i, _behaviourCount - i);

                _behaviours[_behaviourCount] = null;

                if (_enabled)
                    this.DisableBehaviour(behaviour);

                if (_initialized && behaviour is IEntityDispose dispose)
                    dispose.Dispose(this);

                this.OnBehaviourDeleted?.Invoke(this, behaviour);
                this.OnStateChanged?.Invoke(this);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes all behaviours from this entity.
        /// </summary>
        public void ClearBehaviours()
        {
            if (_behaviourCount == 0)
                return;
            
            while (_behaviourCount > 0)
            {
                _behaviourCount--;
                IEntityBehaviour behaviour = _behaviours[_behaviourCount];
                _behaviours[_behaviourCount] = null;

                if (_enabled)
                    this.DisableBehaviour(behaviour);

                if (_initialized && behaviour is IEntityDispose dispose)
                    dispose.Dispose(this);

                this.OnBehaviourDeleted?.Invoke(this, behaviour);
            }

            this.OnStateChanged?.Invoke(this);
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
        public IEntityBehaviour GetBehaviourAt(int index) => index < 0 || index >= _behaviourCount
            ? throw new IndexOutOfRangeException($"Index {index} is out of bounds.")
            : _behaviours[index];

        /// <summary>
        /// Returns a new array of all behaviours attached to this entity.
        /// </summary>
        public IEntityBehaviour[] GetBehaviours()
        {
            IEntityBehaviour[] result = new IEntityBehaviour[_behaviourCount];
            this.CopyBehaviours(result);
            return result;
        }

        public T[] GetBehaviours<T>() where T : IEntityBehaviour
        {
            if (_behaviourCount == 0)
                return Array.Empty<T>();

            Span<int> indexes = stackalloc int[_behaviourCount];
            int count = 0;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T)
                    indexes[count++] = i;

            T[] result = new T[count];
            for (int i = 0; i < count; i++)
                result[i] = (T) _behaviours[indexes[i]];

            return result;
        }

        /// <summary>
        /// Copies all behaviours into the provided array.
        /// </summary>
        public int CopyBehaviours(IEntityBehaviour[] results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            Array.Copy(_behaviours, results, _behaviourCount);
            return _behaviourCount;
        }

        /// <summary>Copies behaviours of type <typeparamref name="T"/> into <paramref name="results"/>.</summary>
        /// <returns>Number of items written.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CopyBehaviours<T>(T[] results) where T : IEntityBehaviour
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            int count = 0;
            int capacity = results.Length;

            if (capacity == 0)
                return 0;

            for (int i = 0; i < _behaviourCount && count < capacity; i++)
                if (_behaviours[i] is T behaviour)
                    results[count++] = behaviour;

            return count;
        }

        /// <summary>
        /// Returns an enumerator for iterating through behaviours.
        /// </summary>
        IEnumerator<IEntityBehaviour> IEntity.GetBehaviourEnumerator() => new BehaviourEnumerator(this);

        /// <summary>
        /// Returns an enumerator for iterating through behaviours.
        /// </summary>
        public BehaviourEnumerator GetBehaviourEnumerator() => new(this);

        public struct BehaviourEnumerator : IEnumerator<IEntityBehaviour>
        {
            public IEntityBehaviour Current => _current;

            object IEnumerator.Current => _current;

            private readonly Entity _entity;
            private int _index;
            private IEntityBehaviour _current;

            public BehaviourEnumerator(Entity entity)
            {
                _entity = entity;
                _index = -1;
                _current = null;
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
                _current = null;
            }

            public void Dispose()
            {
                //Nothing...
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ConstructBehaviours(int capacity = 0) =>
            _behaviours = new IEntityBehaviour[capacity];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityEnable enable)
                enable.Enable(this);

            if (behaviour is IEntityTick update)
                Add(ref _updates, ref _updateCount, update);

            if (behaviour is IEntityFixedTick fixedUpdate)
                Add(ref _fixedUpdates, ref _fixedUpdateCount, fixedUpdate);

            if (behaviour is IEntityLateTick lateUpdate)
                Add(ref _lateUpdates, ref _lateUpdateCount, lateUpdate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DisableBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour is IEntityDisable disable)
                disable.Disable(this);

            if (behaviour is IEntityTick update)
                Remove(ref _updates, ref _updateCount, update, s_updateComparer);

            if (behaviour is IEntityFixedTick fixedUpdate)
                Remove(ref _fixedUpdates, ref _fixedUpdateCount, fixedUpdate, s_fixedUpdateComparer);

            if (behaviour is IEntityLateTick lateUpdate)
                Remove(ref _lateUpdates, ref _lateUpdateCount, lateUpdate, s_lateUpdateComparer);
        }
    }
}