#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if ENABLE_PROFILER
using Unity.Profiling;
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
        private const string NAME_FORMAT = "{0}:{1}";

#if ENABLE_PROFILER
        private static readonly ProfilerMarker s_activateMarker = new($"EntityView<{typeof(E).Name}>.Activate");
        private static readonly ProfilerMarker s_deactivateMarker = new($"EntityView<{typeof(E).Name}>.Deactivate");
#endif
        /// <summary>
        /// List of installers that provide values and behaviors to the attached entity.
        /// </summary>
        [Header("Installing")]
        [Tooltip("Specify the installers that will put values and behaviours to an attached entity")]
        [SerializeField]
#if ODIN_INSPECTOR
        [PropertyOrder(5)]
        [DisableInPlayMode]
#endif
        internal List<MonoEntityInstaller> installers;

        /// <summary>
        /// Gets the entity currently associated with this view.
        /// </summary>
        public E Entity => _entity;

#if ODIN_INSPECTOR
        [Title("Debug")]
        [PropertyOrder(1000)]
        [ShowInInspector]
        [HideInEditorMode]
#endif
        private E _entity;

        /// <summary>
        /// Gets a value indicating whether the view is currently visible (i.e., has an entity assigned).
        /// </summary>
        public bool IsActive => _entity != null;

        /// <summary>
        /// Displays the view and associates it with the specified entity.
        /// </summary>
        /// <param name="entity">The entity to associate with and display through this view.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entity"/> is null.</exception>
        public void Activate(E entity)
        {
#if ENABLE_PROFILER
            using (s_activateMarker.Auto())
#endif
            {
                this.Deactivate();
                _entity = entity ?? throw new ArgumentNullException(nameof(entity));

                this.name = this.FormateName(entity);
                this.OnActivate(entity);

                if (this.installers != null)
                {
                    for (int i = 0, count = this.installers.Count; i < count; i++)
                    {
                        MonoEntityInstaller installer = this.installers[i];
                        if (installer)
                            installer.Install(entity);
                        else
                            Debug.LogWarning(
                                $"EntityView<{typeof(E).Name}>: Oops! Detected null installer!",
                                this);
                    }
                }
            }
        }

        protected virtual string FormateName(E entity) =>
            string.Format(NAME_FORMAT, entity.Name, entity.InstanceID);

        /// <summary>
        /// Hides the view and removes its association with the current entity.
        /// </summary>
        public void Deactivate()
        {
#if ENABLE_PROFILER
            using (s_deactivateMarker.Auto())
#endif
            {
                if (_entity == null)
                    return;

                if (this.installers != null)
                {
                    for (int i = 0, count = this.installers.Count; i < count; i++)
                    {
                        MonoEntityInstaller installer = this.installers[i];
                        if (installer)
                            installer.Uninstall(_entity);
                        else
                            Debug.LogWarning(
                                $"EntityView<{typeof(E).Name}>: Oops! Detected null installer!",
                                this);
                    }
                }

                this.OnDeactivate(_entity);
                _entity = null;
            }
        }

        /// <summary>
        /// Called when the view is shown.
        /// Override this method to add custom logic when an entity is assigned and the view becomes visible.
        /// </summary>
        /// <param name="entity">The entity being displayed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnActivate(E entity)
        {
        }

        /// <summary>
        /// Called when the view is hidden.
        /// Override this method to add custom logic when the entity is removed and the view becomes invisible.
        /// </summary>
        /// <param name="entity">The entity that was being displayed.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDeactivate(E entity)
        {
        }
    }
}
#endif