using System.Collections;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class EntityWorld<E> : EntityRunner<E>, IEntityWorld<E> where E : IEntity
    {
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private readonly Dictionary<int, E> _entities = new();
        private readonly List<E> _cache = new();

        private string _name;

        public EntityWorld() => _name = string.Empty;

        public EntityWorld(params E[] entities)
        {
            _name = string.Empty;
            this.AddEntities(entities);
        }

        public EntityWorld(string name = null, params E[] entities)
        {
            _name = name;
            this.AddEntities(entities);
        }

        public EntityWorld(string name, IEnumerable<E> entities)
        {
            _name = name;
            this.AddEntities(entities);
        }

        public bool Contains(E entity) => _entities.ContainsKey(entity.SpawnedID);

        public E[] GetAll()
        {
            E[] result = new E[_entities.Count];
            this.CopyTo(result);
            return result;
        }

        public int CopyTo(E[] results)
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
            if (!_entities.TryAdd(entity.SpawnedID, entity))
                return false;

            _runner.Add(entity);

            this.AddTags(entity);
            this.AddValues(entity);
            this.Subscribe(entity);

            this.OnStateChanged?.Invoke();
            this.OnAdded?.Invoke(entity);
            return true;
        }

        public bool Del(E entity)
        {
            if (!_entities.Remove(entity.SpawnedID))
                return false;

            _runner.Del(entity);

            this.Unsubscribe(in entity);
            this.RemoveTags(entity);
            this.RemoveValues(entity);

            this.OnStateChanged?.Invoke();
            this.OnRemoved?.Invoke(entity);
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
            _runner.Clear();

            foreach (E entity in _cache)
            {
                this.Unsubscribe(in entity);
                this.OnRemoved?.Invoke(entity);
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