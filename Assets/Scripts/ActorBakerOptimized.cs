using Atomic.Entities;
using UnityEngine;

/**
 * Created by Entity Domain Generator.
 */

namespace SampleGame.Gameplay
{
    /// <summary>
    /// An optimized Unity baker for <see cref="Actor"/> entities that requires a matching <see cref="ActorView"/>.
    /// </summary>
    /// <remarks>
    /// Use this version when you need high-performance conversion of <see cref="Actor"/> entities, leveraging cached <see cref="EntityView{T}"/> components. Automatically enforces <see cref="RequireComponent"/> for <see cref="ActorView"/>.
    /// </remarks>
    /// <remarks>
    /// Created automatically by <b>Entity Domain Generator</b>.
    /// </remarks>
    [RequireComponent(typeof(ActorView))]
    public abstract class ActorBakerOptimized : SceneEntityBakerOptimized<IActor, ActorView>
    {
    }
}
