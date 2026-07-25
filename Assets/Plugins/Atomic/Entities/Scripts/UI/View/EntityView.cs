#if UNITY_5_3_OR_NEWER
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
        [field: SerializeField]
        public string Name { get; private set; }
    }
}
#endif