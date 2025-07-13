namespace Atomic.Entities
{
    public abstract class EntitySingleton<T> : Entity where T : EntitySingleton<T>, new()
    {
        public static T Instance => _instance ??= new T();

        private static T _instance;
    }
}