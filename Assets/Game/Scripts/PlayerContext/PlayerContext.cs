using Atomic.Entities;

namespace SampleGame
{
    public interface IPlayerContext : IEntity
    {
    }

    public sealed class PlayerContext : SceneEntity, IPlayerContext
    {
    }
}