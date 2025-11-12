using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame.Gameplay
{
    /// <summary>
    /// A Unity-integrated factory for creating scene-based <see cref="Actor"/> entities.
    /// </summary>
    /// <remarks>
    /// Derive from this class to create runtime factories that instantiate <see cref="Actor"/> entities directly within the current scene. It integrates with Unity’s object lifecycle and can be attached to GameObjects.
    /// </remarks>
    /// <example>
    /// Attach this factory to a GameObject to dynamically create <see cref="Actor"/> entities at runtime.
    /// </example>
    /// <remarks>
    /// Created automatically by <b>Entity Domain Generator</b>.
    /// </remarks>
    public abstract class SceneActorFactory : SceneEntityFactory<IActor>
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
