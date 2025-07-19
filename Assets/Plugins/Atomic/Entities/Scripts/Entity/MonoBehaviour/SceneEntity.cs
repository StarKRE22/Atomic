using System;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// MonoBehaviour wrapper for an <see cref="Entity"/> that can be installed from the Unity Scene.
    /// Allows composition through Unity Inspector and automated setup via installers and child entities.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public abstract partial class SceneEntity<E> : MonoBehaviour, IEntity<E>, ISerializationCallbackReceiver 
        where E : class, IEntity<E>
    {
        private const int UNDEFINED_INDEX = -1;

        /// <inheritdoc cref="IEntity.OnStateChanged"/>
        public event Action OnStateChanged;

        public int InstanceID => this.instanceId;

        private int instanceId;

        /// <summary>
        /// Indicates whether this entity has already been Constructed.
        /// </summary>
        public bool Installed => _installed;

        private bool _installed;

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [HideInPlayMode]
#endif
        [Tooltip("If this option is enabled, the Install() method will be called on Awake()")]
        [SerializeField]
        private bool installOnAwake = true;

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0)]
        [GUIColor(1f, 0.92156863f, 0.015686275f)]
        [HideInPlayMode]
        [InfoBox(
            "WARINING: If you create Unity objects or another heavy objects in the Install() method, be sure to turn off!",
            InfoMessageType.Warning,
            nameof(installInEditMode))
        ]
#endif
        [Tooltip(
            "If this option is enabled, the Install() method will be called every time OnValidate is called in Edit Mode")]
        [SerializeField]
        private bool installInEditMode;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Tooltip("Should dispose values when OnDestroy() called")]
        [SerializeField]
        private bool disposeValues = true;

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
#endif

#if ODIN_INSPECTOR
        [HideInPlayMode, SceneObjectsOnly]
#endif
        [Tooltip("Specify the installers that will put values and systems to this context")]
        [Space, SerializeField]
        private List<SceneEntityInstaller<E>> installers;

#if ODIN_INSPECTOR
        [HideInPlayMode, SceneObjectsOnly]
#endif
        [Tooltip("Specify child entities that will installed with this entity")]
        [Space, SerializeField]
        private List<SceneEntity<E>> children;

        /// <summary>
        /// Installs all configured installers and child entities into this SceneEntity.
        /// </summary>
        public void Install()
        {
            if (_installed)
                return;

            _installed = true;

            E entity = this as E;
            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Count; i < count; i++)
                {
                    SceneEntityInstaller<E> installer = this.installers[i];
                    if (installer != null)
                        installer.Install(entity);
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null installer!", this);
                }
            }

            if (this.children != null)
            {
                for (int i = 0, count = this.children.Count; i < count; i++)
                {
                    SceneEntity<E> child = this.children[i];
                    if (child != null)
                        child.Install();
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null child entity!", this);
                }
            }
        }

        /// <summary>
        /// Unity Awake callback. Automatically installs the entity if <c>installOnAwake</c> is true.
        /// </summary>
        protected virtual void Awake()
        {
            if (this.installOnAwake) this.Install();
        }

        /// <summary>
        /// Unity OnDestroy callback. Optionally disposes values and cleans up subscriptions.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (this.disposeValues)
                this.DisposeValues();

            this.UnsubscribeAll();
        }

        /// <summary>
        /// Marks the entity as not installed, allowing reinstallation.
        /// </summary>
        public void MarkAsNotInstalled() => _installed = false;
        
        /// <summary>
        /// Removes all subscriptions and callbacks associated with this entity.
        /// </summary>
        public void UnsubscribeAll()
        {
            InternalUtils.Unsubscribe(ref this.OnStateChanged);

            InternalUtils.Unsubscribe(ref this.OnInitialized);
            InternalUtils.Unsubscribe(ref this.OnEnabled);
            InternalUtils.Unsubscribe(ref this.OnDisabled);
            InternalUtils.Unsubscribe(ref this.OnUpdated);
            InternalUtils.Unsubscribe(ref this.OnFixedUpdated);
            InternalUtils.Unsubscribe(ref this.OnLateUpdated);
            InternalUtils.Unsubscribe(ref this.OnDisposed);

            InternalUtils.Unsubscribe(ref this.OnBehaviourAdded);
            InternalUtils.Unsubscribe(ref this.OnBehaviourDeleted);

            InternalUtils.Unsubscribe(ref this.OnValueAdded);
            InternalUtils.Unsubscribe(ref this.OnValueDeleted);
            InternalUtils.Unsubscribe(ref this.OnValueChanged);

            InternalUtils.Unsubscribe(ref this.OnTagAdded);
            InternalUtils.Unsubscribe(ref this.OnTagDeleted);
        }

        /// <inheritdoc/>
        public override string ToString() => $"{nameof(name)}: {name}, {nameof(instanceId)}: {instanceId}";

        public bool Equals(IEntity<E> other) => this.instanceId == other.InstanceID;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity<E> other && other.InstanceID == this.instanceId;

        /// <inheritdoc/>
        public override int GetHashCode() => this.instanceId;

        /// <summary>
        /// Clears all data (tags, values, behaviours) from this entity.
        /// </summary>
        public void Clear()
        {
            this.ClearTags();
            this.ClearValues();
            this.ClearBehaviours();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.ConstructTags(_initialTagCapacity);
            this.ConstructValues(_initialValueCapacity);
            this.ConstructBehaviours(_initialBehaviourCapacity);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}