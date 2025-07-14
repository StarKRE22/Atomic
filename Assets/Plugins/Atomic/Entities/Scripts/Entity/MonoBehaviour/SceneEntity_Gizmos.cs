#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        [Header("Gizmos")]
        [SerializeField]
        private bool _onlySelectedGizmos;

        [SerializeField]
        private bool _onlyEditModeGizmos;

        private void OnDrawGizmos()
        {
            if (!_onlySelectedGizmos)
                this.OnDrawGizmosSelected();
        }

        private void OnDrawGizmosSelected()
        {
            if (EditorApplication.isPlaying && _onlyEditModeGizmos)
                return;

            if (_entity == null)
                return;

            try
            {
                for (int i = 0; i < _entity._behaviourCount; i++)
                {
                    IBehaviour behaviour = _entity._behaviours[i];
                    if (behaviour is IGizmos gizmos) 
                        gizmos.OnGizmosDraw(_entity);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Ops: detected exception in gizmos: {e.Message}");
            }
        }
    }
}
#endif