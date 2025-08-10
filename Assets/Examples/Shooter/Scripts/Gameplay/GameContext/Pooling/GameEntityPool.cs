using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class GameEntityPool : SceneEntityPool<GameEntity>, IEntityPool<IGameEntity>
    {
        IGameEntity IEntityPool<IGameEntity>.Rent() => this.Rent();

        void IEntityPool<IGameEntity>.Return(IGameEntity entity) => this.Return((GameEntity) entity);
    }
}