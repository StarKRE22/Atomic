using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class GameEntityPool : SceneEntityPool<WorldEntity>, IEntityPool<IWorldEntity>
    {
        IWorldEntity IEntityPool<IWorldEntity>.Rent() => this.Rent();

        void IEntityPool<IWorldEntity>.Return(IWorldEntity entity) => this.Return((WorldEntity) entity);
    }
}