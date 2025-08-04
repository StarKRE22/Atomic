#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using static Atomic.Entities.InternalUtils;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for a base entity view targeting general <see cref="IEntity"/> instances.
    /// This component inherits all behavior from <see cref="EntityView{IEntity}"/>.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity View")]
    [DisallowMultipleComponent]
    public class EntityView : EntityView<IEntity>
    {
        public static EntityView Create(CreateArgs args = default) => Create<EntityView>(args);
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
        /// <summary>
        /// Static comparer used to compare behaviours.
        /// </summary>

#if ODIN_INSPECTOR
        [SceneObjectsOnly]
#endif
        [Tooltip("The list of installers used to configure and setup the entity view")]
        [SerializeField]
        private List<EntityViewInstaller<E>> _installers;

        /// <summary>
        /// A collection of behaviours added to the entity via the view.
        /// </summary>
        private IEntityBehaviour[] _behaviours;
        private int _behaviourCount;

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
            entity.AddBehaviours(_behaviours, 0, _behaviourCount);
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
            entity.DelBehaviours(_behaviours, 0, _behaviourCount);
            base.OnHide(entity);
        }

        /// <summary>
        /// Adds the specified behaviour to the view and, if visible, immediately applies it to the entity.
        /// </summary>
        /// <param name="behaviour">The behaviour to add.</param>
        public void AddBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException(nameof(behaviour));

            if (!AddIfAbsent(
                    ref _behaviours,
                    ref _behaviourCount,
                    behaviour,
                    EqualityComparer<IEntityBehaviour>.Default
                ))
                return;

            if (_isVisible)
                _entity.AddBehaviour(behaviour);
        }

        public bool HasBehaviour(IEntityBehaviour behaviour) =>
            behaviour != null && Contains(_behaviours, behaviour, _behaviourCount,
                EqualityComparer<IEntityBehaviour>.Default);

        /// <summary>
        /// Removes the specified behaviour from the view and, if visible, immediately removes it from the entity.
        /// </summary>
        /// <param name="behaviour">The behaviour to remove.</param>
        public void DelBehaviour(IEntityBehaviour behaviour)
        {
            if (behaviour == null || !Remove(
                    ref _behaviours,
                    ref _behaviourCount,
                    behaviour,
                    EqualityComparer<IEntityBehaviour>.Default
                ))
                return;

            if (_isVisible)
                _entity.DelBehaviour(behaviour);
        }

        public IEntityBehaviour GetBehaviourAt(int index)
        {
            return index < 0 || index >= _behaviourCount
                ? throw new ArgumentOutOfRangeException(nameof(index))
                : _behaviours[index];
        }

        /// <summary>
        /// Installs the view by executing all configured <see cref="EntityViewInstaller{E}"/> components.
        /// This method is executed only once upon the first call to <see cref="OnShow(E)"/>.
        /// </summary>
        private void Install()
        {
            if (_installed)
                return;

            if (_installers != null)
            {
                for (int i = 0, count = _installers.Count; i < count; i++)
                {
                    EntityViewInstaller<E> installer = _installers[i];
                    if (installer != null)
                        installer.Install(this);
                }
            }


            _installed = true;
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
            this.OnGizmosDraw();
        }

        internal void OnGizmosDraw()
        {
            if (_entity == null)
                return;

            Debug.Log($"START GIZMOS {_behaviourCount}");
            try
            {
                for (int i = 0, count = _behaviourCount; i < count; i++)
                {
                    Debug.Log("AAA GIZMOS");

                    if (_behaviours[i] is IEntityGizmos gizmos)
                        gizmos.OnGizmosDraw(_entity);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Ops: detected exception in gizmos: {e.Message}");
            }
        }

        #region Static

        [Serializable]
        public struct CreateArgs
        {
            public string name;
            public List<EntityViewInstaller<E>> installers;
            public IEnumerable<IEntityBehaviour> behaviours;
            public bool onlyEditModeGizmos;
            public bool onlySelectedGizmos;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Create<T>(CreateArgs args = default) where T : EntityView<E>
        {
            var gameObject = new GameObject(args.name);
            gameObject.SetActive(false);
            T view = gameObject.AddComponent<T>();
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