// using Atomic.Entities;
//
// /**
//  * Created by Entity Domain Generator.
//  */
//
// namespace SampleGame.Gameplay
// {
//     /// <summary>
//     /// A Unity <see cref="MonoBehaviour"/> proxy that forwards all <see cref="IActor"/> calls to an underlying <see cref="Actor"/> entity.
//     /// </summary>
//     /// <remarks>
//     /// This proxy allows interacting with an <see cref="IActor"/> instance inside the Unity scene while decoupling logic from GameObjects.
//     /// It acts as a transparent forwarder for all entity functionality — values, lifecycle, tags, and behaviours.
//     ///
//     /// Use this component to expose scene-level access to an entity while keeping logic modular and testable.
//     ///
//     /// **Collider Interaction Note**:
//     /// If your entity includes multiple colliders (e.g., hitboxes or triggers),
//     /// place <c>ActorProxy</c> on each and reference the same source <see cref="Actor"/>.
//     /// This provides unified access regardless of which collider was hit.
//     ///
//     /// <example>
//     /// Example: Detecting hits from any collider on an <see cref="IActor"/> entity:
//     /// <code>
//     /// void OnTriggerEnter(Collider other)
//     /// {
//     ///     if (other.TryGetComponent(out IActor proxy))
//     ///     {
//     ///         Debug.Log($"Hit entity: Actor");
//     ///     }
//     /// }
//     /// </code>
//     /// </example>
//     /// </remarks>
//     public sealed class ActorProxy : SceneEntityProxy<Actor>, IActor
//     {
//     }
// }
