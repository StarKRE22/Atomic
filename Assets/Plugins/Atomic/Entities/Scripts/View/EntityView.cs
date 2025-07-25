#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for a base entity view targeting general <see cref="IEntity"/> instances.
    /// This component inherits all behavior from <see cref="EntityViewBase{IEntity}"/>.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity View")]
    [DisallowMultipleComponent]
    public class EntityView : EntityViewBase<IEntity>
    {
    }

    /// <summary>
    /// Provides a base implementation for viewing and managing entities of type <typeparamref name="E"/>
    /// in Unity. This class extends <see cref="EntityViewBase{E}"/> to add support for installing view-specific
    /// behaviours and for drawing gizmos via installed <see cref="EntityViewInstaller{E}"/> instances.
    /// </summary>
    /// <typeparam name="E">
    /// The type of entity to be viewed. Must implement <see cref="IEntity"/>.
    /// </typeparam>
    public abstract class EntityView<E> : EntityViewBase<E> where E : IEntity
    {
#if ODIN_INSPECTOR
        [SceneObjectsOnly]
#endif
        [Tooltip("The list of installers used to configure and setup the entity view")]
        [SerializeField]
        private List<EntityViewInstaller<E>> _installers;

        /// <summary>
        /// A collection of behaviours added to the entity via the view.
        /// </summary>
        private readonly List<IEntityBehaviour> _behaviours = new();

        /// <summary>
        /// Indicates whether the view installation process has been performed.
        /// </summary>
        private bool _installed;

        [Header("Gizmos")]
        [Tooltip("If true, gizmos will be drawn only when the object is selected.")]
        [SerializeField]
        private bool _onlySelectedGizmos;

        [Tooltip("If true, gizmos will be drawn only in Edit Mode, even during play mode.")]
        [SerializeField]
        private bool _onlyEditModeGizmos;

        /// <inheritdoc/>
        /// <summary>
        /// Called when the view is being shown. 
        /// This override installs any defined installers, adds registered behaviours to the entity,
        /// and activates the GameObject.
        /// </summary>
        /// <param name="entity">The entity being shown.</param>
        protected override void OnShow(E entity)
        {
            this.Install();
            entity.AddBehaviours(_behaviours);
            base.OnShow(entity);
        }

        /// <inheritdoc/>
        /// <summary>
        /// Called when the view is being hidden.
        /// This override removes previously added behaviours from the entity and deactivates the GameObject.
        /// </summary>
        /// <param name="entity">The entity being hidden.</param>
        protected override void OnHide(E entity)
        {
            entity.DelBehaviours(_behaviours);
            base.OnHide(entity);
        }

        /// <summary>
        /// Adds the specified behaviour to the view and, if visible, immediately applies it to the entity.
        /// </summary>
        /// <param name="behaviour">The behaviour to add.</param>
        public void AddBehaviour(IEntityBehaviour behaviour)
        {
            _behaviours.Add(behaviour);

            if (_isVisible)
                _entity.AddBehaviour(behaviour);
        }

        /// <summary>
        /// Removes the specified behaviour from the view and, if visible, immediately removes it from the entity.
        /// </summary>
        /// <param name="behaviour">The behaviour to remove.</param>
        public void DelBehaviour(IEntityBehaviour behaviour)
        {
            _behaviours.Remove(behaviour);

            if (_isVisible)
                _entity.DelBehaviour(behaviour);
        }

        /// <summary>
        /// Installs the view by executing all configured <see cref="EntityViewInstaller{E}"/> components.
        /// This method is executed only once upon the first call to <see cref="OnShow(E)"/>.
        /// </summary>
        private void Install()
        {
            if (_installed)
                return;

            _installed = true;

            for (int i = 0, count = _installers.Count; i < count; i++)
            {
                EntityViewInstaller<E> installer = _installers[i];
                if (installer != null)
                    installer.Install(this);
            }
        }

        /// <summary>
        /// Unity callback invoked to draw gizmos for this component.
        /// When <see cref="_onlySelectedGizmos"/> is false, it defers drawing to <see cref="OnDrawGizmosSelected"/>.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!_onlySelectedGizmos)
                this.OnDrawGizmosSelected();
        }

        /// <summary>
        /// Unity callback invoked when the object is selected (or always, if <see cref="_onlySelectedGizmos"/> is false).
        /// Draws custom gizmos using behaviours that implement <see cref="IEntityGizmos{E}"/>.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying && _onlyEditModeGizmos)
                return;
#endif
            if (_entity == null)
                return;

            try
            {
                for (int i = 0, count = _behaviours.Count; i < count; i++)
                {
                    if (_behaviours[i] is IEntityGizmos<E> gizmos)
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
