namespace Atomic.Entities
{
    /// <summary>
    /// Defines a contract for components that can install or configure <see cref="EntityView"/> instances.
    /// Implement this interface to provide custom installation logic for entity views.
    /// </summary>
    public interface IEntityViewInstaller
    {
        /// <summary>
        /// Performs the installation or configuration logic for the specified <see cref="EntityView"/>.
        /// </summary>
        /// <param name="view">The <see cref="EntityView"/> instance to install.</param>
        void Install(EntityView view);
    }
}