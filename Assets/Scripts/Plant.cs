using Atomic.Entities;

namespace SampleGame
{
    public sealed class Plant : SceneEntity, IPlant
    {
    }

    public interface IPlant : IEntity
    {
    }
}