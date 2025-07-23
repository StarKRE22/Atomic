using System;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity World")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1000)]
    public partial class SceneWorld<E> : MonoBehaviour, IEntityWorld<E> where E : SceneEntity<E>
    {
        public event Action OnStateChanged
        {
            add => _world.OnStateChanged += value;
            remove => _world.OnStateChanged -= value;
        }

        private readonly EntityWorld<E> _world = new();

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [HideInPlayMode]
#endif
        [Tooltip("If this option is enabled then EntityWorld add all Entities on a scene on Awake()")]
        [SerializeField]
        private bool scanOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(scanOnAwake))]
#endif
        [Tooltip("If this option is enabled then EntityWorld scan inactive Entities on a scene also")]
        [SerializeField]
        private bool includeInactiveOnScan = true;

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        private void Awake()
        {
            if (!this.scanOnAwake)
                return;

#if UNITY_2023_1_OR_NEWER
            FindObjectsInactive includeInactive = this.includeInactiveOnScan
                ? FindObjectsInactive.Include
                : FindObjectsInactive.Exclude;
            
            E[] entities = FindObjectsByType<E>(includeInactive, FindObjectsSortMode.None);
#else
            SceneEntity[] entities = FindObjectsOfType<SceneEntity>(this.includeInactiveOnScan);
#endif
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                E entity = entities[i];
                if (!entity.Installed)
                    entity.Install();

                this.Add(entity);
            }
        }
    }
}