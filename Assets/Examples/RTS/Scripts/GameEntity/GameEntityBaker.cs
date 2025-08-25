using System;
using Atomic.Entities;

namespace RTSGame
{
    public abstract class GameEntityBaker : SceneEntityBaker<IGameEntity>
    {
        public string Name => this.name;

        protected sealed override IGameEntity Bake()
        {
            var entity = new GameEntity(
                this.Name,
                this.InitialTagCount,
                this.InitialValueCount,
                this.InitialBehaviourCount
            );
            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IGameEntity entity);
    }
}