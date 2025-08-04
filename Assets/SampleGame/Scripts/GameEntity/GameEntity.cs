using Atomic.Entities;

namespace SampleGame
{
    public interface IGameEntity : IEntity
    {
    }
    
    public sealed class GameEntity : SceneEntity, IGameEntity
    {
    }
}