namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for singleton entities.
    /// Ensures a single globally accessible instance of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The concrete entity singleton type.</typeparam>
    public abstract class EntitySingleton<E> : Entity<E> where E : EntitySingleton<E>, new()
    {
        /// <summary>
        /// The global instance of the singleton entity.
        /// Created on first access.
        /// </summary>
        public static E Instance => _instance ??= new E();

        private static E _instance;
    }
}