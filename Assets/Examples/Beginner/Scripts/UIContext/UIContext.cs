using Atomic.Entities;

namespace BeginnerGame
{
    public interface IUIContext : IEntity
    {
    }

    public sealed class UIContext : SceneEntitySingleton<UIContext>, IUIContext
    {
    }
}