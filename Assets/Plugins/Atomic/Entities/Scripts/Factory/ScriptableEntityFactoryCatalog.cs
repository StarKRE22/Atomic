using System.Collections.Generic;
using System.Linq;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "EntityFactoryCatalog",
        menuName = "Atomic/Entities/New EntityFactoryCatalog"
    )]
    public sealed class ScriptableEntityFactoryCatalog : ScriptableObject
    {
        [SerializeField]
        private ScriptableEntityFactory[] _entities;

        public IEnumerable<KeyValuePair<string, IEntityFactory>> GetEntities()
        {
            return _entities.Select(it => new KeyValuePair<string, IEntityFactory>(it.name, it));
        }
    }
}