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
    /// Base class for all entity views.
    /// Provides core functionality for showing, hiding, and naming views bound to <see cref="IEntity"/>.
    /// </summary>
    /// <typeparam name="E">The type of <see cref="IEntity"/> associated with this view.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/UI/EntityView%601.md")]
    public abstract partial class EntityView<E> : MonoBehaviour where E : class, IEntity
    {
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
#endif
        /// <summary>
        /// If true, <see cref="GameObject.SetActive"/> will be called when <see cref="Show(E)"/>
        /// and <see cref="Hide"/> are invoked.
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

#if ODIN_INSPECTOR
        [ShowIf(nameof(overrideName))]
#endif
        /// <summary>
        /// Custom display name for the view, used only if <see cref="overrideName"/> is enabled.
        /// </summary>
        [Tooltip("The custom name to use for the view when _overrideName is enabled")]
        [SerializeField]
        internal string customName;

        /// <summary>
        /// List of installers that provide values and behaviors to the attached entity.
        /// </summary>
        [Header("Installing")]
        [Tooltip("Specify the installers that will put values and behaviours to an attached entity")]
        [SerializeField]
        internal List<SceneEntityInstaller> installers;

        /// <summary>
        /// Gets the display name of the view.
        /// Returns <see cref="customName"/> if <see cref="overrideName"/> is true; 
        /// otherwise, returns the <see cref="GameObject.name"/>.
        /// </summary>
        public virtual string Name => this.overrideName
            ? this.customName
            : this != null
                ? this.name
                : "#Unknown";

        /// <summary>
        /// Gets the entity currently associated with this view.
        /// </summary>
#if ODIN_INSPECTOR
        [Title("Debug")]
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

            if (this.controlGameObject && this != null)
                this.gameObject.SetActive(true);

            this.OnShow(entity);

            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Count; i < count; i++)
                {
                    SceneEntityInstaller installer = this.installers[i];
                    if (installer)
                        installer.Install(entity);
                    else
                        Debug.LogWarning($"EntityView {this.Name}: Oops! Detected null installer!", this);
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

            if (this.installers != null)
            {
                for (int i = 0, count = this.installers.Count; i < count; i++)
                {
                    SceneEntityInstaller installer = this.installers[i];
                    if (installer)
                        installer.Uninstall(_entity);
                    else
                        Debug.LogWarning($"EntityView {this.Name}: Oops! Detected null installer!", this);
                }
            }

            this.OnHide(_entity);

            if (this.controlGameObject && this != null)
                this.gameObject.SetActive(false);

            _entity = null;
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

        /// <summary>
        /// Assigns the GameObject's name to the custom name field.
        /// </summary>
        [ContextMenu("Assign Custom Name From GameObject")]
        private void AssignCustomNameFromGameObject()
        {
            this.customName = this.name;
        }
    }
}
#endif