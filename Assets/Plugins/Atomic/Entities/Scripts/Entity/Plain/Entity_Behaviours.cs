using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Atomic.Entities.AtomicHelper;

namespace Atomic.Entities
{
    public partial class Entity
    {
        private static readonly IEqualityComparer<IBehaviour> s_behaviourComparer = 
            EqualityComparer<IBehaviour>.Default;
        
        private static readonly ArrayPool<IBehaviour> s_behaviourPool = 
            ArrayPool<IBehaviour>.Shared;

        public event Action<IEntity, IBehaviour> OnBehaviourAdded;
        public event Action<IEntity, IBehaviour> OnBehaviourDeleted;

        public int BehaviourCount => _behaviourCount;

        internal IBehaviour[] _behaviours;
        internal int _behaviourCount;

        private void InitializeBehaviours(in IEnumerable<IBehaviour> behaviours)
        {
            if (behaviours == null)
            {
                this.InitializeBehaviours(0);
            }
            else
            {
                this.InitializeBehaviours(behaviours.Count());

                foreach (IBehaviour behaviour in behaviours)
                    if (behaviour != null)
                        _behaviours[_behaviourCount++] = behaviour;
            }
        }

        private void InitializeBehaviours(in int capacity = 0)
        {
            _behaviours = new IBehaviour[capacity];
        }

        public bool HasBehaviour(in IBehaviour behaviour)
        {
            if (behaviour == null)
                return false;

            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] == behaviour)
                    return true;

            return false;
        }

        public bool HasBehaviour<T>() where T : IBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T)
                    return true;

            return false;
        }

        public void AddBehaviour(in IBehaviour behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException(nameof(behaviour));

            Add(ref _behaviours, ref _behaviourCount, in behaviour);

            if (this.initialized && behaviour is IInit initBehaviour)
                initBehaviour.Init(in this.owner);

            if (this.enabled)
                this.EnableBehaviour(in behaviour);

            this.OnBehaviourAdded?.Invoke(this, behaviour);
            this.OnStateChanged?.Invoke();
        }
        
        public bool DelBehaviour<T>() where T : IBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
            {
                IBehaviour behaviour = _behaviours[i];
                if (behaviour is T)
                    return this.DelBehaviour(in behaviour);
            }

            return false;
        }

        public bool DelBehaviour(in IBehaviour behaviour)
        {
            if (behaviour == null)
                return false;

            if (!Remove(ref _behaviours, ref _behaviourCount, in behaviour, in s_behaviourComparer))
                return false;

            if (this.enabled)
                this.DisableBehaviour(in behaviour);

            if (this.initialized && behaviour is IDispose dispose)
                dispose.Dispose(in this.owner);

            this.OnBehaviourDeleted?.Invoke(this, behaviour);
            this.OnStateChanged?.Invoke();
            return true;
        }

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

        public T GetBehaviour<T>() where T : IBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T result)
                    return result;

            throw new Exception($"Entity Behaviour of type {typeof(T).Name} is not found!");
        }

        public bool TryGetBehaviour<T>(out T behaviour) where T : IBehaviour
        {
            for (int i = 0; i < _behaviourCount; i++)
                if (_behaviours[i] is T tBehaviour)
                    behaviour = tBehaviour;

            behaviour = default;
            return false;
        }

        public IBehaviour GetBehaviourAt(in int index)
        {
            return _behaviours[index];
        }

        public IBehaviour[] GetBehaviours()
        {
            IBehaviour[] result = new IBehaviour[_behaviourCount];
            this.GetBehaviours(result);
            return result;
        }

        public int GetBehaviours(in IBehaviour[] results)
        {
            Array.Copy(_behaviours, results, _behaviourCount);
            return _behaviourCount;
        }

        public IEnumerator<IBehaviour> BehaviourEnumerator()
        {
            return new _BehaviourEnumerator(this);
        }

        private struct _BehaviourEnumerator : IEnumerator<IBehaviour>
        {
            public IBehaviour Current => _current;

            object IEnumerator.Current => _current;

            private readonly Entity _entity;
            private int _index;
            private IBehaviour _current;

            public _BehaviourEnumerator(in Entity entity)
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
    }
}