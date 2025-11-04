#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity-compatible world manager for scene-based entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The specific type of scene entity this world manages. Must inherit from <see cref="SceneEntity"/>.</typeparam>
    /// <remarks>
    /// This component integrates with Unity’s lifecycle events (Awake, Start, OnEnable, etc.) to automatically
    /// manage entity enabling, updating, and cleanup. It wraps a runtime <see cref="EntityWorld{E}"/> instance internally.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject in the scene to automatically scan and manage entities of type <typeparamref name="E"/>.
    /// </example>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Worlds/SceneEntityWorld%601.md")]
    public abstract partial class SceneEntityWorld<E> : MonoBehaviour, IEntityWorld<E> where E : SceneEntity
    {
        private readonly EntityWorld<E> _world = new();

        private bool isStarted;

        /// <inheritdoc />
        public event Action OnStateChanged
        {
            add => _world.OnStateChanged += value;
            remove => _world.OnStateChanged -= value;
        }

        /// <inheritdoc />
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }
    }
}
#endif