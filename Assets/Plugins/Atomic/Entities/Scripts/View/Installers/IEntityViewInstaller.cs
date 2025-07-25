namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="IEntityViewInstaller{E}"/> specialized for <see cref="IEntity"/>.
    /// </summary>
    public interface IEntityViewInstaller : IEntityViewInstaller<IEntity>
    {
    }

    /// <summary>
    /// Defines an interface for installing or configuring a specific <see cref="EntityView{E}"/> instance.
    /// </summary>
    /// <typeparam name="E">The type of entity associated with the view. Must implement <see cref="IEntity"/>.</typeparam>
    public interface IEntityViewInstaller<E> where E : IEntity
    {
        /// <summary>
        /// Performs installation logic for the specified view.
        /// This can include injecting dependencies, registering components, or initializing the view state.
        /// </summary>
        /// <param name="view">The entity view to configure or initialize.</param>
        void Install(EntityView<E> view);
    }
}