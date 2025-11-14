using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace ShooterGame.Gameplay
{
    /// <summary>
    /// A base class for singleton scene entities. Ensures a single instance of the entity exists
    /// per scene or globally, depending on the <see cref="_dontDestroyOnLoad"/> flag.
    /// </summary>
    /// <typeparam name="E">The concrete type of the singleton scene entity.</typeparam>
    /// </summary>
    public sealed class GameContext : SceneEntitySingleton<GameContext>, IGameContext
    {
    }
}
