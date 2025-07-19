#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides gizmo drawing functionality for the <see cref="SceneEntity"/> component,
    /// allowing visual debugging in both play mode and edit mode.
    /// </summary>
    public partial class SceneEntity<E>
    {
        [Header("Gizmos")]
        [SerializeField]
        private bool _onlySelectedGizmos;

        [SerializeField]
        private bool _onlyEditModeGizmos;

        /// <summary>
        /// Called by Unity to draw gizmos in the scene view.
        /// Will delegate to <see cref="OnDrawGizmosSelected"/> unless only selected drawing is enabled.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!_onlySelectedGizmos)
                this.OnDrawGizmosSelected();
        }

        /// <summary>
        /// Draws gizmos for this entity and its behaviours when selected.
        /// Gizmos will only be drawn in edit mode if allowed by configuration.
        /// </summary>
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
                    IBehaviour<E> behaviour = _entity._behaviours[i];
                    if (behaviour is IGizmos<E> gizmos)
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