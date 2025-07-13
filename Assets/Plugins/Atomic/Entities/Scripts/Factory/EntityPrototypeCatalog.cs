using System.Collections.Generic;
using System.Linq;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "EntityPrototypeCatalog",
        menuName = "Atomic/Entities/New EntityPrototypeCatalog"
    )]
    public sealed class EntityPrototypeCatalog : ScriptableObject
    {
        [SerializeField]
        private EntityPrototype[] _entities;

        public IEnumerable<KeyValuePair<string, IEntityFactory>> GetEntities()
        {
            return _entities.Select(it => new KeyValuePair<string, IEntityFactory>(it.name, it));
        }
    }
}