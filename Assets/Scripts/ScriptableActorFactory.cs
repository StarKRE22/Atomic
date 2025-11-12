using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame.Gameplay
{
    /// <summary>
    /// A reusable asset-based factory for creating <see cref="Actor"/> entities.
    /// </summary>
    /// <remarks>
    /// Derive from this class to define reusable, configurable factories for <see cref="Actor"/> entities that can be created from Unity’s asset system. Ideal for asset-driven design patterns using <see cref="ScriptableObject"/>.
    /// </remarks>
    /// <example>
    /// Create a new <see cref="ScriptableObject"/> instance in your project to define a reusable <see cref="Actor"/> factory.
    /// </example>
    /// <remarks>
    /// Created automatically by <b>Entity Domain Generator</b>.
    /// </remarks>
    public abstract class ScriptableActorFactory : ScriptableEntityFactory<IActor>
    {
        public string Name => this.name;

        public sealed override IActor Create()
        {
            var entity = new Actor(
                this.Name,
                this.initialTagCapacity,
                this.initialValueCapacity,
                this.initialBehaviourCapacity
            );
            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IActor entity);
    }
}
