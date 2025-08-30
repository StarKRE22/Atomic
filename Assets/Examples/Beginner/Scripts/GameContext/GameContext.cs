using Atomic.Entities;

namespace BeginnerGame
{
    public interface IGameContext : IEntity
    {
    }

    public sealed class GameContext : SceneEntitySingleton<GameContext>, IGameContext
    {
    }
}