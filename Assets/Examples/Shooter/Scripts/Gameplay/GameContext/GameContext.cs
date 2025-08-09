using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public interface IGameContext : IEntity
    {
    }

    public sealed class GameContext : SceneEntitySingleton<GameContext>, IGameContext
    {
    }
}