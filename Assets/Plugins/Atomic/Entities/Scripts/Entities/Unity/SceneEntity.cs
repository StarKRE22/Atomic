#if UNITY_5_3_OR_NEWER
using System;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a MonoBehaviour implementation for an <see cref="IEntity"/> that can be installed from the Unity Scene.
    /// Allows composition through Unity Inspector and automated setup via installers and child entities.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Entities/SceneEntity.md")]
    public partial class SceneEntity : MonoBehaviour, IEntity
    {
        private const int UNDEFINED_INDEX = -1;

        /// <inheritdoc cref="IEntity.OnStateChanged"/>
        public event Action<IEntity> OnStateChanged;

        /// <inheritdoc cref="IEntity.InstanceID" />
        public int InstanceID
        {
            get => _instanceId;
            internal set => _instanceId = value;
        }

        /// <inheritdoc />
        int IEntity.InstanceID
        {
            get => _instanceId;
            set => _instanceId = value;
        }

        /// <inheritdoc />
        public string Name
        {
            get => this.name;
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnStateChanged?.Invoke(this);
                }
            }
        }
        
        /// <inheritdoc/>
        /// <summary>
        /// Gets a value indicating whether this entity is valid within the Unity scene context.
        /// </summary>
        /// <remarks>
        /// Returns <see langword="true"/> if the underlying <see cref="GameObject"/> is not destroyed 
        /// and the entity has a valid assigned <see cref="InstanceID"/>.
        /// </remarks>
        /// <value>
        /// <see langword="true"/> if the entity is valid; otherwise, <see langword="false"/>.
        /// </value>
        public bool IsValid => this != null && _instanceId > 0;

        internal int _instanceId;
        
        /// <inheritdoc/>
        public override string ToString() => $"{nameof(name)}: {name}, {nameof(_instanceId)}: {_instanceId}";

        // ReSharper disable once UnusedMember.Global
        public bool Equals(IEntity other) => other != null && _instanceId == other.InstanceID;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity other && other.InstanceID == _instanceId;

        /// <inheritdoc/>
        public override int GetHashCode() => _instanceId;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Construct()
        {
            this.ConstructTags();
            this.ConstructValues();
            this.ConstructBehaviours();
        }
    }
}
#endif