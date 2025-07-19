using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class World<E>
    {
        private readonly Dictionary<int, List<E>> _tags = new();

        public bool GetWithTag(int tag, out E result)
        {
            result = default;
            if (!_tags.TryGetValue(tag, out List<E> entities))
                return false;

            if (entities.Count == 0)
                return false;

            result = entities[0]; 
            return true;
        }

        public IReadOnlyList<E> GetAllWithTag(int tag)
        {
            if (_tags.TryGetValue(tag, out List<E> entities)) 
                return entities;
            
            entities = new List<E>();
            _tags.Add(tag, entities);
            return entities;
        }

        public int GetAllWithTag(int tag, E[] results)
        {
            if (!_tags.TryGetValue(tag, out List<E> entities))
                return 0;

            int count = entities.Count;
            for (int i = 0; i < count; i++) 
                results[i] = entities[i];

            return count;
        }

        private void AddTags(E entity)
        {
            using IEnumerator<int> tags = entity.GetTagEnumerator();
            while (tags.MoveNext())
            {
                int tag = tags.Current;
                if (!_tags.TryGetValue(tag, out List<E> entities))
                {
                    entities = new List<E>();
                    _tags.Add(tag, entities);
                }

                entities.Add(entity);
            }
        }

        private void RemoveTags(E entity)
        {
            using IEnumerator<int> tags = entity.GetTagEnumerator();
            while (tags.MoveNext())
            {
                int tag = tags.Current;
                if (_tags.TryGetValue(tag, out List<E> entities))
                    entities.Remove(entity);
            }
        }

        private void OnTagAdded(E entity, int tag)
        {
            if (!_tags.TryGetValue(tag, out List<E> entities))
            {
                entities = new List<E>();
                _tags.Add(tag, entities);
            }

            entities.Add(entity);
            this.OnStateChanged?.Invoke();
        }

        private void OnTagRemoved(E obj, int tag)
        {
            if (_tags.TryGetValue(tag, out List<E> entities))
            {
                entities.Remove(obj);
                this.OnStateChanged?.Invoke();
            }
        }
    }
}