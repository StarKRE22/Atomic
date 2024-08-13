#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity Gizmos")]
    [RequireComponent(typeof(SceneEntity))]
    public sealed class SceneEntityGizmos : MonoBehaviour
    {
        [SerializeField]
        private bool drawGizmos = true;
        
        [SerializeField]
        private bool drawGizmosSelected = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(drawGizmos))]
#endif
        [Space]
        [SerializeReference]
        private IEntityGizmos[] gizmoses;

#if ODIN_INSPECTOR
        [ShowIf(nameof(drawGizmosSelected))]
#endif
        [SerializeReference]
        private IEntityGizmos[] gizmosesSelected;

        private SceneEntity _sceneEntity;
        
        // ReSharper disable once Unity.RedundantEventFunction
        private void Start()
        {
            //Required for enable check in Unity inspector :)
        }

        private void OnValidate()
        {
            _sceneEntity = this.GetComponent<SceneEntity>();
        }

        private void OnDrawGizmos()
        {
            if (EditorApplication.isPlaying)
            {
                return;
            }
            
            if (!this.enabled)
            {
                return;
            }
            
            if (!this.drawGizmos)
            {
                return;
            }

            if (this.gizmoses == null)
            {
                return;
            }

            if (_sceneEntity == null)
            {
                _sceneEntity = this.GetComponent<SceneEntity>();
            }

            for (int i = 0, count = this.gizmoses.Length; i < count; i++)
            {
                try
                {
                    IEntityGizmos gizmos = this.gizmoses[i];
                    gizmos?.OnGizmosDraw(_sceneEntity);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!this.enabled)
            {
                return;
            }
            
            if (!this.drawGizmosSelected)
            {
                return;
            }

            if (this.gizmoses == null)
            {
                return;
            }

            if (_sceneEntity == null)
            {
                _sceneEntity = this.GetComponent<SceneEntity>();
            }

            for (int i = 0, count = this.gizmosesSelected.Length; i < count; i++)
            {
                try
                {
                    IEntityGizmos gizmos = this.gizmosesSelected[i];
                    gizmos?.OnGizmosDraw(_sceneEntity);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}
#endif