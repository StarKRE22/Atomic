using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class EntityWorld : IEntityWorld
    {
        public event Action OnStateChanged;

        public event Action<IEntity> OnAdded;
        public event Action<IEntity> OnDeleted;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public IReadOnlyCollection<IEntity> All => _entities.Values;
        public int Count => _entities.Count;

        private readonly Dictionary<int, IEntity> _entities = new();
        private readonly List<IEntity> _cache = new();

        private string _name;

        public EntityWorld()
        {
            _name = string.Empty;
        }

        public EntityWorld(params IEntity[] entities)
        {
            _name = string.Empty;
            this.AddEntities(entities);
        }

        public EntityWorld(in string name = null, params IEntity[] entities)
        {
            _name = name;
            this.AddEntities(entities);
        }

        public EntityWorld(in string name, IEnumerable<IEntity> entities)
        {
            _name = name;
            this.AddEntities(entities);
        }

        public bool Has(in IEntity entity)
        {
            return _entities.ContainsKey(entity.Id);
        }

        public IEntity[] GetAll()
        {
            IEntity[] result = new IEntity[_entities.Count];
            this.GetAll(result);
            return result;
        }

        public int GetAll(in IEntity[] results)
        {
            _entities.Values.CopyTo(results, 0);
            return _entities.Count;
        }

        public void CopyTo(ICollection<IEntity> results)
        {
            results.Clear();
            foreach (IEntity entity in _entities.Values) 
                results.Add(entity);
        }

        public bool Add(in IEntity entity)
        {
            if (!_entities.TryAdd(entity.Id, entity))
                return false;

            _entityUpdater.Add(entity);

            this.AddTags(entity);
            this.AddValues(entity);
            this.Subscribe(entity);

            this.OnStateChanged?.Invoke();
            this.OnAdded?.Invoke(entity);
            return true;
        }

        public bool Del(in IEntity entity)
        {
            if (!_entities.Remove(entity.Id))
                return false;

            _entityUpdater.Del(entity);

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
            _entityUpdater.Clear();

            foreach (IEntity entity in _cache)
            {
                this.Unsubscribe(in entity);
                this.OnDeleted?.Invoke(entity);
            }

            this.OnStateChanged?.Invoke();
        }

        private void Subscribe(in IEntity entity)
        {
            entity.OnTagAdded += this.OnTagAdded;
            entity.OnTagDeleted += this.OnTagRemoved;

            entity.OnValueAdded += this.OnValueAdded;
            entity.OnValueDeleted += this.OnValueRemoved;
        }

        private void Unsubscribe(in IEntity entity)
        {
            entity.OnTagAdded -= this.OnTagAdded;
            entity.OnTagDeleted -= this.OnTagRemoved;

            entity.OnValueAdded -= this.OnValueAdded;
            entity.OnValueDeleted -= this.OnValueRemoved;
        }

        public IEnumerator<IEntity> GetEnumerator() => _entities.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}