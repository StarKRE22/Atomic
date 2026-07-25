using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public class GameEntityView : EntityView<IGameEntity>
    {
        public GameEntityType Type => _type;
        
        [SerializeField]
        private GameEntityType _type;
    }
}