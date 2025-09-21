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
    public partial class SceneEntity
    {
        [Header("Gizmos")]
        [Tooltip("Should draw gizmos only when this GameObject is selected?")]
        [SerializeField]
        private bool _onlySelectedGizmos;

        [Tooltip("Should draw gizmos only when Unity is not playing?")]
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

            try
            {
                for (int i = 0; i < _behaviourCount; i++)
                    if (_behaviours[i] is IEntityGizmos gizmos)
                        gizmos.DrawGizmos(this);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"SceneEntity: Ops! Detected exception in OnDrawGizmos: {e.StackTrace}", this);
            }
        }
    }
}
#endif