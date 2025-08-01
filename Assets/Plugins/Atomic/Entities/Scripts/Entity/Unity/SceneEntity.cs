#if UNITY_5_3_OR_NEWER
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
    public partial class SceneEntity : MonoBehaviour, IEntity
    {
        private const int UNDEFINED_INDEX = -1;

        /// <inheritdoc cref="IEntity.OnStateChanged"/>
        public event Action OnStateChanged;

        /// <inheritdoc />
        public int InstanceID
        {
            get => _instanceId;
            internal set => _instanceId = value;
        }

        /// <inheritdoc />
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        /// <summary>
        /// Indicates whether this entity has already been installed.
        /// </summary>
        public bool Installed => _installed;

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [HideInPlayMode]
#endif
        [Tooltip("If this option is enabled, the Install() method will be called on Awake()")]
        [SerializeField]
        internal bool installOnAwake = true;

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

        [SerializeField,
         Tooltip("Enable automatic syncing with Unity MonoBehaviour lifecycle (Start/OnEnable/OnDisable).")]
        private bool useUnityLifecycle = true;

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
#endif

#if ODIN_INSPECTOR
        [HideInPlayMode, SceneObjectsOnly]
#endif
        [Tooltip("Specify the installers that will put values and systems to this context")]
        [Space, SerializeField]
        internal List<SceneEntityInstaller> installers;

#if ODIN_INSPECTOR
        [HideInPlayMode, SceneObjectsOnly]
#endif
        [Tooltip("Specify child entities that will installed with this entity")]
        [Space, SerializeField]
        internal List<SceneEntity> children;

        private int _instanceId;
        private bool _installed;
        private bool _started;

        protected virtual void Awake()
        {
            EntityRegistry.Instance.Register(this, out _instanceId);

            if (this.installOnAwake)
                this.Install();
        }

        protected virtual void OnEnable()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Enable();
                UpdateManager.Instance.Add(this);
            }
        }

        protected virtual void Start()
        {
            if (this.useUnityLifecycle)
            {
                this.Spawn();
                this.Enable();
                UpdateManager.Instance.Add(this);

                _started = true;
            }
        }

        protected virtual void OnDisable()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Disable();
                UpdateManager.Instance.Del(this);
            }
        }

        protected virtual void OnDestroy()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Despawn();
                _started = false;
            }

            if (this.disposeValues)
                this.DisposeValues();

            this.UnsubscribeAll();

            EntityRegistry.Instance.Unregister(ref _instanceId);
        }

        /// <summary>
        /// Installs all configured installers and child entities into this SceneEntity.
        /// </summary>
        public void Install()
        {
            if (_installed)
                return;

            this.OnInstall();

            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Count; i < count; i++)
                {
                    SceneEntityInstaller installer = this.installers[i];
                    if (installer != null)
                        installer.Install(this);
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null installer!", this);
                }
            }

            if (this.children != null)
            {
                for (int i = 0, count = this.children.Count; i < count; i++)
                {
                    SceneEntity child = this.children[i];
                    if (child != null)
                        child.Install();
                    else
                        Debug.LogWarning("SceneEntity: Ops! Detected null child entity!", this);
                }
            }

            _installed = true;
        }

        protected virtual void OnInstall()
        {
        }

        /// <summary>
        /// Marks the entity as not installed, allowing reinstallation.
        /// </summary>
        public void MarkAsNotInstalled() => _installed = false;

        /// <inheritdoc/>
        public override string ToString() => $"{nameof(name)}: {name}, {nameof(_instanceId)}: {_instanceId}";

        // ReSharper disable once UnusedMember.Global
        public bool Equals(IEntity other) => other != null && _instanceId == other.InstanceID;

        /// <inheritdoc/>
        public override bool Equals(object obj) => obj is IEntity other && other.InstanceID == _instanceId;

        /// <inheritdoc/>
        public override int GetHashCode() => _instanceId;

        /// <summary>
        /// Removes all subscriptions and callbacks associated with this entity.
        /// </summary>
        public void UnsubscribeAll()
        {
            this.OnStateChanged = null;

            this.OnSpawned = null;
            this.OnEnabled = null;
            this.OnDisabled = null;

            this.OnUpdated = null;
            this.OnFixedUpdated = null;
            this.OnLateUpdated = null;
            this.OnDespawned = null;

            this.OnBehaviourAdded = null;
            this.OnBehaviourDeleted = null;

            this.OnValueAdded = null;
            this.OnValueDeleted = null;
            this.OnValueChanged = null;

            this.OnTagAdded = null;
            this.OnTagDeleted = null;
        }
    }
}
#endif