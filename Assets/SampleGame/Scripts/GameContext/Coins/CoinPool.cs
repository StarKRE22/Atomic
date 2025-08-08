using Atomic.Entities;

namespace SampleGame
{
    public sealed class CoinPool : SceneEntityPool<GameEntity>, IEntityPool<IGameEntity>
    {
        IGameEntity IEntityPool<IGameEntity>.Rent() => this.Rent();

        void IEntityPool<IGameEntity>.Return(IGameEntity entity) => this.Return((GameEntity) entity);
    }
}