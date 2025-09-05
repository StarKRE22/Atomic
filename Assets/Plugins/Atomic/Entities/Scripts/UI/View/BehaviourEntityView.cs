#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using static Atomic.Entities.EntityUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a visual component for an <see cref="IEntity"/> in the scene.
    /// Handles installation, behaviours, visibility, and gizmo drawing.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity View")]
    [DisallowMultipleComponent]
    public class BehaviourEntityView : EntityView
    {
        [Space]
        [Tooltip("The list of installers used to configure and setup the entity view")]
        [SerializeField]
        private List<EntityViewInstaller> _installers;

        /// <summary>
        /// The collection of behaviours added to the entity via the view.
        /// </summary>
        [Tooltip("The behaviours that will be applied to the entity when this view is shown")]
        private IEntityBehaviour[] _behaviours;

        /// <summary>
        /// The current number of behaviours stored in the _behaviours array.
        /// </summary>
        private int _behaviourCount;

        /// <summary>
        /// Indicates whether the view installation process has been performed.
        /// </summary>
        [Tooltip("Indicates whether all installers have been executed for this view")]
        private bool _installed;

        [Header("Gizmos")]
        [Tooltip("If true, gizmos will be drawn only when the object is selected.")]
        [SerializeField]
        private bool _onlySelectedGizmos;

        [Tooltip("If true, gizmos will be drawn only in Edit Mode, even during play mode.")]
        [SerializeField]
        private bool _onlyEditModeGizmos;

        /// <summary>
        /// Called when the view is being shown.
        /// Installs any defined installers, adds registered behaviours to the entity,
        /// and activates the GameObject.
        /// </summary>
        /// <param name="entity">The entity being shown.</param>
        protected override void OnShow(IEntity entity)
        {
            this.Install();
            entity.AddBehaviours(_behaviours, 0, _behaviourCount);
            base.OnShow(entity);
        }

        /// <summary>
        /// Called when the view is being hidden.
        /// Removes previously added behaviours from the entity and deactivates the GameObject.
        /// </summary>
        /// <param name="entity">The entity being hidden.</param>
        protected override void OnHide(IEntity entity)
        {
            entity.DelBehaviours(_behaviours, 0, _behaviourCount);
            base.OnHide(entity);
        }

        /// <summary>
        /// Adds the specified behaviour to the view and, if visible, immediately applies it to the entity.
        /// </summary>
        /// <param name="behaviour">The behaviour to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if behaviour is null.</exception>
        public void AddBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException(nameof(behaviour));

            if (!AddIfAbsent(ref _behaviours, ref _behaviourCount, behaviour,
                    EqualityComparer<IEntityBehaviour>.Default))
                return;

            if (_isVisible)
                _entity.AddBehaviour(behaviour);
        }

        /// <summary>
        /// Checks whether the view contains the specified behaviour.
        /// </summary>
        /// <param name="behaviour">The behaviour to check.</param>
        /// <returns>True if the behaviour is present; otherwise, false.</returns>
        public bool HasBehaviour(IEntityBehaviour behaviour) =>
            behaviour != null && Contains(_behaviours, behaviour, _behaviourCount,
                EqualityComparer<IEntityBehaviour>.Default);

        /// <summary>
        /// Removes the specified behaviour from the view and, if visible, immediately removes it from the entity.
        /// </summary>
        /// <param name="behaviour">The behaviour to remove.</param>
        public void DelBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour == null || !Remove(ref _behaviours, ref _behaviourCount, behaviour,
                    EqualityComparer<IEntityBehaviour>.Default))
                return;

            if (_isVisible)
                _entity.DelBehaviour(behaviour);
        }

        /// <summary>
        /// Retrieves the behaviour at the specified index.
        /// </summary>
        /// <param name="index">The index of the behaviour.</param>
        /// <returns>The behaviour at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if index is out of range.</exception>
        public IEntityBehaviour GetBehaviourAt(int index)
        {
            return index < 0 || index >= _behaviourCount
                ? throw new ArgumentOutOfRangeException(nameof(index))
                : _behaviours[index];
        }

        /// <summary>
        /// Installs the view by executing all configured <see cref="EntityViewInstaller"/> components.
        /// This method is executed only once upon the first call to <see cref="OnShow"/>.
        /// </summary>
        private void Install()
        {
            if (_installed)
                return;

            if (_installers != null)
            {
                for (int i = 0, count = _installers.Count; i < count; i++)
                {
                    EntityViewInstaller installer = _installers[i];
                    if (installer != null)
                        installer.Install(this);
                }
            }

            _installed = true;
        }

        /// <summary>
        /// Unity callback invoked to draw gizmos for this component.
        /// When <see cref="_onlySelectedGizmos"/> is false, defers drawing to <see cref="OnDrawGizmosSelected"/>.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (!_onlySelectedGizmos)
                this.OnDrawGizmosSelected();
        }

        /// <summary>
        /// Unity callback invoked when the object is selected.
        /// Draws custom gizmos using behaviours that implement <see cref="IEntityGizmos{E}"/>.
        /// </summary>
        private void OnDrawGizmosSelected()
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
                for (int i = 0, count = _behaviourCount; i < count; i++)
                    if (_behaviours[i] is IEntityGizmos gizmos)
                        gizmos.DrawGizmos(_entity);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Ops: detected exception in gizmos: {e.Message}");
            }
        }

        #region Static

        /// <summary>
        /// Arguments used to create an <see cref="BehaviourEntityView"/> instance.
        /// </summary>
        [Serializable]
        public struct CreateArgs
        {
            [Tooltip("The name of the new GameObject to create for the EntityView.")]
            public string name;

            [Tooltip("Installers that will configure the EntityView upon creation.")]
            public List<EntityViewInstaller> installers;

            [Tooltip("Behaviours that will be added to the entity when the view is shown.")]
            public IEnumerable<IEntityBehaviour> behaviours;

            [Tooltip("If true, gizmos will be drawn only in Edit Mode.")]
            public bool onlyEditModeGizmos;

            [Tooltip("If true, gizmos will be drawn only when the object is selected.")]
            public bool onlySelectedGizmos;
        }

        /// <summary>
        /// Creates a new <see cref="BehaviourEntityView"/> GameObject and sets up its behaviours and installers.
        /// </summary>
        /// <param name="args">The creation arguments.</param>
        /// <returns>The created <see cref="BehaviourEntityView"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BehaviourEntityView Create(in CreateArgs args = default)
        {
            var gameObject = new GameObject(args.name);
            gameObject.SetActive(false);

            BehaviourEntityView view = gameObject.AddComponent<BehaviourEntityView>();
            view._installers = args.installers;
            view._behaviours = args.behaviours?.ToArray();
            view._onlyEditModeGizmos = args.onlyEditModeGizmos;
            view._onlySelectedGizmos = args.onlySelectedGizmos;

            gameObject.SetActive(true);
            return view;
        }

        #endregion
    }
}
#endif