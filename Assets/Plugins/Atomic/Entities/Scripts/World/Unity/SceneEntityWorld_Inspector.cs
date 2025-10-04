using Sirenix.OdinInspector;
using UnityEngine;

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
        [Header("Entity Registration")]
        [Tooltip("If this option is enabled then EntityWorld add all Entities on a scene on Awake()")]
        [SerializeField]
        private protected bool registerOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(registerOnAwake))]
#endif
        [Tooltip("If this option is enabled then EntityWorld scan inactive Entities on a scene also")]
        [SerializeField]
        private bool includeInactiveOnRegister = true;
    }
}