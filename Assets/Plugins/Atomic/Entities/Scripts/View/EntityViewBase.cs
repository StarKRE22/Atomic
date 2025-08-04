#if UNITY_5_3_OR_NEWER
using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic base class for entity views working with general <see cref="IEntity"/> instances.
    /// Inherits from <see cref="EntityViewBase{IEntity}"/> to provide default behavior for non-generic use cases.
    /// </summary>
    /// <remarks>
    /// Useful when a generic parameter is not required or when working with polymorphic collections of views.
    /// </remarks>
    public abstract class EntityViewBase : EntityViewBase<IEntity>
    {
    }

    /// <summary>
    /// A base MonoBehaviour implementation of <see cref="IEntityView{E}"/> for displaying or managing
    /// the visual representation of an entity of type <typeparamref name="E"/> in a Unity scene.
    /// </summary>
    /// <typeparam name="E">The type of entity this view handles. Must implement <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This base class handles binding and visibility logic. Override <see cref="OnShow(E)"/> and <see cref="OnHide(E)"/> to customize behavior.
    /// </remarks>
    public abstract class EntityViewBase<E> : MonoBehaviour, IEntityView<E> where E : IEntity
    {
        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [Title("Debug")]
        [ShowInInspector, HideInEditorMode]
#endif
        public virtual string Name => this.name;

        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public E Entity => _entity;

        /// <inheritdoc/>
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public bool IsVisible => _isVisible;

        private protected E _entity;
        private protected bool _isVisible;

        /// <inheritdoc/>
        public void Show(E entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            _isVisible = true;
            this.OnShow(entity);
        }

        /// <inheritdoc/>
        public void Hide()
        {
            if (!_isVisible)
                return;

            this.OnHide(_entity);
            _isVisible = false;
            _entity = default;
        }

        /// <summary>
        /// Called when the view is shown. Override to implement custom behavior (e.g. enable visuals).
        /// </summary>
        /// <param name="entity">The entity being shown.</param>
        protected virtual void OnShow(E entity) => this.gameObject.SetActive(true);

        /// <summary>
        /// Called when the view is hidden. Override to implement custom behavior (e.g. disable visuals).
        /// </summary>
        /// <param name="entity">The entity being hidden.</param>
        protected virtual void OnHide(E entity) => this.gameObject.SetActive(false);

        public static void Destroy(EntityViewBase<E> view, float time = 0)
        {
            if (view)
            {
                view.Hide();
                Destroy(view.gameObject, time);
            }
        }
    }
}
#endif