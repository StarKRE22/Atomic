using Atomic.Entities;

namespace SampleGame
{
    public interface IUIContext : IEntity
    {
    }

    public sealed class UIContext : SceneEntitySingleton<UIContext>, IUIContext
    {
    }
}