using Atomic.Entities;

namespace BeginnerGame
{
    public interface IGameEntity : IEntity
    {
    }
    
    public sealed class GameEntity : SceneEntity, IGameEntity
    {
    }
}