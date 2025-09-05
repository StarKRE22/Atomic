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
    /// Base class for all entity views. Implements <see cref="IEntityView"/> to provide
    /// basic functionality for showing, hiding, and naming views associated with <see cref="IEntity"/>.
    /// </summary>
    public abstract partial class EntityView<E> : MonoBehaviour, IEntityView<E> where E : IEntity
    {
        [Tooltip("Should activate and deactivate GameObject when Show / Hide are invoked?")]
        [SerializeField]
        private bool _controlGameObject = true;

        [Header("Name")]
        [Tooltip("If true, the view will use the custom name instead of the GameObject's name")]
        [SerializeField]
        private bool _overrideName;

        [Tooltip("The custom name to use for the view when _overrideName is enabled")]
        [SerializeField]
        private string _customName;

        [Header("Aspects")]
        [Tooltip("Specify the aspects that will put values and behaviours to an attached entity")]
        [SerializeField]
        internal List<SceneEntityAspect<E>> _aspects;

        /// <summary>
        /// Gets the display name of the view. Returns <see cref="_customName"/> if <see cref="_overrideName"/> is true; otherwise, returns the GameObject's name.
        /// </summary>
        public virtual string Name => _overrideName ? _customName : this.name;

        /// <summary>
        /// Gets the entity currently associated with this view.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public E Entity => _entity;

        private E _entity;

        /// <summary>
        /// Gets a value indicating whether the view is currently visible (active in the scene).
        /// </summary>
        public bool IsVisible => _entity != null;

        /// <summary>
        /// Displays the view and associates it with the specified entity.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        public void Show(E entity)
        {
            _entity = entity ?? throw new ArgumentNullException(nameof(entity));
            
            if (_controlGameObject)
                this.gameObject.SetActive(true);
            
            this.OnShow(entity);
            
            if (_aspects != null)
            {
                for (int i = 0, count = _aspects.Count; i < count; i++)
                {
                    SceneEntityAspect<E> aspect = _aspects[i];
                    if (aspect)
                        aspect.Apply(entity);
                    else
                        Debug.LogWarning("EntityView: Ops! Detected null aspect!", this);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnShow(E entity)
        {
        }

        /// <summary>
        /// Hides the view and removes its association with the entity.
        /// </summary>
        public void Hide()
        {
            if (_entity == null)
                return;

            if (_aspects != null)
            {
                for (int i = 0, count = _aspects.Count; i < count; i++)
                {
                    SceneEntityAspect<E> aspect = _aspects[i];
                    if (aspect)
                        aspect.Discard(_entity);
                    else
                        Debug.LogWarning("EntityView: Ops! Detected null aspect!", this);
                }
            }
            
            this.OnHide(_entity);
            
            if (_controlGameObject)
                this.gameObject.SetActive(false);

            _entity = default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnHide(E entity)
        {
        }
    }
}
#endif