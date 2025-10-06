#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class EntityView<E>
    {
        [Header("Gizmos")]
        [Tooltip("If true, gizmos will be drawn only when the object is selected.")]
        [SerializeField]
        private bool _onlySelectedGizmos;

        [Tooltip("If true, gizmos will be drawn only in Edit Mode, even during play mode.")]
        [SerializeField]
        private bool _onlyEditModeGizmos;

        /// <summary>
        /// Unity callback invoked to draw gizmos for this component.
        /// When <see cref="_onlySelectedGizmos"/> is false, defers drawing to <see cref="OnDrawGizmosSelected"/>.
        /// </summary>
        private protected virtual void OnDrawGizmos()
        {
            if (!_onlySelectedGizmos)
                this.OnDrawGizmosSelected();
        }

        /// <summary>
        /// Unity callback invoked when the object is selected.
        /// Draws custom gizmos using behaviours that implement <see cref="IEntityGizmos{E}"/>.
        /// </summary>
        private protected virtual void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying && _onlyEditModeGizmos)
                return;
#endif
            this.OnGizmosDraw();
        }

        /// <summary>
        /// Internal method for drawing gizmos using entity behaviours that implement <see cref="IEntityGizmos{E}"/>.
        /// </summary>
        internal void OnGizmosDraw()
        {
            if (_entity == null)
                return;

            try
            {
                for (int i = 0, count = _entity.BehaviourCount; i < count; i++)
                    if (_entity.GetBehaviourAt(i) is IEntityGizmos gizmos)
                        gizmos.DrawGizmos(_entity);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Ops: detected exception in gizmos: {e.Message}");
            }
        }
    }
}
#endif