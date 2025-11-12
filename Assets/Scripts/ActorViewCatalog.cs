using Atomic.Entities;
using UnityEngine;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame.Gameplay
{
    /// <summary>
    /// A Unity <see cref="ScriptableObject"/> catalog that maps entity identifiers to their corresponding view prefabs.
    /// </summary>
    /// <remarks>
    /// This catalog defines the available views for <see cref="IActor"/> entities.
    /// It is used by runtime view pools and factories to spawn and manage the correct prefab instances.
    /// </remarks>
    /// <remarks>
    /// Created automatically by <b>Entity Domain Generator</b>.
    /// </remarks>
    [CreateAssetMenu(
        fileName = "ActorViewCatalog",
        menuName = "SampleGame/Gameplay/New ActorViewCatalog"
    )]
    public sealed class ActorViewCatalog : EntityViewCatalog<IActor, ActorView>
    {
    }
}
