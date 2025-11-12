using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame.Gameplay
{
    /// <summary>
    /// Represents a scriptable-based entity aspect that can be applied or discarded on a specific entity type.
    /// </summary>
    /// <remarks>
    /// Inherit from this class to create reusable <see cref="ScriptableObject"/> assets that encapsulate logic to apply and discard behaviors or properties on <see cref="Actor"/> entities during runtime. Create and configure instances via the Unity project assets.
    /// </remarks>
    /// <remarks>
    /// Created automatically by <b>Entity Domain Generator</b>.
    /// </remarks>
    public abstract class ScriptableActorAspect : ScriptableEntityAspect<IActor>
    {
    }
}
