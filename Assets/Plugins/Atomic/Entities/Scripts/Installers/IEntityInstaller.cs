namespace Atomic.Entities
{
    /// <summary>
    /// Defines a generic mechanism for configuring or injecting data into an <see cref="IEntity"/> instance.
    /// </summary>
    public interface IEntityInstaller
    {
        /// <summary>
        /// Installs data, configuration, or behaviors into the specified <see cref="IEntity"/>.
        /// </summary>
        /// <param name="entity">The entity to configure or initialize.</param>
        void Install(IEntity entity);
    }
}