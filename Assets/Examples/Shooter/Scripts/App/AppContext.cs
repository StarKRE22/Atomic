using Atomic.Entities;
using System.Collections.Generic;

/**
 * Created by Entity Domain Generator.
 */

namespace ShooterGame.App
{
    /// <summary>
    /// A base class for singleton scene entities. Ensures a single instance of the entity exists
    /// per scene or globally, depending on the <see cref="_dontDestroyOnLoad"/> flag.
    /// </summary>
    public sealed class AppContext : SceneEntitySingleton<AppContext>, IAppContext
    {
    }
}
