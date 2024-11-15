namespace Atomic.Contexts
{
    public abstract class SingletonSceneContext<T> : SceneContext where T : SceneContext
    {
        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<T>();
                return _instance;
            }
        }

        private static T _instance;
    }
}