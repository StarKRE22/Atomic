using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// An optimized variant of <see cref="SceneEntityBaker{E}"/> that integrates tightly with a corresponding <see cref="EntityView{E}"/>.
    /// </summary>
    /// <typeparam name="E">The type of entity produced by this baker. Must implement <see cref="IEntity"/>.</typeparam>
    /// <typeparam name="V">The type of <see cref="EntityView{E}"/> associated with this baker. Used for efficient pooling and binding.</typeparam>
    /// <remarks>
    /// This baker provides a higher-performance workflow for scene-based entities by coupling each baked entity
    /// with its specific <see cref="EntityView{E}"/>. The view acts as a lightweight bridge between Unity components
    /// and the pure C# entity model.
    ///
    /// During baking, this component:
    /// <list type="number">
    /// <item>Creates a new entity instance via the assigned <see cref="ScriptableEntityFactory{E}"/>.</item>
    /// <item>Installs any additional data or logic defined in <see cref="Install"/>.</item>
    /// <item>Associates the entity with its view for runtime interaction and pooling.</item>
    /// </list>
    ///
    /// When the entity is released (e.g., destroyed or recycled), the view is returned to the
    /// assigned <see cref="EntityViewPool{E, V}"/> for reuse.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Baking/SceneEntityBakerOptimized%602.md")]
    public abstract class SceneEntityBakerOptimized<E, V> : SceneEntityBaker<E>, ISceneEntityBakerOptimized
        where E : class, IEntity
        where V : EntityView<E>
    {
        [Header("Entity View Reference")]
        [SerializeField]
        private V _view;

        [Header("Factory Reference")]
        [SerializeField]
        private ScriptableEntityFactory<E> _factory;

        [Header("Pooling Reference (Optional)")]
        [SerializeField]
        private EntityViewPool<E, V> _viewPool;

        /// <summary>
        /// Creates a new entity instance using the configured <see cref="_factory"/>.
        /// The entity is immediately passed through <see cref="Install"/> for initialization.
        /// </summary>
        /// <returns>The newly created entity instance.</returns>
        protected override E Create()
        {
            E entity = _factory.Create();
            this.Install(entity);
            return entity;
        }

        /// <summary>
        /// Resets serialized references to ensure correct linkage between components in the Editor.
        /// Automatically assigns the local <see cref="_view"/> and searches for the nearest <see cref="_viewPool"/>.
        /// </summary>
        protected override void Reset()
        {
            base.Reset();
            this.Refresh();
        }

        public void Refresh()
        {
            _view = this.GetComponent<V>();
            _viewPool = FindObjectOfType<EntityViewPool<E, V>>();
        }

        /// <summary>
        /// Called immediately after <see cref="Create"/> to configure or bind data to the entity.
        /// Derived classes should override this method to apply initialization logic.
        /// </summary>
        /// <param name="entity">The entity instance being baked.</param>
        protected abstract void Install(E entity);

        /// <summary>
        /// Handles returning the view instance back to the configured pool when the entity is released.
        /// </summary>
        protected override void Release()
        {
            if (_viewPool != null && _view != null)
                _viewPool.Return(_view.Name, _view);
        }
    }
}