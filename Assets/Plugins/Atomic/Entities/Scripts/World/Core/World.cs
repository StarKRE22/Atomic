using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class World<E> : IWorld<E> where E : IEntity<E>
    {
        public event Action OnStateChanged;

        public event Action<E> OnAdded;
        public event Action<E> OnDeleted;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public IReadOnlyCollection<E> All => _entities.Values;
        public int Count => _entities.Count;

        private readonly Dictionary<int, E> _entities = new();
        private readonly List<E> _cache = new();

        private string _name;

        public World() => _name = string.Empty;

        public World(params E[] entities)
        {
            _name = string.Empty;
            this.AddEntities(entities);
        }

        public World(string name = null, params E[] entities)
        {
            _name = name;
            this.AddEntities(entities);
        }

        public World(string name, IEnumerable<E> entities)
        {
            _name = name;
            this.AddEntities(entities);
        }

        public bool Has(E entity) => _entities.ContainsKey(entity.InstanceID);

        public E[] GetAll()
        {
            E[] result = new E[_entities.Count];
            this.GetAll(result);
            return result;
        }

        public int GetAll(E[] results)
        {
            _entities.Values.CopyTo(results, 0);
            return _entities.Count;
        }

        public void CopyTo(ICollection<E> results)
        {
            results.Clear();
            foreach (E entity in _entities.Values) 
                results.Add(entity);
        }

        public bool Add(E entity)
        {
            if (!_entities.TryAdd(entity.InstanceID, entity))
                return false;

            _loop.Add(entity);

            this.AddTags(entity);
            this.AddValues(entity);
            this.Subscribe(entity);

            this.OnStateChanged?.Invoke();
            this.OnAdded?.Invoke(entity);
            return true;
        }

        public bool Del(E entity)
        {
            if (!_entities.Remove(entity.InstanceID))
                return false;

            _loop.Del(entity);

            this.Unsubscribe(in entity);
            this.RemoveTags(entity);
            this.RemoveValues(entity);

            this.OnStateChanged?.Invoke();
            this.OnDeleted?.Invoke(entity);
            return true;
        }

        public void Clear()
        {
            int count = _entities.Count;
            if (count == 0)
                return;

            _cache.Clear();
            _cache.AddRange(_entities.Values);
            
            _entities.Clear();
            _tags.Clear();
            _values.Clear();
            _loop.Clear();

            foreach (E entity in _cache)
            {
                this.Unsubscribe(in entity);
                this.OnDeleted?.Invoke(entity);
            }

            this.OnStateChanged?.Invoke();
        }

        private void Subscribe(in E entity)
        {
            entity.OnTagAdded += this.OnTagAdded;
            entity.OnTagDeleted += this.OnTagRemoved;

            entity.OnValueAdded += this.OnValueAdded;
            entity.OnValueDeleted += this.OnValueRemoved;
        }

        private void Unsubscribe(in E entity)
        {
            entity.OnTagAdded -= this.OnTagAdded;
            entity.OnTagDeleted -= this.OnTagRemoved;

            entity.OnValueAdded -= this.OnValueAdded;
            entity.OnValueDeleted -= this.OnValueRemoved;
        }

        public IEnumerator<E> GetEnumerator() => _entities.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}