using Atomic.Entities;
using UnityEngine;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame.Gameplay
{
    /// <summary>
    /// Converts a scene GameObject into a C# <see cref="IActor"/> entity.
    /// </summary>
    /// <remarks>
    /// Derive from this class to define baking logic for <see cref="Actor"/> entities in the scene.
    /// This version provides standard conversion hooks without additional optimizations.
    /// </remarks>
    /// <remarks>
    /// Created automatically by <b>Entity Domain Generator</b>.
    /// </remarks>
    public abstract class ActorBaker : SceneEntityBaker<IActor>
    {
    }
}
