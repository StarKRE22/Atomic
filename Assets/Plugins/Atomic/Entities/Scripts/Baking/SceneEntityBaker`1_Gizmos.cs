#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntityBaker<E>
    {
        [Header("Gizmos")]
        [SerializeField]
        private bool onlySelectedGizmos;

        [SerializeField]
        private bool onlyEditModeGizmos;

        /// <summary>
        /// Called by Unity to draw gizmos in the scene view.
        /// Will delegate to <see cref="OnDrawGizmosSelected"/> unless only selected drawing is enabled.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!this.onlySelectedGizmos)
                this.OnDrawGizmosSelected();
        }
         
        /// <summary>
        /// Draws gizmos for this entity and its behaviours when selected.
        /// Gizmos will only be drawn in edit mode if allowed by configuration.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (_previewEntity == null || EditorApplication.isPlaying && this.onlyEditModeGizmos)
                return;
         
            try
            {
                for (int i = 0; i < _previewEntity.BehaviourCount; i++)
                    if (_previewEntity.GetBehaviourAt(i) is IEntityGizmos gizmos)
                        gizmos.DrawGizmos(_previewEntity);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"SceneEntity: Ops! Detected exception in OnDrawGizmos: {e.StackTrace}", this);
            }
        }
    }
}
#endif