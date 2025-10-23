#if UNITY_5_3_OR_NEWER
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
        public partial class SceneEntityWorld<E>
        {
#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
                [Header("Lifecycle")]
                [SerializeField]
                [Tooltip("Enable automatic syncing with Unity MonoBehaviour lifecycle (Start/OnEnable/OnDisable).")]
                private protected bool useUnityLifecycle = true;

                [Tooltip("Should don't destroy if scene changed?")]
                [SerializeField]
                private bool dontDestroyOnLoad;
        
#if ODIN_INSPECTOR
        [HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
#endif
                [Header("Collecting")]
                [Tooltip("If this option is enabled then EntityWorld add all Entities on a scene on Awake()")]
                [SerializeField]
                private protected bool collectOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(collectOnAwake))]
#endif
                [Tooltip("If this option is enabled then EntityWorld scan inactive Entities on a scene also")]
                [SerializeField]
                private bool includeInactiveOnCollect = true;
        }
}
#endif