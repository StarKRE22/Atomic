#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Default entity view component.
    /// </summary>
    /// <remarks>
    /// This is a non-generic wrapper around <see cref="EntityView{E}"/> fixed to <see cref="IEntity"/>.
    /// Useful when the specific entity type is unknown or irrelevant.
    /// </remarks>
    [AddComponentMenu("Atomic/Entities/Entity View")]
    [DisallowMultipleComponent]
    public class EntityView : EntityView<IEntity>
    {
        /// <summary>
        /// Creates a new <see cref="EntityView"/> GameObject and sets up its aspects.
        /// </summary>
        /// <param name="args">The creation arguments.</param>
        /// <returns>The created <see cref="EntityView"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntityView Create(in CreateArgs args = default) => Create<EntityView>(args);
    }

    /// <summary>
    /// Base class for all entity views.
    /// Provides core functionality for showing, hiding, and naming views bound to <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="E">The type of <see cref="IEntity"/> associated with this view.</typeparam>
    public abstract partial class EntityView<E> : MonoBehaviour where E : IEntity
    {
        /// <summary>
        /// If true, <see cref="GameObject.SetActive"/> will be called when <see cref="Show(E)"/> and <see cref="Hide"/> are invoked.
        /// </summary>
        [Tooltip("Should activate and deactivate GameObject when Show / Hide are invoked?")]
        [SerializeField]
        internal bool controlGameObject = true;

        /// <summary>
        /// Determines whether the view should use a custom name instead of the GameObject's name.
        /// </summary>
        [Header("Name")]
        [Tooltip("If true, the view will use the custom name instead of the GameObject's name")]
        [SerializeField]
        internal bool overrideName;

        /// <summary>
        /// Custom display name for the view, used only if <see cref="overrideName"/> is enabled.
        /// </summary>
        [Tooltip("The custom name to use for the view when _overrideName is enabled")]
        [SerializeField]
        internal string customName;

        /// <summary>
        /// List of aspects that provide values and behaviors to the attached entity.
        /// </summary>
        [Header("Aspects")]
        [Tooltip("Specify the aspects that will put values and behaviours to an attached entity")]
        [SerializeField]
        internal List<SceneEntityAspect<E>> aspects;

        /// <summary>
        /// Gets the display name of the view.
        /// Returns <see cref="customName"/> if <see cref="overrideName"/> is true; 
        /// otherwise, returns the <see cref="GameObject.name"/>.
        /// </summary>
        public virtual string Name => overrideName ? customName : this.name;

        /// <summary>
        /// Gets the entity currently associated with this view.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public E Entity => _entity;

        private E _entity;

        /// <summary>
        /// Gets a value indicating whether the view is currently visible (i.e., has an entity assigned).
        /// </summary>
        public bool IsVisible => _entity != null;

        /// <summary>
        /// Displays the view and associates it with the specified entity.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null.</exception>
        public void Show(E entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));

            if (this.controlGameObject)
                this.gameObject.SetActive(true);

            this.OnShow(entity);

            if (this.aspects != null)
            {
                for (int i = 0, count = this.aspects.Count; i < count; i++)
                {
                    SceneEntityAspect<E> aspect = this.aspects[i];
                    if (aspect)
                        aspect.Apply(entity);
                    else
                        Debug.LogWarning("EntityView: Oops! Detected null aspect!", this);
                }
            }
        }

        /// <summary>
        /// Called when the view is shown.
        /// Override this method to add custom logic when an entity is assigned and the view becomes visible.
        /// </summary>
        /// <param name="entity">The entity being displayed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnShow(E entity)
        {
        }

        /// <summary>
        /// Hides the view and removes its association with the current entity.
        /// </summary>
        public void Hide()
        {
            if (_entity == null)
                return;

            if (this.aspects != null)
            {
                for (int i = 0, count = this.aspects.Count; i < count; i++)
                {
                    SceneEntityAspect<E> aspect = this.aspects[i];
                    if (aspect)
                        aspect.Discard(_entity);
                    else
                        Debug.LogWarning("EntityView: Oops! Detected null aspect!", this);
                }
            }

            this.OnHide(_entity);

            if (this.controlGameObject)
                this.gameObject.SetActive(false);

            _entity = default;
        }

        /// <summary>
        /// Called when the view is hidden.
        /// Override this method to add custom logic when the entity is removed and the view becomes invisible.
        /// </summary>
        /// <param name="entity">The entity that was being displayed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnHide(E entity)
        {
        }
    }
}
#endif