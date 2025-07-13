using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class EntityWorld
    {
        private readonly Dictionary<int, List<IEntity>> _values = new();

        public bool GetWithValue(in int valueKey, out IEntity result)
        {
            result = null;
            if (!_values.TryGetValue(valueKey, out List<IEntity> entities))
                return false;

            if (entities.Count == 0)
                return false;

            result = entities[0];
            return true;
        }

        public IReadOnlyList<IEntity> GetAllWithValue(in int valueKey)
        {
            if (!_values.TryGetValue(valueKey, out List<IEntity> entities))
            {
                entities = new List<IEntity>();
                _values.Add(valueKey, entities);
                return entities;
            }

            return entities;
        }

        public int GetAllWithValue(in int valueKey, in IEntity[] results)
        {
            if (!_values.TryGetValue(valueKey, out List<IEntity> entities))
                return 0;

            int count = entities.Count;
            for (int i = 0; i < count; i++) results[i] = entities[i];

            return count;
        }

        private void AddValues(IEntity entity)
        {
            using IEnumerator<KeyValuePair<int, object>> values = entity.ValueEnumerator();
            while (values.MoveNext())
            {
                (int key, _) = values.Current;
                if (!_values.TryGetValue(key, out List<IEntity> entities))
                {
                    entities = new List<IEntity>();
                    _values.Add(key, entities);
                }

                entities.Add(entity);
            }
            
            this.OnStateChanged?.Invoke();
        }

        private void RemoveValues(IEntity entity)
        {
            using IEnumerator<KeyValuePair<int, object>> values = entity.ValueEnumerator();
            while (values.MoveNext())
            {
                (int key, _) = values.Current;
                if (_tags.TryGetValue(key, out List<IEntity> entities))
                    entities.Remove(entity);
            }
            
            this.OnStateChanged?.Invoke();
        }

        private void OnValueAdded(IEntity entity, int valueKey)
        {
            if (!_values.TryGetValue(valueKey, out List<IEntity> entities))
            {
                entities = new List<IEntity>();
                _values.Add(valueKey, entities);
            }

            entities.Add(entity);
            this.OnStateChanged?.Invoke();
        }

        private void OnValueRemoved(IEntity entity, int valueKey)
        {
            if (_values.TryGetValue(valueKey, out List<IEntity> entities))
            {
                entities.Remove(entity);
                this.OnStateChanged?.Invoke();
            }
        }
    }
}