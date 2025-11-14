using Atomic.Entities;
using System.Collections.Generic;

/**
 * Created by Entity Domain Generator.
 */

namespace ShooterGame.Gameplay
{
    /// <summary>
    /// Represents a Unity <see cref="SceneEntity"/> implementation for <see cref="IPlayerContext"/>.
    /// This component can be instantiated directly in a Scene and composed via the Unity Inspector.
    /// </summary>
    public sealed class PlayerContext : SceneEntity, IPlayerContext
    {
    }
}
