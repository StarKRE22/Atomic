using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame
{
    /// <summary>
    /// Represents a scriptable-based entity aspect that can be applied or discarded on a specific entity type.
    /// </summary>
    /// <remarks>
    /// Inherit from this class to create reusable <see cref="MonoBehaviour"/> components that encapsulate
    /// logic to apply and discard behaviors or properties on <see cref="Actor"/> entities during runtime.
    /// Attach this component to a GameObject in your scene to use it.
    /// </remarks>
    public abstract class ScriptableActorAspect : ScriptableEntityAspect<Actor>
    {
    }
}
