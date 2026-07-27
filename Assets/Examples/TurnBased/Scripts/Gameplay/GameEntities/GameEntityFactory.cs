using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
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
            Args<IGameContext> args
        )
        {
            GameEntity entity = new GameEntity(
                _type.ToString(),
                tagCapacity,
                valueCapacity,
                behaviourCapacity
            );
            entity.AddEntityType( _type);
            this.Install(entity, args);
            return entity;
        }

        protected abstract void Install(IGameEntity entity, Args<IGameContext> args);
    }
}