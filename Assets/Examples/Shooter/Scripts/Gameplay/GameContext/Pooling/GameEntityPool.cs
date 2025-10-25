using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class GameEntityPool : SceneEntityPool<Actor>, IEntityPool<IActor>
    {
        IActor IEntityPool<IActor>.Rent() => this.Rent();

        void IEntityPool<IActor>.Return(IActor entity) => this.Return((Actor) entity);
    }
}