using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame
{
    /// <summary>
    /// Represents a Unity <see cref="SceneEntity"/> implementation for <see cref="IActor"/>.
    /// This component can be instantiated directly in a Scene and composed via the Unity Inspector.
    /// </summary>
    public sealed class Actor : SceneEntity, IActor
    {
    }
}
