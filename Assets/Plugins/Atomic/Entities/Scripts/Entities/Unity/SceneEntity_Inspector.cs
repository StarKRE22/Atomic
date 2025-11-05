#if UNITY_5_3_OR_NEWER

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        [Header("Lifecycle")]
        [Tooltip("Enable automatic syncing with Unity MonoBehaviour lifecycle.")]
#if ODIN_INSPECTOR
        [DisableInPlayMode]
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
        [FormerlySerializedAs("children")]
        [Tooltip("Specify child entities that will installed and uninstalled with this entity")]
        [Space(8), SerializeField]
        internal List<SceneEntity> childInstallers;

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
    }
}
#endif