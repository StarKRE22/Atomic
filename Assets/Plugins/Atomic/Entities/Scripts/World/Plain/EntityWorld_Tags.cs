using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial class EntityWorld
    {
        private readonly Dictionary<int, List<IEntity>> _tags = new();

        public bool GetWithTag(in int tag, out IEntity result)
        {
            result = null;
            if (!_tags.TryGetValue(tag, out List<IEntity> entities))
                return false;

            if (entities.Count == 0)
                return false;

            result = entities[0]; 
            return true;
        }

        public IReadOnlyList<IEntity> GetAllWithTag(in int tag)
        {
            if (_tags.TryGetValue(tag, out List<IEntity> entities)) 
                return entities;
            
            entities = new List<IEntity>();
            _tags.Add(tag, entities);
            return entities;
        }

        public int GetAllWithTag(in int tag, in IEntity[] results)
        {
            if (!_tags.TryGetValue(tag, out List<IEntity> entities))
                return 0;

            int count = entities.Count;
            for (int i = 0; i < count; i++) 
                results[i] = entities[i];

            return count;
        }

        private void AddTags(in IEntity entity)
        {
            using IEnumerator<int> tags = entity.TagEnumerator();
            while (tags.MoveNext())
            {
                int tag = tags.Current;
                if (!_tags.TryGetValue(tag, out List<IEntity> entities))
                {
                    entities = new List<IEntity>();
                    _tags.Add(tag, entities);
                }

                entities.Add(entity);
            }
        }

        private void RemoveTags(IEntity entity)
        {
            using IEnumerator<int> tags = entity.TagEnumerator();
            while (tags.MoveNext())
            {
                int tag = tags.Current;
                if (_tags.TryGetValue(tag, out List<IEntity> entities))
                    entities.Remove(entity);
            }
        }

        private void OnTagAdded(IEntity entity, int tag)
        {
            if (!_tags.TryGetValue(tag, out List<IEntity> entities))
            {
                entities = new List<IEntity>();
                _tags.Add(tag, entities);
            }

            entities.Add(entity);
            this.OnStateChanged?.Invoke();
        }

        private void OnTagRemoved(IEntity obj, int tag)
        {
            if (_tags.TryGetValue(tag, out List<IEntity> entities))
            {
                entities.Remove(obj);
                this.OnStateChanged?.Invoke();
            }
        }
    }
}