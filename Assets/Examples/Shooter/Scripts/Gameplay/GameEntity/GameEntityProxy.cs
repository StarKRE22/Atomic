using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace ShooterGame.Gameplay
{
    /// <summary>
    /// A Unity <see cref="MonoBehaviour"/> proxy that forwards all <see cref="IGameEntity"/> calls to an underlying <see cref="GameEntity"/> entity.
    /// </summary>
    /// <remarks>
    /// This proxy allows interacting with an <see cref="IGameEntity"/> instance inside the Unity scene while decoupling logic from GameObjects.
    /// It acts as a transparent forwarder for all entity functionality — values, lifecycle, tags, and behaviours.
    ///
    /// Use this component to expose scene-level access to an entity while keeping logic modular and testable.
    ///
    /// **Collider Interaction Note**:
    /// If your entity includes multiple colliders (e.g., hitboxes or triggers),
    /// place <c>GameEntityProxy</c> on each and reference the same source <see cref="GameEntity"/>.
    /// This provides unified access regardless of which collider was hit.
    ///
    /// <example>
    /// Example: Detecting hits from any collider on an <see cref="IGameEntity"/> entity:
    /// <code>
    /// void OnTriggerEnter(Collider other)
    /// {
    ///     if (other.TryGetComponent(out IGameEntity proxy))
    ///     {
    ///         Debug.Log($"Hit entity: GameEntity");
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    public sealed class GameEntityProxy : SceneEntityProxy<GameEntity>, IGameEntity
    {
    }
}
