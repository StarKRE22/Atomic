using Atomic.Entities;
using System.Collections.Generic;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame.Gameplay
{
    /// <summary>
    /// A base class for singleton scene entities. Ensures a single instance of the entity exists
    /// per scene or globally, depending on the <see cref="_dontDestroyOnLoad"/> flag.
    /// </summary>
    /// <typeparam name="E">The concrete type of the singleton scene entity.</typeparam>
    /// </summary>
    public sealed class Actor : SceneEntitySingleton<Actor>, IActor
    {
    }
}
