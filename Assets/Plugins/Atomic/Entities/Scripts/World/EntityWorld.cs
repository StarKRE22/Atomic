using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public sealed class EntityWorld : IEntityWorld
    {
        #region Main

        public event Action OnStateChanged;

        public event Action<IEntity> OnEntityAdded;
        public event Action<IEntity> OnEntityDeleted;

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public IReadOnlyList<IEntity> Entities => this.entityList;
        public int EntityCount => this.entityList.Count;

        private readonly Dictionary<int, IEntity> entityMap = new();
        private readonly List<IEntity> entityList = new();

        private string name;

        public EntityWorld()
        {
            this.name = string.Empty;
        }

        public EntityWorld(params IEntity[] entities)
        {
            this.name = string.Empty;
            this.AddEntities(entities);
        }

        public EntityWorld(string name = null, params IEntity[] entities)
        {
            this.name = name;
            this.AddEntities(entities);
        }

        public EntityWorld(string name, IEnumerable<IEntity> entities)
        {
            this.name = name;
            this.AddEntities(entities);
        }

        public bool HasEntity(IEntity entity)
        {
            return this.entityMap.ContainsKey(entity.InstanceId);
        }

        public int CopyEntitiesTo(IEntity[] results)
        {
            int count = this.entityList.Count;

            for (int i = 0; i < count; i++)
            {
                results[i] = this.entityList[i];
            }

            return count;
        }

        public bool AddEntity(IEntity entity)
        {
            if (!this.entityMap.TryAdd(entity.InstanceId, entity))
            {
                return false;
            }

            this.AddTags(entity);
            this.AddValues(entity);
            this.Subscribe(entity);

            this.entityList.Add(entity);

            this.OnStateChanged?.Invoke();
            this.OnEntityAdded?.Invoke(entity);
            return true;
        }

        public bool DelEntity(IEntity entity)
        {
            if (!this.entityMap.Remove(entity.InstanceId, out entity))
            {
                return false;
            }

            this.Unsubscribe(entity);
            this.DelTags(entity);
            this.DelValues(entity);
            this.entityList.Remove(entity);

            this.OnStateChanged?.Invoke();
            this.OnEntityDeleted?.Invoke(entity);
            return true;
        }

        public void ClearEntities()
        {
            if (this.entityMap.Count == 0)
            {
                return;
            }

            foreach (IEntity entity in this.entityMap.Values)
            {
                this.Unsubscribe(entity);
                this.OnEntityDeleted?.Invoke(entity);
            }

            this.entityList.Clear();
            this.entityMap.Clear();
            this.tags.Clear();
            this.values.Clear();

            this.OnStateChanged?.Invoke();
        }

        private void Subscribe(IEntity entity)
        {
            entity.OnTagAdded += this.OnAddTag;
            entity.OnTagDeleted += this.OnRemoveTag;
            entity.OnTagsCleared += this.DelTags;

            entity.OnValueAdded += this.OnAddValue;
            entity.OnValueDeleted += this.OnRemoveValue;
            entity.OnValuesCleared += this.DelValues;
        }

        private void Unsubscribe(IEntity entity)
        {
            entity.OnTagAdded -= this.OnAddTag;
            entity.OnTagDeleted -= this.OnRemoveTag;
            entity.OnTagsCleared -= this.DelTags;

            entity.OnValueAdded -= this.OnAddValue;
            entity.OnValueDeleted -= this.OnRemoveValue;
            entity.OnValuesCleared -= this.DelValues;
        }

        #endregion

        #region Tags

        private readonly Dictionary<int, List<IEntity>> tags = new();

        public IEntity GetEntityWithTag(int tag)
        {
            if (!this.tags.TryGetValue(tag, out List<IEntity> entities))
            {
                return null;
            }

            if (entities.Count == 0)
            {
                return null;
            }

            return entities[0];
        }

        public IReadOnlyList<IEntity> GetEntitiesWithTag(int tag)
        {
            if (!this.tags.TryGetValue(tag, out List<IEntity> entities))
            {
                entities = new List<IEntity>();
                this.tags.Add(tag, entities);
                return entities;
            }

            return entities;
        }

        public int GetEntitiesWithTag(int tag, IEntity[] results)
        {
            if (!this.tags.TryGetValue(tag, out List<IEntity> entities))
            {
                return 0;
            }

            int count = entities.Count;
            for (int i = 0; i < count; i++)
            {
                results[i] = entities[i];
            }

            return count;
        }

        private void AddTags(IEntity entity)
        {
            foreach (int tag in entity.Tags)
            {
                if (!this.tags.TryGetValue(tag, out List<IEntity> entities))
                {
                    entities = new List<IEntity>();
                    this.tags.Add(tag, entities);
                }

                entities.Add(entity);
            }
        }

        private void DelTags(IEntity entity)
        {
            foreach (int tag in entity.Tags)
            {
                if (this.tags.TryGetValue(tag, out List<IEntity> entities))
                {
                    entities.Remove(entity);
                }
            }
        }

        private void OnAddTag(IEntity entity, int tag)
        {
            if (!this.tags.TryGetValue(tag, out List<IEntity> entities))
            {
                entities = new List<IEntity>();
                this.tags.Add(tag, entities);
            }

            entities.Add(entity);
            this.OnStateChanged?.Invoke();
        }

        private void OnRemoveTag(IEntity obj, int tag)
        {
            if (this.tags.TryGetValue(tag, out List<IEntity> entities))
            {
                entities.Remove(obj);
                this.OnStateChanged?.Invoke();
            }
        }

        #endregion

        #region Values

        private readonly Dictionary<int, List<IEntity>> values = new();

        public IEntity GetEntityWithValue(int valueId)
        {
            if (!this.values.TryGetValue(valueId, out List<IEntity> entities))
            {
                return null;
            }

            if (entities.Count == 0)
            {
                return null;
            }

            return entities[0];
        }

        public IReadOnlyList<IEntity> GetEntitiesWithValue(int valueId)
        {
            if (!this.values.TryGetValue(valueId, out List<IEntity> entities))
            {
                entities = new List<IEntity>();
                this.values.Add(valueId, entities);
                return entities;
            }

            return entities;
        }

        public int GetEntitiesWithValue(int valueId, IEntity[] results)
        {
            if (!this.values.TryGetValue(valueId, out List<IEntity> entities))
            {
                return 0;
            }

            int count = entities.Count;
            for (int i = 0; i < count; i++)
            {
                results[i] = entities[i];
            }

            return count;
        }

        private void AddValues(IEntity entity)
        {
            foreach (int key in entity.Values.Keys)
            {
                if (!this.values.TryGetValue(key, out List<IEntity> entities))
                {
                    entities = new List<IEntity>();
                    this.values.Add(key, entities);
                }

                entities.Add(entity);
            }

            this.OnStateChanged?.Invoke();
        }

        private void DelValues(IEntity entity)
        {
            foreach (int key in entity.Values.Keys)
            {
                if (this.tags.TryGetValue(key, out List<IEntity> entities))
                {
                    entities.Remove(entity);
                }
            }

            this.OnStateChanged?.Invoke();
        }

        private void OnAddValue(IEntity entity, int valueKey, object _)
        {
            if (!this.values.TryGetValue(valueKey, out List<IEntity> entities))
            {
                entities = new List<IEntity>();
                this.values.Add(valueKey, entities);
            }

            entities.Add(entity);
            this.OnStateChanged?.Invoke();
        }

        private void OnRemoveValue(IEntity entity, int valueKey, object _)
        {
            if (this.values.TryGetValue(valueKey, out List<IEntity> entities))
            {
                entities.Remove(entity);
                this.OnStateChanged?.Invoke();
            }
        }

        #endregion

        #region Lifecycle

        private readonly List<IEntity> _cache = new();

        public void InitEntities()
        {
            int count = this.entityList.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entityList);

            for (int i = 0; i < count; i++)
            {
                _cache[i].Init();
            }
        }

        public void EnableEntities()
        {
            int count = this.entityList.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entityList);

            for (int i = 0; i < count; i++)
            {
                _cache[i].Enable();
            }
        }

        public void DisableEntities()
        {
            int count = this.entityList.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entityList);

            for (int i = 0; i < count; i++)
            {
                _cache[i].Disable();
            }
        }

        public void DisposeEntities()
        {
            int count = this.entityList.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entityList);

            for (int i = 0; i < count; i++)
            {
                _cache[i].Dispose();
            }
        }

        public void UpdateEntities(float deltaTime)
        {
            int count = this.entityList.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entityList);

            for (int i = 0; i < count; i++)
            {
                _cache[i].OnUpdate(deltaTime);
            }
        }

        public void FixedUpdateEntities(float deltaTime)
        {
            int count = this.entityList.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entityList);

            for (int i = 0; i < count; i++)
            {
                _cache[i].OnFixedUpdate(deltaTime);
            }
        }

        public void LateUpdateEntities(float deltaTime)
        {
            int count = this.entityList.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entityList);

            for (int i = 0; i < count; i++)
            {
                _cache[i].OnLateUpdate(deltaTime);
            }
        }

        #endregion
    }
}