using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class GameEntityView : EntityView<IGameEntity>
    {
        public GameEntityType Type => _type;
        
        [SerializeField]
        private GameEntityType _type;
    }
}