using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public abstract class GameEntityBaker : SceneEntityBaker<IGameEntity>
    {
        [SerializeField]
        private GameEntityFactory _factory;

        public sealed override IGameEntity Create()
        {
            IGameEntity entity = _factory.Create();
            this.Install(entity);
            Destroy(this.gameObject);
            return entity;
        }

        protected abstract void Install(IGameEntity entity);
    }
}