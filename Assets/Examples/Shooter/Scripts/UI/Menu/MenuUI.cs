using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace ShooterGame.UI
{
    /// <summary>
    /// A base class for singleton scene entities. Ensures a single instance of the entity exists
    /// per scene or globally, depending on the <see cref="_dontDestroyOnLoad"/> flag.
    /// </summary>
    public sealed class MenuUI : SceneEntitySingleton<MenuUI>, IMenuUI
    {
    }
}
