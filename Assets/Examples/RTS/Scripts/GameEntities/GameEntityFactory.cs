using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public abstract class GameEntityFactory : ScriptableEntityFactory<IGameEntity, Args<IGameContext>>
    {
        public GameEntityType Type => _type;
        
        [SerializeField]
        private Const<GameEntityType> _type;
        
        protected sealed override IGameEntity Create(int tagCapacity,
            int valueCapacity,
            int behaviourCapacity,
            Entity.Settings settings,
            Args<IGameContext> gameContext)
        {
            GameEntity entity = new GameEntity(
                this.name,
                tagCapacity,
                valueCapacity,
                behaviourCapacity
            );
            entity.AddEntityType(_type);
            this.Install(entity, gameContext.value);
            return entity;
        }

        protected abstract void Install(IGameEntity entity, IGameContext gameContext);
    }
}