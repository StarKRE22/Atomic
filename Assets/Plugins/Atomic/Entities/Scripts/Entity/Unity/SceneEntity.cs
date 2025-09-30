#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;
using UnityEngine.Serialization;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a MonoBehaviour implementation for an <see cref="IEntity"/> that can be installed from the Unity Scene.
    /// Allows composition through Unity Inspector and automated setup via installers and child entities.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public partial class SceneEntity : MonoBehaviour, IEntity
    {
        private const int UNDEFINED_INDEX = -1;

        /// <inheritdoc cref="IEntity.OnStateChanged"/>
        public event Action<IEntity> OnStateChanged;

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
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnStateChanged?.Invoke(this);
                }
            }
        }

        [Header("Lifecycle")]
        [Tooltip("Enable automatic syncing with Unity MonoBehaviour lifecycle.")]
#if ODIN_INSPECTOR
        [DisableInPlayMode]
        // [GUIColor(0f, 0.83f, 1f)]
#endif
        [SerializeField]
        private bool useUnityLifecycle = true;

        [Tooltip("Should dispose values when Dispose() called")]
#if ODIN_INSPECTOR
        [DisableInPlayMode]
#endif
        [SerializeField]
        private bool disposeValues = true;

#if ODIN_INSPECTOR
        // [GUIColor(0f, 0.83f, 1f)]
        [DisableInPlayMode]
#endif
        [Header("Installing")]
        [Tooltip("If this option is enabled, the Install() method will be called on Awake()")]
        [SerializeField]
        internal bool installOnAwake = true;

        [Tooltip("Should invoke Uninstall() when OnDestroy() called")]
        [SerializeField]
        private bool uninstallOnDestroy = true;

#if ODIN_INSPECTOR
        [DisableInPlayMode]
#endif
        [Space]
        [Tooltip("Specify the ScriptableObject installers that will put values and behaviours to this entity")]
        [SerializeField]
        internal List<ScriptableEntityInstaller> scriptableInstallers;

#if ODIN_INSPECTOR
        [DisableInPlayMode]
#endif
        [Space]
        [SerializeField]
        [Tooltip("Specify the MonoBehaviour installers that will put values and behaviours to this entity")]
        [FormerlySerializedAs("installers")]
        internal List<SceneEntityInstaller> sceneInstallers;

#if ODIN_INSPECTOR
        [DisableInPlayMode]
#endif
        [Tooltip("Specify child entities that will installed with this entity")]
        [Space(8), SerializeField]
        internal List<SceneEntity> children;
        
        [Header("Gizmos")]
        [Tooltip("Should draw gizmos only when this GameObject is selected?")]
        [SerializeField]
        private bool onlySelectedGizmos;

        [Tooltip("Should draw gizmos only when Unity is not playing?")]
        [SerializeField]
        private bool onlyEditModeGizmos;
        
        
        [Tooltip(
                "If this option is enabled, the installing, precomputing, and lifecycle will be called every time OnValidate is called in Edit Mode")]
#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0)]
        // [GUIColor(0f, 0.83f, 1f)]
        // [GUIColor(1f, 0.92156863f, 0.015686275f)]
        [DisableInPlayMode]
        [InfoBox(
                "WARNING: If you create Unity objects or another heavy objects in the Install() method, be sure to turn off!",
                InfoMessageType.Warning,
                nameof(autoCompile))
        ]
#endif
        [Header("Editor")]
        [SerializeField]
        private bool autoCompile;
        
        /// <summary>
        /// Initial tag capacity used to optimize tag allocation.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Optimization", 1)]
#else
        [Header("Optimization")]
#endif
        [Min(1)]
        [SerializeField]
        private int initialTagCapacity = 1;

        /// <summary>
        /// Initial value capacity used to optimize value allocation.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Optimization", 2)]
#endif
        [Min(1)]
        [SerializeField]
        private int initialValueCapacity = 1;

        /// <summary>
        /// Initial behaviour capacity used to optimize behaviour allocation.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Optimization", 3)]
#endif
        [Min(0)]
        [SerializeField]
        private int initialBehaviourCapacity;
        
        private int _instanceId;

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