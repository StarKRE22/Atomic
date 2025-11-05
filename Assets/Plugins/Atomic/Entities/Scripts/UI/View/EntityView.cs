#if UNITY_5_3_OR_NEWER
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Default entity view component.
    /// </summary>
    /// <remarks>
    /// This is a non-generic wrapper around <see cref="EntityView{E}"/> fixed to <see cref="IEntity"/>.
    /// Useful when the specific entity type is unknown or irrelevant.
    /// </remarks>
    [AddComponentMenu("Atomic/Entities/Entity View")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/UI/EntityView.md")]
    public class EntityView : EntityView<IEntity>
    {
        /// <summary>
        /// Creates a new <see cref="EntityView"/> GameObject and sets up its installers.
        /// </summary>
        /// <param name="args">The creation arguments.</param>
        /// <returns>The created <see cref="EntityView"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntityView Create(in CreateArgs args = default) => Create<EntityView>(args);
    }
}
#endif