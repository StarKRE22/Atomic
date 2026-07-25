using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// An optimized variant of <see cref="MonoEntityBaker{TArgs}"/> that integrates tightly with a corresponding <see cref="EntityView{E}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity produced by this baker. Must implement <see cref="IEntity"/>.</typeparam>
    /// <typeparam name="TView">The type of <see cref="EntityView{E}"/> associated with this baker. Used for efficient pooling and binding.</typeparam>
    /// <remarks>
    /// This baker provides a higher-performance workflow for scene-based entities by coupling each baked entity
    /// with its specific <see cref="EntityView{E}"/>. The view acts as a lightweight bridge between Unity components
    /// and the pure C# entity model.
    ///
    /// During baking, this component:
    /// <list type="number">
    /// <item>Creates a new entity instance via the assigned <see cref="ScriptableEntityFactory{TArgs}"/>.</item>
    /// <item>Installs any additional data or logic defined in <see cref="Override"/>.</item>
    /// <item>Associates the entity with its view for runtime interaction and pooling.</item>
    /// </list>
    ///
    /// When the entity is released (e.g., destroyed or recycled), the view is returned to the
    /// assigned <see cref="EntityViewPool{E, V}"/> for reuse.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Baking/SceneEntityBakerOptimized%602.md")]
    public abstract class MonoEntityBakerOptimized<TKey, TEntity, TView, TArgs> : MonoEntityBaker<TEntity, TArgs>
        where TEntity : class, IEntity
        where TView : EntityView<TEntity>
        where TArgs : IArgs

    {
        [SerializeField]
        private TView _view;

        [Space]
        [SerializeField]
        private ScriptableEntityFactory<TEntity, TArgs> _factory;

        /// <summary>
        /// Creates a new entity instance using the configured <see cref="_factory"/>.
        /// The entity is immediately passed through <see cref="Override"/> for initialization.
        /// </summary>
        /// <returns>The newly created entity instance.</returns>
        protected override TEntity Create(
            int tagCapacity,
            int valueCapacity,
            int behaviourCapacity,
            Entity.Settings settings,
            TArgs args
        )
        {
            TEntity entity = _factory.Create(args);
            this.Override(entity, args);
            return entity;
        }

        /// <summary>
        /// Resets serialized references to ensure correct linkage between components in the Editor.
        /// Automatically assigns the local <see cref="_view"/> and searches for the nearest <see cref="ViewPoolGlobal"/>.
        /// </summary>
        protected override void Reset()
        {
            base.Reset();
            _view = this.GetComponent<TView>();
        }

        /// <summary>
        /// Called immediately after <see cref="Create"/> to configure or bind data to the entity.
        /// Derived classes should override this method to apply initialization logic.
        /// </summary>
        /// <param name="entity">The entity instance being baked.</param>
        protected abstract void Override(TEntity entity, TArgs args);

        /// <summary>
        /// Handles returning the view instance to the configured pool when the entity is released.
        /// </summary>
        protected override void Release()
        {
            EntityWorldViewSingleton<TKey, TEntity, TView>.Instance.Pool.Return(_view);
            Destroy(this);
        }
    }
}