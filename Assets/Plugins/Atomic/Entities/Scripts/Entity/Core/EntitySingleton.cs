namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for singleton entities.
    /// Ensures a single globally accessible instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The concrete entity singleton type.</typeparam>
    public abstract class EntitySingleton<T> : Entity where T : EntitySingleton<T>, new()
    {
        /// <summary>
        /// The global instance of the singleton entity.
        /// Created on first access.
        /// </summary>
        public static T Instance => _instance ??= new T();

        private static T _instance;
    }
}