using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class World<E>
    {
        private readonly Dictionary<int, List<E>> _values = new();

        public bool GetWithValue(int valueKey, out E result)
        {
            result = default;
            if (!_values.TryGetValue(valueKey, out List<E> entities))
                return false;

            if (entities.Count == 0)
                return false;

            result = entities[0];
            return true;
        }

        public IReadOnlyList<E> GetAllWithValue(int valueKey)
        {
            if (!_values.TryGetValue(valueKey, out List<E> entities))
            {
                entities = new List<E>();
                _values.Add(valueKey, entities);
                return entities;
            }

            return entities;
        }

        public int GetAllWithValue(int valueKey, E[] results)
        {
            if (!_values.TryGetValue(valueKey, out List<E> entities))
                return 0;

            int count = entities.Count;
            for (int i = 0; i < count; i++) results[i] = entities[i];

            return count;
        }

        private void AddValues(E entity)
        {
            using IEnumerator<KeyValuePair<int, object>> values = entity.GetValueEnumerator();
            while (values.MoveNext())
            {
                (int key, _) = values.Current;
                if (!_values.TryGetValue(key, out List<E> entities))
                {
                    entities = new List<E>();
                    _values.Add(key, entities);
                }

                entities.Add(entity);
            }
            
            this.OnStateChanged?.Invoke();
        }

        private void RemoveValues(E entity)
        {
            using IEnumerator<KeyValuePair<int, object>> values = entity.GetValueEnumerator();
            while (values.MoveNext())
            {
                (int key, _) = values.Current;
                if (_tags.TryGetValue(key, out List<E> entities))
                    entities.Remove(entity);
            }
            
            this.OnStateChanged?.Invoke();
        }

        private void OnValueAdded(E entity, int valueKey)
        {
            if (!_values.TryGetValue(valueKey, out List<E> entities))
            {
                entities = new List<E>();
                _values.Add(valueKey, entities);
            }

            entities.Add(entity);
            this.OnStateChanged?.Invoke();
        }

        private void OnValueRemoved(E entity, int valueKey)
        {
            if (_values.TryGetValue(valueKey, out List<E> entities))
            {
                entities.Remove(entity);
                this.OnStateChanged?.Invoke();
            }
        }
    }
}