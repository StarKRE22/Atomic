using System;

namespace Atomic.Contexts
{
    public abstract class SingletonSceneContext<T> : SceneContext where T : SceneContext
    {
        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<T>();
                
                if (_instance == null)
                    throw new NullReferenceException("Can't find Scene context on scene!");

                return _instance;
            }
        }

        private static T _instance;
    }
}