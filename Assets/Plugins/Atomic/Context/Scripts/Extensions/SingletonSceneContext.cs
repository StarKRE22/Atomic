namespace Atomic.Contexts
{
    public abstract class SingletonSceneContext<T> : SceneContext where T : SceneContext
    {
        public static T Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}