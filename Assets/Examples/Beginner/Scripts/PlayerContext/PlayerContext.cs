using Atomic.Entities;

namespace BeginnerGame
{
    public interface IPlayerContext : IEntity
    {
    }

    public sealed class PlayerContext : SceneEntity, IPlayerContext
    {
    }
}