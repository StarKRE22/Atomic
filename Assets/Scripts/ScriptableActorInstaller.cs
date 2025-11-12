using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame.Gameplay
{
    /// <summary>
    /// A Unity <see cref="ScriptableObject"/> that defines reusable logic for installing or configuring an <see cref="IActor"/>.
    /// </summary>
    /// <remarks>
    /// This is useful for defining shared configuration logic that can be applied to multiple entities, such as setting default values, tags, or attaching behaviors.
    /// Supports both runtime and edit-time contexts via utility methods.
    /// </remarks>
    public abstract class ScriptableActorInstaller : ScriptableEntityInstaller<IActor>
    {
    }
}
